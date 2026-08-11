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
    "docs/roadmap/crm-sprint-10-p5-controlled-runtime-pilot-scaffold.md",
    "docs/roadmap/crm-controlled-runtime-pilot-scaffold-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-scaffold-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-scaffold-overview.md",
    "docs/integration/crm-controlled-runtime-pilot-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-disabled-client-contract.md",
    "docs/integration/crm-controlled-runtime-pilot-health-smoke-contract.md",
    "docs/integration/crm-controlled-runtime-pilot-preflight-checklist.md",
    "docs/operations/crm-controlled-runtime-pilot-scaffold-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-scaffold-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-scaffold-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-scaffold.ps1",
    "tools/crm-controlled-runtime-pilot-preflight.ps1",
    "tools/crm-controlled-runtime-pilot-smoke.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P5ControlledRuntimePilotScaffoldExists: true.",
    "CrmSprint10P4RuntimeDesignReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ControlledRuntimePilotScaffoldAttempted: true.",
    "ControlledRuntimePilotScaffoldPrepared: true.",
    "ControlledRuntimePilotFeatureFlagsPrepared: true.",
    "ControlledRuntimePilotDisabledClientPrepared: true.",
    "ControlledRuntimePilotHealthSmokeContractPrepared: true.",
    "ControlledRuntimePilotPreflightPrepared: true.",
    "ControlledRuntimePilotRunbookPrepared: true.",
    "ControlledRuntimePilotSecurityDecisionPrepared: true.",
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
    "ControlledRuntimePilotScaffoldReadiness: ScaffoldPreparedDisabledOnly.",
    "NextGate: CrmSprint10P6ControlledRuntimePilotValidation."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot scaffold verified."
exit 0
