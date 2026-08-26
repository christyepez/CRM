$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-10-p47t-supply-real-production-target-evidence-and-stabilize-architecturedependencytests.md",
    "docs/testing/crm-sprint-10-p47t-architecturedependencytests-root-cause.md",
    "docs/testing/crm-sprint-10-p47t-architecturedependencytests-test-only-remediation.md",
    "docs/testing/crm-sprint-10-p47t-architecturetests-stability-evidence.md",
    "docs/roadmap/crm-sprint-10-p47t-production-evidence-matrix.md",
    "docs/operations/crm-sprint-10-p47t-production-target-evidence.md",
    "docs/operations/crm-sprint-10-p47t-rollback-baseline-evidence.md",
    "docs/operations/crm-sprint-10-p47t-monitoring-target-evidence.md",
    "docs/operations/crm-sprint-10-p47t-approval-drift-assessment.md",
    "docs/roadmap/crm-sprint-10-p48-entry-conditions-p47t.md",
    "docs/roadmap/crm-sprint-10-p47t-risk-register.md"
)

foreach ($path in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Missing required P47T file: $path" }
}

$task = Get-Content -Raw -LiteralPath "docs/roadmap/crm-sprint-10-p47t-supply-real-production-target-evidence-and-stabilize-architecturedependencytests.md"
$requiredMarkers = @(
    "P47TSupplyRealProductionTargetEvidenceAndStabilizeArchitectureDependencyTestsExists: true",
    "P47SPullRequest: #130",
    "P47SMergeCommit: b12221a3f77ea04134bb672d60e0f617f4d9fbf1",
    "P47TBaseMainCommit: b12221a3f77ea04134bb672d60e0f617f4d9fbf1",
    "HistoricalStatePreserved: true",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionActivated: false",
    "ApprovalConsumed: false",
    "ExternalInputsResolved: 0",
    "ProductionTargetResolutionDecision: NotResolved",
    "ProductionTargetFrozen: false",
    "ProductionMonitoringTargetResolved: false",
    "ProductionMonitoringReadyForRetry: false",
    "RollbackBaselineIdentified: false",
    "RollbackReadyForRetry: false",
    "ArchitectureTestsStatus: Passed",
    "ArchitectureTestsFixApplied: true",
    "ArchitectureTestsRuntimeBehaviorChanged: false",
    "ArchitectureTestsBlocking: false",
    "NewFinalApprovalPacketId: NotCreated",
    "FinalApprovalPacketFrozen: false",
    "CandidateImageIdentityMatched: true",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "PreviousHumanApprovalReusable: false",
    "ExistingHumanApprovalStillValidForRetry: false",
    "NewHumanApprovalRequiredForRetry: true",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "CriticalProductionBlockers: 3",
    "P47TDecision: NotReadyForNewHumanApproval"
)

foreach ($marker in $requiredMarkers) {
    if (-not $task.Contains($marker)) { throw "Missing required P47T marker: $marker" }
}

$forbiddenMarkers = @(
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionActivated: true",
    "ProductionTrafficSwitched: true",
    "ProductionDataChangesExecuted: true",
    "ApprovalConsumed: true",
    "ProductionTargetResolutionDecision: Resolved",
    "ProductionTargetFrozen: true",
    "ProductionMonitoringTargetResolved: true",
    "ProductionMonitoringReadyForRetry: true",
    "RollbackReadyForRetry: true",
    "FinalApprovalPacketFrozen: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true"
)

foreach ($marker in $forbiddenMarkers) {
    if ($task.Contains($marker)) { throw "Forbidden P47T marker found: $marker" }
}

Write-Host "PASS CRM P47T guardrails passed."

