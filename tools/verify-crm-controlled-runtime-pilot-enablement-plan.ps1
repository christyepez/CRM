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
    "docs/roadmap/crm-sprint-10-p7-controlled-runtime-pilot-enablement-plan.md",
    "docs/roadmap/crm-controlled-runtime-pilot-enablement-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-enablement-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-entry-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-exit-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-rollback-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-preflight-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-smoke-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-evidence-plan.md",
    "docs/operations/crm-controlled-runtime-pilot-enablement-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-enablement-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-enablement-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-enablement-plan.ps1",
    "tools/crm-controlled-runtime-pilot-enablement-readiness.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P7ControlledRuntimePilotEnablementPlanExists: true.",
    "CrmSprint10P6ValidationReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ControlledRuntimePilotEnablementPlanAttempted: true.",
    "ControlledRuntimePilotEnablementPlanPrepared: true.",
    "ControlledRuntimePilotEntryChecklistPrepared: true.",
    "ControlledRuntimePilotExitChecklistPrepared: true.",
    "ControlledRuntimePilotFeatureFlagsPlanPrepared: true.",
    "ControlledRuntimePilotSafeConfigurationPrepared: true.",
    "ControlledRuntimePilotApprovalPlanPrepared: true.",
    "ControlledRuntimePilotRollbackPlanPrepared: true.",
    "ControlledRuntimePilotPreflightPlanPrepared: true.",
    "ControlledRuntimePilotSmokePlanPrepared: true.",
    "ControlledRuntimePilotEvidencePlanPrepared: true.",
    "ControlledRuntimePilotEnablementRunbookPrepared: true.",
    "ControlledRuntimePilotEnablementSecurityDecisionPrepared: true.",
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
    "ControlledRuntimePilotEnablementPlanReadiness: PlannedDisabledOnly.",
    "NextGate: CrmSprint10P8ControlledRuntimePilotEnablementDryRun."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot enablement plan verified."
exit 0
