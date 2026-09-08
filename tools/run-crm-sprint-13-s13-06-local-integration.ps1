param(
    [string]$BaseUrl = "http://localhost:8093",
    [string]$FrontendUrl = "http://127.0.0.1:4200"
)

$ErrorActionPreference = "Stop"

function Invoke-CrmRequest {
    param([string]$Method, [string]$Path, [object]$Body = $null)
    $watch = [Diagnostics.Stopwatch]::StartNew()
    try {
        if ($null -eq $Body) {
            $response = Invoke-WebRequest -UseBasicParsing -Method $Method -Uri "$BaseUrl$Path" -TimeoutSec 15
        } else {
            $json = $Body | ConvertTo-Json -Compress
            $response = Invoke-WebRequest -UseBasicParsing -Method $Method -Uri "$BaseUrl$Path" -Body $json -ContentType "application/json" -TimeoutSec 15
        }
        $watch.Stop()
        [pscustomobject]@{ Method=$Method; Path=$Path; Status=[int]$response.StatusCode; Ms=$watch.ElapsedMilliseconds; Body=$response.Content }
    } catch {
        $watch.Stop()
        $status = 0
        $content = ""
        if ($_.Exception.Response) { $status = [int]$_.Exception.Response.StatusCode }
        if ($_.ErrorDetails -and $_.ErrorDetails.Message) { $content = $_.ErrorDetails.Message }
        [pscustomobject]@{ Method=$Method; Path=$Path; Status=$status; Ms=$watch.ElapsedMilliseconds; Body=$content }
    }
}
function Assert-Status {
    param([object]$Response, [int[]]$Expected, [string]$Scenario)
    if ($Response.Status -notin $Expected) {
        throw "$Scenario expected $($Expected -join '/') but got $($Response.Status). Body: $($Response.Body)"
    }
}

$runId = [guid]::NewGuid().ToString("N").Substring(0, 8)
$leadId = "22222222-2222-2222-2222-222222222222"
$contactId = "33333333-3333-3333-3333-333333333333"
$leadSubject = "S13 Integration Lead Activity $runId"
$contactSubject = "S13 Integration Contact Activity $runId"
$results = New-Object System.Collections.Generic.List[object]

foreach ($path in @("/health", "/health/live", "/health/ready", "/api/crm/readiness")) {
    $response = Invoke-CrmRequest GET $path
    Assert-Status $response @(200) "Health/readiness $path"
    $results.Add($response)
}

$leads = Invoke-CrmRequest GET "/api/crm/foundation/leads"
Assert-Status $leads @(200) "Foundation Lead list"
$results.Add($leads)
$leadJson = $leads.Body | ConvertFrom-Json
if (-not (@($leadJson.data) | Where-Object { $_.id -eq $leadId })) { throw "Required synthetic foundation Lead target is missing." }

$contacts = Invoke-CrmRequest GET "/api/crm/foundation/contacts"
Assert-Status $contacts @(200) "Foundation Contact list"
$results.Add($contacts)
$contactJson = $contacts.Body | ConvertFrom-Json
if (-not (@($contactJson.data) | Where-Object { $_.id -eq $contactId })) { throw "Required synthetic foundation Contact target is missing." }

$listBefore = Invoke-CrmRequest GET "/api/crm/foundation/activities"
Assert-Status $listBefore @(200) "Initial Activity list"
$results.Add($listBefore)
$initialActivities = $listBefore.Body | ConvertFrom-Json
$initialCount = @($initialActivities).Count

$leadCreate = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type = "Call"
    subject = "  $leadSubject  "
    scheduledAtUtc = [DateTimeOffset]::UtcNow.AddHours(2).ToString("o")
    leadId = $leadId
}
Assert-Status $leadCreate @(200) "Create Lead-target Activity"
$results.Add($leadCreate)
$leadCreated = $leadCreate.Body | ConvertFrom-Json
$createdLeadActivityId = $leadCreated.id
if ([string]::IsNullOrWhiteSpace($createdLeadActivityId) -or -not $leadCreated.changed -or $leadCreated.activity.subject -ne $leadSubject) {
    throw "Lead-target Activity create did not return expected id/changed/normalized subject."
}
$readLead = Invoke-CrmRequest GET "/api/crm/foundation/activities/$createdLeadActivityId"
Assert-Status $readLead @(200) "Read after Lead Activity create"
$results.Add($readLead)
$readLeadJson = $readLead.Body | ConvertFrom-Json
if ($readLeadJson.leadId -ne $leadId -or $null -ne $readLeadJson.contactId) { throw "Lead-target relationship was not preserved." }

