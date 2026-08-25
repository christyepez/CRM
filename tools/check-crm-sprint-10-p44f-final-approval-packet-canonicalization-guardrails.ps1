$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-canonicalization-and-hash-refreeze.md",
    "docs/architecture/crm-sprint-10-p44f-approval-packet-hash-root-cause-analysis.md",
    "docs/architecture/crm-sprint-10-p44f-canonical-approval-packet-schema.md",
    "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json",
    "tools/approval-packet-hash.ps1",
    "tools/test-crm-sprint-10-p44f-canonical-hash.ps1",
    "docs/operations/crm-sprint-10-p44f-hash-reproducibility-evidence.md",
    "docs/operations/crm-sprint-10-p44f-candidate-image-identity-revalidation.md",
    "docs/operations/crm-sprint-10-p44f-nonproduction-revalidation.md",
    "docs/architecture/crm-sprint-10-p44f-runtime-drift-revalidation.md",
    "docs/roadmap/crm-sprint-10-p44f-decision.md",
    "docs/roadmap/crm-sprint-10-p44f-p44g-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p44f-risk-register.md",
    "tools/check-crm-sprint-10-p44f-final-approval-packet-canonicalization-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44f-final-approval-packet-canonicalization.ps1",
    "tools/crm-sprint-10-p44f-final-approval-packet-canonicalization.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44F file: $path" }
}
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P44FFinalApprovalPacketCanonicalizationExists: true",
    "P44FIntegrityRemediationOnly: true",
    "P44EPullRequest: #122",
    "P44EMergeCommit: 5062814cc33f7ad44bf5c985d866c27323ada009",
    "P44FBaseMainCommit: 5062814cc33f7ad44bf5c985d866c27323ada009",
    "P44EHistoricalDecision: NoGo",
    "HistoricalStatePreserved: true",
    "Old FinalApprovalPacketId: CRM-S10-P44D-PACKET-V2",
    "NewFinalApprovalPacketId: CRM-S10-P44F-PACKET-V3",
    "NewFinalApprovalPacketHash: 55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c",
    "CanonicalizationVersion: crm-approval-packet-canonical-json-v1",
    "CanonicalHashTool: tools/approval-packet-hash.ps1",
    "CanonicalPacketHashStable: true",
    "FinalApprovalPacketFrozen: true",
    "P44EHashMismatchResolved: true",
    "ProductionApprovalDriftDetected: false",
    "NonProductionRuntimeStable: true",
    "CandidateImageIdentityMatched: true",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "ApprovedProductionExternalDependencies: none",
    "LocalOnlyArtifactAcceptedForP45: false",
    "LocalOnlyRollbackAccepted: false",
    "SbomScannerResidualRiskAccepted: false",
    "CriticalProductionBlockers: 0",
    "HighBlockingRisks: 0",
    "P44FDecision: ReadyForHumanApprovalOnCanonicalPacketWithConditions",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: false",
    "ProductionApprovalDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "ProductionExecutionAuthorized: false",
    "P45Authorized: false",
    "ProductionActivated: false",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P44F marker: $marker" }
}
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
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true",
    "ProductionDataChangesExecuted: true",
    "LocalOnlyArtifactAcceptedForP45: true",
    "LocalOnlyRollbackAccepted: true",
    "SbomScannerResidualRiskAccepted: true"
)) {
    if ($docs -match "(?m)^$([regex]::Escape($bad))$") { throw "Forbidden P44F marker detected: $bad" }
}
$packet = Get-Content (Join-Path $root "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json") -Raw
foreach ($field in @("timestamp","generatedAt","validatedAt","machineName","absolutePath","containerId","currentProcessId","workingDirectory")) {
    if ($packet -match ('"' + [regex]::Escape($field) + '"')) { throw "Forbidden dynamic field in canonical packet: $field" }
}
$hash = (& powershell.exe -NoProfile -ExecutionPolicy Bypass -File (Join-Path $root "tools/approval-packet-hash.ps1") (Join-Path $root "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json")).Trim()
if ($hash -ne "55be737c45256180f8fa157c4c5d26e9d6a8cadbc234945a995e96da660b078c") { throw "Canonical packet hash mismatch: $hash" }
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P44F final approval packet canonicalization guardrails passed."
