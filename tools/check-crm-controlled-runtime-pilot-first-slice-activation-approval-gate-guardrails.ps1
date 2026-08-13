$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p18-controlled-runtime-pilot-first-slice-activation-approval-gate.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-evidence-summary.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-approvers.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-decision-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-compliance-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-blockers.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-raci.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-communication-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-audit-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-rollback.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-activation-approval-gate-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-activation-approval-gate.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-activation-approval-gate.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P18 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "ActivationApprovalGateOnly: true",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstSliceActivationApprovalGateReadiness: ActivationApprovalGatePreparedNoGo"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P18 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"))) {
    if ($joined -like "*$pattern*") {
        throw "Forbidden P18 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice activation approval gate guardrails passed."
