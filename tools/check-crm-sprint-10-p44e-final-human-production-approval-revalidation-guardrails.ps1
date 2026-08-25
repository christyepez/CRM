$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p44e-final-human-production-approval-revalidation-gate.md",
    "docs/roadmap/crm-sprint-10-p44e-final-approval-packet-identity-validation.md",
    "docs/roadmap/crm-sprint-10-p44e-final-human-production-approval-record.md",
    "docs/roadmap/crm-sprint-10-p44e-residual-risk-acceptance-matrix.md",
    "docs/operations/crm-sprint-10-p44e-nonproduction-revalidation.md",
    "docs/operations/crm-sprint-10-p44e-candidate-image-identity-validation.md",
    "docs/architecture/crm-sprint-10-p44e-production-approval-drift-validation.md",
    "docs/roadmap/crm-sprint-10-p44e-technical-approval-decision.md",
    "docs/roadmap/crm-sprint-10-p44e-production-approval-decision.md",
    "docs/roadmap/crm-sprint-10-p44e-approval-expiration-rules.md",
    "docs/roadmap/crm-sprint-10-p44e-p45-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p44e-p45-immutable-image-gate.md",
    "docs/roadmap/crm-sprint-10-p44e-risk-register.md",
    "tools/check-crm-sprint-10-p44e-final-human-production-approval-revalidation-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44e-final-human-production-approval-revalidation.ps1",
    "tools/crm-sprint-10-p44e-final-human-production-approval-revalidation.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44E file: $path" }
}
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P44EFinalHumanProductionApprovalRevalidationGateExists: true",
    "P44EFinalHumanProductionApprovalRevalidationGateOnly: true",
    "P44DPullRequest: #121",
    "P44DMergeCommit: ed107e9c1a1cd47c6e420952ac8b0ef0cc15d67b",
    "P44EBaseMainCommit: ed107e9c1a1cd47c6e420952ac8b0ef0cc15d67b",
    "P44HistoricalDecision: NoGo",
    "P44AHistoricalDecision: NoGo",
    "P44BHistoricalDecision: ReadyForFinalHumanApprovalWithConditions",
    "P44CHistoricalDecision: NoGo",
    "P44DHistoricalDecision: ReadyForFinalApprovalRevalidationWithConditions",
    "HistoricalStatePreserved: true",
    "FinalApprovalPacketId: CRM-S10-P44D-PACKET-V2",
    "ExpectedFinalApprovalPacketHash: 15c4f02bfb5f09824d6facb41629e262db2d7fa571458c548b4bb882c554ca12",
    "ActualFinalApprovalPacketHash: 0a212d1d11c1a70a2b1019f04dc1607d776c0b2c4f7c67829fac1cdf584fdf44",
    "FinalApprovalPacketIdentityMatched: false",
    "NonProductionRuntimeStable: true",
    "ProductionRuntimeTargetCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c",
    "ProductionCandidateImage: crm-api:prod-candidate-8623c619",
    "ExpectedCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37",
    "ActualCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37",
    "CandidateImageIdentityMatched: true",
    "ProductionCandidateImageDigest: crm-api@sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37",
    "ProductionArtifactPublished: false",
    "ProductionTargetImageDecision: ImmutableLocallyOnly",
    "ProductionExecutionScope: CRM API first slice only",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "ApprovedProductionExternalDependencies: none",
    "RollbackMechanismAvailable: true",
    "RollbackTargetImmutable: true",
    "RollbackArtifactPresent: true",
    "SBOMAvailable: false",
    "OfficialImageScannerAvailable: false",
    "LocalOnlyArtifactAcceptedForP45: false",
    "LocalOnlyRollbackAccepted: false",
    "SbomScannerResidualRiskAccepted: false",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "ProductionApprovalDriftDetected: true",
    "CriticalProductionBlockers: 1",
    "HighBlockingRisks: 0",
    "TechnicalProductionApprovalPassed: false",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: false",
    "HumanProductionApproverReference: none",
    "HumanProductionApprovalDecision: NoGo",
    "ProductionApprovalDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "ProductionExecutionAuthorized: false",
    "P45Authorized: false",
    "ProductionScopeFrozen: true",
    "ProductionTargetFrozen: true",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "ProductionTrafficSwitched: false"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P44E marker: $marker" }
}
foreach ($bad in @(
    "ProductionApprovalDecision: Go",
    "HumanProductionApprovalRecorded: true",
    "HumanProductionApprovalDecision: Go",
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
    "CrmProductionReady: true",
    "LocalOnlyArtifactAcceptedForP45: true",
    "LocalOnlyRollbackAccepted: true",
    "SbomScannerResidualRiskAccepted: true",
    "TechnicalProductionApprovalPassed: true"
)) {
    if ($docs -match "(?m)^$([regex]::Escape($bad))$") { throw "Forbidden P44E marker detected: $bad" }
}
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P44E content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P44E final human production approval revalidation guardrails passed."
