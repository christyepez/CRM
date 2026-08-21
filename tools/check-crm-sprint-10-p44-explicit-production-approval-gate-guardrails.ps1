$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p44-explicit-production-activation-approval-gate.md",
    "docs/roadmap/crm-sprint-10-p44-production-approval-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p44-production-target-freeze.md",
    "docs/roadmap/crm-sprint-10-p44-production-approval-scope.md",
    "docs/roadmap/crm-sprint-10-p44-production-approval-record.md",
    "docs/security/crm-sprint-10-p44-security-production-approval-decision.md",
    "docs/architecture/crm-sprint-10-p44-architecture-production-approval-decision.md",
    "docs/operations/crm-sprint-10-p44-devops-qa-monitoring-rollback-approval-decisions.md",
    "docs/roadmap/crm-sprint-10-p44-residual-risk-acceptance-matrix.md",
    "docs/roadmap/crm-sprint-10-p44-approval-drift-expiration-rules.md",
    "docs/roadmap/crm-sprint-10-p44-p45-entry-conditions.md",
    "tools/check-crm-sprint-10-p44-explicit-production-approval-gate-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44-explicit-production-approval-gate.ps1",
    "tools/crm-sprint-10-p44-explicit-production-approval-gate.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44 file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P44ExplicitProductionActivationApprovalGateExists: true",
    "P44ProductionApprovalGateOnly: true",
    "P43MergeCommit: 46415e26b6ce4877694be74898108fcbc87bf606",
    "P44BaseMainCommit: 46415e26b6ce4877694be74898108fcbc87bf606",
    "ProductionReadinessRemediationDecision: ReadyForApprovalGate",
    "TechnicalProductionApprovalPassed: true",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: false",
    "ProductionApprovalDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "ProductionExecutionAuthorized: false",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "ProductionApprovalDriftDetected: false",
    "CriticalProductionBlockers: 0",
    "HighBlockingRisks: 0",
    "ProductionScopeFrozen: true",
    "ProductionTargetFrozen: true",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "RuntimePortalCallsEnabled: false",
    "PortalRoutesActivated: false",
    "PortalNavigationActivated: false",
    "CommonDbRuntimeEnabled: false",
    "NextGate: HumanApprovalRequiredBeforeCrmSprint10P45ControlledProductionActivationExecution"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P44 marker: $marker" } }
foreach ($bad in @(
    "ProductionApprovalDecision: Go",
    "HumanProductionApprovalRecorded: true",
    "ProductionApprovalExecuted: true",
    "ProductionExecutionAuthorized: true",
    "ProductionActivated: true",
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionTrafficSwitched: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true",
    "CrmProductionReady: true"
)) { if ($docs -like "*$bad*") { throw "Forbidden P44 marker detected: $bad" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P44 content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P44 explicit production approval gate guardrails passed."
