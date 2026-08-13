$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$service = Get-Content (Join-Path $root "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService.cs") -Raw
$disabled = Get-Content (Join-Path $root "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledNonProductionActivationService.cs") -Raw
$program = Get-Content (Join-Path $root "src/CRM.Api/Program.cs") -Raw
$report = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-report.md") -Raw
$evidence = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-evidence-matrix.md") -Raw
$endpoint = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-foundation-endpoint.md") -Raw
$composeDoc = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-compose.md") -Raw
$security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-security-decision.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

foreach ($expected in @(
    "NonProductionActivationScaffoldOnly: true",
    "NonProductionActivationExecuted: false",
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "CommonDbRuntimeEnabled: false"
)) {
    if ($service -notlike "*$expected*") {
        throw "P21 scaffold service missing expected disabled marker: $expected"
    }
}

foreach ($expected in @("ActivationAttempted: false", "ActivationExecuted: false", "ExternalCallAttempted: false", "PortalCouplingEnabled: false")) {
    if ($disabled -notlike "*$expected*") {
        throw "P21 disabled service missing no-op marker: $expected"
    }
}

if ($program -notlike "*/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold*") {
    throw "P21 foundation endpoint is not registered."
}

foreach ($expected in @(
    "NonProductionActivationScaffoldValidatedDisabledOnly: true",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldValidationReadiness: NonProductionActivationScaffoldValidatedDisabledOnly"
)) {
    if ((@($report, $security) -join "`n") -notlike "*$expected*") {
        throw "P22 validation report/security decision missing expected marker: $expected"
    }
}

foreach ($expected in @(
    "FirstSliceNonProductionActivationScaffoldValidationEvidenceMatrixPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationFoundationEndpointPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationComposePrepared: true"
)) {
    if ((@($evidence, $endpoint, $composeDoc) -join "`n") -notlike "*$expected*") {
        throw "P22 evidence docs missing expected marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation scaffold validation verified."
