$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$p25 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p25-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation.md") -Raw
$p26 = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p26-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.md") -Raw
$approval = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.md") -Raw
$matrix = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-matrix.md") -Raw
$criteria = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-criteria.md") -Raw
$security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-security-decision.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

if ($p25 -notlike "*NonProductionActivationControlledImplementationValidatedDisabledOnly: true*") {
    throw "P25 disabled-only validation evidence was not found."
}

foreach ($expected in @(
    "ExplicitApprovalPrepared: true",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationExplicitApprovalGateOnly: true",
    "NonProductionActivationExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationExplicitApprovalReadiness: ExplicitApprovalPreparedNoGoNow",
    "NextGate: CrmSprint10P27ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionPlan"
)) {
    if ((@($p26, $approval, $matrix, $criteria, $security) -join "`n") -notlike "*$expected*") {
        throw "P26 explicit approval evidence missing marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation explicit approval verified."
