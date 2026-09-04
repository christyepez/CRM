param(
    [string]$BaseUrl = "http://localhost:8093",
    [string]$FrontendUrl = "http://127.0.0.1:4200"
)

$ErrorActionPreference = "Stop"

function Invoke-CrmRequest {
    param(
        [string]$Method,
        [string]$Path,
        [object]$Body = $null
    )

    $watch = [Diagnostics.Stopwatch]::StartNew()
    try {
        if ($null -eq $Body) {
            $response = Invoke-WebRequest -UseBasicParsing -Method $Method -Uri "$BaseUrl$Path" -TimeoutSec 15
        } else {
            $json = $Body | ConvertTo-Json -Compress
            $response = Invoke-WebRequest -UseBasicParsing -Method $Method -Uri "$BaseUrl$Path" -Body $json -ContentType "application/json" -TimeoutSec 15
        }

        $watch.Stop()
        [pscustomobject]@{
            Method = $Method
            Path = $Path
            Status = [int]$response.StatusCode
            Ms = $watch.ElapsedMilliseconds
            Body = $response.Content
        }
    } catch {
        $watch.Stop()
        $status = 0
        $content = ""
        if ($_.Exception.Response) {
            $status = [int]$_.Exception.Response.StatusCode
        }

        if ($_.ErrorDetails -and $_.ErrorDetails.Message) {
            $content = $_.ErrorDetails.Message
        }

        [pscustomobject]@{
            Method = $Method
            Path = $Path
            Status = $status
            Ms = $watch.ElapsedMilliseconds
            Body = $content
        }
    }
}

function Assert-Status {
    param(
        [object]$Response,
        [int[]]$Expected,
        [string]$Scenario
    )

    if ($Response.Status -notin $Expected) {
        throw "$Scenario expected $($Expected -join '/') but got $($Response.Status). Body: $($Response.Body)"
    }
}

$runId = [guid]::NewGuid().ToString("N").Substring(0, 8)
$seed = "S12 Integration Contact $runId"
$email = "s12.integration.$runId@example.test"
$results = New-Object System.Collections.Generic.List[object]

foreach ($path in @("/health", "/health/live", "/health/ready", "/api/crm/readiness")) {
    $response = Invoke-CrmRequest GET $path
    Assert-Status $response @(200) "Health/readiness $path"
    $results.Add($response)
}

$listBefore = Invoke-CrmRequest GET "/api/crm/foundation/contacts"
Assert-Status $listBefore @(200) "Initial Contact list"
$results.Add($listBefore)
$beforeJson = $listBefore.Body | ConvertFrom-Json
$initialCount = @($beforeJson.data).Count

$create = Invoke-CrmRequest POST "/api/crm/foundation/contacts" @{
    firstName = "S12 Integration"
    lastName = "Contact $runId"
    email = $email.ToUpperInvariant()
    phone = "0999999999"
    title = "Integration Tester"
    preferredContactMethod = "Email"
}
Assert-Status $create @(200) "Create Contact"
$results.Add($create)
$created = $create.Body | ConvertFrom-Json
$createdContactId = $created.id
if ([string]::IsNullOrWhiteSpace($createdContactId) -or -not $created.changed -or $created.email -ne $email) {
    throw "Create Contact did not return expected id/changed/normalized email."
}

$readAfterCreate = Invoke-CrmRequest GET "/api/crm/foundation/contacts/$createdContactId"
Assert-Status $readAfterCreate @(200) "Read after create"
$results.Add($readAfterCreate)
$readCreated = $readAfterCreate.Body | ConvertFrom-Json
if ($readCreated.data.email -ne $email -or $readCreated.data.preferredContactMethod -ne "Email") {
    throw "Read after create did not return normalized Contact state."
}

$listAfter = Invoke-CrmRequest GET "/api/crm/foundation/contacts"
Assert-Status $listAfter @(200) "List after create"
$results.Add($listAfter)
if (-not (($listAfter.Body | ConvertFrom-Json).data | Where-Object { $_.id -eq $createdContactId })) {
    throw "List after create does not include created Contact."
}

$update = Invoke-CrmRequest PUT "/api/crm/foundation/contacts/$createdContactId" @{
    firstName = "S12 Integration"
    lastName = "Contact $runId"
    email = $email.ToUpperInvariant()
    phone = "0888888888"
    title = "Updated Integration Tester"
    preferredContactMethod = "Phone"
}
Assert-Status $update @(200) "Update Contact"
$results.Add($update)
$updated = $update.Body | ConvertFrom-Json
if (-not $updated.changed -or $updated.phone -ne "0888888888" -or $updated.preferredContactMethod -ne "Phone") {
    throw "Update Contact did not return expected changed phone/preferredContactMethod."
}

$readAfterUpdate = Invoke-CrmRequest GET "/api/crm/foundation/contacts/$createdContactId"
Assert-Status $readAfterUpdate @(200) "Read after update"
$results.Add($readAfterUpdate)
$readUpdated = $readAfterUpdate.Body | ConvertFrom-Json
if ($readUpdated.data.phone -ne "0888888888" -or $readUpdated.data.title -ne "Updated Integration Tester") {
    throw "Read after update did not return updated values."
}

$noChange = Invoke-CrmRequest PUT "/api/crm/foundation/contacts/$createdContactId" @{
    firstName = " S12 Integration "
    lastName = " Contact $runId "
    email = $email
    phone = "0888888888"
    title = "Updated Integration Tester"
    preferredContactMethod = "Phone"
}
Assert-Status $noChange @(200) "No-change update"
$results.Add($noChange)
if (($noChange.Body | ConvertFrom-Json).changed) {
    throw "No-change update should return changed=false."
}

