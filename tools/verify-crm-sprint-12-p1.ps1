$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-11-lead-qualification-closure.md",
    "docs/roadmap/crm-sprint-12-p1-contact-management-functional-baseline.md",
    "docs/roadmap/crm-sprint-12-contact-management-roadmap.md",
    "codex/prompts/sprint-12-contact-management-s12-01.md",
    "tools/verify-crm-sprint-11-s11-07.ps1"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required Sprint 12 P1 evidence file: $file"
    }
}

$baseline = Get-Content "docs/roadmap/crm-sprint-12-p1-contact-management-functional-baseline.md" -Raw
$roadmap = Get-Content "docs/roadmap/crm-sprint-12-contact-management-roadmap.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw

$baselineMarkers = @(
    "ContactDomainStatus: PartiallyImplemented",
    "ContactApplicationStatus: FoundationOnly",
    "ContactPersistenceArchitecture: Foundation/NonProduction seam",
    "ContactApiStatus: FoundationImplemented",
    "ContactFrontendStatus: DashboardReferenceOnly",
    "LeadContactRelationshipExists: false",
    "LeadContactDecision: ContractOnlyLater",
    "AccountRelationshipRequiredForFoundation: false",
    "ProductiveContactRouteEnabled: false",
    "Sprint12P1Decision: ReadyForS1201ContactContractsAndDomainRules"
)

foreach ($marker in $baselineMarkers) {
    if (-not $baseline.Contains($marker)) {
        throw "Sprint 12 P1 baseline missing marker: $marker"
    }
}

foreach ($story in @("S12-01", "S12-02", "S12-03", "S12-04", "S12-05", "S12-06", "S12-07")) {
    if (-not $roadmap.Contains($story)) {
        throw "Sprint 12 roadmap missing story: $story"
    }
}

if (-not $program.Contains('/api/crm/foundation/contacts')) {
    throw "Foundation Contact route is missing."
}

if ($program.Contains('"\/api\/crm\/contacts"') -or $program.Contains('"/api/crm/contacts"')) {
    throw "Productive Contact route must remain unavailable by default."
}

if (-not ($nextTask.Contains("CRM Sprint 12 S12-01 - Contact Contracts and Domain Rules") -or $nextTask.Contains("CRM Sprint 12 S12-02 - Contact Application Service") -or $nextTask.Contains("CRM Sprint 12 S12-03 - Contact Foundation API Integration") -or $nextTask.Contains("CRM Sprint 12 S12-04 - Contact Management Frontend Foundation Page") -or $nextTask.Contains("CRM Sprint 12 S12-05 - Contact Management Test and Guardrail Hardening"))) {
    throw "codex/next-task.md must point to Sprint 12 S12-01 or an approved Sprint 12 follow-up."
}

if ($nextTask.Contains("CRM Sprint 12 P1 - Contact Management Functional Baseline and Backlog")) {
    throw "codex/next-task.md must not point back to Sprint 12 P1 after completion."
}

if (-not $tasks.Contains("Sprint12P1Decision: ReadyForS1201ContactContractsAndDomainRules")) {
    throw "codex/TASKS.md must record Sprint 12 P1 decision."
}

Write-Host "CRM Sprint 12 P1 verification passed."
