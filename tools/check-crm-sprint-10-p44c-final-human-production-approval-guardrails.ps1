$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p44c-final-human-production-approval-gate.md",
    "docs/operations/crm-sprint-10-p44c-production-approval-revalidation.md",
    "docs/roadmap/crm-sprint-10-p44c-human-production-approval-record.md",
    "docs/roadmap/crm-sprint-10-p44c-residual-production-risk-acceptance.md",
    "docs/roadmap/crm-sprint-10-p44c-production-approval-drift-validation.md",
    "docs/roadmap/crm-sprint-10-p44c-final-production-target-freeze.md",
    "docs/roadmap/crm-sprint-10-p44c-final-production-scope-freeze.md",
    "docs/operations/crm-sprint-10-p44c-final-artifact-identity-evidence.md",
    "docs/operations/crm-sprint-10-p44c-rollback-acceptance.md",
    "docs/security/crm-sprint-10-p44c-security-condition-acceptance.md",
    "docs/roadmap/crm-sprint-10-p44c-approval-expiration-rules.md",
    "docs/roadmap/crm-sprint-10-p44c-p45-entry-conditions.md",
    "tools/check-crm-sprint-10-p44c-final-human-production-approval-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44c-final-human-production-approval.ps1",
    "tools/crm-sprint-10-p44c-final-human-production-approval.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44C file: $path" }
}
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P44CFinalHumanProductionApprovalGateExists: true",
    "P44CFinalHumanProductionApprovalGateOnly: true",
    "P44BPullRequest: #119",
    "P44BMergeCommit: 3782216be2f5fff4dc8c152e3ecd1314da950406",
    "P44CBaseMainCommit: 3782216be2f5fff4dc8c152e3ecd1314da950406",
    "P44HistoricalDecision: NoGo",
    "P44AHistoricalDecision: NoGo",
    "P44BTechnicalPreconditionsDecision: ReadyForFinalHumanApprovalWithConditions",
    "HistoricalStatePreserved: true",
    "NonProductionRuntimeStable: false",
    "ProductionRuntimeTargetCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c",
    "ProductionCandidateImage: crm-api:prod-candidate-8623c619",
    "ProductionCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37",
    "ProductionCandidateImageDigest: crm-api@sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37",
    "ProductionArtifactPublished: false",
    "ProductionTargetImageDecision: ImmutableLocallyOnly",
    "ProductionExecutionScope: CRM API first slice only",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "ApprovedProductionExternalDependencies: none",
    "LocalOnlyArtifactAcceptedForP45: false",
    "LocalOnlyRollbackAccepted: false",
    "SbomScannerResidualRiskAccepted: false",
    "CriticalProductionBlockers: 1",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: false",
    "HumanProductionApprovalDecision: NoGo",
    "ProductionApprovalDriftDetected: true",
    "ProductionApprovalDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "ProductionExecutionAuthorized: false",
    "ProductionScopeFrozen: true",
    "ProductionTargetFrozen: true",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "P45Blocked: true"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P44C marker: $marker" }
}
foreach ($bad in @(
    "ProductionApprovalDecision: Go",
    "HumanProductionApprovalRecorded: true",
    "HumanProductionApprovalDecision: Go",
    "ProductionApprovalExecuted: true",
    "ProductionExecutionAuthorized: true",
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
    "SbomScannerResidualRiskAccepted: true"
)) {
    if ($docs -match "(?m)^$([regex]::Escape($bad))$") { throw "Forbidden P44C marker detected: $bad" }
}
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P44C content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P44C final human production approval guardrails passed."