$contactCreate = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type = "Email"
    subject = $contactSubject
    scheduledAtUtc = [DateTimeOffset]::UtcNow.AddHours(3).ToString("o")
    contactId = $contactId
}
Assert-Status $contactCreate @(200) "Create Contact-target Activity"
$results.Add($contactCreate)
$contactCreated = $contactCreate.Body | ConvertFrom-Json
$createdContactActivityId = $contactCreated.id
if ([string]::IsNullOrWhiteSpace($createdContactActivityId) -or -not $contactCreated.changed) { throw "Contact-target Activity create failed." }

$missingTarget = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type = "Task"; subject = "Missing target $runId"; scheduledAtUtc = [DateTimeOffset]::UtcNow.AddHours(4).ToString("o")
}
Assert-Status $missingTarget @(400) "Missing target"
$results.Add($missingTarget)

$multipleTargets = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type = "Meeting"; subject = "Multiple targets $runId"; scheduledAtUtc = [DateTimeOffset]::UtcNow.AddHours(4).ToString("o"); leadId=$leadId; contactId=$contactId
}
Assert-Status $multipleTargets @(400) "Multiple targets"
$results.Add($multipleTargets)
$missingLeadId = [guid]::NewGuid().ToString("D")
$missingLead = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type = "Call"; subject = "Missing lead $runId"; scheduledAtUtc = [DateTimeOffset]::UtcNow.AddHours(5).ToString("o"); leadId=$missingLeadId
}
Assert-Status $missingLead @(404) "Missing Lead target"
$results.Add($missingLead)

$missingContactId = [guid]::NewGuid().ToString("D")
$missingContact = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type = "Email"; subject = "Missing contact $runId"; scheduledAtUtc = [DateTimeOffset]::UtcNow.AddHours(5).ToString("o"); contactId=$missingContactId
}
Assert-Status $missingContact @(404) "Missing Contact target"
$results.Add($missingContact)

$updatedSchedule = [DateTimeOffset]::UtcNow.AddDays(2).ToString("o")
$updatedSubject = "S13 Updated Lead Activity $runId"
$update = Invoke-CrmRequest PUT "/api/crm/foundation/activities/$createdLeadActivityId" @{
    type = "Meeting"; subject = $updatedSubject; scheduledAtUtc = $updatedSchedule; leadId=$leadId
}
Assert-Status $update @(200) "Update Activity"
$results.Add($update)
$updated = $update.Body | ConvertFrom-Json
if (-not $updated.changed -or $updated.activity.subject -ne $updatedSubject -or $updated.activity.type -ne "Meeting") { throw "Activity update did not persist expected state." }

$readAfterUpdate = Invoke-CrmRequest GET "/api/crm/foundation/activities/$createdLeadActivityId"
Assert-Status $readAfterUpdate @(200) "Read after Activity update"
$results.Add($readAfterUpdate)
$readUpdated = $readAfterUpdate.Body | ConvertFrom-Json
if ($readUpdated.subject -ne $updatedSubject -or $readUpdated.type -ne "Meeting") { throw "Read after update is inconsistent." }
$noChange = Invoke-CrmRequest PUT "/api/crm/foundation/activities/$createdLeadActivityId" @{
    type = "Meeting"; subject = "  $updatedSubject  "; scheduledAtUtc = $readUpdated.scheduledAtUtc; leadId=$leadId
}
Assert-Status $noChange @(200) "No-change Activity update"
$results.Add($noChange)
if (($noChange.Body | ConvertFrom-Json).changed) { throw "No-change Activity update should return changed=false." }

$complete = Invoke-CrmRequest POST "/api/crm/foundation/activities/$createdLeadActivityId/complete"
Assert-Status $complete @(200) "Complete Activity"
$results.Add($complete)
$completed = $complete.Body | ConvertFrom-Json
if (-not $completed.changed -or $completed.status -ne "Completed" -or $null -eq $completed.activity.completedAtUtc) { throw "Complete Activity did not return completed state." }

$repeatComplete = Invoke-CrmRequest POST "/api/crm/foundation/activities/$createdLeadActivityId/complete"
Assert-Status $repeatComplete @(200) "Repeat complete"
$results.Add($repeatComplete)
if (($repeatComplete.Body | ConvertFrom-Json).changed) { throw "Repeat complete should be idempotent changed=false." }

$cancel = Invoke-CrmRequest POST "/api/crm/foundation/activities/$createdContactActivityId/cancel"
Assert-Status $cancel @(200) "Cancel Activity"
$results.Add($cancel)
$cancelled = $cancel.Body | ConvertFrom-Json
if (-not $cancelled.changed -or $cancelled.status -ne "Cancelled") { throw "Cancel Activity did not return cancelled state." }

