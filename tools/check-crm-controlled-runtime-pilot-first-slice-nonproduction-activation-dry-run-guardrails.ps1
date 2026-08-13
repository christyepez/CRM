$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p17-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-steps.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-prerequisites.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-approvals.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-environment-separation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-pre-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-post-smoke.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-evidence-matrix.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P17 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "NonProductionActivationDryRunOnly: true",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunReadiness: NonProductionActivationDryRunCompletedDisabledOnly"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P17 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"))) {
    if ($joined -like "*$pattern*") {
        throw "Forbidden P17 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry run guardrails passed."
