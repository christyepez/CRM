$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Domain/ContactManagement/ContactManagementOperation.cs",
    "src/CRM.Domain/ContactManagement/ContactManagementErrorCode.cs",
    "src/CRM.Domain/ContactManagement/ContactManagementCommand.cs",
    "src/CRM.Domain/ContactManagement/ContactManagementRuleResult.cs",
    "src/CRM.Domain/ContactManagement/ContactManagementPolicy.cs",
    "tests/CRM.UnitTests/ContactManagementPolicyTests.cs",
    "tests/CRM.ArchitectureTests/ContactManagementArchitectureTests.cs",
    "docs/roadmap/crm-sprint-12-s12-01-contact-contracts-domain-rules.md",
    "codex/prompts/sprint-12-contact-management-s12-02.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing Sprint 12 S12-01 artifact: $file"
    }
}

$policy = Get-Content "src/CRM.Domain/ContactManagement/ContactManagementPolicy.cs" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-12-s12-01-contact-contracts-domain-rules.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw

$markers = @(
    "ContactMethodRequired: false",
    "ProductiveContactRouteEnabled: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "LeadContactRuntimeImplemented: false",
    "S1201Decision: Implemented"
)

foreach ($marker in $markers) {
    if (-not $doc.Contains($marker)) {
        throw "S12-01 roadmap document missing marker: $marker"
    }
}

foreach ($marker in @("PreferredContactMethodRequiresEmail", "PreferredContactMethodRequiresPhone", "InvalidAccountReferenceFormat", "ContactNotFound")) {
    if (-not $policy.Contains($marker)) {
        throw "ContactManagementPolicy missing deterministic rule marker: $marker"
    }
}

if (-not ($nextTask.Contains("CRM Sprint 12 S12-02 - Contact Application Service") -or $nextTask.Contains("CRM Sprint 12 S12-03 - Contact Foundation API Integration") -or $nextTask.Contains("CRM Sprint 12 S12-04 - Contact Management Frontend Foundation Page") -or $nextTask.Contains("CRM Sprint 12 S12-05 - Contact Management Test and Guardrail Hardening"))) {
    throw "codex/next-task.md must point to S12-02 or an approved Sprint 12 follow-up."
}

if (-not ($nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-02.md") -or $nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-03.md") -or $nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-04.md") -or $nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-05.md"))) {
    throw "codex/next-task.md must reference S12-02 or an approved Sprint 12 follow-up prompt."
}

if (-not $tasks.Contains("S1201Decision: Implemented")) {
    throw "codex/TASKS.md must record S12-01 implementation."
}

if ($program.Contains('MapPost("/api/crm/contacts') -or $program.Contains('MapPut("/api/crm/contacts') -or $program.Contains('MapDelete("/api/crm/contacts')) {
    throw "Productive Contact route must remain locked/unavailable."
}

Write-Host "CRM Sprint 12 S12-01 verification passed."
