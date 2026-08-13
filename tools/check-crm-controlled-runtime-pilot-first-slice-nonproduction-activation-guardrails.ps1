$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p16-controlled-runtime-pilot-first-slice-nonproduction-activation-plan.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-prerequisites.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-approvals.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-environment-separation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-pre-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-post-smoke.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-evidence-plan.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-plan.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-plan.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P16 file: $path"
    }
}

$scanPaths = $paths
$joined = ($scanPaths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "NonProductionActivationPlanOnly: true",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationPlanReadiness: NonProductionActivationPlanPreparedNoGo"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P16 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"))) {
    if ($joined -like "*$pattern*") {
        throw "Forbidden P16 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation plan guardrails passed."
