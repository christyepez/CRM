$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Application/ContactManagement/IContactManagementService.cs",
    "src/CRM.Application/ContactManagement/ContactManagementService.cs",
    "src/CRM.Application/ContactManagement/ContactManagementApplicationContracts.cs",
    "tests/CRM.UnitTests/ContactManagementServiceTests.cs",
    "docs/roadmap/crm-sprint-12-s12-02-contact-application-service.md",
    "codex/prompts/sprint-12-contact-management-s12-03.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing Sprint 12 S12-02 artifact: $file"
    }
}

$service = Get-Content "src/CRM.Application/ContactManagement/ContactManagementService.cs" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-12-s12-02-contact-application-service.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$applicationSources = Get-ChildItem "src/CRM.Application/ContactManagement" -Filter "*.cs" | Get-Content -Raw

foreach ($marker in @("IContactFoundationStore", "ContactManagementPolicy", "SavePreviewAsync", "GetPreviewByIdAsync")) {
    if (-not $service.Contains($marker)) {
        throw "ContactManagementService missing required marker: $marker"
    }
}

foreach ($marker in @(
    "ContactManagementImplementationStatus: ApplicationServiceImplemented",
    "ContactManagementPolicyInvoked: true",
    "DomainRulesDuplicatedInApplication: false",
    "NoChangePersistenceSuppressed: true",
    "ProductiveContactRouteEnabled: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "S1202Decision: Implemented")) {
    if (-not $doc.Contains($marker)) {
        throw "S12-02 roadmap document missing marker: $marker"
    }
}

if (-not $nextTask.Contains("CRM Sprint 12 S12-03 - Contact Foundation API Integration")) {
    throw "codex/next-task.md must point to S12-03."
}

if (-not $nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-03.md")) {
    throw "codex/next-task.md must reference the S12-03 prompt."
}

if (-not $tasks.Contains("S1202Decision: Implemented")) {
    throw "codex/TASKS.md must record S12-02 implementation."
}

if ($program.Contains('MapPost("/api/crm/contacts') -or $program.Contains('MapPut("/api/crm/contacts') -or $program.Contains('MapDelete("/api/crm/contacts')) {
    throw "Productive Contact route must remain locked/unavailable."
}

$forbiddenApplicationMarkers = @("CRM.Infrastructure", "SqlConnection", "UseSqlServer", "AuthorizationHeader", "Bearer", "DbContext", "MigrationBuilder")
foreach ($marker in $forbiddenApplicationMarkers) {
    if ($applicationSources -match [regex]::Escape($marker)) {
        throw "S12-02 application sources contain forbidden runtime coupling marker: $marker"
    }
}

Write-Host "CRM Sprint 12 S12-02 verification passed."
