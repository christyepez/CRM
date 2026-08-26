$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$expectedHash = "55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c"
$packetPath = Join-Path $root "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json"
$approvalHashTool = Join-Path $root "tools/approval-packet-hash.ps1"

if (-not (Test-Path $packetPath)) { throw "Missing canonical packet V3." }
$hashes = 1..3 | ForEach-Object { (& powershell.exe -NoProfile -ExecutionPolicy Bypass -File $approvalHashTool $packetPath).Trim() }
foreach ($hash in $hashes) {
    if ($hash -ne $expectedHash) { throw "Canonical hash mismatch: $hash" }
}

$paths = @(
    "docs/roadmap/crm-sprint-10-p44h-record-explicit-human-production-approval-canonical-packet-v3.md",
    "docs/roadmap/crm-sprint-10-p44h-explicit-human-production-approval-record.md",
    "docs/roadmap/crm-sprint-10-p44h-human-residual-risk-acceptance-record.md",
    "docs/operations/crm-sprint-10-p44h-canonical-packet-revalidation-evidence.md",
    "docs/operations/crm-sprint-10-p44h-candidate-image-revalidation-evidence.md",
    "docs/operations/crm-sprint-10-p44h-approval-drift-validation.md",
    "docs/operations/crm-sprint-10-p44h-final-technical-approval-decision.md",
    "docs/roadmap/crm-sprint-10-p44h-final-human-approval-decision.md",
    "docs/roadmap/crm-sprint-10-p44h-p45-authorization-record.md",
    "docs/operations/crm-sprint-10-p44h-approval-expiration-rules.md",
    "docs/roadmap/crm-sprint-10-p44h-p45-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p44h-p45-mandatory-stop-conditions.md",
    "docs/roadmap/crm-sprint-10-p44h-risk-register.md",
    "tools/check-crm-sprint-10-p44h-record-explicit-human-production-approval-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44h-record-explicit-human-production-approval.ps1",
    "tools/crm-sprint-10-p44h-record-explicit-human-production-approval.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44H file: $path" }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "P44HExplicitHumanProductionApprovalRecordOnly: true",
    "P44GPullRequest: #124",
    "P44GMergeCommit: 0fb9e03f66c1b85f67ac266316688c986f214061",
    "P44GHistoricalDecision: NoGo",
    "HistoricalStatePreserved: true",
    "FinalApprovalPacketId: CRM-S10-P44F-PACKET-V3",
    "ActualFinalApprovalPacketHash: 55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c",
    "FinalApprovalPacketIdentityMatched: true",
    "CanonicalPacketHashStable: true",
    "CandidateImageIdentityMatched: true",
    "NonProductionRuntimeStable: true",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "ProductionApprovalDriftDetected: false",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "ApprovedProductionExternalDependencies: none",
    "LocalOnlyArtifactAcceptedForP45: true",
    "LocalOnlyRollbackAccepted: true",
    "SbomScannerResidualRiskAccepted: true",
    "R1Decision: AcceptedByHuman",
    "R2Decision: AcceptedByHuman",
    "R3Decision: AcceptedByHuman",
    "TechnicalProductionApprovalPassed: true",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: true",
    "HumanProductionApprovalDecision: Go",
    "ProductionApprovalDecision: Go",
    "ProductionApprovalExecuted: true",
    "ProductionExecutionAuthorized: true",
    "P45Authorized: true",
    "ProductionActivationDecision: Go",
    "CrmProductionReady: true",
    "ProductionActivated: false",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionTrafficSwitched: false"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P44H marker: $marker" }
}

$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($bad in @(
    "ProductionActivated: true",
    "ProductionExecutionStarted: true",
    "ProductionDeploymentExecuted: true",
    "ProductionTrafficSwitched: true",
    "PortalIncludedInProductionExecution: true",
    "CommonDbIncludedInProductionExecution: true",
    "ProductionDataChangesApproved: true",
    "ProductionDataChangesExecuted: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "P45CandidateImageRebuildAllowed: true"
)) {
    if ($docs -match "(?m)^$([regex]::Escape($bad))$") { throw "Forbidden P44H marker detected: $bad" }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM P44H explicit human production approval guardrails passed."

