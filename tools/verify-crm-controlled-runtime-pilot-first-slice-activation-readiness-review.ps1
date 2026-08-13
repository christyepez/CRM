$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$review = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-review.md") -Raw
$flags = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-feature-flags-review.md") -Raw
$p19 = @(
    Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.md") -Raw
    Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p19-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.md") -Raw
    Get-Content (Join-Path $root "codex/TASKS.md") -Raw
) -join "`n"
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

foreach ($expected in @(
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "PortalAuthDuplicated: false"
)) {
    if ($review -notlike "*$expected*") {
        throw "P20 readiness review missing expected marker: $expected"
    }
}

foreach ($expected in @("FirstSlice: false", "PortalClient: false", "GatewayRoutes: false", "PortalNavigation: false")) {
    if ($flags -notlike "*$expected*") {
        throw "P20 feature flag review missing false marker: $expected"
    }
}

if ($p19 -notlike "*NonProductionActivationImplementationPlanOnly: true*") {
    throw "P19 implementation plan evidence was not found."
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice activation readiness review verified."