$repeatCancel = Invoke-CrmRequest POST "/api/crm/foundation/activities/$createdContactActivityId/cancel"
Assert-Status $repeatCancel @(200) "Repeat cancel"
$results.Add($repeatCancel)
if (($repeatCancel.Body | ConvertFrom-Json).changed) { throw "Repeat cancel should be idempotent changed=false." }
$completeCancelled = Invoke-CrmRequest POST "/api/crm/foundation/activities/$createdContactActivityId/complete"
Assert-Status $completeCancelled @(400,409) "Complete cancelled Activity"
$results.Add($completeCancelled)

$cancelCompleted = Invoke-CrmRequest POST "/api/crm/foundation/activities/$createdLeadActivityId/cancel"
Assert-Status $cancelCompleted @(400,409) "Cancel completed Activity"
$results.Add($cancelCompleted)

$updateCompleted = Invoke-CrmRequest PUT "/api/crm/foundation/activities/$createdLeadActivityId" @{
    type="Call"; subject="Cannot update completed $runId"; scheduledAtUtc=[DateTimeOffset]::UtcNow.AddDays(3).ToString("o"); leadId=$leadId
}
Assert-Status $updateCompleted @(400,409) "Update completed Activity"
$results.Add($updateCompleted)

$updateCancelled = Invoke-CrmRequest PUT "/api/crm/foundation/activities/$createdContactActivityId" @{
    type="Email"; subject="Cannot update cancelled $runId"; scheduledAtUtc=[DateTimeOffset]::UtcNow.AddDays(3).ToString("o"); contactId=$contactId
}
Assert-Status $updateCancelled @(400,409) "Update cancelled Activity"
$results.Add($updateCancelled)

$invalidType = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type="Visit"; subject="Invalid type $runId"; scheduledAtUtc=[DateTimeOffset]::UtcNow.AddHours(2).ToString("o"); leadId=$leadId
}
Assert-Status $invalidType @(400) "Invalid Activity type"
$results.Add($invalidType)
foreach ($forbiddenLeak in @("System.", "Exception", "ConnectionString", "password", "Bearer")) {
    if ($invalidType.Body -like "*$forbiddenLeak*") { throw "Invalid Activity response leaked forbidden marker: $forbiddenLeak" }
}
$subjectRequired = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type="Task"; subject="   "; scheduledAtUtc=[DateTimeOffset]::UtcNow.AddHours(2).ToString("o"); leadId=$leadId
}
Assert-Status $subjectRequired @(400) "Subject required"
$results.Add($subjectRequired)

$subjectTooLong = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type="Task"; subject=("X" * 161); scheduledAtUtc=[DateTimeOffset]::UtcNow.AddHours(2).ToString("o"); leadId=$leadId
}
Assert-Status $subjectTooLong @(400) "Subject max length"
$results.Add($subjectTooLong)

$historicalSubject = "S13 Historical Activity $runId"
$historical = Invoke-CrmRequest POST "/api/crm/foundation/activities" @{
    type="Task"; subject=$historicalSubject; scheduledAtUtc="2020-01-01T08:00:00+00:00"; leadId=$leadId
}
Assert-Status $historical @(200) "Historical schedule"
$results.Add($historical)
$historicalJson = $historical.Body | ConvertFrom-Json
if ($historicalJson.status -ne "Scheduled") { throw "Historical Activity must remain Scheduled; Overdue is derived UI only." }

$listAfterWrites = Invoke-CrmRequest GET "/api/crm/foundation/activities"
Assert-Status $listAfterWrites @(200) "Activity list after writes"
$results.Add($listAfterWrites)
$listAfterJson = $listAfterWrites.Body | ConvertFrom-Json
foreach ($expectedId in @($createdLeadActivityId, $createdContactActivityId, $historicalJson.id)) {
    $activityIds = @($listAfterJson | ForEach-Object { [string]$_.id })
    if ($activityIds -notcontains [string]$expectedId) { throw "Activity list is missing expected Activity $expectedId." }
}
foreach ($negative in @(
    @{ Method="GET"; Path="/api/crm/activities" },
    @{ Method="POST"; Path="/api/crm/activities"; Body=@{subject="No productive"} },
    @{ Method="PUT"; Path="/api/crm/activities/$createdLeadActivityId"; Body=@{subject="No productive"} },
    @{ Method="POST"; Path="/api/crm/activities/$createdLeadActivityId/complete" },
    @{ Method="POST"; Path="/api/crm/activities/$createdLeadActivityId/cancel" },
    @{ Method="DELETE"; Path="/api/crm/activities/$createdLeadActivityId" }
)) {
    $response = Invoke-CrmRequest $negative.Method $negative.Path $negative.Body
    Assert-Status $response @(404,423) "Productive Activity route negative $($negative.Method) $($negative.Path)"
    $results.Add($response)
}

