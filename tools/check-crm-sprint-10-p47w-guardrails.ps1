$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-10-p47w-freeze-local-simulated-production-target.md",
    "docs/testing/crm-sprint-10-p47w-web-api-surface-validation.md",
    "docs/roadmap/crm-sprint-10-p47w-simulated-production-target-manifest-v1.json",
    "docs/roadmap/crm-sprint-10-p47w-simulated-production-rollback-baseline-v1.json",
    "docs/roadmap/crm-sprint-10-p47w-simulated-production-final-approval-packet-v5.json",
    "docs/roadmap/crm-sprint-10-p47w-hash-evidence.md",
    "docs/operations/crm-sprint-10-p47w-monitoring-freeze-evidence.md",
    "docs/operations/crm-sprint-10-p47w-nonprod-isolation-evidence.md",
    "docs/security/crm-sprint-10-p47w-security-evidence.md",
    "docs/roadmap/crm-sprint-10-p48-entry-conditions-p47w.md",
    "docs/roadmap/crm-sprint-10-p47w-risk-register.md",
    "tools/hash-crm-sprint-10-p47w-json.ps1"
)

foreach ($path in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Missing required P47W file: $path" }
}

$task = Get-Content -Raw -LiteralPath "docs/roadmap/crm-sprint-10-p47w-freeze-local-simulated-production-target.md"
$requiredMarkers = @(
    "P47WFreezeLocalSimulatedProductionTargetExists: true",
    "OPS04PullRequest: #134",
    "OPS04MergeCommit: cb7b1cc3cf9fd632cb83f4eb56a6787aa1ddbbc6",
    "P47WBaseMainCommit: cb7b1cc3cf9fd632cb83f4eb56a6787aa1ddbbc6",
    "EnvironmentClassification: SimulatedProduction",
    "RealProduction: false",
    "LocalSimulation: true",
    "ApplicationType: APIOnly",
    "FrontendProjectPresent: true",
    "FrontendIncludedInCurrentProductionScope: false",
    "RootUrlStatusCode: 404",
    "SwaggerStatus: 404",
    "ExpectedSimulatedProductionAccess: APIHealthOnly",
    "WebAccessStatus: ExpectedBehavior",
    "Health: HTTP 200",
    "Liveness: HTTP 200",
    "Readiness: HTTP 200",
    "CRMReadiness: HTTP 200 ReadyForFoundationOnly",
    "ProductiveRoutesExposureValid: true",
    "ContainerRunning: true",
    "DockerHealth: healthy",
    "RestartCount: 0",
    "BoundToLoopback: true",
    "CandidateImageIdentityMatched: true",
    "ProductionTargetManifestId: CRM-S10-P47W-SIMPROD-TARGET-V1",
    "ProductionTargetManifestHash: 075b67f6bf492e446908b21f365523252d91c76c5cc62e70faa62831313b61b5",
    "ProductionTargetFrozen: true",
    "RollbackBaselineId: CRM-S10-P47W-SIMPROD-ROLLBACK-V1",
    "RollbackBaselineHash: 9d4e5a95f5be179516f7fac160f855adb8595e7b8012acc9270fe6f6a93edf1d",
    "RollbackReadyForRetry: true",
    "RollbackBaselineFrozen: true",
    "ProductionMonitoringTargetResolved: true",
    "ProductionMonitoringReadyForRetry: true",
    "NonProdUnaffected: true",
    "SeparateComposeProject: true",
    "SeparateContainer: true",
    "SeparatePort: true",
    "SeparateNetwork: true",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "ArchitectureTestsStatus: Passed",
    "ArchitectureTestsBlocking: false",
    "PortalIncluded: false",
    "CommonDbIncluded: false",
    "ProductionDataChangesApproved: false",
    "NewFinalApprovalPacketId: CRM-S10-P47W-SIMPROD-PACKET-V5",
    "NewFinalApprovalPacketHash: f33a6af176066e90dbc674ae9393318dd934646cc6a747ef5ffd31ca988593a9",
    "CanonicalPacketHashStable: true",
    "FinalApprovalPacketFrozen: true",
    "CriticalProductionBlockers: 0",
    "P47WDecision: ReadyForNewHumanSimulatedProductionApproval",
    "P48AllowedNow: true"
)

foreach ($marker in $requiredMarkers) {
    if (-not $task.Contains($marker)) { throw "Missing required P47W marker: $marker" }
}

$forbiddenMarkers = @(
    "RealProduction: true",
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionActivated: true",
    "PortalIncluded: true",
    "CommonDbIncluded: true",
    "ProductionDataChangesApproved: true",
    "WebAccessStatus: RuntimeFailure",
    "CriticalProductionBlockers: 1"
)

foreach ($marker in $forbiddenMarkers) {
    if ($task.Contains($marker)) { throw "Forbidden P47W marker found: $marker" }
}

$targetHash = (& powershell.exe -NoProfile -ExecutionPolicy Bypass -File "tools\hash-crm-sprint-10-p47w-json.ps1" "docs\roadmap\crm-sprint-10-p47w-simulated-production-target-manifest-v1.json").Trim()
$rollbackHash = (& powershell.exe -NoProfile -ExecutionPolicy Bypass -File "tools\hash-crm-sprint-10-p47w-json.ps1" "docs\roadmap\crm-sprint-10-p47w-simulated-production-rollback-baseline-v1.json").Trim()
$packetHash = (& powershell.exe -NoProfile -ExecutionPolicy Bypass -File "tools\hash-crm-sprint-10-p47w-json.ps1" "docs\roadmap\crm-sprint-10-p47w-simulated-production-final-approval-packet-v5.json").Trim()

if ($targetHash -ne "075b67f6bf492e446908b21f365523252d91c76c5cc62e70faa62831313b61b5") { throw "Target hash mismatch: $targetHash" }
if ($rollbackHash -ne "9d4e5a95f5be179516f7fac160f855adb8595e7b8012acc9270fe6f6a93edf1d") { throw "Rollback hash mismatch: $rollbackHash" }
if ($packetHash -ne "f33a6af176066e90dbc674ae9393318dd934646cc6a747ef5ffd31ca988593a9") { throw "Packet hash mismatch: $packetHash" }

Write-Host "PASS CRM P47W guardrails passed."
