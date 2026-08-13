$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$program = Get-Content (Join-Path $root "src/CRM.Api/Program.cs") -Raw
$p24Service = Get-Content (Join-Path $root "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService.cs") -Raw
$p24Disabled = Get-Content (Join-Path $root "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledControlledNonProductionActivationService.cs") -Raw
$p24Wrapper = Get-Content (Join-Path $root "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation.ps1") -Raw
$report = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-report.md") -Raw
$evidence = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-evidence-matrix.md") -Raw
$endpoint = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-foundation-endpoint.md") -Raw
$dryRun = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-dry-run.md") -Raw
$security = Get-Content (Join-Path $root "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-security-decision.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

if ($p24Wrapper -notlike "*controlled implementation passed*") {
    throw "P24 wrapper was not found or does not contain expected pass text."
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
    if ($p24Service -notlike "*$expected*") {
        throw "P24 status service missing marker: $expected"
    }
}

foreach ($expected in @("ControlledImplementationExecuted: false", "ActivationAttempted: false", "ActivationExecuted: false", "ExternalCallAttempted: false", "PortalCouplingEnabled: false")) {
    if ($p24Disabled -notlike "*$expected*") {
        throw "P24 disabled service missing no-op marker: $expected"
    }
}

foreach ($expected in @(
    "NonProductionActivationControlledImplementationValidatedDisabledOnly: true",
    "NonProductionActivationControlledImplementationExecuted: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationValidationReadiness: ControlledImplementationValidatedDisabledOnly",
    "NextGate: CrmSprint10P26ControlledRuntimePilotFirstSliceNonProductionActivationExplicitApproval"
)) {
    if ((@($report, $evidence, $endpoint, $dryRun, $security) -join "`n") -notlike "*$expected*") {
        throw "P25 validation evidence missing marker: $expected"
    }
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation controlled implementation validation verified."
