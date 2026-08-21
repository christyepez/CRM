$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p36-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-decision.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-consolidated-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-decision-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-go-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-no-go-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-blockers.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-execution-plan-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-approval-gate-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-readiness-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-dry-run-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-security-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-architecture-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-devops-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-qa-uat-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-monitoring-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-rollback-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-portal-first-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-common-db-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-p37-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P36 file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p36Only = ($paths | Where-Object { $_ -ne "codex/TASKS.md" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P36ControlledRuntimePilotFirstSliceNonProductionActivationFinalGoNoGoGateExists: true",
    "CrmSprint10P35ExecutionPlanValidationReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "NonProductionActivationFinalGoNoGoGateOnly: true",
    "NonProductionActivationFinalGoNoGoGatePrepared: true",
    "NonProductionActivationFinalGoNoGoDecision: NoGo",
    "NonProductionActivationFinalGoApproved: false",
    "NonProductionActivationExecutionPlanValidated: true",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationFinalGoNoGoGateReadiness: FinalGoNoGoGatePreparedNoGoNow",
    "NextGate: CrmSprint10P37ControlledRuntimePilotFirstSliceNonProductionActivationControlledExecutionPreparation"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P36 marker: $marker" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) { if ($p36Only -like "*$pattern*") { throw "Forbidden P36 content detected: $pattern" } }
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P36 final GO/NO-GO gate guardrails passed."
