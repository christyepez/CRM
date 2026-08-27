# CRM Sprint 10 OPS-04 - Local Docker Compose Simulated Production Infrastructure

OPS04LocalSimulatedProductionExists: true
ApprovalReference: explicit-user-chat-approval-local-simulated-production
OPS04BaseMainCommit: 777c444a075c3d2a8d19dff99df6dd40bbab5929

ArchitectureApprovalDecision: Approved
ProductionArchitectureDecision: ApprovedForLocalSimulatedProduction
AzureContainerAppsArchitectureStatus: Deferred
SelectedDeploymentPlatform: LocalDockerCompose
EnvironmentClassification: SimulatedProduction
RealProduction: false
SimulatedProduction: true

DockerComposeProjectName: crm-prod-sim
ContainerName: crm-api-prod-sim
NetworkName: crm-prod-sim-net

TargetHostIdentifier: localhost-local-docker-engine
TargetHostType: LocalDevelopmentMachine
TargetOperatingSystem: Microsoft Windows NT 10.0.26200.0
TargetRuntimeIdentifier: desktop-linux/DockerEngine/29.4.2

DeploymentMechanism: LocalDockerComposeControlled
DeploymentEntryPoint: docker compose -p crm-prod-sim --env-file .env.prod-sim.example -f docker-compose.prod-sim.yml up -d
RollbackEntryPoint: docker compose -p crm-prod-sim --env-file .env.prod-sim.example -f docker-compose.prod-sim.yml down

ProductionConfigurationSource: LocalDockerComposeEnvironment
ProductionConfigurationReference: .env.prod-sim.example
ProductionSecretSourceReference: NoExternalProductionSecretsRequiredForCurrentSlice
SensitiveValuesCommitted: false

LocalhostUseAuthorizedForSimulatedProduction: true
RealProductionBaseUrl: NotApplicableForSimulation
ProductionBaseUrl: http://127.0.0.1:8094
ProductionServicePort: 8080
ProductionPublishedPort: 8094
ProductionNetworkBoundary: LocalDockerDedicatedNetwork
ProductionProtocol: HTTP

CandidateImageTag: crm-api:prod-candidate-8623c619
ExpectedCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
ActualCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
CandidateImageIdentityMatched: true

CurrentProductionServicePresentBefore: false
CurrentProductionServicePresentAfter: true
ProductionDeploymentState: FirstDeployment
FirstDeploymentConfirmed: true
PreDeploymentCRMServicePresent: false
PreDeploymentCRMPortBindingPresent: false
PreDeploymentCRMNetworkPresent: false

RollbackBaselineType: NoPreviousDeployment
RollbackTarget: PreDeploymentNoCRMState
RollbackMechanismDefined: true
RollbackMechanismDeterministic: true
RollbackTargetDeterministic: true
RollbackValidationDefined: true
RollbackMonitoringAvailable: true
RollbackReadyForRetry: true
RollbackTestExecuted: true
RollbackTestResult: Passed
RedeployIdentityMatched: true

ProductionMonitoringTargetResolved: true
ProductionMonitoringReadyForRetry: true
ProductionLogSource: docker logs crm-api-prod-sim
ProductionMetricSource: docker stats / docker inspect local simulation
ProductionAvailabilitySource: HTTP health/readiness probes
ProductionRestartSignalSource: Docker RestartCount
ProductionErrorRateSource: application/container logs + HTTP validation
ProductionLatencySource: controlled local HTTP probe timing

Health: HTTP 200
Liveness: HTTP 200
Readiness: HTTP 200
CRMReadiness: HTTP 200 ReadyForFoundationOnly
DockerHealthcheckConfigured: true
DockerHealth: healthy
ContainerUser: 65532:65532

NonProdUnaffected: true
SimulatedProdUsesDedicatedPort: true
SimulatedProdUsesDedicatedContainer: true
SimulatedProdUsesDedicatedComposeProject: true

PortalIncluded: false
CommonDbIncluded: false
ProductionDataChangesApproved: false
ApprovedProductionExternalDependencies: none
NoCrmOwnedSqlServer: true

OperationsInputsTotal: 12
OperationsInputsResolved: 12
OperationsInputsMissing: 0
OperationsEvidenceReadyForP47W: true
P47WAllowedNow: true
P48AllowedNow: false

CriticalInfrastructureBlockers: 0
OPS04Decision: ProvisionedAndValidated
NextStep: CRM Sprint 10 P47W - Freeze Local Simulated Production Target, Rollback Baseline, Monitoring Evidence and Final Approval Packet V5
