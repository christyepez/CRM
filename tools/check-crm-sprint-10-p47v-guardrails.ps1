$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-10-p47v-operations-evidence-intake-production-target-freeze.md",
    "docs/operations/crm-sprint-10-p47v-operations-production-evidence.md",
    "docs/roadmap/crm-sprint-10-p47v-operations-production-evidence.json",
    "docs/roadmap/crm-sprint-10-p47v-missing-operations-inputs.md",
    "docs/operations/crm-sprint-10-p47v-production-target-freeze-status.md",
    "docs/operations/crm-sprint-10-p47v-rollback-freeze-status.md",
    "docs/operations/crm-sprint-10-p47v-monitoring-freeze-status.md",
    "docs/roadmap/crm-sprint-10-p47v-final-approval-packet-status.md",
    "docs/roadmap/crm-sprint-10-p48-entry-conditions-p47v.md",
    "docs/roadmap/crm-sprint-10-p47v-risk-register.md"
)

foreach ($path in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Missing required P47V file: $path" }
}

$task = Get-Content -Raw -LiteralPath "docs/roadmap/crm-sprint-10-p47v-operations-evidence-intake-production-target-freeze.md"
$requiredMarkers = @(
    "P47VOperationsEvidenceIntakeProductionTargetFreezeExists: true",
    "P47UPullRequest: #132",
    "P47UMergeCommit: 6a9d7703eb72463abf4ed573cde180b94d97dd33",
    "P47VBaseMainCommit: 6a9d7703eb72463abf4ed573cde180b94d97dd33",
    "P47UHistoricalDecision: NotReadyForNewHumanApproval",
    "HistoricalStatePreserved: true",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionActivated: false",
    "ProductionTrafficSwitched: false",
    "ProductionDataChangesExecuted: false",
    "ApprovalConsumed: false",
    "P45RetryAuthorized: false",
    "OperationsInputsTotal: 12",
    "OperationsInputsResolved: 0",
    "OperationsInputsMissing: 12",
    "ProductionPlatformResolved: false",
    "ProductionTargetResolutionDecision: NotResolved",
    "ProductionTargetConnectivityValidated: false",
    "DeploymentMechanismFrozen: false",
    "ProductionNetworkResolved: false",
    "ProductionTargetManifestId: NotCreated",
    "ProductionTargetManifestHash: NotCreated",
    "ProductionTargetFrozen: false",
    "ProductionMonitoringTargetResolved: false",
    "ProductionMonitoringReadyForRetry: false",
    "CurrentProductionServicePresent: MissingRequiredOperationsInput",
    "ProductionDeploymentState: Unknown",
    "RollbackBaselineType: NotResolved",
    "RollbackBaselineId: NotCreated",
    "RollbackBaselineHash: NotCreated",
    "RollbackBaselineIdentified: false",
    "RollbackReadyForRetry: false",
    "RollbackBaselineFrozen: false",
    "ArchitectureTestsStatus: Passed",
    "ArchitectureTestsBlocking: false",
    "CandidateImageIdentityMatched: true",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "NewFinalApprovalPacketId: NotCreated",
    "NewFinalApprovalPacketHash: NotCreated",
    "FinalApprovalPacketFrozen: false",
    "PreviousHumanApprovalReusable: false",
    "ExistingHumanApprovalStillValidForRetry: false",
    "NewHumanApprovalRequiredForRetry: true",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "CriticalProductionBlockers: 3",
    "P47VDecision: NotReadyForNewHumanApproval",
    "P48AllowedNow: false",
    "NextGate: OperationsMustSupplyRealProductionEvidenceBeforeP48"
)

foreach ($marker in $requiredMarkers) {
    if (-not $task.Contains($marker)) { throw "Missing required P47V marker: $marker" }
}

$forbiddenMarkers = @(
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionActivated: true",
    "ProductionTrafficSwitched: true",
    "ProductionDataChangesExecuted: true",
    "ApprovalConsumed: true",
    "P45RetryAuthorized: true",
    "ProductionTargetResolutionDecision: Resolved",
    "ProductionTargetConnectivityValidated: true",
    "ProductionTargetFrozen: true",
    "ProductionMonitoringTargetResolved: true",
    "ProductionMonitoringReadyForRetry: true",
    "RollbackReadyForRetry: true",
    "RollbackBaselineFrozen: true",
    "FinalApprovalPacketFrozen: true",
    "P47VDecision: ReadyForNewHumanApproval",
    "P48AllowedNow: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true"
)

foreach ($marker in $forbiddenMarkers) {
    if ($task.Contains($marker)) { throw "Forbidden P47V marker found: $marker" }
}

Write-Host "PASS CRM P47V guardrails passed."
