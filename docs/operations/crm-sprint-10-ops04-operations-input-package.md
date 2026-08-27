# CRM OPS-04 Operations Input Package for P47W

OPERATIONS_INPUTS:

DeploymentPlatform: DockerCompose

TargetHostIdentifier: localhost-local-docker-engine

TargetHostType: LocalDevelopmentMachine

TargetOperatingSystem: Microsoft Windows NT 10.0.26200.0

TargetRuntimeIdentifier: desktop-linux/DockerEngine/29.4.2

DeploymentMechanism: LocalDockerComposeControlled

DeploymentEntryPoint: docker compose -p crm-prod-sim --env-file .env.prod-sim.example -f docker-compose.prod-sim.yml up -d

ProductionConfigurationSource: LocalDockerComposeEnvironment

ProductionConfigurationReference: .env.prod-sim.example

ProductionSecretSourceReference: NoExternalProductionSecretsRequiredForCurrentSlice

ProductionNetworkBoundary: LocalDockerDedicatedNetwork

ProductionProtocol: HTTP

ProductionBaseUrl: http://127.0.0.1:8094

ProductionDnsName: 127.0.0.1

ProductionServicePort: 8080

ProductionPublishedPort: 8094

ProductionMonitoringSources:

LogSource: docker logs crm-api-prod-sim

MetricSource: docker stats / docker inspect local simulation

AvailabilitySource: HTTP health/readiness probes

ErrorRateSource: application/container logs + HTTP validation

LatencySource: controlled local HTTP probe timing

RestartSignalSource: Docker RestartCount

CurrentProductionServicePresent: true

FirstDeploymentConfirmed: true

RollbackBaselineType: NoPreviousDeployment

RollbackTarget: PreDeploymentNoCRMState

PreDeploymentCRMServicePresent: false

PreDeploymentCRMRoutePresent: false

PreDeploymentCRMPortBindingPresent: false

ProductionOperationsOwner: ExplicitUserOwner

ProductionDeploymentOwner: ExplicitUserOwner

ProductionMonitoringOwner: ExplicitUserOwner

RollbackDecisionOwner: ExplicitUserOwner

InfrastructureApprovalOwner: ExplicitUserOwner

SecurityApprovalOwner: ExplicitUserOwner

ApplicationOwner: ExplicitUserOwner
