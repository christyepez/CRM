$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-13-s13-06-activity-local-integration.md",
    "tools/run-crm-sprint-13-s13-06-local-integration.ps1",
    "tools/verify-crm-sprint-13-s13-06.ps1",
    "codex/prompts/sprint-13-activity-follow-up-s13-07.md",
    "frontend/crm-web/src/main.ts",
    "frontend/crm-web/proxy.conf.json",
    "frontend/crm-web/tools/serve-local-integration.mjs",
    "src/CRM.Api/Program.cs",
    "src/CRM.Application/ActivityManagement/ActivityManagementService.cs",
    "src/CRM.Domain/ActivityManagement/ActivityManagementPolicy.cs",
    "src/CRM.Infrastructure/Persistence/Foundation/InMemoryActivityFoundationStore.cs"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing S13-06 artifact or prerequisite: $file"
    }
}

$doc = Get-Content "docs/roadmap/crm-sprint-13-s13-06-activity-local-integration.md" -Raw
$runner = Get-Content "tools/run-crm-sprint-13-s13-06-local-integration.ps1" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw
$proxy = Get-Content "frontend/crm-web/proxy.conf.json" -Raw
$server = Get-Content "frontend/crm-web/tools/serve-local-integration.mjs" -Raw
$service = Get-Content "src/CRM.Application/ActivityManagement/ActivityManagementService.cs" -Raw
$policy = Get-Content "src/CRM.Domain/ActivityManagement/ActivityManagementPolicy.cs" -Raw
$store = Get-Content "src/CRM.Infrastructure/Persistence/Foundation/InMemoryActivityFoundationStore.cs" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "ActivityManagementImplementationStatus: LocalIntegrationValidated",
    "ActivityManagementLocalIntegration: Validated",
    "ProductiveActivityRouteEnabled: false",
    "DeleteRouteAvailable: false",
    "PortalRuntimeEnabled: false",
    "TokenRuntimeObserved: false",
    "CommonDbRuntimeObserved: false",
    "RuntimePersistenceClassification: FoundationOnly",
    "AccountRuntimeObserved: false",
    "OpportunityRuntimeObserved: false",
    "AssignmentRuntimeObserved: false",
    "PiiPayloadLogged: false",
    "SensitiveRuntimeLogDetected: false",
    "CriticalIntegrationLogErrors: false",
    "FrontendRuntimeErrors: false",
    "ReadAfterWriteConsistent: true",
    "RealDataDetected: false",
    "SimulatedProductionTouched: false",
    "S1306Decision: Implemented")) {
    if (-not $doc.Contains($marker)) {
        throw "S13-06 integration document missing marker: $marker"
    }
}

foreach ($marker in @(
    "GET /api/crm/foundation/activities",
    "POST foundation Activity",
    "PUT -> 200",
    'POST `/complete`',
    'POST `/cancel`',
    "Missing Lead",
    "Missing Contact",
    "Historical schedule",
    "Overdue is derived UI only",
    "DELETE foundation Activity route negative",
    "LatencyAverageMs")) {
    if (-not ($doc.Contains($marker) -or $runner.Contains($marker))) {
        throw "S13-06 local integration evidence marker missing: $marker"
    }
}

foreach ($marker in @(
    "http://localhost:8093",
    "http://127.0.0.1:4200",
    "/api/crm/foundation/activities",
    "/foundation/activities",
    "MissingTargetScenario",
    "MultipleTargetsScenario",
    "LeadTargetNotFoundScenario",
    "ContactTargetNotFoundScenario",
    "NoChangeUpdateScenario",
    "CompleteActivityScenario",
    "CancelActivityScenario",
    "HistoricalScheduleScenario",
    "OverdueDerivedPresentation",
    "ProductiveActivityRouteAvailable",
    "DeleteRouteAvailable",
    "ProxyOrCorsValidation")) {
    if (-not $runner.Contains($marker)) {
        throw "S13-06 runner missing marker: $marker"
    }
}

foreach ($marker in @(
    'MapGet("/api/crm/foundation/activities"',
    'MapGet("/api/crm/foundation/activities/{id}"',
    'MapPost("/api/crm/foundation/activities"',
    'MapPut("/api/crm/foundation/activities/{id}"',
    'MapPost("/api/crm/foundation/activities/{id}/complete"',
    'MapPost("/api/crm/foundation/activities/{id}/cancel"')) {
    if (-not $program.Contains($marker)) {
        throw "Foundation Activity API route missing: $marker"
    }
}

