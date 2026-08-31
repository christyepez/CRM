$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Domain/LeadQualification/LeadQualificationPolicy.cs",
    "src/CRM.Domain/LeadQualification/LeadQualificationCommand.cs",
    "src/CRM.Domain/LeadQualification/LeadQualificationRuleResult.cs",
    "src/CRM.Application/Foundation/LeadQualificationContracts.cs",
    "tests/CRM.UnitTests/LeadQualificationPolicyTests.cs",
    "tests/CRM.UnitTests/LeadQualificationContractsTests.cs",
    "tests/CRM.ArchitectureTests/LeadQualificationArchitectureTests.cs",
    "docs/roadmap/crm-sprint-11-s11-01-lead-qualification-contracts.md",
    "codex/prompts/sprint-11-lead-qualification-s11-02.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S11-01 file: $file"
    }
}

$program = Get-Content "src/CRM.Api/Program.cs" -Raw
if ($program.Contains('"/api/crm/leads"')) {
    throw "Productive lead route must remain locked/not registered."
}

$qualificationSource = Get-ChildItem "src/CRM.Domain/LeadQualification","src/CRM.Application/Foundation" -Filter "*.cs" -Recurse |
    Where-Object { $_.Name -like "*LeadQualification*" } |
    Get-Content -Raw

if ($qualificationSource -match "Authorization|ConnectionString|DbContext") {
    throw "S11-01 qualification contracts must not introduce auth/db runtime coupling."
}

Write-Host "CRM Sprint 11 S11-01 verification passed."

