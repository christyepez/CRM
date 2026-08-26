$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-10-p47r-production-target-external-inputs-resolution.md",
    "docs/roadmap/crm-sprint-10-p47r-external-input-resolution-matrix.md",
    "docs/operations/crm-sprint-10-p47r-production-target-discovery-evidence.md",
    "docs/operations/crm-sprint-10-p47r-current-production-state-evidence.md",
    "docs/operations/crm-sprint-10-p47r-deployment-state-classification.md",
    "docs/operations/crm-sprint-10-p47r-rollback-baseline-evidence.md",
    "docs/operations/crm-sprint-10-p47r-monitoring-target-binding.md",
    "docs/testing/crm-sprint-10-p47r-architecture-tests-investigation.md",
    "docs/operations/crm-sprint-10-p47r-approval-drift-assessment.md",
    "docs/roadmap/crm-sprint-10-p47r-risk-register.md",
    "docs/roadmap/crm-sprint-10-p48-entry-conditions-p47r.md"
)

foreach ($path in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Missing required P47R file: $path" }
}

$task = Get-Content -Raw -LiteralPath "docs/roadmap/crm-sprint-10-p47r-production-target-external-inputs-resolution.md"
$requiredMarkers = @(
    "P47RProductionTargetExternalInputsResolutionExists: true",
    "P47PullRequest: #128",
    "P47MergeCommit: e7dbb8fd1dd8122a5507c9dc4af19a0253ecc67b",
    "P47RBaseMainCommit: e7dbb8fd1dd8122a5507c9dc4af19a0253ecc67b",
    "P47HistoricalDecision: NotReadyForNewHumanApproval",
    "HistoricalStatePreserved: true",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionActivated: false",
    "ProductionDataChangesExecuted: false",
    "P45RetryAuthorized: false",
    "ExternalInputsTotal: 8",
    "ExternalInputsResolved: 0",
    "ExternalInputsRemaining: 8",
    "ProductionTargetResolutionDecision: NotResolved",
    "ProductionTargetFrozen: false",
    "ProductionMonitoringTargetResolved: false",
    "ProductionMonitoringReadyForRetry: false",
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
    "P47RDecision: NotReadyForNewHumanApproval"
)

foreach ($marker in $requiredMarkers) {
    if (-not $task.Contains($marker)) { throw "Missing required P47R marker: $marker" }
}

$forbiddenMarkers = @(
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionActivated: true",
    "ProductionTrafficSwitched: true",
    "ProductionDataChangesExecuted: true",
    "P45RetryAuthorized: true",
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
    if ($task.Contains($marker)) { throw "Forbidden P47R marker found: $marker" }
}

Write-Host "PASS CRM P47R production target external inputs guardrails passed."

