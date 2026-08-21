$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p26-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-evidence-summary.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-raci.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-security-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-architecture-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-devops-rollback-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-qa-uat-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-monitoring-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-p27-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P26 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p26Only = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P26ControlledRuntimePilotFirstSliceNonProductionActivationExplicitApprovalExists: true",
    "CrmSprint10P25ControlledImplementationValidationReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationExplicitApprovalAttempted: true",
    "FirstSliceNonProductionActivationExplicitApprovalPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalMatrixPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalCriteriaPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalEvidenceSummaryPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalRaciPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalSecurityChecklistPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalArchitectureChecklistPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalDevOpsRollbackChecklistPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalQaUatChecklistPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalMonitoringChecklistPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalP27ConditionsPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalRunbookPrepared: true",
    "FirstSliceNonProductionActivationExplicitApprovalSecurityDecisionPrepared: true",
    "NonProductionActivationExplicitApprovalGateOnly: true",
    "ExplicitApprovalPrepared: true",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationControlledImplementationValidatedDisabledOnly: true",
    "NonProductionActivationControlledImplementationExecuted: false",
    "ConditionalGoFutureDefined: true",
    "ConditionalGoFutureExecuted: false",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoDefined: true",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "RealPortalPrivateUrlsPresent: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "RealCommonDbConnectionConfigured: false",
    "SharedPortalTablesAccessEnabled: false",
    "CrossDomainMigrationsPresent: false",
    "PortalDatabaseDirectAccessEnabled: false",
    "PortalAuthDuplicated: false",
    "PortalMenuDuplicated: false",
    "PortalPermissionsDuplicated: false",
    "PortalAuditDuplicated: false",
    "PortalNotificationDuplicated: false",
    "PortalConfigurationDuplicated: false",
    "SsoOidcProductionConfigured: false",
    "RealSecretProviderConfigured: false",
    "RealNotificationProviderConfigured: false",
    "RealObservabilityProviderConfigured: false",
    "BrowserTokenStorageDetected: false",
    "SecretsPresent: false",
    "EnvRealFileCommitted: false",
    "PrivateUrlsPresent: false",
    "RealDataPresent: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationExplicitApprovalReadiness: ExplicitApprovalPreparedNoGoNow",
    "NextGate: CrmSprint10P27ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionPlan"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P26 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) {
    if ($p26Only -like "*$pattern*") {
        throw "Forbidden P26 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation explicit approval guardrails passed."
