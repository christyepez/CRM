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
    "docs/roadmap/crm-sprint-10-p9-controlled-runtime-pilot-enablement-approval-gate.md",
    "docs/roadmap/crm-controlled-runtime-pilot-enablement-approval-gate-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-enablement-approval-gate-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-gate.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-gate-evidence-summary.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-gate-approvers.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-gate-decision-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-gate-compliance-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-gate-blockers.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-gate-raci.md",
    "docs/integration/crm-controlled-runtime-pilot-enablement-approval-gate-communication-plan.md",
    "docs/operations/crm-controlled-runtime-pilot-enablement-approval-gate-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-enablement-approval-gate-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-approval-gate-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-approval-gate.ps1",
    "tools/crm-controlled-runtime-pilot-approval-gate.ps1"
)

foreach ($file in $requiredFiles) { RequirePath $file }

$text = ""
foreach ($file in $requiredFiles) {
    if (Test-Path $file) { $text += "`n" + (Get-Content -Raw $file) }
}
if (Test-Path "codex/TASKS.md") { $text += "`n" + (Get-Content -Raw "codex/TASKS.md") }

foreach ($marker in @(
    "CrmSprint10P9ControlledRuntimePilotEnablementApprovalGateExists: true.",
    "CrmSprint10P8DryRunReviewed: true.",
    "PortalSprint21ContractAlignmentReviewed: true.",
    "ProductizationStatus: PreparationOnly.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false.",
    "ControlledRuntimePilotApprovalGateAttempted: true.",
    "ControlledRuntimePilotApprovalGatePrepared: true.",
    "ControlledRuntimePilotApprovalGateEvidenceSummaryPrepared: true.",
    "ControlledRuntimePilotApprovalGateApproversPrepared: true.",
    "ControlledRuntimePilotApprovalGateDecisionCriteriaPrepared: true.",
    "ControlledRuntimePilotApprovalGateComplianceChecklistPrepared: true.",
    "ControlledRuntimePilotApprovalGateBlockersPrepared: true.",
    "ControlledRuntimePilotApprovalGateRaciPrepared: true.",
    "ControlledRuntimePilotApprovalGateCommunicationPlanPrepared: true.",
    "ControlledRuntimePilotApprovalGateRunbookPrepared: true.",
    "ControlledRuntimePilotApprovalGateSecurityDecisionPrepared: true.",
    "ApprovalGateOnly: true.",
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
    "ControlledRuntimePilotApprovalGateReadiness: ApprovalGatePreparedNoGo.",
    "NextGate: CrmSprint10P10ControlledRuntimePilotConditionalEnablementDesign."
)) {
    RequireMarker $text $marker
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM controlled runtime pilot approval gate verified."
exit 0
