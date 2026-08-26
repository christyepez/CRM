$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$expectedHash = "55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c"
$packetPath = Join-Path $root "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json"
$hashTool = Join-Path $root "tools/approval-packet-hash.ps1"

$hash = (& powershell.exe -NoProfile -ExecutionPolicy Bypass -File $hashTool $packetPath).Trim()
if ($hash -ne $expectedHash) { throw "Canonical packet hash mismatch: $hash" }

$paths = @(
    "docs/roadmap/crm-sprint-10-p46-production-post-abort-validation-and-remediation-readiness.md",
    "docs/operations/crm-sprint-10-p46-p45-post-abort-validation.md",
    "docs/operations/crm-sprint-10-p46-production-untouched-evidence.md",
    "docs/operations/crm-sprint-10-p46-production-target-resolution-analysis.md",
    "docs/operations/crm-sprint-10-p46-production-target-manifest-draft.md",
    "docs/operations/crm-sprint-10-p46-rollback-baseline-analysis.md",
    "docs/operations/crm-sprint-10-p46-first-deployment-rollback-model.md",
    "docs/operations/crm-sprint-10-p46-approval-reuse-assessment.md",
    "docs/operations/crm-sprint-10-p46-candidate-artifact-revalidation.md",
    "docs/operations/crm-sprint-10-p46-packet-revalidation.md",
    "docs/operations/crm-sprint-10-p46-runtime-drift-revalidation.md",
    "docs/testing/crm-sprint-10-p46-architecture-tests-investigation.md",
    "docs/roadmap/crm-sprint-10-p46-risk-register.md",
    "docs/roadmap/crm-sprint-10-p47-entry-conditions.md",
    "tools/check-crm-sprint-10-p46-production-post-abort-validation-guardrails.ps1",
    "tools/verify-crm-sprint-10-p46-production-post-abort-validation.ps1",
    "tools/crm-sprint-10-p46-production-post-abort-validation.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P46 file: $path" }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "P45HistoricalExecutionResult: AbortedBeforeExecution",
    "HistoricalStatePreserved: true",
    "ProductionUntouchedAfterP45Abort: true",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionActivated: false",
    "ProductionTrafficSwitched: false",
    "ProductionDataChangesExecuted: false",
    "ApprovalConsumed: false",
    "FinalApprovalPacketIdentityMatched: true",
    "CanonicalPacketHashStable: true",
    "CandidateImageStillPresent: true",
    "CandidateImageIdentityMatched: true",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "ProductionTargetDefinitionStatus: MissingRequiredExternalConfiguration",
    "ProductionTargetResolutionDecision: NotResolved",
    "RollbackBaselineIdentified: false",
    "RollbackMechanismDefined: false",
    "RollbackReadyForRetry: false",
    "ExistingHumanApprovalStillValidForRetry: false",
    "NewHumanApprovalRequiredForRetry: true",
    "ArchitectureTestsStatus: NonConclusive",
    "CriticalProductionBlockers: 3",
    "P46Decision: ReadyForProductionRetryAfterExternalInputs"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P46 marker: $marker" }
}

$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($bad in @(
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionActivated: true",
    "ProductionTrafficSwitched: true",
    "ProductionDataChangesExecuted: true",
    "ApprovalConsumed: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "ExistingHumanApprovalStillValidForRetry: true",
    "RollbackReadyForRetry: true",
    "ProductionTargetResolutionDecision: Resolved"
)) {
    if ($docs -match "(?m)^$([regex]::Escape($bad))$") { throw "Forbidden P46 marker detected: $bad" }
}

Write-Host "PASS CRM P46 post-abort validation guardrails passed."

