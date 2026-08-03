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
    "docs/roadmap/crm-sprint-10-p2-common-db-controlled-activation-plan.md",
    "docs/roadmap/crm-common-db-controlled-activation-go-no-go.md",
    "docs/roadmap/crm-common-db-controlled-activation-risk-register.md",
    "docs/database/crm-common-db-controlled-activation-strategy.md",
    "docs/database/crm-common-db-boundary-with-portal.md",
    "docs/database/crm-common-db-logical-model.md",
    "docs/database/crm-common-db-prerequisites-checklist.md",
    "docs/database/crm-common-db-rollback-plan.md",
    "docs/security/crm-common-db-controlled-activation-security-decision.md",
    "docs/integration/crm-to-portal-sprint21-contract-reference.md",
    "docs/operations/crm-common-db-controlled-activation-runbook.md",
    "tools/check-crm-common-db-controlled-activation-guardrails.ps1",
    "tools/verify-crm-common-db-controlled-activation-plan.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P2CommonDbControlledActivationPlanExists: true.",
    "CrmBaseFrozenReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "CommonDbControlledActivationPlanAttempted: true.",
    "CommonDbStrategyPrepared: true.",
    "CommonDbBoundaryWithPortalPrepared: true.",
    "CommonDbLogicalModelPrepared: true.",
    "CommonDbPrerequisitesChecklistPrepared: true.",
    "CommonDbRollbackPlanPrepared: true.",
    "CommonDbSecurityDecisionPrepared: true.",
    "PortalSprint21ContractReferencePrepared: true.",
    "CommonDbRuntimeEnabled: false.",
    "RealCommonDbConnectionConfigured: false.",
    "RealConnectionStringsPresent: false.",
    "SharedPortalTablesAccessEnabled: false.",
    "CrossDomainMigrationsPresent: false.",
    "PortalDatabaseDirectAccessEnabled: false.",
    "PortalAuthDuplicated: false.",
    "PortalMenuDuplicated: false.",
    "PortalPermissionsDuplicated: false.",
    "PortalAuditDuplicated: false.",
    "PortalNotificationDuplicated: false.",
    "PortalConfigurationDuplicated: false.",
    "PortalRuntimeCouplingEnabled: false.",
    "ProductivePortalNavigationEnabled: false.",
    "SsoOidcProductionConfigured: false.",
    "RealSecretProviderConfigured: false.",
    "RealNotificationProviderConfigured: false.",
    "RealObservabilityProviderConfigured: false.",
    "BrowserTokenStorageDetected: false.",
    "SecretsPresent: false.",
    "EnvRealFileCommitted: false.",
    "PrivateUrlsPresent: false.",
    "RealDataPresent: false.",
    "CommonDbControlledActivationReadiness: PlanPreparedContractOnly.",
    "NextGate: CrmSprint10P3PortalConsumerContractAlignment."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM Common DB controlled activation plan verified."
exit 0