$programWithoutFoundation = $program.
    Replace('"/api/crm/foundation/activities"', '""').
    Replace('"/api/crm/foundation/activities/{id}"', '""').
    Replace('"/api/crm/foundation/activities/{id}/complete"', '""').
    Replace('"/api/crm/foundation/activities/{id}/cancel"', '""')

foreach ($forbidden in @(
    '"/api/crm/activities"',
    'MapDelete("/api/crm/activities',
    'MapDelete("/api/crm/foundation/activities',
    "UseAuthentication",
    "UseAuthorization",
    "UseSqlServer",
    "SqlConnection")) {
    if ($programWithoutFoundation.Contains($forbidden)) {
        throw "Forbidden Activity API/runtime marker detected: $forbidden"
    }
}

if (-not $frontend.Contains("{ path: 'foundation/activities', component: ActivityManagementPageComponent }")) {
    throw "Frontend Activity foundation route is missing."
}

$activityStart = $frontend.IndexOf("type ActivityType")
$activityEnd = $frontend.IndexOf("selector: 'crm-home'")
$activitySource = if ($activityStart -ge 0 -and $activityEnd -gt $activityStart) { $frontend.Substring($activityStart, $activityEnd - $activityStart) } else { "" }

foreach ($marker in @(
    "/api/crm/foundation/activities",
    "createActivity(request",
    "updateActivity(id: string",
    "completeActivity(id: string)",
    "cancelActivity(id: string)",
    "toISOString()",
    "statusLabel(activity)",
    "overdue")) {
    if (-not $activitySource.Contains($marker)) {
        throw "Activity frontend marker missing: $marker"
    }
}

foreach ($forbidden in @(
    "/api/crm/activities",
    "path: 'activities'",
    "deleteActivity",
    ".delete<",
    "Delete activity",
    "localStorage",
    "sessionStorage",
    "Authorization",
    "Bearer",
    "bypassSecurityTrustHtml",
    "innerHTML",
    "console.log",
    "AccountId",
    "OpportunityId",
    "AssignedUserId",
    "OwnerId",
    "AgentId")) {
    if ($activitySource.Contains($forbidden)) {
        throw "Forbidden Activity frontend marker detected: $forbidden"
    }
}

foreach ($marker in @("Lead", "Contact", "NotFound", "GetByIdAsync", "SaveAsync")) {
    if (-not $service.Contains($marker)) {
        throw "Activity management service seam marker missing: $marker"
    }
}

foreach ($marker in @("Completed", "Cancelled", "Scheduled", "LeadId", "ContactId")) {
    if (-not $policy.Contains($marker)) {
        throw "Activity management policy marker missing: $marker"
    }
}

foreach ($marker in @("List<Activity>", "lock (sync)", "GetByIdAsync", "SaveAsync")) {
    if (-not $store.Contains($marker)) {
        throw "In-memory Activity foundation store marker missing: $marker"
    }
}

if (-not ($proxy.Contains("http://localhost:8093") -and $proxy.Contains("/api"))) {
    throw "Angular proxy must route /api to local CRM API."
}

if (-not ($server.Contains("127.0.0.1") -and $server.Contains("4200") -and $server.Contains("localhost:8093"))) {
    throw "Local integration frontend server must bind loopback and proxy to CRM API."
}

$pointsToS1307 = $nextTask.Contains("CRM Sprint 13 S13-07 - Activity / Follow-Up Sprint Closure") -and
    $nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-07.md") -and
    $nextTask.Contains("S13-06 merge commit required")

$pointsToForwardHandoff = $nextTask.Contains("CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog") -and
    $nextTask.Contains("codex/prompts/sprint-14-opportunity-pipeline-p1.md") -and
    $nextTask.Contains("S13-07 merge commit required")

if (-not ($pointsToS1307 -or $pointsToForwardHandoff)) {
    throw "codex/next-task.md must point to S13-07 or an approved post-S13-07 Sprint 14 handoff."
}

foreach ($marker in @(
    "S1306Decision: Implemented",
    "NextTaskPhase: CRM Sprint 13 S13-07 - Activity / Follow-Up Sprint Closure")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md missing S13-06 marker: $marker"
    }
}

Write-Host "CRM Sprint 13 S13-06 verification passed."
