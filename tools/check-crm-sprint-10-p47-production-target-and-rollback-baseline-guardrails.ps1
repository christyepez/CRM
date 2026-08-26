$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-10-p47-production-target-and-rollback-baseline-resolution.md",
    "docs/roadmap/crm-sprint-10-p47-production-target-manifest.json",
    "docs/roadmap/crm-sprint-10-p47-rollback-baseline-manifest.json",
    "docs/roadmap/crm-sprint-10-p47-final-approval-packet-v4.json",
    "docs/roadmap/crm-sprint-10-p47-required-external-inputs.md",
    "docs/operations/crm-sprint-10-p47-production-target-resolution-evidence.md",
    "docs/operations/crm-sprint-10-p47-current-production-state-evidence.md",
    "docs/operations/crm-sprint-10-p47-deployment-classification.md",
    "docs/operations/crm-sprint-10-p47-rollback-readiness-evidence.md",
    "docs/operations/crm-sprint-10-p47-target-specific-deployment-runbook.md",
    "docs/operations/crm-sprint-10-p47-production-monitoring-target-binding.md",
    "docs/operations/crm-sprint-10-p47-approval-drift-assessment.md",
    "docs/roadmap/crm-sprint-10-p47-packet-v4-hash-evidence.md",
    "docs/roadmap/crm-sprint-10-p48-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p47-risk-register.md"
)

foreach ($path in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Missing required P47 file: $path" }
}

$task = Get-Content -Raw -LiteralPath "docs/roadmap/crm-sprint-10-p47-production-target-and-rollback-baseline-resolution.md"
$requiredMarkers = @(
    "P47ProductionTargetAndRollbackBaselineResolutionExists: true",
    "P46PullRequest: #127",
    "P46MergeCommit: 44b4556c00f51ff840a9f517a5dbd99b80e237ec",
    "P47BaseMainCommit: 44b4556c00f51ff840a9f517a5dbd99b80e237ec",
    "P45HistoricalExecutionResult: AbortedBeforeExecution",
    "P46HistoricalDecision: ReadyForProductionRetryAfterExternalInputs",
    "HistoricalStatePreserved: true",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionActivated: false",
    "ProductionTrafficSwitched: false",
    "ProductionDataChangesExecuted: false",
    "P45RetryAuthorized: false",
    "ProductionTargetResolutionDecision: NotResolved",
    "ProductionTargetFrozen: false",
    "RollbackBaselineIdentified: false",
    "RollbackReadyForRetry: false",
    "PreviousHumanApprovalReusable: false",
    "ExistingHumanApprovalStillValidForRetry: false",
    "NewHumanApprovalRequiredForRetry: true",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "CommonDbRuntimeEnabled: false",
    "P47Decision: NotReadyForNewHumanApproval"
)

foreach ($marker in $requiredMarkers) {
    if (-not $task.Contains($marker)) { throw "Missing required P47 marker: $marker" }
}

$forbiddenMarkers = @(
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionActivated: true",
    "ProductionTrafficSwitched: true",
    "ProductionDataChangesExecuted: true",
    "P45RetryAuthorized: true",
    "PreviousHumanApprovalReusable: true",
    "ExistingHumanApprovalStillValidForRetry: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "ProductionTargetResolutionDecision: Resolved",
    "ProductionTargetFrozen: true",
    "RollbackReadyForRetry: true",
    "FinalApprovalPacketV4Frozen: true"
)

foreach ($marker in $forbiddenMarkers) {
    if ($task.Contains($marker)) { throw "Forbidden P47 marker found: $marker" }
}

$packetHash = & "$PSScriptRoot\approval-packet-v4-hash.ps1" "docs/roadmap/crm-sprint-10-p47-final-approval-packet-v4.json"
if (-not $packetHash -or $packetHash.Length -ne 64) { throw "Invalid P47 packet hash." }

Write-Host "PASS CRM P47 production target and rollback baseline guardrails passed. PacketHash=$packetHash"

