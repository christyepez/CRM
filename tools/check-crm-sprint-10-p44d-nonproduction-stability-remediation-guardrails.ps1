$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p44d-nonproduction-stability-remediation-and-final-approval-revalidation-preparation.md",
    "docs/operations/crm-sprint-10-p44d-nonproduction-root-cause-analysis.md",
    "docs/operations/crm-sprint-10-p44d-nonproduction-runtime-restoration-evidence.md",
    "docs/operations/crm-sprint-10-p44d-candidate-image-identity-revalidation.md",
    "docs/architecture/crm-sprint-10-p44d-runtime-drift-validation.md",
    "docs/roadmap/crm-sprint-10-p44d-approval-drift-root-cause.md",
    "docs/operations/crm-sprint-10-p44d-rollback-revalidation.md",
    "docs/operations/crm-sprint-10-p44d-monitoring-revalidation.md",
    "docs/security/crm-sprint-10-p44d-security-compensating-controls.md",
    "docs/roadmap/crm-sprint-10-p44d-final-production-approval-packet-v2.md",
    "docs/roadmap/crm-sprint-10-p44d-decision.md",
    "docs/roadmap/crm-sprint-10-p44d-p44e-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p44d-risk-register.md",
    "tools/check-crm-sprint-10-p44d-nonproduction-stability-remediation-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44d-nonproduction-stability-remediation.ps1",
    "tools/crm-sprint-10-p44d-nonproduction-stability-remediation.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44D file: $path" }
}
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P44DNonProductionStabilityRemediationExists: true",
    "P44DRemediationAndRevalidationOnly: true",
    "P44CPullRequest: #120",
    "P44CMergeCommit: f3242c910b242f8311b9aa97bfe950aa1efb9dd5",
    "P44DBaseMainCommit: f3242c910b242f8311b9aa97bfe950aa1efb9dd5",
    "P44HistoricalDecision: NoGo",
    "P44AHistoricalDecision: NoGo",
    "P44BHistoricalDecision: ReadyForFinalHumanApprovalWithConditions",
    "P44CHistoricalDecision: NoGo",
    "HistoricalStatePreserved: true",
    "NonProductionRuntimeBefore: Exited",
    "NonProductionExitRootCause: ContainerRuntimeFailure",
    "NonProductionRuntimeAfter: Running",
    "NonProductionRuntimeStable: true",
    "CandidateImageIdentityMatched: true",
    "RuntimeSourceDriftDetected: false",
    "DockerBuildInputDriftDetected: false",
    "RuntimeConfigurationDriftDetected: false",
    "DependencyDriftDetected: false",
    "ProductionApprovalDriftDetected: false",
    "RollbackMechanismAvailable: true",
    "RollbackTargetImmutable: true",
    "RollbackArtifactPresent: true",
    "SBOMAvailable: false",
    "OfficialImageScannerAvailable: false",
    "ProductionMonitoringReady: true",
    "ProductionExecutionScope: CRM API first slice only",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "ApprovedProductionExternalDependencies: none",
    "LocalOnlyArtifactAcceptedForP45: false",
    "LocalOnlyRollbackAccepted: false",
    "SbomScannerResidualRiskAccepted: false",
    "CriticalProductionBlockers: 0",
    "HighBlockingRisks: 0",
    "P44DDecision: ReadyForFinalApprovalRevalidationWithConditions",
    "HumanProductionApprovalRequired: true",
    "HumanProductionApprovalRecorded: false",
    "ProductionApprovalDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "ProductionExecutionAuthorized: false",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "ProductionExecutionStarted: false",
    "ProductionDeploymentExecuted: false",
    "P45Authorized: false"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P44D marker: $marker" }
}
foreach ($bad in @(
    "ProductionApprovalDecision: Go",
    "HumanProductionApprovalRecorded: true",
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
    if ($docs -match "(?m)^$([regex]::Escape($bad))$") { throw "Forbidden P44D marker detected: $bad" }
}
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P44D content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P44D nonproduction stability remediation guardrails passed."
