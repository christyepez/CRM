$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$p26 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p26-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.md") -Raw
$p27 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p27-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.md") -Raw
$plan = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.md") -Raw
$commands = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-command-matrix.md") -Raw
$security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-security-decision.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

if ($p26 -notlike "*ExplicitApprovalPrepared: true*" -or $p26 -notlike "*ExplicitApprovalExecuted: false*") {
    throw "P26 explicit approval evidence was not found."
}

foreach ($expected in @(
    "DryRunExecutionPlanPrepared: true",
    "DryRunExecuted: false",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationDryRunExecutionPlanOnly: true",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionPlanReadiness: DryRunExecutionPlanPreparedNoGoNow",
    "NextGate: CrmSprint10P28ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionValidation"
)) {
    if ((@($p27, $plan, $commands, $security) -join "`n") -notlike "*$expected*") {
        throw "P27 dry-run execution plan evidence missing marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution plan verified."
