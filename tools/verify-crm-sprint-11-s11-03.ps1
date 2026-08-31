$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Api/Foundation/LeadQualificationApiContracts.cs",
    "docs/roadmap/crm-sprint-11-s11-03-lead-qualification-api-foundation.md",
    "codex/prompts/sprint-11-lead-qualification-s11-04.md",
    "tests/CRM.UnitTests/LeadQualificationApiContractsTests.cs",
    "tools/verify-crm-sprint-11-s11-03.ps1"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S11-03 file: $file"
    }
}

$program = Get-Content "src/CRM.Api/Program.cs" -Raw
if (-not $program.Contains("/api/crm/foundation/leads/{leadId}/qualification")) {
    throw "Foundation Lead Qualification endpoint is not registered."
}

if ($program.Contains("/api/crm/leads/{leadId}/qualification")) {
    throw "Productive Lead Qualification route must not be registered."
}

if ($program.Contains("LeadQualificationPolicy")) {
    throw "API endpoint must not depend on domain policy directly."
}

$apiSource = Get-Content "src/CRM.Api/Foundation/LeadQualificationApiContracts.cs" -Raw
if ($apiSource -match "ConnectionString|DbContext|Bearer|token|Authorization") {
    throw "S11-03 API contracts must not introduce auth/db runtime coupling."
}

Write-Host "CRM Sprint 11 S11-03 verification passed."

