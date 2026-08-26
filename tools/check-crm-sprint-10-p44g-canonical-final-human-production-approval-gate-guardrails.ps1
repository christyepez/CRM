$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$expectedHash = "55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c"
$packetPath = Join-Path $root "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json"
$approvalHashTool = Join-Path $root "tools/approval-packet-hash.ps1"

if (-not (Test-Path $packetPath)) { throw "Missing canonical packet V3." }
if (-not (Test-Path $approvalHashTool)) { throw "Missing canonical hash tool." }

$hashes = 1..3 | ForEach-Object {
    (& powershell.exe -NoProfile -ExecutionPolicy Bypass -File $approvalHashTool $packetPath).Trim()
}
foreach ($hash in $hashes) {
    if ($hash -ne $expectedHash) { throw "Canonical hash mismatch: $hash" }
}

$p44gPaths = @(
    "docs/roadmap/crm-sprint-10-p44g-canonical-final-human-production-approval-gate.md",
    "docs/roadmap/crm-sprint-10-p44g-human-production-approval-record.md",
    "docs/roadmap/crm-sprint-10-p44g-residual-risk-acceptance-matrix.md",
    "docs/operations/crm-sprint-10-p44g-canonical-packet-identity-revalidation.md",
    "docs/operations/crm-sprint-10-p44g-candidate-image-identity-revalidation.md",
    "docs/operations/crm-sprint-10-p44g-technical-production-approval-decision.md",
    "docs/operations/crm-sprint-10-p44g-approval-expiration-rules.md",
    "docs/roadmap/crm-sprint-10-p44g-p45-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p44g-risk-register.md",
    "tools/check-crm-sprint-10-p44g-canonical-final-human-production-approval-gate-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44g-canonical-final-human-production-approval-gate.ps1",
    "tools/crm-sprint-10-p44g-canonical-final-human-production-approval-gate.ps1",
    "codex/TASKS.md"
)
foreach ($path in $p44gPaths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44G file: $path" }
}

$joined = ($p44gPaths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P44GCanonicalFinalHumanProductionApprovalGateExists: true",
    "P44GCanonicalFinalHumanProductionApprovalGateOnly: true",
    "P44FPullRequest: #123",
    "P44FMergeCommit: d3b360052807dd08251c74e18a7c3209cd11bb01",
    "P44GBaseMainCommit: d3b360052807dd08251c74e18a7c3209cd11bb01",
    "HistoricalStatePreserved: true",
    "FinalApprovalPacketId: CRM-S10-P44F-PACKET-V3",
    "ExpectedFinalApprovalPacketHash: 55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c",
    "ActualFinalApprovalPacketHash: 55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c",
    "FinalApprovalPacketIdentityMatched: true",
    "CanonicalPacketHashStable: true",
    "NonProductionRuntimeStable: true",
    "CandidateImageIdentityMatched: true",
    "ProductionApprovalDriftDetected: false",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "TechnicalProductionApprovalPassed: true",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: false",
    "LocalOnlyArtifactAcceptedForP45: false",
    "LocalOnlyRollbackAccepted: false",
    "SbomScannerResidualRiskAccepted: false",
    "ProductionApprovalDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "ProductionExecutionAuthorized: false",
    "P45Authorized: false",
    "ProductionActivated: false",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionTrafficSwitched: false"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P44G marker: $marker" }
}

$docs = ($p44gPaths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($bad in @(
    "ProductionApprovalDecision: Go",
    "HumanProductionApprovalRecorded: true",
    "ProductionApprovalExecuted: true",
    "ProductionExecutionAuthorized: true",
    "P45Authorized: true",
    "ProductionActivated: true",
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionTrafficSwitched: true",
    "LocalOnlyArtifactAcceptedForP45: true",
    "LocalOnlyRollbackAccepted: true",
    "SbomScannerResidualRiskAccepted: true",
    "PortalIncludedInProductionExecution: true",
    "CommonDbIncludedInProductionExecution: true",
    "ProductionDataChangesApproved: true"
)) {
    if ($docs -match "(?m)^$([regex]::Escape($bad))$") { throw "Forbidden P44G marker detected: $bad" }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM P44G canonical final human production approval gate guardrails passed."

