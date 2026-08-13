$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$p21Service = Get-Content (Join-Path $root "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService.cs") -Raw
$p21Disabled = Get-Content (Join-Path $root "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledNonProductionActivationService.cs") -Raw
$p22Report = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-report.md") -Raw
$p23Gate = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate.md") -Raw
$p23Decision = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-decision-matrix.md") -Raw
$p23Security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-security-decision.md") -Raw
$p23Conditions = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-p24-conditions.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

foreach ($expected in @(
    "NonProductionActivationScaffoldOnly: true",
    "NonProductionActivationExecuted: false",
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "CommonDbRuntimeEnabled: false"
)) {
    if ($p21Service -notlike "*$expected*") {
        throw "P21 scaffold service missing disabled marker: $expected"
    }
}

foreach ($expected in @("ActivationAttempted: false", "ActivationExecuted: false", "ExternalCallAttempted: false", "PortalCouplingEnabled: false")) {
    if ($p21Disabled -notlike "*$expected*") {
        throw "P21 disabled activation service missing no-op marker: $expected"
    }
}

foreach ($expected in @(
    "NonProductionActivationScaffoldValidatedDisabledOnly: true",
    "ConditionalFutureGoExecuted: false"
)) {
    if ($p22Report -notlike "*$expected*") {
        throw "P22 scaffold validation evidence missing marker: $expected"
    }
}

foreach ($expected in @(
    "CrmSprint10P23ControlledRuntimePilotFirstSliceNonProductionActivationFinalApprovalGateExists: true",
    "NonProductionActivationFinalApprovalGateOnly: true",
    "ConditionalGoFutureDefined: true",
    "ConditionalGoFutureExecuted: false",
    "NonProductionActivationExecuted: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationFinalApprovalGateReadiness: FinalApprovalGatePreparedConditionalGoFutureNoGoNow",
    "NextGate: CrmSprint10P24ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementation"
)) {
    if ((@($p23Gate, $p23Decision, $p23Security, $p23Conditions) -join "`n") -notlike "*$expected*") {
        throw "P23 final approval gate evidence missing marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation final approval gate verified."
