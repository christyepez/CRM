$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p44b-production-approval-preconditions-remediation-and-immutable-target-freeze.md",
    "docs/operations/crm-sprint-10-p44b-nonproduction-runtime-restoration-evidence.md",
    "docs/architecture/crm-sprint-10-p44b-runtime-code-drift-validation.md",
    "docs/operations/crm-sprint-10-p44b-production-artifact-manifest.md",
    "docs/operations/crm-sprint-10-p44b-production-candidate-image-evidence.md",
    "docs/security/crm-sprint-10-p44b-candidate-image-security-evidence.md",
    "docs/roadmap/crm-sprint-10-p44b-production-target-freeze-v2.md",
    "docs/roadmap/crm-sprint-10-p44b-production-scope-freeze-v2.md",
    "docs/operations/crm-sprint-10-p44b-monitoring-revalidation.md",
    "docs/operations/crm-sprint-10-p44b-rollback-revalidation.md",
    "docs/roadmap/crm-sprint-10-p44b-technical-preconditions-decision.md",
    "docs/roadmap/crm-sprint-10-p44b-final-production-approval-packet.md",
    "docs/roadmap/crm-sprint-10-p44b-p44c-entry-conditions.md",
    "docs/roadmap/crm-sprint-10-p44b-risk-register.md",
    "tools/check-crm-sprint-10-p44b-production-approval-preconditions-guardrails.ps1",
    "tools/verify-crm-sprint-10-p44b-production-approval-preconditions.ps1",
    "tools/crm-sprint-10-p44b-production-approval-preconditions.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P44B file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P44BProductionApprovalPreconditionsRemediationAndImmutableTargetFreezeExists: true",
    "P44BPreconditionsRemediationOnly: true",
    "P44AMergeCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c",
    "P44BBaseMainCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c",
    "P44HistoricalStatePreserved: true",
    "P44AHistoricalStatePreserved: true",
    "P44HistoricalApprovalDecision: NoGo",
    "P44AHistoricalApprovalDecision: NoGo",
    "NonProductionRuntimeBefore: NotRunning",
    "NonProductionRuntimeAfter: Running",
    "NonProductionRuntimeStable: true",
    "RuntimeCodeChangedSinceP43: false",
    "RuntimeConfigurationChangedSinceP43: false",
    "DockerBuildInputsChangedSinceP43: false",
    "ProductionRuntimeTargetCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c",
    "Production Candidate Image: crm-api:prod-candidate-8623c619",
    "ProductionTargetImageDecision: ImmutableLocallyOnly",
    "ProductionArtifactPublished: false",
    "RegistryDigestAvailable: false",
    "ProductionScopeFrozen: true",
    "ProductionTargetFrozen: true",
    "PortalIncludedInProductionExecution: false",
    "CommonDbIncludedInProductionExecution: false",
    "ProductionDataChangesApproved: false",
    "ApprovedProductionExternalDependencies: none",
    "ProductionMonitoringReadyForApproval: true",
    "RollbackReadyForApproval: true",
    "CriticalProductionBlockers: 0",
    "HighBlockingRisks: 0",
    "P44BTechnicalPreconditionsDecision: ReadyForFinalHumanApprovalWithConditions",
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
    "P44CReady: false"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P44B marker: $marker" } }
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
    "CrmProductionReady: true"
)) { if ($docs -like "*$bad*") { throw "Forbidden P44B marker detected: $bad" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P44B content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P44B production approval preconditions guardrails passed."
