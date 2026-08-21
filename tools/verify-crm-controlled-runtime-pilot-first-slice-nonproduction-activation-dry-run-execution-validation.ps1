$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$p27 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p27-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.md") -Raw
$p28 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p28-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation.md") -Raw
$report = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-report.md") -Raw
$evidence = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-evidence-matrix.md") -Raw
$security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-security-decision.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

if ($p27 -notlike "*DryRunExecutionPlanPrepared: true*" -or $p27 -notlike "*DryRunExecuted: false*") {
    throw "P27 dry-run execution plan evidence was not found."
}

foreach ($expected in @(
    "DryRunExecutionPlanValidated: true",
    "DryRunExecuted: false",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationDryRunExecutionValidationOnly: true",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionValidationReadiness: DryRunExecutionValidationPreparedNoGoNow",
    "NextGate: CrmSprint10P29ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionApproval"
)) {
    if ((@($p28, $report, $evidence, $security) -join "`n") -notlike "*$expected*") {
        throw "P28 dry-run execution validation evidence missing marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution validation verified."
