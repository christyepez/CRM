$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-10-p47s-provide-production-target-rollback-monitoring-evidence.md",
    "docs/roadmap/crm-sprint-10-p47s-production-evidence-matrix.md",
    "docs/operations/crm-sprint-10-p47s-production-target-evidence.md",
    "docs/roadmap/crm-sprint-10-p47s-production-target-manifest-hash-evidence.md",
    "docs/operations/crm-sprint-10-p47s-current-production-state-evidence.md",
    "docs/operations/crm-sprint-10-p47s-deployment-state-classification.md",
    "docs/operations/crm-sprint-10-p47s-rollback-evidence.md",
    "docs/roadmap/crm-sprint-10-p47s-rollback-baseline-hash-evidence.md",
    "docs/operations/crm-sprint-10-p47s-monitoring-evidence.md",
    "docs/testing/crm-sprint-10-p47s-architecture-tests-root-cause-analysis.md",
    "docs/testing/crm-sprint-10-p47s-architecture-tests-fix-validation-evidence.md",
    "docs/operations/crm-sprint-10-p47s-approval-drift-assessment.md",
    "docs/roadmap/crm-sprint-10-p48-entry-conditions-p47s.md",
    "docs/roadmap/crm-sprint-10-p47s-risk-register.md"
)

foreach ($path in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Missing required P47S file: $path" }
}

$task = Get-Content -Raw -LiteralPath "docs/roadmap/crm-sprint-10-p47s-provide-production-target-rollback-monitoring-evidence.md"
$requiredMarkers = @(
    "P47SProvideProductionTargetRollbackMonitoringEvidenceExists: true",
    "P47RPullRequest: #129",
    "P47RMergeCommit: 708d566f70f072d44011ed9f5d3c5aa1148dcc31",
    "P47SBaseMainCommit: 708d566f70f072d44011ed9f5d3c5aa1148dcc31",
    "P47RHistoricalDecision: NotReadyForNewHumanApproval",
    "HistoricalStatePreserved: true",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionActivated: false",
    "ProductionDataChangesExecuted: false",
    "P45RetryAuthorized: false",
    "ApprovalConsumed: false",
    "ExternalInputsResolved: 0",
    "ProductionTargetResolutionDecision: NotResolved",
    "ProductionTargetFrozen: false",
    "ProductionMonitoringTargetResolved: false",
    "ProductionMonitoringReadyForRetry: false",
    "CurrentProductionServicePresent: Unknown",
    "ProductionDeploymentState: Unknown",
    "RollbackBaselineIdentified: false",
    "RollbackReadyForRetry: false",
    "RollbackBaselineFrozen: false",
    "NewFinalApprovalPacketId: NotCreated",
    "FinalApprovalPacketFrozen: false",
    "CandidateImageIdentityMatched: true",
    "ArchitectureTestsStatus: Timeout",
    "ArchitectureTestsBlocking: true",
    "PreviousHumanApprovalReusable: false",
    "ExistingHumanApprovalStillValidForRetry: false",
    "NewHumanApprovalRequiredForRetry: true",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "CriticalProductionBlockers: 4",
    "P47SDecision: NotReadyForNewHumanApproval"
)

foreach ($marker in $requiredMarkers) {
    if (-not $task.Contains($marker)) { throw "Missing required P47S marker: $marker" }
}

$forbiddenMarkers = @(
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionActivated: true",
    "ProductionTrafficSwitched: true",
    "ProductionDataChangesExecuted: true",
    "P45RetryAuthorized: true",
    "ApprovalConsumed: true",
    "ProductionTargetResolutionDecision: Resolved",
    "ProductionTargetFrozen: true",
    "ProductionMonitoringTargetResolved: true",
    "ProductionMonitoringReadyForRetry: true",
    "RollbackReadyForRetry: true",
    "RollbackBaselineFrozen: true",
    "PreviousHumanApprovalReusable: true",
    "ExistingHumanApprovalStillValidForRetry: true",
    "FinalApprovalPacketFrozen: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true"
)

foreach ($marker in $forbiddenMarkers) {
    if ($task.Contains($marker)) { throw "Forbidden P47S marker found: $marker" }
}

Write-Host "PASS CRM P47S production evidence guardrails passed."

