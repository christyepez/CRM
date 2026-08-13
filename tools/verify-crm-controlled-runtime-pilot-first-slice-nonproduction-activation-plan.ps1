$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$plan = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-plan.md") -Raw
$goNoGo = Get-Content (Join-Path $root "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-go-no-go.md") -Raw
$p15 = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-report.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

foreach ($expected in @(
    "NonProductionActivationPlanOnly: true",
    "NonProductionActivationExecuted: false",
    "FirstSliceNonProductionActivationPlanPrepared: true"
)) {
    if ($plan -notlike "*$expected*") {
        throw "P16 plan missing expected marker: $expected"
    }
}

if ($goNoGo -notlike "*ProductionActivationDecision: NoGo*") {
    throw "P16 GO/NO-GO must preserve production NoGo."
}

if ($p15 -notlike "*ControlledRuntimePilotFirstSliceScaffoldValidationReadiness: FirstSliceScaffoldValidatedDisabledOnly*") {
    throw "P15 validation readiness evidence was not found."
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation plan verified."
