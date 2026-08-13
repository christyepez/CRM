$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$plan = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.md") -Raw
$flags = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-feature-flags.md") -Raw
$p18 = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

foreach ($expected in @(
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "PortalAuthDuplicated: false"
)) {
    if ($plan -notlike "*$expected*") {
        throw "P19 implementation plan missing expected marker: $expected"
    }
}

foreach ($expected in @("FirstSlice: false", "PortalClient: false", "GatewayRoutes: false", "PortalNavigation: false")) {
    if ($flags -notlike "*$expected*") {
        throw "P19 feature flag plan missing false marker: $expected"
    }
}

if ($p18 -notlike "*RuntimePortalCallsEnabled: false*") {
    throw "P18 approval gate evidence was not found."
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation implementation plan verified."
