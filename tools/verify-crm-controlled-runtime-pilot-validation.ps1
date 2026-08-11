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
    "docs/roadmap/crm-sprint-10-p6-controlled-runtime-pilot-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-validation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-validation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-validation-report.md",
    "docs/integration/crm-controlled-runtime-pilot-validation-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-feature-flag-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-disabled-client-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-health-smoke-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-preflight-validation.md",
    "docs/operations/crm-controlled-runtime-pilot-validation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-validation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-validation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-validation.ps1",
    "tools/crm-controlled-runtime-pilot-validate-all.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P6ControlledRuntimePilotValidationExists: true.",
    "CrmSprint10P5ScaffoldReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ControlledRuntimePilotValidationAttempted: true.",
    "ControlledRuntimePilotValidationReportPrepared: true.",
    "ControlledRuntimePilotEvidenceMatrixPrepared: true.",
    "ControlledRuntimePilotFeatureFlagValidationPrepared: true.",
    "ControlledRuntimePilotDisabledClientValidationPrepared: true.",
    "ControlledRuntimePilotHealthSmokeValidationPrepared: true.",
    "ControlledRuntimePilotPreflightValidationPrepared: true.",
    "ControlledRuntimePilotValidationRunbookPrepared: true.",
    "ControlledRuntimePilotValidationSecurityDecisionPrepared: true.",
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
    "ControlledRuntimePilotValidationReadiness: ValidatedDisabledOnly.",
    "NextGate: CrmSprint10P7ControlledRuntimePilotEnablementPlan."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot validation verified."
exit 0
