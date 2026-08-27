$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-10-p47u-supply-real-production-target-rollback-monitoring-evidence.md",
    "docs/roadmap/crm-sprint-10-p47u-production-input-matrix.md",
    "docs/operations/crm-sprint-10-p47u-human-operations-input-request.md",
    "docs/operations/crm-sprint-10-p47u-production-target-evidence.md",
    "docs/operations/crm-sprint-10-p47u-current-production-state-evidence.md",
    "docs/operations/crm-sprint-10-p47u-rollback-evidence.md",
    "docs/operations/crm-sprint-10-p47u-monitoring-evidence.md",
    "docs/operations/crm-sprint-10-p47u-approval-drift-assessment.md",
    "docs/roadmap/crm-sprint-10-p47u-final-approval-packet-status.md",
    "docs/roadmap/crm-sprint-10-p48-entry-conditions-p47u.md",
    "docs/roadmap/crm-sprint-10-p47u-risk-register.md"
)

foreach ($path in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Missing required P47U file: $path" }
}

$task = Get-Content -Raw -LiteralPath "docs/roadmap/crm-sprint-10-p47u-supply-real-production-target-rollback-monitoring-evidence.md"
$requiredMarkers = @(
    "P47USupplyRealProductionTargetRollbackMonitoringEvidenceExists: true",
    "P47TPullRequest: #131",
    "P47TMergeCommit: 2e2fdd9efacfc92dc1c643fb265bc8285009bfaf",
    "P47UBaseMainCommit: 2e2fdd9efacfc92dc1c643fb265bc8285009bfaf",
    "P47THistoricalDecision: NotReadyForNewHumanApproval",
    "HistoricalStatePreserved: true",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionActivated: false",
    "ProductionTrafficSwitched: false",
    "ProductionDataChangesExecuted: false",
    "ApprovalConsumed: false",
    "P45RetryAuthorized: false",
    "ExternalInputsTotal: 12",
    "ExternalInputsResolved: 0",
    "ExternalInputsRemaining: 12",
    "ProductionPlatformResolved: false",
    "ProductionTargetDefinitionStatus: MissingRequiredExternalOperationsEvidence",
    "ProductionTargetResolutionDecision: NotResolved",
    "ProductionTargetConnectivityValidated: false",
    "ProductionTargetManifestId: NotCreated",
    "ProductionTargetManifestHash: NotCreated",
    "ProductionTargetFrozen: false",
    "ProductionMonitoringTargetResolved: false",
    "ProductionMonitoringReadyForRetry: false",
    "CurrentProductionServicePresent: Unknown",
    "ProductionDeploymentState: Unknown",
    "RollbackBaselineType: NotResolved",
    "RollbackBaselineIdentified: false",
    "RollbackReadyForRetry: false",
    "RollbackBaselineFrozen: false",
    "ArchitectureTestsStatus: Passed",
    "ArchitectureTestsBlocking: false",
    "NewFinalApprovalPacketId: NotCreated",
    "NewFinalApprovalPacketHash: NotCreated",
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
    "P47UDecision: NotReadyForNewHumanApproval",
    "P48AllowedNow: false",
    "NextGate: Human/Operations Production Input Required Before Any P48 Approval Gate"
)

foreach ($marker in $requiredMarkers) {
    if (-not $task.Contains($marker)) { throw "Missing required P47U marker: $marker" }
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
    "ProductionTargetFrozen: true",
    "ProductionMonitoringTargetResolved: true",
    "ProductionMonitoringReadyForRetry: true",
    "RollbackReadyForRetry: true",
    "RollbackBaselineFrozen: true",
    "FinalApprovalPacketFrozen: true",
    "P47UDecision: ReadyForNewHumanApproval",
    "P48AllowedNow: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true"
)

foreach ($marker in $forbiddenMarkers) {
    if ($task.Contains($marker)) { throw "Forbidden P47U marker found: $marker" }
}

Write-Host "PASS CRM P47U guardrails passed."