$deleteFoundation = Invoke-CrmRequest DELETE "/api/crm/foundation/activities/$createdLeadActivityId"
Assert-Status $deleteFoundation @(404,405) "DELETE foundation Activity route negative"
$results.Add($deleteFoundation)

$frontendRoute = Invoke-WebRequest -UseBasicParsing -Method GET -Uri "$FrontendUrl/foundation/activities" -TimeoutSec 15
if ($frontendRoute.StatusCode -ne 200 -or ($frontendRoute.Content -notmatch "CRM Foundation" -and $frontendRoute.Content -notmatch "crm-root")) {
    throw "Frontend Activity route did not return Angular shell."
}

$frontendApi = Invoke-WebRequest -UseBasicParsing -Method GET -Uri "$FrontendUrl/api/crm/foundation/activities" -TimeoutSec 15
if ($frontendApi.StatusCode -ne 200) { throw "Frontend proxy did not reach Activity foundation API." }

$frontendSource = Get-Content (Join-Path $PSScriptRoot "..\frontend\crm-web\src\main.ts") -Raw
if ($frontendSource -notmatch "/foundation/activities" -or $frontendSource -notmatch "statusLabel" -or $frontendSource -notmatch "Overdue") {
    throw "Frontend source does not contain expected Activity route/derived overdue behavior."
}
$latency = @($results | Where-Object { $_.Path -like "/api/crm/foundation/activities*" -and $_.Status -in @(200,400,404,405,409) })
$sorted = @($latency | Sort-Object Ms)
$average = [math]::Round(($latency | Measure-Object Ms -Average).Average, 2)
$minimum = ($latency | Measure-Object Ms -Minimum).Minimum
$p95Index = [math]::Min($sorted.Count - 1, [math]::Ceiling($sorted.Count * 0.95) - 1)
$p95 = $sorted[$p95Index].Ms

[pscustomobject]@{
    LocalBackendUrl=$BaseUrl; LocalFrontendUrl=$FrontendUrl; FrontendApiRoutingMode="Proxy"
    InitialActivityCount=$initialCount; AvailableFoundationLeadCount=@($leadJson.data).Count; AvailableFoundationContactCount=@($contactJson.data).Count
    IntegrationActivitySeed=$runId; CreatedLeadActivityId=$createdLeadActivityId; CreatedContactActivityId=$createdContactActivityId
    CreateLeadActivityScenario="PASS"; ReadAfterLeadCreate="PASS"; CreateContactActivityScenario="PASS"
    MissingTargetScenario="PASS"; MultipleTargetsScenario="PASS"; LeadTargetNotFoundScenario="PASS"; ContactTargetNotFoundScenario="PASS"
    UpdateActivityScenario="PASS"; ReadAfterUpdate="PASS"; NoChangeUpdateScenario="PASS"
    CompleteActivityScenario="PASS"; RepeatCompleteScenario="PASS"; CancelActivityScenario="PASS"; RepeatCancelScenario="PASS"
    CompleteCancelledStatus=$completeCancelled.Status; CancelCompletedStatus=$cancelCompleted.Status; UpdateCompletedStatus=$updateCompleted.Status; UpdateCancelledStatus=$updateCancelled.Status
    InvalidActivityTypeScenario="PASS"; SubjectRequiredScenario="PASS"; SubjectMaxLengthScenario="PASS"
    HistoricalScheduleScenario="PASS"; OverdueDerivedPresentation="SourceVerified"; FrontendRouteStatus=[int]$frontendRoute.StatusCode
    FrontendToActivityApiConnectivity="PASS"; ProxyOrCorsValidation="PASS"; ProductiveActivityRouteAvailable=$false; DeleteRouteAvailable=$false
    ReadAfterWriteConsistent=$true; FrontendNormalizationObserved=$true; RuntimePersistenceClassification="FoundationOnly"
    IntegrationLatencySamples=$latency.Count; LatencyMinMs=$minimum; LatencyAverageMs=$average; LatencyP95Ms=$p95
    NetworkEvidence=($results | Select-Object Method,Path,Status,Ms)
} | ConvertTo-Json -Depth 8
