$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p44a-human-production-approval-reapproval-gate.md",
    "docs/roadmap/crm-sprint-10-p44a-human-production-approval-record.md",
    "docs/roadmap/crm-sprint-10-p44a-production-approval-revalidation.md",
    "docs/roadmap/crm-sprint-10-p44a-production-drift-validation.md",
    "docs/roadmap/crm-sprint-10-p44a-production-target-revalidation.md",
    "docs/roadmap/crm-sprint-10-p44a-production-scope-revalidation.md",
    "docs/roadmap/crm-sprint-10-p44a-risk-acceptance-revalidation.md",
    "docs/roadmap/crm-sprint-10-p44a-approval-expiration-rules.md",
    "docs/roadmap/crm-sprint-10-p44a-p45-entry-conditions.md",
    "tools/check-crm-sprint-10-p44a-human-production-approval-reapproval-gate-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44a-human-production-approval-reapproval-gate.ps1",
    "tools/crm-sprint-10-p44a-human-production-approval-reapproval-gate.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44A file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P44AHumanProductionApprovalReApprovalGateExists: true",
    "P44AHumanProductionApprovalGateOnly: true",
    "P44HistoricalStatePreserved: true",
    "P44HistoricalApprovalDecision: NoGo",
    "P44MergeCommit: 3e905c1e586f0954f56f3bf2dd7aa4f2c01d029a",
    "P44ABaseMainCommit: 3e905c1e586f0954f56f3bf2dd7aa4f2c01d029a",
    "TechnicalProductionApprovalPassed: true",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: false",
    "HumanProductionApprovalDecision: NotRecorded",
    "ProductionApprovalDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "ProductionExecutionAuthorized: false",
    "ProductionApprovalDriftDetected: false",
    "CriticalProductionBlockers: 0",
    "HighBlockingRisks: 0",
    "ProductionScopeFrozen: true",
    "ProductionTargetFrozen: true",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "ApprovedProductionExternalDependencies: none",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "NonProductionRuntimeStable: false",
    "NextGate: HumanApprovalAndNonProductionRuntimeStabilityRequiredBeforeCrmSprint10P45ControlledProductionActivationExecution"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P44A marker: $marker" } }
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
)) { if ($docs -like "*$bad*") { throw "Forbidden P44A marker detected: $bad" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P44A content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P44A human production approval re-approval gate guardrails passed."
