$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docker-compose.prod-sim.yml",
    ".env.prod-sim.example",
    "docs/operations/crm-sprint-10-ops04-local-simulated-production.md",
    "docs/architecture/crm-sprint-10-ops04-local-simulated-production-architecture.md",
    "docs/operations/crm-sprint-10-ops04-predeployment-baseline.md",
    "docs/operations/crm-sprint-10-ops04-production-target-evidence.md",
    "docs/operations/crm-sprint-10-ops04-rollback-evidence.md",
    "docs/operations/crm-sprint-10-ops04-monitoring-evidence.md",
    "docs/security/crm-sprint-10-ops04-security-evidence.md",
    "docs/operations/crm-sprint-10-ops04-nonprod-isolation-evidence.md",
    "docs/operations/crm-sprint-10-ops04-operations-input-package.md",
    "docs/roadmap/crm-sprint-10-ops04-risk-register.md"
)

foreach ($path in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Missing required OPS-04 file: $path" }
}

$task = Get-Content -Raw -LiteralPath "docs/operations/crm-sprint-10-ops04-local-simulated-production.md"
$requiredMarkers = @(
    "OPS04LocalSimulatedProductionExists: true",
    "ApprovalReference: explicit-user-chat-approval-local-simulated-production",
    "OPS04BaseMainCommit: 777c444a075c3d2a8d19dff99df6dd40bbab5929",
    "ArchitectureApprovalDecision: Approved",
    "ProductionArchitectureDecision: ApprovedForLocalSimulatedProduction",
    "SelectedDeploymentPlatform: LocalDockerCompose",
    "EnvironmentClassification: SimulatedProduction",
    "RealProduction: false",
    "SimulatedProduction: true",
    "DockerComposeProjectName: crm-prod-sim",
    "ContainerName: crm-api-prod-sim",
    "NetworkName: crm-prod-sim-net",
    "ProductionBaseUrl: http://127.0.0.1:8094",
    "ProductionServicePort: 8080",
    "ProductionPublishedPort: 8094",
    "CandidateImageIdentityMatched: true",
    "CurrentProductionServicePresentBefore: false",
    "CurrentProductionServicePresentAfter: true",
    "ProductionDeploymentState: FirstDeployment",
    "RollbackBaselineType: NoPreviousDeployment",
    "RollbackTarget: PreDeploymentNoCRMState",
    "RollbackMechanismDefined: true",
    "RollbackMechanismDeterministic: true",
    "RollbackReadyForRetry: true",
    "RollbackTestExecuted: true",
    "RollbackTestResult: Passed",
    "RedeployIdentityMatched: true",
    "ProductionMonitoringTargetResolved: true",
    "ProductionMonitoringReadyForRetry: true",
    "DockerHealth: healthy",
    "ContainerUser: 65532:65532",
    "NonProdUnaffected: true",
    "PortalIncluded: false",
    "CommonDbIncluded: false",
    "ProductionDataChangesApproved: false",
    "CriticalInfrastructureBlockers: 0",
    "OperationsInputsResolved: 12",
    "OperationsInputsMissing: 0",
    "OperationsEvidenceReadyForP47W: true",
    "P47WAllowedNow: true",
    "P48AllowedNow: false",
    "OPS04Decision: ProvisionedAndValidated"
)

foreach ($marker in $requiredMarkers) {
    if (-not $task.Contains($marker)) { throw "Missing required OPS-04 marker: $marker" }
}

$compose = Get-Content -Raw -LiteralPath "docker-compose.prod-sim.yml"
if ($compose.Contains("build:")) { throw "OPS-04 compose must not contain build." }
if ($compose -match "sqlserver|mssql|postgres|mysql") { throw "OPS-04 compose must not define a database service." }
if ($compose -match "0\.0\.0\.0:8094") { throw "OPS-04 compose must bind 8094 to 127.0.0.1." }

Write-Host "PASS CRM OPS-04 guardrails passed."
