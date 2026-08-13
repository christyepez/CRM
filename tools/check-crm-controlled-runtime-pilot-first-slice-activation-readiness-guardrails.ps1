$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p20-controlled-runtime-pilot-first-slice-activation-readiness-review.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-activation-readiness-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-activation-readiness-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-review.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-evidence-summary.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-gaps.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-blockers.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-residual-risks.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-approval-review.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-implementation-plan-review.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-feature-flags-review.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-safe-configuration-review.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-disabled-client-review.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-qa-uat-review.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-rollback-review.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-readiness-evidence-audit-review.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-activation-readiness-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-activation-readiness-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-activation-readiness-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-activation-readiness-review.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-activation-readiness-review.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P20 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "ActivationReadinessReviewOnly: true",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstSliceActivationReadinessReviewReadiness: ActivationReadinessReviewPreparedNoGo"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P20 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"))) {
    if ($joined -like "*$pattern*") {
        throw "Forbidden P20 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice activation readiness guardrails passed."
