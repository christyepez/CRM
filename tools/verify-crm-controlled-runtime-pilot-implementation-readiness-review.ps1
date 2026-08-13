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
    "docs/roadmap/crm-sprint-10-p12-controlled-runtime-pilot-implementation-readiness-review.md",
    "docs/roadmap/crm-controlled-runtime-pilot-implementation-readiness-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-implementation-readiness-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-review.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-evidence-summary.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-gaps.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-entry-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-blockers.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-residual-risks.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-decision-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-approval-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-verification-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-implementation-readiness-pr-separation.md",
    "docs/operations/crm-controlled-runtime-pilot-implementation-readiness-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-implementation-readiness-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-implementation-readiness-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-implementation-readiness-review.ps1",
    "tools/crm-controlled-runtime-pilot-implementation-readiness-review.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P12ControlledRuntimePilotImplementationReadinessReviewExists: true.",
    "CrmSprint10P11ImplementationPlanReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ImplementationReadinessReviewAttempted: true.",
    "ImplementationReadinessReviewPrepared: true.",
    "ImplementationReadinessEvidenceSummaryPrepared: true.",
    "ImplementationReadinessChecklistPrepared: true.",
    "ImplementationReadinessGapsPrepared: true.",
    "ImplementationReadinessEntryCriteriaPrepared: true.",
    "ImplementationReadinessBlockersPrepared: true.",
    "ImplementationReadinessResidualRisksPrepared: true.",
    "ImplementationReadinessDecisionMatrixPrepared: true.",
    "ImplementationReadinessApprovalPlanPrepared: true.",
    "ImplementationReadinessVerificationPlanPrepared: true.",
    "ImplementationReadinessPrSeparationPrepared: true.",
    "ImplementationReadinessRunbookPrepared: true.",
    "ImplementationReadinessSecurityDecisionPrepared: true.",
    "ReadinessReviewOnly: true.",
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
    "ControlledRuntimePilotImplementationReadinessReviewReadiness: ReadinessReviewPreparedNoGo.",
    "NextGate: CrmSprint10P13ControlledRuntimePilotFirstImplementationSliceDesign."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot implementation readiness review verified."
exit 0
