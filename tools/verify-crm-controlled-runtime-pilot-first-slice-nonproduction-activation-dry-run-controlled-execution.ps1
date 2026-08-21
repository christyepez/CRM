$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$p29 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p29-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.md") -Raw
$p30 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p30-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution.md") -Raw
$report = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-report.md") -Raw
$security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-security-decision.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

if ($p29 -notlike "*DryRunExecutionApprovalPrepared: true*" -or $p29 -notlike "*DryRunExecutionApprovalExecuted: false*") {
    throw "P29 dry-run execution approval evidence was not found."
}

foreach ($expected in @(
    "DryRunControlledExecutionExecuted: true",
    "DryRunExecuted: true",
    "DryRunExternalCallExecuted: false",
    "DryRunPortalCallExecuted: false",
    "DryRunActivationExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecutionReadiness: DryRunControlledExecutionCompletedLocalNoOpNoGoNow",
    "NextGate: CrmSprint10P31ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecutionValidation"
)) {
    if ((@($p30, $report, $security) -join "`n") -notlike "*$expected*") {
        throw "P30 controlled dry-run evidence missing marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run controlled execution verified."
