$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p43-production-readiness-remediation-and-explicit-production-activation-gate-preparation.md",
    "docs/roadmap/crm-sprint-10-p43-production-readiness-remediation-matrix.md",
    "docs/operations/crm-sprint-10-p43-devops-production-readiness-remediation.md",
    "docs/operations/crm-sprint-10-p43-production-deployment-runbook.md",
    "docs/testing/crm-sprint-10-p43-production-test-matrix.md",
    "docs/testing/crm-sprint-10-p43-performance-evidence-and-thresholds.md",
    "docs/operations/crm-sprint-10-p43-observability-remediation-alert-catalog.md",
    "docs/operations/crm-sprint-10-p43-operations-support-readiness.md",
    "docs/operations/crm-sprint-10-p43-backup-recovery-readiness.md",
    "docs/security/crm-sprint-10-p43-security-production-readiness-remediation.md",
    "docs/architecture/crm-sprint-10-p43-architecture-production-readiness-remediation.md",
    "docs/integration/crm-sprint-10-p43-portal-common-db-production-readiness.md",
    "docs/operations/crm-sprint-10-p43-environment-promotion-matrix.md",
    "docs/operations/crm-sprint-10-p43-production-configuration-manifest.md",
    "docs/roadmap/crm-sprint-10-p43-proposed-production-execution-scope.md",
    "docs/roadmap/crm-sprint-10-p43-production-target-freeze-model.md",
    "docs/operations/crm-sprint-10-p43-production-rollback-readiness.md",
    "docs/operations/crm-sprint-10-p43-production-abort-criteria.md",
    "docs/roadmap/crm-sprint-10-p43-p44-approval-record-and-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p43-residual-risk-register.md",
    "tools/check-crm-sprint-10-p43-production-readiness-remediation-guardrails.ps1",
    "tools/verify-crm-sprint-10-p43-production-readiness-remediation.ps1",
    "tools/crm-sprint-10-p43-production-readiness-remediation.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P43 file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P43ProductionReadinessRemediationAndExplicitProductionActivationGatePreparationExists: true",
    "P43ProductionReadinessRemediationOnly: true",
    "P42MergeCommit: c24a58c05943119cf17a386ca391e5825b39eaa2",
    "P43BaseMainCommit: c24a58c05943119cf17a386ca391e5825b39eaa2",
    "NonProductionPilotClosureDecision: ClosedSuccessfully",
    "ProductionReadinessAssessment: ReadyWithConditions",
    "ProductionReadinessRemediationDecision: ReadyForApprovalGate",
    "ConditionsTotal: 17",
    "ConditionsRemediated: 17",
    "ConditionsPartial: 0",
    "ConditionsOpen: 0",
    "CriticalProductionBlockers: 0",
    "HighBlockingRisks: 0",
    "SecurityReadyForApproval: true",
    "ArchitectureReadyForApproval: true",
    "DevOpsReadyForApproval: true",
    "QAReadyForApproval: true",
    "ObservabilityReadyForApproval: true",
    "OperationsReadyForApproval: true",
    "RollbackReadyForApproval: true",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: false",
    "ProductionScopeFrozen: true",
    "ProductionTargetPreparedForFreeze: true",
    "ProductionActivationDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "ProductionExecutionAuthorized: false",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimeCouplingEnabled: false",
    "PortalRoutesActivated: false",
    "PortalNavigationActivated: false",
    "PortalServicesInCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ProductionMonitoringReadyForApproval: true",
    "NextGate: CrmSprint10P44ExplicitProductionActivationApprovalGate"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P43 marker: $marker" } }
foreach ($bad in @(
    "ProductionActivationDecision: Go",
    "ProductionApprovalExecuted: true",
    "ProductionExecutionAuthorized: true",
    "ProductionActivated: true",
    "CrmProductionReady: true",
    "RuntimePortalCallsEnabled: true",
    "RuntimeCouplingEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true",
    "CommonDbRuntimeEnabled: true"
)) { if ($docs -like "*$bad*") { throw "Forbidden P43 marker detected: $bad" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P43 content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P43 production readiness remediation guardrails passed."
