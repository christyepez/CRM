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
    "docs/roadmap/crm-sprint-10-p3-portal-consumer-contract-alignment.md",
    "docs/roadmap/crm-portal-consumer-contract-alignment-go-no-go.md",
    "docs/roadmap/crm-portal-consumer-contract-alignment-risk-register.md",
    "docs/integration/crm-portal-consumer-contract-matrix.md",
    "docs/integration/crm-portal-consumer-compliance-checklist.md",
    "docs/integration/crm-portal-consumer-navigation-contract.md",
    "docs/integration/crm-portal-consumer-claims-permissions-contract.md",
    "docs/integration/crm-portal-consumer-audit-contract.md",
    "docs/integration/crm-portal-consumer-configuration-contract.md",
    "docs/integration/crm-portal-consumer-notification-contract.md",
    "docs/integration/crm-portal-consumer-health-observability-contract.md",
    "docs/integration/crm-portal-consumer-known-gaps.md",
    "docs/security/crm-portal-consumer-contract-alignment-security-decision.md",
    "docs/operations/crm-portal-consumer-contract-alignment-runbook.md",
    "tools/check-crm-portal-consumer-contract-alignment-guardrails.ps1",
    "tools/verify-crm-portal-consumer-contract-alignment.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P3PortalConsumerContractAlignmentExists: true.",
    "CrmSprint10P2CommonDbReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "PortalConsumerContractAlignmentAttempted: true.",
    "CrmPortalConsumerContractMatrixPrepared: true.",
    "CrmPortalConsumerComplianceChecklistPrepared: true.",
    "CrmPortalNavigationContractPrepared: true.",
    "CrmPortalClaimsPermissionsContractPrepared: true.",
    "CrmPortalAuditContractPrepared: true.",
    "CrmPortalConfigurationContractPrepared: true.",
    "CrmPortalNotificationContractPrepared: true.",
    "CrmPortalHealthObservabilityContractPrepared: true.",
    "CrmPortalKnownGapsPrepared: true.",
    "PortalRuntimeCouplingEnabled: false.",
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
    "PortalConsumerContractAlignmentReadiness: AlignedContractOnly.",
    "NextGate: CrmSprint10P4ControlledRuntimeIntegrationDesign."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM Portal consumer contract alignment verified."
exit 0
