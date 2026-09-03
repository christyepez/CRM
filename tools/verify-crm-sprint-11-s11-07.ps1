$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-11-s11-01-lead-qualification-contracts.md",
    "docs/roadmap/crm-sprint-11-s11-02-lead-qualification-application-service.md",
    "docs/roadmap/crm-sprint-11-s11-03-lead-qualification-api-foundation.md",
    "docs/roadmap/crm-sprint-11-s11-04-lead-intake-frontend-foundation.md",
    "docs/roadmap/crm-sprint-11-s11-05-lead-qualification-test-guardrail-hardening.md",
    "docs/roadmap/crm-sprint-11-s11-06-lead-qualification-local-integration.md",
    "docs/roadmap/crm-sprint-11-lead-qualification-closure.md",
    "codex/prompts/sprint-12-contact-management-p1.md",
    "tools/verify-crm-sprint-11-s11-06.ps1"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S11-07 evidence file: $file"
    }
}

$closure = Get-Content "docs/roadmap/crm-sprint-11-lead-qualification-closure.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$source = Get-ChildItem -Path "src/CRM.Domain/LeadQualification","src/CRM.Application/Foundation","src/CRM.Api/Foundation","frontend/crm-web/src" -Recurse -File |
    Where-Object { $_.FullName -notmatch "\\bin\\|\\obj\\|node_modules|dist" } |
    ForEach-Object { Get-Content $_.FullName -Raw }
$sourceText = $source -join "`n"

$requiredClosureMarkers = @(
    "S1107Decision: ClosedSuccessfully",
    "LeadQualificationDomainClosure: PASS",
    "LeadQualificationApplicationClosure: PASS",
    "Lead Qualification API closure: PASS",
    "Lead Qualification frontend closure: PASS",
    "Lead Qualification integration closure: PASS",
    "Lead Qualification security closure: PASS",
    "CriticalClosureBlockers: 0",
    'RecommendedNextSliceId: `S12-CONTACT-MGMT`',
    "RecommendedNextSprint: Sprint12"
)

foreach ($marker in $requiredClosureMarkers) {
    if (-not $closure.Contains($marker)) {
        throw "S11-07 closure document missing marker: $marker"
    }
}

if (-not $program.Contains('/api/crm/foundation/leads/{leadId}/qualification')) {
    throw "Foundation Lead Qualification route is missing."
}

if ($program.Contains('/api/crm/leads/{leadId}/qualification')) {
    throw "Productive Lead Qualification route must remain unavailable."
}

$authPattern = "UseAuthentication|UseAuthorization|localStorage|sessionStorage|access_token|refresh_token|Bearer "
if ($sourceText -match $authPattern) {
    throw "S11-07 detected token/auth runtime behavior in source."
}

$dataPattern = "MigrationBuilder|new SqlConnection|SqlConnection\(|AddDbContext\("
if ($sourceText -match $dataPattern) {
    throw "S11-07 detected database runtime/schema coupling."
}

if (-not $tasks.Contains("S1107Decision: ClosedSuccessfully") -or -not $tasks.Contains("NextTaskPhase: CRM Sprint 12 P1 - Contact Management Functional Baseline and Backlog")) {
    throw "codex/TASKS.md must record S11-07 closure and Sprint 12 handoff."
}

if ($nextTask.Contains("CRM Sprint 11 S11-07 - Lead Qualification Sprint Closure")) {
    throw "codex/next-task.md must not point back to S11-07 after closure."
}

if (-not ($nextTask.Contains("CRM Sprint 12 P1 - Contact Management Functional Baseline and Backlog") -or $nextTask.Contains("CRM Sprint 12 S12-01 - Contact Contracts and Domain Rules"))) {
    throw "codex/next-task.md must point to Sprint 12 Contact Management planning or the selected first implementation story."
}

Write-Host "CRM Sprint 11 S11-07 verification passed."
