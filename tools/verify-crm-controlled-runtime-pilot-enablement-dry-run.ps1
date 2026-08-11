param()

$ErrorActionPreference = "Continue"
$failures = @()

function RequirePath($Path) {
    if (-not (Test-Path $Path)) { $script:failures += "Missing required file: $Path" }
}

function RequireMarker($Text, $Marker) {
    if ($Text -notmatch [regex]::Escape($Marker)) { $script:failures += "Missing marker: $Marker" }
}

$requiredFiles = @(
    "docs/roadmap/crm-sprint-10-p8-controlled-runtime-pilot-enablement-dry-run.md",
    "docs/roadmap/crm-controlled-runtime-pilot-enablement-dry-run-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-enablement-dry-run-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-report.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-steps.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-entry-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-approval-result.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-preflight.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-smoke.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-dry-run-evidence-matrix.md",
    "docs/operations/crm-controlled-runtime-pilot-enablement-dry-run-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-enablement-dry-run-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-enablement-dry-run-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-enablement-dry-run.ps1",
    "tools/crm-controlled-runtime-pilot-enablement-dry-run.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P8ControlledRuntimePilotEnablementDryRunExists: true.",
    "CrmSprint10P7EnablementPlanReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ControlledRuntimePilotEnablementDryRunAttempted: true.",
    "ControlledRuntimePilotEnablementDryRunReportPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunStepsPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunEntryChecklistPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunApprovalResultPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunSafeConfigurationPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunFeatureFlagsPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunPreflightPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunSmokePrepared: true.",
    "ControlledRuntimePilotEnablementDryRunRollbackPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunEvidencePrepared: true.",
    "ControlledRuntimePilotEnablementDryRunRunbookPrepared: true.",
    "ControlledRuntimePilotEnablementDryRunSecurityDecisionPrepared: true.",
    "RuntimePortalCouplingEnabled: false.",
    "RuntimePortalCallsEnabled: false.",
    "ProductivePortalNavigationEnabled: false.",
    "ProductivePortalGatewayRoutesEnabled: false.",
    "RealPortalPrivateUrlsPresent: false.",
    "PortalServicesInCrmCompose: false.",
    "CommonDbRuntimeEnabled: false.",
    "RealCommonDbConnectionConfigured: false.",
    "SharedPortalTablesAccessEnabled: false.",
    "CrossDomainMigrationsPresent: false.",
    "PortalDatabaseDirectAccessEnabled: false.",
    "PortalAuthDuplicated: false.",
    "PortalMenuDuplicated: false.",
    "PortalPermissionsDuplicated: false.",
    "PortalAuditDuplicated: false.",
    "PortalNotificationDuplicated: false.",
    "PortalConfigurationDuplicated: false.",
    "SsoOidcProductionConfigured: false.",
    "RealSecretProviderConfigured: false.",
    "RealNotificationProviderConfigured: false.",
    "RealObservabilityProviderConfigured: false.",
    "BrowserTokenStorageDetected: false.",
    "SecretsPresent: false.",
    "EnvRealFileCommitted: false.",
    "PrivateUrlsPresent: false.",
    "RealDataPresent: false.",
    "ControlledRuntimePilotEnablementDryRunReadiness: DryRunCompletedDisabledOnly.",
    "DryRunOnly: true.",
    "NextGate: CrmSprint10P9ControlledRuntimePilotEnablementApprovalGate."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot enablement dry run verified."
exit 0
