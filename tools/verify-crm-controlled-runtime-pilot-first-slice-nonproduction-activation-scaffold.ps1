$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$service = Get-Content (Join-Path $root "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService.cs") -Raw
$disabled = Get-Content (Join-Path $root "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledNonProductionActivationService.cs") -Raw
$program = Get-Content (Join-Path $root "src/CRM.Api/Program.cs") -Raw
$p20 = @(
    Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-review.md") -Raw
    Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p20-controlled-runtime-pilot-first-slice-activation-readiness-review.md") -Raw
    Get-Content (Join-Path $root "codex/TASKS.md") -Raw
) -join "`n"
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

foreach ($expected in @(
    "NonProductionActivationScaffoldOnly: true",
    "NonProductionActivationExecuted: false",
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "CommonDbRuntimeEnabled: false"
)) {
    if ($service -notlike "*$expected*") {
        throw "P21 scaffold service missing expected marker: $expected"
    }
}

foreach ($expected in @("ActivationAttempted: false", "ActivationExecuted: false", "ExternalCallAttempted: false", "PortalCouplingEnabled: false")) {
    if ($disabled -notlike "*$expected*") {
        throw "P21 disabled activation service missing no-op marker: $expected"
    }
}

if ($program -notlike "*/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold*") {
    throw "P21 foundation endpoint is not registered."
}

if ($p20 -notlike "*ActivationReadinessReviewOnly: true*") {
    throw "P20 readiness review evidence was not found."
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation scaffold verified."
