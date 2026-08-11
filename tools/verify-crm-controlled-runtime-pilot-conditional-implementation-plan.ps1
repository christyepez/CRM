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
    "docs/roadmap/crm-sprint-10-p11-controlled-runtime-pilot-conditional-enablement-implementation-plan.md",
    "docs/roadmap/crm-controlled-runtime-pilot-conditional-implementation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-conditional-implementation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-phases.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-wbs.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-pr-sequence.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-change-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-configuration-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-feature-flag-rollout.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-client-enablement.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-gateway-navigation.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-health-smoke-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-qa-uat.md",
    "docs/integration/crm-controlled-runtime-pilot-conditional-implementation-evidence-plan.md",
    "docs/operations/crm-controlled-runtime-pilot-conditional-implementation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-conditional-implementation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-conditional-implementation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-conditional-implementation-plan.ps1",
    "tools/crm-controlled-runtime-pilot-conditional-implementation-plan.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P11ControlledRuntimePilotConditionalImplementationPlanExists: true.",
    "CrmSprint10P10ConditionalDesignReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ConditionalImplementationPlanAttempted: true.",
    "ConditionalImplementationPlanPrepared: true.",
    "ConditionalImplementationPhasesPrepared: true.",
    "ConditionalImplementationWbsPrepared: true.",
    "ConditionalImplementationPrSequencePrepared: true.",
    "ConditionalImplementationChangeMatrixPrepared: true.",
    "ConditionalImplementationConfigurationPlanPrepared: true.",
    "ConditionalImplementationFeatureFlagRolloutPrepared: true.",
    "ConditionalImplementationClientEnablementPrepared: true.",
    "ConditionalImplementationGatewayNavigationPrepared: true.",
    "ConditionalImplementationHealthSmokeValidationPrepared: true.",
    "ConditionalImplementationRollbackPrepared: true.",
    "ConditionalImplementationQaUatPrepared: true.",
    "ConditionalImplementationEvidencePlanPrepared: true.",
    "ConditionalImplementationRunbookPrepared: true.",
    "ConditionalImplementationSecurityDecisionPrepared: true.",
    "ImplementationPlanOnly: true.",
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
    "ControlledRuntimePilotConditionalImplementationPlanReadiness: ImplementationPlanPreparedNoGo.",
    "NextGate: CrmSprint10P12ControlledRuntimePilotImplementationReadinessReview."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot conditional implementation plan verified."
exit 0
