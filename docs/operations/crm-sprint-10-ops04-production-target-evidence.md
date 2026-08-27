# CRM OPS-04 Simulated Production Target Evidence

EnvironmentClassification: SimulatedProduction
RealProduction: false
LocalSimulation: true

DeploymentPlatform: DockerCompose
TargetHostIdentifier: localhost-local-docker-engine
TargetHostType: LocalDevelopmentMachine
TargetOperatingSystem: Microsoft Windows NT 10.0.26200.0
TargetRuntimeIdentifier: desktop-linux/DockerEngine/29.4.2
DockerComposeProjectName: crm-prod-sim
ContainerName: crm-api-prod-sim
NetworkName: crm-prod-sim-net

ProductionBaseUrl: http://127.0.0.1:8094
ProductionServicePort: 8080
ProductionPublishedPort: 8094
LocalhostUseAuthorizedForSimulatedProduction: true
RealProductionBaseUrl: NotApplicableForSimulation

CurrentProductionServicePresentAfter: true
ProductionDeploymentState: FirstDeployment
