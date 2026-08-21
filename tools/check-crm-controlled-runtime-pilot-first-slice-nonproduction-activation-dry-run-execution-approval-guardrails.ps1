$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p29-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-final-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-raci.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-security-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-devops-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-qa-uat-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-monitoring-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-rollback-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-p30-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P29 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p29Only = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P29ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionApprovalExists: true",
    "CrmSprint10P28DryRunExecutionValidationReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalAttempted: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalMatrixPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalFinalCriteriaPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalRaciPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalEvidencePrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalSecurityChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalDevOpsChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalQaUatChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalMonitoringChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalRollbackChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalP30ConditionsPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalRunbookPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionApprovalSecurityDecisionPrepared: true",
    "NonProductionActivationDryRunExecutionApprovalGateOnly: true",
    "DryRunExecutionApprovalPrepared: true",
    "DryRunExecutionApprovalExecuted: false",
    "NonProductionActivationDryRunExecutionValidationOnly: true",
    "DryRunExecutionPlanValidated: true",
    "DryRunExecuted: false",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionApprovalReadiness: DryRunExecutionApprovalPreparedNoGoNow",
    "NextGate: CrmSprint10P30ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecution"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P29 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) {
    if ($p29Only -like "*$pattern*") {
        throw "Forbidden P29 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution approval guardrails passed."