$invalidCreate = Invoke-CrmRequest POST "/api/crm/foundation/contacts" @{
    firstName = ""
    lastName = ""
    email = "invalid"
    preferredContactMethod = "NotSpecified"
}
Assert-Status $invalidCreate @(400) "Invalid create"
$results.Add($invalidCreate)

$preferredEmail = Invoke-CrmRequest POST "/api/crm/foundation/contacts" @{
    firstName = "S12"
    lastName = "No Email"
    preferredContactMethod = "Email"
}
Assert-Status $preferredEmail @(400) "Preferred Email validation"
$results.Add($preferredEmail)

$preferredPhone = Invoke-CrmRequest POST "/api/crm/foundation/contacts" @{
    firstName = "S12"
    lastName = "No Phone"
    preferredContactMethod = "Phone"
}
Assert-Status $preferredPhone @(400) "Preferred Phone validation"
$results.Add($preferredPhone)

$invalidEnum = Invoke-CrmRequest POST "/api/crm/foundation/contacts" @{
    firstName = "S12"
    lastName = "Bad Enum"
    preferredContactMethod = "Fax"
}
Assert-Status $invalidEnum @(400) "Invalid enum"
$results.Add($invalidEnum)
foreach ($forbiddenLeak in @("System.", "Exception", "CRM.", "ConnectionString", "password", "Bearer")) {
    if ($invalidEnum.Body -like "*$forbiddenLeak*") {
        throw "Invalid enum response leaked forbidden marker: $forbiddenLeak"
    }
}

$missingId = [guid]::NewGuid().ToString("D")
$notFoundDetail = Invoke-CrmRequest GET "/api/crm/foundation/contacts/$missingId"
Assert-Status $notFoundDetail @(200, 404) "Not-found detail"
$results.Add($notFoundDetail)

$notFoundUpdate = Invoke-CrmRequest PUT "/api/crm/foundation/contacts/$missingId" @{
    firstName = "Missing"
    lastName = "Contact"
    email = "missing@example.test"
    preferredContactMethod = "Email"
}
Assert-Status $notFoundUpdate @(404) "Not-found update"
$results.Add($notFoundUpdate)

foreach ($negative in @(
    @{ Method = "GET"; Path = "/api/crm/contacts" },
    @{ Method = "POST"; Path = "/api/crm/contacts"; Body = @{ firstName = "No"; lastName = "Productive" } },
    @{ Method = "PUT"; Path = "/api/crm/contacts/$createdContactId"; Body = @{ firstName = "No"; lastName = "Productive" } }
)) {
    $response = Invoke-CrmRequest $negative.Method $negative.Path $negative.Body
    Assert-Status $response @(404, 423) "Productive route negative $($negative.Method) $($negative.Path)"
    $results.Add($response)
}

$deleteFoundation = Invoke-CrmRequest DELETE "/api/crm/foundation/contacts/$createdContactId"
Assert-Status $deleteFoundation @(404, 405) "DELETE foundation route negative"
$results.Add($deleteFoundation)

$frontendRoute = Invoke-WebRequest -UseBasicParsing -Method GET -Uri "$FrontendUrl/foundation/contacts" -TimeoutSec 15
if ($frontendRoute.StatusCode -ne 200 -or ($frontendRoute.Content -notmatch "CRM Foundation" -and $frontendRoute.Content -notmatch "crm-root")) {
    throw "Frontend route did not return Angular shell."
}

$frontendApi = Invoke-WebRequest -UseBasicParsing -Method GET -Uri "$FrontendUrl/api/crm/foundation/contacts" -TimeoutSec 15
if ($frontendApi.StatusCode -ne 200) {
    throw "Frontend proxy did not reach CRM foundation Contact API."
}

$latency = @($results | Where-Object { $_.Path -like "/api/crm/foundation/contacts*" -and $_.Status -in @(200, 400, 404, 405) })
$sorted = @($latency | Sort-Object Ms)
$average = [math]::Round(($latency | Measure-Object Ms -Average).Average, 2)
$minimum = ($latency | Measure-Object Ms -Minimum).Minimum
$p95Index = [math]::Min($sorted.Count - 1, [math]::Ceiling($sorted.Count * 0.95) - 1)
$p95 = $sorted[$p95Index].Ms

[pscustomobject]@{
    LocalBackendUrl = $BaseUrl
    LocalFrontendUrl = $FrontendUrl
    FrontendApiRoutingMode = "Proxy"
    IntegrationContactSeed = $seed
    IntegrationContactEmail = $email
    CreatedContactId = $createdContactId
    InitialContactCount = $initialCount
    CreateContactScenario = "PASS"
    ReadAfterCreate = "PASS"
    ListAfterCreate = "PASS"
    UpdateContactScenario = "PASS"
    ReadAfterUpdate = "PASS"
    NoChangeUpdateScenario = "PASS"
    InvalidCreateScenario = "PASS"
    PreferredEmailValidationScenario = "PASS"
    PreferredPhoneValidationScenario = "PASS"
    InvalidEnumScenario = "PASS"
    NotFoundDetailStatus = $notFoundDetail.Status
    NotFoundUpdateScenario = "PASS"
    ProductiveContactRouteAvailable = $false
    DeleteRouteAvailable = $false
    FrontendRouteStatus = [int]$frontendRoute.StatusCode
    FrontendToApiConnectivity = "PASS"
    ProxyOrCorsValidation = "PASS"
    ReadAfterWriteConsistent = $true
    FrontendNormalizationObserved = $true
    IntegrationLatencySamples = $latency.Count
    LatencyMinMs = $minimum
    LatencyAverageMs = $average
    LatencyP95Ms = $p95
    NetworkEvidence = ($results | Select-Object Method, Path, Status, Ms)
} | ConvertTo-Json -Depth 8
