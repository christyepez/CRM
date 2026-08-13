$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$dryRun = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run.md") -Raw
$flags = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-feature-flags.md") -Raw
$p16 = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-plan.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

foreach ($expected in @(
    "NonProductionActivationDryRunOnly: true",
    "NonProductionActivationExecuted: false",
    "FirstSliceNonProductionActivationDryRunPrepared: true"
)) {
    if ($dryRun -notlike "*$expected*") {
        throw "P17 dry run missing expected marker: $expected"
    }
}

foreach ($expected in @("FirstSlice: false", "PortalClient: false", "GatewayRoutes: false", "PortalNavigation: false")) {
    if ($flags -notlike "*$expected*") {
        throw "P17 flags dry run missing false marker: $expected"
    }
}

if ($p16 -notlike "*NonProductionActivationPlanOnly: true*") {
    throw "P16 activation plan evidence was not found."
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry run verified."
