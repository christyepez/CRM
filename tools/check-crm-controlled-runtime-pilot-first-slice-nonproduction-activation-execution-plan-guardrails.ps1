$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p34-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-operational-sequence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-command-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-request-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-pre-checks.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-execution-steps.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-post-checks.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-security-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-architecture-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-devops-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-qa-uat-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-monitoring-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-rollback-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-communications.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-observability.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-portal-first-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-common-db-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-p35-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P34 file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p34Only = ($paths | Where-Object { $_ -ne "codex/TASKS.md" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P34ControlledRuntimePilotFirstSliceNonProductionActivationExecutionPlanExists: true",
    "CrmSprint10P33ExecutionApprovalGateReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "NonProductionActivationExecutionPlanOnly: true",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationExecutionPlanReadiness: ExecutionPlanPreparedNoGoNow",
    "NextGate: CrmSprint10P35ControlledRuntimePilotFirstSliceNonProductionActivationExecutionPlanValidation"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P34 marker: $marker" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) { if ($p34Only -like "*$pattern*") { throw "Forbidden P34 content detected: $pattern" } }
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P34 execution plan guardrails passed."
