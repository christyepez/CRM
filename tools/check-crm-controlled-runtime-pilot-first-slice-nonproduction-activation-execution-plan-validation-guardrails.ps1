$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p35-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-report.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-operational-sequence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-command-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-request-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-pre-checks.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-execution-steps.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-post-checks.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-security-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-architecture-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-devops-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-qa-uat-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-monitoring-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-rollback-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-communications.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-observability.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-portal-first-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-common-db-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-p36-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P35 file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p35Only = ($paths | Where-Object { $_ -ne "codex/TASKS.md" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P35ControlledRuntimePilotFirstSliceNonProductionActivationExecutionPlanValidationExists: true",
    "CrmSprint10P34ExecutionPlanReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "NonProductionActivationExecutionPlanValidationOnly: true",
    "NonProductionActivationExecutionPlanValidated: true",
    "NonProductionActivationExecutionPlanPrepared: true",
    "NonProductionActivationExecutionPlanExecuted: false",
    "NonProductionActivationExecutionApprovalExecuted: false",
    "NonProductionActivationReadinessApprovedForExecution: false",
    "DryRunControlledExecutionValidated: true",
    "DryRunExecuted: true",
    "DryRunExternalCallExecuted: false",
    "DryRunPortalCallExecuted: false",
    "DryRunActivationExecuted: false",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationControlledImplementationExecuted: false",
    "ConditionalGoFutureExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "PortalAuthDuplicated: false",
    "PortalMenuDuplicated: false",
    "PortalPermissionsDuplicated: false",
    "PortalAuditDuplicated: false",
    "PortalNotificationDuplicated: false",
    "PortalConfigurationDuplicated: false",
    "SecretsPresent: false",
    "EnvRealFileCommitted: false",
    "PrivateUrlsPresent: false",
    "RealDataPresent: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationExecutionPlanValidationReadiness: ExecutionPlanValidatedNoGoNow",
    "NextGate: CrmSprint10P36ControlledRuntimePilotFirstSliceNonProductionActivationFinalGoNoGoGate"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P35 marker: $marker" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) { if ($p35Only -like "*$pattern*") { throw "Forbidden P35 content detected: $pattern" } }
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P35 execution plan validation guardrails passed."
