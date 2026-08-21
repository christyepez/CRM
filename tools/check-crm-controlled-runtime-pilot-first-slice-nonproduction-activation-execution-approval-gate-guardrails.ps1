$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p33-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-approval-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-raci.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-entry-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-exit-criteria.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-security-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-architecture-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-devops-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-qa-uat-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-monitoring-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-rollback-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-portal-first-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-common-db-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-p34-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P33 file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p33Only = ($paths | Where-Object { $_ -ne "codex/TASKS.md" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P33ControlledRuntimePilotFirstSliceNonProductionActivationExecutionApprovalGateExists: true",
    "CrmSprint10P32ReadinessReviewReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "NonProductionActivationExecutionApprovalGateOnly: true",
    "NonProductionActivationExecutionApprovalPrepared: true",
    "NonProductionActivationExecutionApprovalExecuted: false",
    "NonProductionActivationReadinessApprovedForExecution: false",
    "DryRunControlledExecutionValidated: true",
    "DryRunExecuted: true",
    "DryRunExternalCallExecuted: false",
    "DryRunPortalCallExecuted: false",
    "DryRunActivationExecuted: false",
    "DryRunExecutionApprovalExecuted: false",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationControlledImplementationExecuted: false",
    "ConditionalGoFutureExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationExecutionApprovalGateReadiness: ExecutionApprovalGatePreparedNoGoNow",
    "NextGate: CrmSprint10P34ControlledRuntimePilotFirstSliceNonProductionActivationExecutionPlan"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P33 marker: $marker" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) { if ($p33Only -like "*$pattern*") { throw "Forbidden P33 content detected: $pattern" } }
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P33 execution approval gate guardrails passed."
