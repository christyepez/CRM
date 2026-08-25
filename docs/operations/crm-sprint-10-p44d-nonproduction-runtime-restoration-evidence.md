# CRM Sprint 10 P44D - NonProduction Runtime Restoration Evidence

Environment: NonProduction
ProductionEnvironmentDetected: false

RuntimePortalCallsEnabled: false
PortalRoutesActivated: false
PortalNavigationActivated: false
CommonDbRuntimeEnabled: false

DockerComposeConfigValid: true
ComposeServices: crm-api only
ExpectedPort: 8093:8080
ProductionSecretsDetected: false
ProductionEndpointsDetected: false
PortalServicesDetected: false
CommonDbServicesDetected: false

ContainerStatus: running
ContainerId: 90a6ced8ef0be59a08d76075fd09dae656534f959164291c3865e74226d131cd
ContainerImage: crm-crm-api
ContainerImageId: sha256:7f188db4186cfae88d52611ebb3a99048e99636c51252657651c4b3368faf238
RestartCount: 0
StartedAt: 2026-08-25T20:16:35.870280032Z
PortMapping: 0.0.0.0:8093->8080/tcp
HealthStatus: available-through-endpoints

HealthPassed: true
LivenessPassed: true
ReadinessPassed: true
SmokeRegressionPassed: true
CriticalRuntimeErrors: 0
UnhandledExceptions: 0
CpuObserved: 0.01%
MemoryObserved: 32.84MiB / 7.461GiB

NonProductionRuntimeStable: true
