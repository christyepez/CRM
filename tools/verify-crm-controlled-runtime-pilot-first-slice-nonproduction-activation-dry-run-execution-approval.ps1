$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$p28 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p28-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation.md") -Raw
$p29 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p29-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.md") -Raw
$approval = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.md") -Raw
$matrix = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-matrix.md") -Raw
$security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-security-decision.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

if ($p28 -notlike "*DryRunExecutionPlanValidated: true*" -or $p28 -notlike "*DryRunExecuted: false*") {
    throw "P28 dry-run execution validation evidence was not found."
}

foreach ($expected in @(
    "DryRunExecutionApprovalPrepared: true",
    "DryRunExecutionApprovalExecuted: false",
    "DryRunExecuted: false",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationDryRunExecutionApprovalGateOnly: true",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionApprovalReadiness: DryRunExecutionApprovalPreparedNoGoNow",
    "NextGate: CrmSprint10P30ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecution"
)) {
    if ((@($p29, $approval, $matrix, $security) -join "`n") -notlike "*$expected*") {
        throw "P29 dry-run execution approval evidence missing marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution approval verified."
