$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Application/Foundation/LeadQualificationService.cs",
    "src/CRM.Application/Foundation/LeadQualificationContracts.cs",
    "src/CRM.Domain/LeadQualification/LeadQualificationPolicy.cs",
    "tests/CRM.UnitTests/LeadQualificationServiceTests.cs",
    "docs/roadmap/crm-sprint-11-s11-02-lead-qualification-application-service.md",
    "codex/prompts/sprint-11-lead-qualification-s11-03.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S11-02 file: $file"
    }
}

$service = Get-Content "src/CRM.Application/Foundation/LeadQualificationService.cs" -Raw
if (-not $service.Contains("LeadQualificationPolicy.Evaluate")) {
    throw "LeadQualificationService must invoke LeadQualificationPolicy."
}

if (-not $service.Contains("ILeadFoundationStore")) {
    throw "LeadQualificationService must use the existing foundation seam."
}

$program = Get-Content "src/CRM.Api/Program.cs" -Raw
if ($program.Contains('"/api/crm/leads"')) {
    throw "Productive lead route must remain locked/not registered."
}

if ($service -match "Authorization|ConnectionString|DbContext|UseSqlServer") {
    throw "S11-02 service must not introduce auth/db runtime coupling."
}

Write-Host "CRM Sprint 11 S11-02 verification passed."

