param(
    [string]$BaseUrl = "http://127.0.0.1:4200",
    [string]$BackendUrl = "http://localhost:8093"
)

$ErrorActionPreference = "Stop"
$results = New-Object System.Collections.Generic.List[object]
$latencies = New-Object System.Collections.Generic.List[double]

function Invoke-Scenario($Name, $Method, $Path, $Body, $ExpectedStatus) {
    $uri = "$BaseUrl$Path"
    $watch = [System.Diagnostics.Stopwatch]::StartNew()
    try {
        $params = @{
            Method = $Method
            Uri = $uri
            UseBasicParsing = $true
            TimeoutSec = 10
        }
        if ($null -ne $Body) {
            $params.Body = ($Body | ConvertTo-Json -Depth 8)
            $params.ContentType = "application/json"
        }

        $response = Invoke-WebRequest @params
        $status = [int]$response.StatusCode
        $content = $response.Content
    } catch {
        $status = [int]$_.Exception.Response.StatusCode
        if ($_.ErrorDetails.Message) {
            $content = $_.ErrorDetails.Message
        } else {
            $content = $_.Exception.Message
        }
    } finally {
        $watch.Stop()
        $latencies.Add($watch.Elapsed.TotalMilliseconds)
    }

    $safeContent = if ($null -eq $content) { "" } else { [string]$content }
    $bodyPreview = if ($safeContent.Length -gt 240) { $safeContent.Substring(0, 240) } else { $safeContent }
    $pass = $status -eq $ExpectedStatus
    $results.Add([pscustomobject]@{
        Scenario = $Name
        Method = $Method
        Path = $Path
        Status = $status
        ExpectedStatus = $ExpectedStatus
        Pass = $pass
        LatencyMs = [Math]::Round($watch.Elapsed.TotalMilliseconds, 2)
        BodyPreview = $bodyPreview
    })

    if (-not $pass) {
        throw "$Name returned HTTP $status; expected $ExpectedStatus. Body: $content"
    }

    if ($safeContent.TrimStart().StartsWith("{") -or $safeContent.TrimStart().StartsWith("[")) {
        return $content | ConvertFrom-Json
    }

    return $safeContent
}

function New-SyntheticLead($Name) {
    $created = Invoke-Scenario "Create-$Name" "POST" "/api/crm/foundation/leads" @{
        firstName = $Name
        lastName = "Synthetic"
        email = "$Name@example.invalid"
        companyName = "Synthetic Foundation"
        title = "Foundation Lead"
        phone = "000-000-0000"
    } 200
    return $created.data.id
}

$frontendBaseUrl = $BaseUrl

$BaseUrl = $BackendUrl
$health = Invoke-Scenario "Health" "GET" "/health" $null 200
$live = Invoke-Scenario "Live" "GET" "/health/live" $null 200
$ready = Invoke-Scenario "Ready" "GET" "/health/ready" $null 200

$BaseUrl = $frontendBaseUrl
$frontend = Invoke-Scenario "FrontendRoute" "GET" "/foundation/leads/qualification" $null 200
$leadList = Invoke-Scenario "FoundationLeadSource" "GET" "/api/crm/foundation/leads" $null 200

$qualify = Invoke-Scenario "Qualify" "POST" "/api/crm/foundation/leads/lead-preview-001/qualification" @{
    decision = "Qualify"
    comment = "Synthetic local integration qualification"
} 200

$idempotent = Invoke-Scenario "IdempotentQualify" "POST" "/api/crm/foundation/leads/lead-preview-001/qualification" @{
    decision = "Qualify"
} 200

$disqualifyLeadId = New-SyntheticLead "s11-06-disqualify"
$disqualify = Invoke-Scenario "Disqualify" "POST" "/api/crm/foundation/leads/$disqualifyLeadId/qualification" @{
    decision = "Disqualify"
    disqualificationReason = "NoInterest"
    comment = "Synthetic local integration disqualification"
} 200

$otherLeadId = New-SyntheticLead "s11-06-other"
$other = Invoke-Scenario "OtherReason" "POST" "/api/crm/foundation/leads/$otherLeadId/qualification" @{
    decision = "Disqualify"
    disqualificationReason = "Other"
    otherReason = "Synthetic local integration other reason"
} 200

$validationLeadId = New-SyntheticLead "s11-06-validation"
$validation = Invoke-Scenario "ValidationError" "POST" "/api/crm/foundation/leads/$validationLeadId/qualification" @{
    decision = "Disqualify"
} 400

$missing = Invoke-Scenario "LeadNotFound" "POST" "/api/crm/foundation/leads/s11-06-missing/qualification" @{
    decision = "Qualify"
} 404

$invalidLeadId = New-SyntheticLead "s11-06-invalid-transition"
$discard = Invoke-Scenario "InvalidTransitionSetup" "POST" "/api/crm/foundation/leads/$invalidLeadId/qualification" @{
    decision = "Disqualify"
    disqualificationReason = "Duplicate"
} 200
$invalid = Invoke-Scenario "InvalidTransition" "POST" "/api/crm/foundation/leads/$invalidLeadId/qualification" @{
    decision = "Qualify"
} 409

$productive = Invoke-Scenario "ProductiveRouteNegative" "POST" "/api/crm/leads/lead-preview-001/qualification" @{
    decision = "Qualify"
} 404

$readAfterWrite = Invoke-Scenario "ReadAfterWrite" "GET" "/api/crm/foundation/leads/lead-preview-001" $null 200

$sorted = @($latencies | Sort-Object)
$avg = ($latencies | Measure-Object -Average).Average
$p95Index = [Math]::Min($sorted.Count - 1, [Math]::Ceiling($sorted.Count * 0.95) - 1)

[pscustomobject]@{
    BaseUrl = $BaseUrl
    BackendUrl = $BackendUrl
    BackendViaProxy = "$BaseUrl/api"
    LeadCountAtStart = $leadList.data.Count
    QualifyCurrentStatus = $qualify.currentStatus
    IdempotentChanged = $idempotent.changed
    DisqualifyLeadId = $disqualifyLeadId
    DisqualifyCurrentStatus = $disqualify.currentStatus
    OtherReasonStatus = $other.currentStatus
    ValidationErrorCode = $validation.errorCode
    LeadNotFoundErrorCode = $missing.errorCode
    InvalidTransitionErrorCode = $invalid.errorCode
    ProductiveRouteStatus = 404
    ReadAfterWriteStatus = $readAfterWrite.data.status
    LatencySamples = $latencies.Count
    LatencyMinMs = [Math]::Round($sorted[0], 2)
    LatencyAverageMs = [Math]::Round($avg, 2)
    LatencyP95Ms = [Math]::Round($sorted[$p95Index], 2)
    Results = $results
} | ConvertTo-Json -Depth 12
