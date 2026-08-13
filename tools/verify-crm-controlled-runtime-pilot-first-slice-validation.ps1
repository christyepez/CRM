$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$p14Service = Get-Content (Join-Path $root "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceScaffoldStatusService.cs") -Raw
$p14Client = Get-Content (Join-Path $root "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledPortalRuntimeClient.cs") -Raw
$program = Get-Content (Join-Path $root "src/CRM.Api/Program.cs") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
$p15Report = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-report.md") -Raw

foreach ($expected in @(
    "ProductionActivationDecision: `"NoGo`"",
    "CrmProductionReady: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstImplementationSliceScaffoldReadiness: `"FirstSliceScaffoldPreparedDisabledOnly`""
)) {
    if ($p14Service -notlike "*$expected*") {
        throw "P14 scaffold service validation failed. Missing: $expected"
    }
}

foreach ($expected in @("Attempted: false", "ExternalCallAttempted: false", "PortalCouplingEnabled: false", "PortalRoutesEnabled: false", "PortalNavigationEnabled: false")) {
    if ($p14Client -notlike "*$expected*") {
        throw "P14 disabled client validation failed. Missing: $expected"
    }
}

if ($program -notlike "*/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-scaffold*") {
    throw "P14 foundation endpoint is not registered."
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

if ($p15Report -notlike "*ControlledRuntimePilotFirstSliceScaffoldValidationReadiness: FirstSliceScaffoldValidatedDisabledOnly*") {
    throw "P15 validation report is missing readiness marker."
}

Write-Host "PASS CRM controlled runtime pilot first slice validation verified."
