$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$expectedHash = "55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c"
$packetPath = Join-Path $root "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json"
$approvalHashTool = Join-Path $root "tools/approval-packet-hash.ps1"

$hashes = 1..3 | ForEach-Object { (& powershell.exe -NoProfile -ExecutionPolicy Bypass -File $approvalHashTool $packetPath).Trim() }
foreach ($hash in $hashes) {
    if ($hash -ne $expectedHash) { throw "Canonical hash mismatch: $hash" }
}

$paths = @(
    "docs/roadmap/crm-sprint-10-p45-controlled-production-activation-execution.md",
    "docs/operations/crm-sprint-10-p45-entry-condition-validation.md",
    "docs/operations/crm-sprint-10-p45-pre-execution-freeze.md",
    "docs/operations/crm-sprint-10-p45-production-target-discovery.md",
    "docs/operations/crm-sprint-10-p45-approval-consumption-record.md",
    "docs/operations/crm-sprint-10-p45-deployment-evidence.md",
    "docs/operations/crm-sprint-10-p45-image-identity-evidence.md",
    "docs/operations/crm-sprint-10-p45-production-smoke-evidence.md",
    "docs/operations/crm-sprint-10-p45-security-evidence.md",
    "docs/operations/crm-sprint-10-p45-monitoring-evidence.md",
    "docs/operations/crm-sprint-10-p45-scope-boundary-evidence.md",
    "docs/operations/crm-sprint-10-p45-abort-evaluation.md",
    "docs/operations/crm-sprint-10-p45-rollback-evidence.md",
    "docs/roadmap/crm-sprint-10-p45-execution-decision.md",
    "docs/roadmap/crm-sprint-10-p45-risk-register.md",
    "docs/roadmap/crm-sprint-10-p46-entry-conditions.md",
    "tools/check-crm-sprint-10-p45-controlled-production-activation-execution-guardrails.ps1",
    "tools/verify-crm-sprint-10-p45-controlled-production-activation-execution.ps1",
    "tools/crm-sprint-10-p45-controlled-production-activation-execution.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P45 file: $path" }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "P45ControlledProductionActivationExecution: true",
    "P44HPullRequest: #125",
    "P44HMergeCommit: f462d03ecceee0b4d8faa34bbd2b11df21bcca97",
    "FinalApprovalPacketId: CRM-S10-P44F-PACKET-V3",
    "ActualFinalApprovalPacketHash: 55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c",
    "FinalApprovalPacketIdentityMatched: true",
    "CanonicalPacketHashStable: true",
    "HumanProductionApprovalRecorded: true",
    "HumanProductionApprovalDecision: Go",
    "LocalOnlyArtifactAcceptedForP45: true",
    "LocalOnlyRollbackAccepted: true",
    "SbomScannerResidualRiskAccepted: true",
    "RuntimeTargetCommitMatched: true",
    "CandidateImageIdentityMatched: true",
    "ProductionApprovalDriftDetected: false",
    "ProductionTargetResolved: false",
    "RollbackPreflightPassed: false",
    "P45PreExecutionValidated: false",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "DeploymentResult: AbortedBeforeExecution",
    "ProductionAbortTriggered: true",
    "RollbackTriggered: false",
    "ApprovalConsumed: false",
    "ProductionExecutionResult: AbortedBeforeExecution",
    "ProductionActivated: false",
    "ProductionTrafficSwitched: false",
    "RuntimePortalCallsEnabled: false",
    "PortalRoutesActivated: false",
    "PortalNavigationActivated: false",
    "CommonDbRuntimeEnabled: false",
    "ProductionDataChangesExecuted: false"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P45 marker: $marker" }
}

$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($bad in @(
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionActivated: true",
    "ProductionTrafficSwitched: true",
    "RuntimePortalCallsEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true",
    "CommonDbRuntimeEnabled: true",
    "ProductionDataChangesExecuted: true",
    "ApprovalConsumed: true",
    "P45PreExecutionValidated: true",
    "ProductionTargetResolved: true",
    "RollbackTriggered: true"
)) {
    if ($docs -match "(?m)^$([regex]::Escape($bad))$") { throw "Forbidden P45 marker detected: $bad" }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM P45 controlled production activation execution guardrails passed with AbortedBeforeExecution."

