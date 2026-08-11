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
    "docs/roadmap/crm-sprint-10-p4-controlled-runtime-integration-design.md",
    "docs/roadmap/crm-controlled-runtime-integration-design-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-integration-design-risk-register.md",
    "docs/integration/crm-controlled-runtime-topology.md",
    "docs/integration/crm-controlled-runtime-activation-sequence.md",
    "docs/integration/crm-controlled-runtime-rollback-design.md",
    "docs/integration/crm-controlled-runtime-preflight-validations.md",
    "docs/integration/crm-controlled-runtime-health-smoke-design.md",
    "docs/integration/crm-controlled-runtime-observability-design.md",
    "docs/integration/crm-controlled-runtime-gateway-navigation-boundary.md",
    "docs/integration/crm-controlled-runtime-auth-claims-boundary.md",
    "docs/integration/crm-controlled-runtime-common-db-boundary.md",
    "docs/integration/crm-controlled-runtime-crosscutting-boundary.md",
    "docs/security/crm-controlled-runtime-integration-security-decision.md",
    "docs/operations/crm-controlled-runtime-integration-design-runbook.md",
    "tools/check-crm-controlled-runtime-integration-design-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-integration-design.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P4ControlledRuntimeIntegrationDesignExists: true.",
    "CrmSprint10P3PortalConsumerAlignmentReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ControlledRuntimeIntegrationDesignAttempted: true.",
    "ControlledRuntimeTopologyPrepared: true.",
    "ControlledRuntimeActivationSequencePrepared: true.",
    "ControlledRuntimeRollbackDesignPrepared: true.",
    "ControlledRuntimePreflightValidationsPrepared: true.",
    "ControlledRuntimeHealthSmokeDesignPrepared: true.",
    "ControlledRuntimeObservabilityDesignPrepared: true.",
    "GatewayNavigationBoundaryPrepared: true.",
    "AuthClaimsPermissionsBoundaryPrepared: true.",
    "CommonDbBoundaryPrepared: true.",
    "CrosscuttingBoundaryPrepared: true.",
    "RuntimePortalCouplingEnabled: false.",
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
    "ControlledRuntimeIntegrationDesignReadiness: DesignedContractOnly.",
    "NextGate: CrmSprint10P5ControlledRuntimePilotScaffold."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime integration design verified."
exit 0
