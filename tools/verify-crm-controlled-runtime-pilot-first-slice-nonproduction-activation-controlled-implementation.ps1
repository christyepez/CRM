$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$program = Get-Content (Join-Path $root "src/CRM.Api/Program.cs") -Raw
$service = Get-Content (Join-Path $root "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService.cs") -Raw
$disabled = Get-Content (Join-Path $root "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledControlledNonProductionActivationService.cs") -Raw
$p23 = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate.md") -Raw
$doc = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation.md") -Raw
$dryRun = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-dry-run.md") -Raw
$security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-security-decision.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

if ($p23 -notlike "*ControlledRuntimePilotFirstSliceNonProductionActivationFinalApprovalGateReadiness: FinalApprovalGatePreparedConditionalGoFutureNoGoNow*") {
    throw "P23 final approval gate evidence was not found."
}

if ($program -notlike "*/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation*") {
    throw "P24 foundation endpoint is not registered."
}

foreach ($expected in @(
    "NonProductionActivationControlledImplementationPrepared: true",
    "NonProductionActivationControlledImplementationExecuted: false",
    "ConditionalGoFutureExecuted: false",
    "NonProductionActivationExecuted: false",
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "CommonDbRuntimeEnabled: false"
)) {
    if ($service -notlike "*$expected*") {
        throw "P24 status service missing marker: $expected"
    }
}

foreach ($expected in @("ControlledImplementationExecuted: false", "ActivationAttempted: false", "ActivationExecuted: false", "ExternalCallAttempted: false", "PortalCouplingEnabled: false")) {
    if ($disabled -notlike "*$expected*") {
        throw "P24 disabled service missing no-op marker: $expected"
    }
}

foreach ($expected in @(
    "ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationReadiness: ControlledImplementationPreparedDisabledOnly",
    "NextGate: CrmSprint10P25ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationValidation"
)) {
    if ((@($doc, $dryRun, $security) -join "`n") -notlike "*$expected*") {
        throw "P24 evidence docs missing marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation controlled implementation verified."
