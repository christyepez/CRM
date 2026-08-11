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
    "docs/roadmap/crm-sprint-10-p10-controlled-runtime-pilot-conditional-enablement-design.md",
    "docs/roadmap/crm-controlled-runtime-pilot-conditional-enablement-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-conditional-enablement-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-design.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-disabled-client-design.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-gateway-routes-design.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-navigation-design.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-health-smoke-design.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-preflight-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-rollback-design.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-enablement-blockers.md",
    "docs/operations/crm-controlled-runtime-pilot-conditional-enablement-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-conditional-enablement-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-conditional-enablement-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-conditional-enablement-design.ps1",
    "tools/crm-controlled-runtime-pilot-conditional-enablement-design.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P10ControlledRuntimePilotConditionalEnablementDesignExists: true.",
    "CrmSprint10P9ApprovalGateReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ControlledRuntimePilotConditionalEnablementDesignAttempted: true.",
    "ControlledRuntimePilotConditionalEnablementDesignPrepared: true.",
    "ConditionalEnablementFeatureFlagsPrepared: true.",
    "ConditionalEnablementSafeConfigurationPrepared: true.",
    "ConditionalEnablementDisabledClientDesignPrepared: true.",
    "ConditionalEnablementGatewayRoutesDesignPrepared: true.",
    "ConditionalEnablementNavigationDesignPrepared: true.",
    "ConditionalEnablementHealthSmokeDesignPrepared: true.",
    "ConditionalEnablementPreflightPlanPrepared: true.",
    "ConditionalEnablementRollbackDesignPrepared: true.",
    "ConditionalEnablementEvidenceMatrixPrepared: true.",
    "ConditionalEnablementBlockersPrepared: true.",
    "ConditionalEnablementRunbookPrepared: true.",
    "ConditionalEnablementSecurityDecisionPrepared: true.",
    "ConditionalFutureGoDefined: true.",
    "ConditionalFutureGoExecuted: false.",
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
    "ControlledRuntimePilotConditionalEnablementDesignReadiness: ConditionalDesignPreparedNoGo.",
    "NextGate: CrmSprint10P11ControlledRuntimePilotConditionalEnablementImplementationPlan."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot conditional enablement design verified."
exit 0
