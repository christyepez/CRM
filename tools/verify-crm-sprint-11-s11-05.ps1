$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Domain/LeadQualification/LeadQualificationPolicy.cs",
    "src/CRM.Application/Foundation/LeadQualificationService.cs",
    "src/CRM.Api/Foundation/LeadQualificationApiContracts.cs",
    "frontend/crm-web/src/main.ts",
    "frontend/crm-web/src/styles.css",
    "tests/CRM.UnitTests/LeadQualificationPolicyTests.cs",
    "tests/CRM.UnitTests/LeadQualificationServiceTests.cs",
    "tests/CRM.UnitTests/LeadQualificationApiContractsTests.cs",
    "tests/CRM.UnitTests/LeadQualificationApiEndpointTests.cs",
    "tests/CRM.ArchitectureTests/LeadQualificationArchitectureTests.cs",
    "docs/roadmap/crm-sprint-11-s11-05-lead-qualification-test-guardrail-hardening.md",
    "codex/prompts/sprint-11-lead-qualification-s11-06.md",
    "tools/verify-crm-sprint-11-s11-01.ps1",
    "tools/verify-crm-sprint-11-s11-02.ps1",
    "tools/verify-crm-sprint-11-s11-03.ps1",
    "tools/verify-crm-sprint-11-s11-04.ps1"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S11-05 file: $file"
    }
}

& .\tools\verify-crm-sprint-11-s11-01.ps1
& .\tools\verify-crm-sprint-11-s11-02.ps1
& .\tools\verify-crm-sprint-11-s11-03.ps1
& .\tools\verify-crm-sprint-11-s11-04.ps1

$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$apiContracts = Get-Content "src/CRM.Api/Foundation/LeadQualificationApiContracts.cs" -Raw
$application = Get-Content "src/CRM.Application/Foundation/LeadQualificationService.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw
$frontendQualificationStart = $frontend.IndexOf("type LeadQualificationDecision")
$frontendQualificationEnd = $frontend.IndexOf("bootstrapApplication(AppComponent, {")
if ($frontendQualificationStart -lt 0 -or $frontendQualificationEnd -le $frontendQualificationStart) {
    throw "Unable to isolate Lead Qualification frontend slice."
}
$frontendQualification = $frontend.Substring($frontendQualificationStart, $frontendQualificationEnd - $frontendQualificationStart)

if (-not $program.Contains('/api/crm/foundation/leads/{leadId}/qualification')) {
    throw "Foundation qualification API route missing."
}

if ($program.Contains('/api/crm/leads/{leadId}/qualification')) {
    throw "Productive qualification API route must remain unavailable."
}

if (-not $application.Contains("LeadQualificationPolicy.Evaluate") -or -not $application.Contains("ILeadFoundationStore")) {
    throw "Application service must keep policy and foundation store isolation."
}

foreach ($field in @("Decision", "DisqualificationReason", "OtherReason", "Comment")) {
    if (-not $apiContracts.Contains($field)) {
        throw "Missing API request contract field: $field"
    }
}

foreach ($field in @("leadId", "previousStatus", "currentStatus", "decision", "disqualificationReason", "allowed", "changed", "errorCode", "message", "foundationMode", "persistenceMode", "productiveLeadQualificationRouteEnabled", "portalRuntimeEnabled", "commonDbRuntimeEnabled")) {
    if (-not $frontendQualification.Contains($field)) {
        throw "Missing frontend response/request semantic field: $field"
    }
}

foreach ($enumValue in @("Qualify", "Disqualify", "InvalidContactInformation", "Duplicate", "NoInterest", "OutOfTarget", "Unreachable", "Other")) {
    if (-not $frontendQualification.Contains("'$enumValue'")) {
        throw "Missing frontend enum parity value: $enumValue"
    }
}

if (-not $frontendQualification.Contains("if (this.qualificationForm.invalid || this.isSubmitting())")) {
    throw "Duplicate submission protection is missing."
}

if (-not $frontendQualification.Contains("/crm/foundation/leads/") -or $frontendQualification.Contains("/api/crm/leads")) {
    throw "Frontend must use only foundation Lead Qualification API routes."
}

if ($frontendQualification -match "innerHTML|bypassSecurityTrustHtml|document\.querySelector|localStorage|sessionStorage|access_token|refresh_token|Request\.Headers|Headers\[|Bearer ") {
    throw "Frontend Lead Qualification slice introduced unsafe DOM/token/header behavior."
}

if (($application + "`n" + $apiContracts + "`n" + $frontendQualification) -match "ConnectionStrings:|Server=|User Id=|Password=|new SqlConnection|SqlConnection\(|UseSqlServer\(|MigrationBuilder|AddDbContext\(") {
    throw "Lead Qualification slice introduced Common DB/SQL/migration coupling."
}

$nextTask = Get-Content "codex/next-task.md" -Raw
if (-not $nextTask.Contains("CRM Sprint 11 S11-06 - Lead Qualification Local Integration Validation")) {
    throw "next-task must point to S11-06."
}

Write-Host "CRM Sprint 11 S11-05 verification passed."
