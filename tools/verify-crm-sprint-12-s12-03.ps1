$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Application/ContactManagement/IContactManagementService.cs",
    "src/CRM.Api/Foundation/ContactManagementApiContracts.cs",
    "tests/CRM.UnitTests/ContactFoundationApiEndpointTests.cs",
    "docs/roadmap/crm-sprint-12-s12-03-contact-foundation-api-integration.md",
    "codex/prompts/sprint-12-contact-management-s12-04.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing Sprint 12 S12-03 artifact: $file"
    }
}

$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$apiContracts = Get-Content "src/CRM.Api/Foundation/ContactManagementApiContracts.cs" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-12-s12-03-contact-foundation-api-integration.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

if (-not $program.Contains("AddSingleton<IContactManagementService, ContactManagementService>")) {
    throw "IContactManagementService must be registered in DI."
}

foreach ($marker in @("MapPost(`"/api/crm/foundation/contacts`"", "MapPut(`"/api/crm/foundation/contacts/{id}`"", "IContactManagementService", "ContactManagementApiResponse.ToApplicationRequest")) {
    if (-not $program.Contains($marker.Replace("`"", '"'))) {
        throw "Program.cs missing Contact foundation API wiring marker: $marker"
    }
}

foreach ($marker in @("FoundationContactCreateRequest", "FoundationContactUpdateRequest", "ContactManagementCreateApplicationRequest", "ContactManagementUpdateApplicationRequest", "ToStatusCode")) {
    if (-not $apiContracts.Contains($marker)) {
        throw "Contact API contracts missing explicit mapping marker: $marker"
    }
}

foreach ($marker in @(
    "ContactManagementImplementationStatus: ApiFoundationIntegrated",
    "ContactManagementApi: FoundationIntegrated",
    "ProductiveContactRouteEnabled: false",
    "DeleteBehaviorAdded: false",
    "LeadContactRuntimeImplemented: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "FoundationContactApiBackwardCompatible: true",
    "MassAssignmentRisk: Controlled",
    "S1203Decision: Implemented")) {
    if (-not $doc.Contains($marker)) {
        throw "S12-03 roadmap document missing marker: $marker"
    }
}

if ($program.Contains('MapPost("/api/crm/contacts') -or $program.Contains('MapPut("/api/crm/contacts') -or $program.Contains('MapDelete("/api/crm')) {
    throw "Productive Contact route or DELETE route must remain unavailable."
}

foreach ($marker in @("Authorization", "Bearer", "UseSqlServer", "SqlConnection", "DbContext", "MigrationBuilder", "ConvertLead")) {
    if ($apiContracts.Contains($marker)) {
        throw "Contact API integration contract contains forbidden marker: $marker"
    }
}

if (-not ($nextTask.Contains("CRM Sprint 12 S12-04 - Contact Management Frontend Foundation Page") -or $nextTask.Contains("CRM Sprint 12 S12-05 - Contact Management Test and Guardrail Hardening"))) {
    throw "codex/next-task.md must point to S12-04 or the approved S12-05 follow-up."
}

if (-not ($nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-04.md") -or $nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-05.md"))) {
    throw "codex/next-task.md must reference the S12-04 prompt or the approved S12-05 prompt."
}

if (-not $tasks.Contains("S1203Decision: Implemented")) {
    throw "codex/TASKS.md must record S12-03 implementation."
}

Write-Host "CRM Sprint 12 S12-03 verification passed."
