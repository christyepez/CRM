$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p39-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-execution-approval-gate.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-entry-conditions-matrix.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-execution-scope-freeze.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-explicit-approval-decision-matrix.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-approval-record.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-security-approval-decision.md",
    "docs/architecture/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-architecture-approval-decision.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-devops-approval-checklist.md",
    "docs/testing/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-qa-uat-approval-checklist.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-monitoring-gate.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-abort-gate.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-rollback-gate.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-risk-register.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-entry-conditions.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-approval-drift-expiration-rules.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39-runbook.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-execution-approval-gate-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-execution-approval-gate.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-execution-approval-gate.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P39 file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p39Docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "P39ApprovalGateOnly: true",
    "P39EntryConditionsEvaluated: true",
    "ExecutionScopeFrozen: true",
    "ApprovalDecision: NoGo",
    "NonProductionExecutionDecision: NoGo",
    "TechnicalApprovalPassed: true",
    "HumanApprovalRequired: true",
    "HumanApprovalRecorded: false",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationExecutionApprovalExecuted: false",
    "NonProductionActivationReadinessApprovedForExecution: false",
    "NonProductionActivationFinalGoApproved: false",
    "NonProductionActivationFinalGoNoGoDecision: NoGo",
    "NonProductionActivationControlledExecutionPreparationValidated: true",
    "NonProductionActivationControlledExecutionExecuted: false",
    "NonProductionActivationExecuted: false",
    "DryRunActivationExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimeCouplingEnabled: false",
    "PortalRoutesActivated: false",
    "PortalNavigationActivated: false",
    "PortalServicesInCompose: false",
    "CommonDbRuntimeEnabled: false",
    "PortalDuplicationDetected: false",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "ProductionExecutionApproved: false",
    "SecurityApprovalPassed: true",
    "ArchitectureApprovalPassed: true",
    "DevOpsApprovalPassed: true",
    "QaUatApprovalPassed: true",
    "MonitoringGatePassed: true",
    "AbortGatePassed: true",
    "RollbackGatePassed: true",
    "ApprovalRecordPrepared: true",
    "ApprovalDriftRulesPrepared: true",
    "P40EntryConditionsPrepared: true",
    "SecretsPresent: false",
    "EnvRealFileCommitted: false",
    "PrivateUrlsPresent: false",
    "RealDataPresent: false"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P39 marker: $marker" } }
foreach ($bad in @(
    "ProductionActivationDecision: Go",
    "CrmProductionReady: true",
    "ProductionActivated: true",
    "ProductionExecutionApproved: true",
    "NonProductionActivationExecuted: true",
    "NonProductionActivationControlledExecutionExecuted: true",
    "DryRunActivationExecuted: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true"
)) { if ($p39Docs -like "*$bad*") { throw "Forbidden P39 execution/production marker detected: $bad" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) { if ($p39Docs -like "*$pattern*") { throw "Forbidden P39 content detected: $pattern" } }
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P39 explicit execution approval gate guardrails passed."
