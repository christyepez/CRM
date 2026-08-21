$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p27-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-pre-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-execution-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-post-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-command-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-foundation-status-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-dry-run-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-observability-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-p28-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P27 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p27Only = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P27ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionPlanExists: true",
    "CrmSprint10P26ExplicitApprovalReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationDryRunExecutionPlanAttempted: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanPreChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanExecutionChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanPostChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanEvidenceMatrixPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanCommandMatrixPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanFoundationStatusValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanDryRunValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanObservabilityValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanRollbackPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanP28ConditionsPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanRunbookPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionPlanSecurityDecisionPrepared: true",
    "NonProductionActivationDryRunExecutionPlanOnly: true",
    "DryRunExecutionPlanPrepared: true",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionPlanReadiness: DryRunExecutionPlanPreparedNoGoNow",
    "NextGate: CrmSprint10P28ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionValidation"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P27 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) {
    if ($p27Only -like "*$pattern*") {
        throw "Forbidden P27 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution plan guardrails passed."
