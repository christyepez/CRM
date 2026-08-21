$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p30-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-report.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-output-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-post-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-no-external-call-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-no-portal-call-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-no-activation-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-feature-flags-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-compose-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-p31-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P30 file: $path" }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p30Only = ($paths | Where-Object { $_ -ne "codex/TASKS.md" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P30ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecutionExists: true",
    "CrmSprint10P29DryRunExecutionApprovalReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationDryRunControlledExecutionAttempted: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionReportPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionEvidenceMatrixPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionOutputValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionPostChecklistPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionNoExternalCallValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionNoPortalCallValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionNoActivationValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionFeatureFlagsValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionComposeValidationPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionRollbackPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionP31ConditionsPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionRunbookPrepared: true",
    "FirstSliceNonProductionActivationDryRunControlledExecutionSecurityDecisionPrepared: true",
    "NonProductionActivationDryRunControlledExecutionOnly: true",
    "DryRunControlledExecutionPrepared: true",
    "DryRunControlledExecutionExecuted: true",
    "DryRunExecuted: true",
    "DryRunExternalCallExecuted: false",
    "DryRunPortalCallExecuted: false",
    "DryRunActivationExecuted: false",
    "DryRunExecutionApprovalPrepared: true",
    "DryRunExecutionApprovalExecuted: false",
    "NonProductionActivationDryRunExecutionValidationOnly: true",
    "DryRunExecutionPlanValidated: true",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecutionReadiness: DryRunControlledExecutionCompletedLocalNoOpNoGoNow",
    "NextGate: CrmSprint10P31ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecutionValidation"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P30 marker: $marker" }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) {
    if ($p30Only -like "*$pattern*") { throw "Forbidden P30 content detected: $pattern" }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run controlled execution guardrails passed."
