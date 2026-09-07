$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Api/Foundation/ActivityManagementApiContracts.cs",
    "tests/CRM.UnitTests/ActivityFoundationApiEndpointTests.cs",
    "docs/roadmap/crm-sprint-13-s13-03-activity-foundation-api.md",
    "codex/prompts/sprint-13-activity-follow-up-s13-04.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S13-03 file: $file"
    }
}

$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$contracts = Get-Content "src/CRM.Api/Foundation/ActivityManagementApiContracts.cs" -Raw
$tests = Get-Content "tests/CRM.UnitTests/ActivityFoundationApiEndpointTests.cs" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-13-s13-03-activity-foundation-api.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "IActivityManagementService",
    "ActivityManagementService",
    "IActivityFoundationStore",
    "InMemoryActivityFoundationStore",
    'MapGet("/api/crm/foundation/activities"',
    'MapGet("/api/crm/foundation/activities/{id}"',
    'MapPost("/api/crm/foundation/activities"',
    'MapPut("/api/crm/foundation/activities/{id}"',
    'MapPost("/api/crm/foundation/activities/{id}/complete"',
    'MapPost("/api/crm/foundation/activities/{id}/cancel"')) {
    if (-not $program.Contains($marker)) {
        throw "Program.cs missing S13-03 marker: $marker"
    }
}

foreach ($marker in @(
    "FoundationActivityCreateRequest",
    "FoundationActivityUpdateRequest",
    "ActivityManagementApiResponse",
    "ToApplicationRequest",
    "ToStatusCode",
    "ProductiveCrudEnabled: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false")) {
    if (-not $contracts.Contains($marker)) {
        throw "Activity API contract missing marker: $marker"
    }
}

foreach ($forbidden in @(
    '"/api/crm/activities"',
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
        throw "Forbidden S13-03 API marker detected: $forbidden"
    }
}

foreach ($marker in @(
    "FoundationActivityCreate_ValidLeadTarget_ReturnsOkAndFoundationFlags",
    "ProductiveActivityRoutes_RemainUnavailable",
    "FoundationActivityDelete_IsNotAvailable")) {
    if (-not $tests.Contains($marker)) {
        throw "S13-03 endpoint test marker missing: $marker"
    }
}

foreach ($marker in @(
    "S1303Decision: Implemented",
    "ActivityFoundationApi: Implemented",
    "ProductiveActivityRouteEnabled: false",
    "DeleteBehaviorAdded: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false")) {
    if (-not $doc.Contains($marker)) {
        throw "S13-03 roadmap marker missing: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 13 S13-05 - Activity / Follow-Up Test and Guardrail Hardening",
    "codex/prompts/sprint-13-activity-follow-up-s13-05.md",
    "S13-04 merge commit required")) {
    if (-not $nextTask.Contains($marker)) {
        throw "codex/next-task.md must point to S13-05 after S13-04: $marker"
    }
}

foreach ($marker in @(
    "S1303Decision: Implemented")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md missing S13-03 marker: $marker"
    }
}

Write-Host "CRM Sprint 13 S13-03 verification passed."
