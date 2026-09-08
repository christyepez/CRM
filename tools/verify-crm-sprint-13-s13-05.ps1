$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Infrastructure/Persistence/Foundation/InMemoryContactFoundationStore.cs",
    "tests/CRM.UnitTests/ActivityFoundationApiEndpointTests.cs",
    "tests/CRM.ArchitectureTests/ActivityCrossLayerGuardrailTests.cs",
    "docs/roadmap/crm-sprint-13-s13-05-activity-test-guardrail-hardening.md",
    "codex/prompts/sprint-13-activity-follow-up-s13-06.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S13-05 file: $file"
    }
}

$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw
$activityStart = $frontend.IndexOf("type ActivityType")
$activityEnd = $frontend.IndexOf("selector: 'crm-home'")
if ($activityStart -lt 0 -or $activityEnd -le $activityStart) {
    throw "Could not isolate Activity frontend source."
}
$activitySource = $frontend.Substring($activityStart, $activityEnd - $activityStart)
$endpointTests = Get-Content "tests/CRM.UnitTests/ActivityFoundationApiEndpointTests.cs" -Raw
$architectureTests = Get-Content "tests/CRM.ArchitectureTests/ActivityCrossLayerGuardrailTests.cs" -Raw
$contactStore = Get-Content "src/CRM.Infrastructure/Persistence/Foundation/InMemoryContactFoundationStore.cs" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-13-s13-05-activity-test-guardrail-hardening.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "FoundationActivityCreate_ContactTarget_ReturnsOkWithContactOnly",
    "FoundationActivityCreate_BothTargets_ReturnsBadRequest",
    "FoundationActivityCreate_MissingContactTarget_ReturnsNotFound",
    "FoundationActivityCreate_PastSchedule_IsAcceptedForHistoricalRecording",
    "FoundationActivityUpdate_CompletedActivity_ReturnsBadRequest",
    "FoundationActivityCancel_CompletedActivity_ReturnsBadRequest",
    "FoundationActivityComplete_CancelledActivity_ReturnsBadRequest",
    "FoundationActivityDelete_IsNotAvailable",
    "ProductiveActivityRoutes_RemainUnavailable")) {
    if (-not $endpointTests.Contains($marker)) {
        throw "S13-05 endpoint test marker missing: $marker"
    }
}

if (-not $contactStore.Contains("33333333-3333-3333-3333-333333333333") -or -not $contactStore.Contains("ActivityFoundationTargetValidation")) {
    throw "S13-05 Contact seed marker missing."
}

foreach ($marker in @(
    "ActivityType_ValuesMatchAcrossDomainApiAndFrontend",
    "ActivityStatus_ValuesMatchAcrossDomainApiAndFrontend",
    "ActivityApiAndFrontend_UseFoundationRoutesOnly",
    "ActivityFrontend_SecurityAndSafetyGuardrailsRemainClosed",
    "ActivityApplication_TargetValidation_IsolatedToLeadAndContactFoundationSeams")) {
    if (-not $architectureTests.Contains($marker)) {
        throw "S13-05 architecture test marker missing: $marker"
    }
}

foreach ($marker in @(
    "/api/crm/foundation/activities",
    "foundationActivitiesRoute = '/api/crm/foundation/activities'",
    "path: 'foundation/activities'",
    "isSubmitting",
    "toISOString()",
    "statusLabel(activity)")) {
    if (-not $frontend.Contains($marker) -and -not $activitySource.Contains($marker)) {
        throw "S13-05 frontend contract marker missing: $marker"
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
    "SqlConnection",
    "UseSqlServer",
    "AccountId",
    "OpportunityId",
    "AssignedUserId",
    "OwnerId",
    "AgentId")) {
    if ($activitySource.Contains($forbidden)) {
        throw "Forbidden Activity frontend marker detected: $forbidden"
    }
}

foreach ($forbidden in @(
    'MapGet("/api/crm/activities',
    'MapPost("/api/crm/activities',
    'MapPut("/api/crm/activities',
    'MapDelete("/api/crm/activities',
    'MapDelete("/api/crm/foundation/activities',
    "UseAuthentication",
    "UseAuthorization",
    "UseSqlServer",
    "SqlConnection")) {
    if ($program.Contains($forbidden)) {
        throw "Forbidden Activity API/runtime marker detected: $forbidden"
    }
}

foreach ($marker in @(
    "S1305Decision: Implemented",
    "ActivityManagementCoverageMatrix",
    "ActivityCrossLayerScenarioMatrix",
    "ActivityTypeParity: PASS",
    "ActivityStatusParity: PASS",
    "ActivityTargetContractParity: PASS",
    "TargetExistenceValidationIsolation: PASS",
    "FoundationActivityApiVerified: true",
    "FrontendUsesFoundationActivityApiOnly: true",
    "ProductiveActivityRouteAvailable: false",
    "DeleteBehaviorAdded: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "SchemaChangesDetected: false",
    "NextTaskPhase: CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation")) {
    if (-not $doc.Contains($marker)) {
        throw "S13-05 roadmap marker missing: $marker"
    }
}

if (-not (
    ($nextTask.Contains("CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation") -and
        $nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-06.md") -and
        $nextTask.Contains("S13-05 merge commit required")) -or
    ($nextTask.Contains("CRM Sprint 13 S13-07 - Activity / Follow-Up Sprint Closure") -and
        $nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-07.md") -and
        $nextTask.Contains("S13-06 merge commit required")))) {
    throw "codex/next-task.md must point to S13-06 or the approved S13-07 handoff."
}

foreach ($marker in @(
    "S1305Decision: Implemented",
    "NextTaskPhase: CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md missing S13-05 marker: $marker"
    }
}

Write-Host "CRM Sprint 13 S13-05 verification passed."
