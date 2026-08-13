$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p19-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-phases.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-wbs.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-pr-sequence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-change-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-client-activation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-health-smoke.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-qa-uat.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-evidence-audit.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P19 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "NonProductionActivationImplementationPlanOnly: true",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationImplementationPlanReadiness: NonProductionActivationImplementationPlanPreparedNoGo"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P19 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"))) {
    if ($joined -like "*$pattern*") {
        throw "Forbidden P19 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation implementation guardrails passed."
