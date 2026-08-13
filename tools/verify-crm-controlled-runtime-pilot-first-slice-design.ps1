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
    "docs/roadmap/crm-sprint-10-p13-controlled-runtime-pilot-first-implementation-slice-design.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-design.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-objective.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-scope.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-file-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-disabled-client.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-health-smoke.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-test-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-acceptance-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-security-checklist.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-design.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-design.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P13ControlledRuntimePilotFirstImplementationSliceDesignExists: true.",
    "CrmSprint10P12ReadinessReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "FirstImplementationSliceDesignAttempted: true.",
    "FirstImplementationSliceDesignPrepared: true.",
    "FirstSliceObjectivePrepared: true.",
    "FirstSliceScopePrepared: true.",
    "FirstSliceFileBoundariesPrepared: true.",
    "FirstSliceFeatureFlagsPrepared: true.",
    "FirstSliceSafeConfigurationPrepared: true.",
    "FirstSliceDisabledClientPrepared: true.",
    "FirstSliceHealthSmokePrepared: true.",
    "FirstSliceTestPlanPrepared: true.",
    "FirstSliceRollbackPrepared: true.",
    "FirstSliceAcceptanceCriteriaPrepared: true.",
    "FirstSliceSecurityChecklistPrepared: true.",
    "FirstSliceRunbookPrepared: true.",
    "FirstSliceSecurityDecisionPrepared: true.",
    "FirstImplementationSliceDesignOnly: true.",
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
    "ControlledRuntimePilotFirstImplementationSliceDesignReadiness: FirstSliceDesignPreparedNoGo.",
    "NextGate: CrmSprint10P14ControlledRuntimePilotFirstImplementationSliceScaffold."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot first slice design verified."
exit 0
