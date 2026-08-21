$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p28-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-report.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-pre-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-execution-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-post-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-command-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-foundation-status.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-dry-run-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-observability.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-security-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-p29-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P28 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p28Only = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P28ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionValidationExists: true",
    "CrmSprint10P27DryRunExecutionPlanReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationDryRunExecutionValidationAttempted: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationReportPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationEvidenceMatrixPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationPreChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationExecutionChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationPostChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationCommandMatrixPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationFoundationStatusPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationDryRunPlanPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationObservabilityPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationRollbackPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationSecurityChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationP29ConditionsPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationRunbookPrepared: true",
    "FirstSliceNonProductionActivationDryRunExecutionValidationSecurityDecisionPrepared: true",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionValidationReadiness: DryRunExecutionValidationPreparedNoGoNow",
    "NextGate: CrmSprint10P29ControlledRuntimePilotFirstSliceNonProductionActivationDryRunExecutionApproval"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P28 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) {
    if ($p28Only -like "*$pattern*") {
        throw "Forbidden P28 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution validation guardrails passed."
