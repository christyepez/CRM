# P44B NonProduction Runtime Restoration Evidence

Environment: NonProduction
ProductionEnvironmentDetected: false

Before:

- docker compose ps: no running services.
- health/readiness: not reachable from P44A evidence.

After:

- Start command: `docker compose --env-file .env.example up -d crm-api`
- CrmApiContainerRunning: true
- ContainerId: 90a6ced8ef0be59a08d76075fd09dae656534f959164291c3865e74226d131cd
- Image: crm-crm-api
- Port: 8093->8080
- HealthStatus: Running
- RestartCount: 0

Health Validation:

- `/health`: 200
- `/health/live`: 200
- `/health/ready`: 200
- `/api/crm/readiness`: 200
- `/readiness`: 404 endpoint not available

SmokeRegressionPassed: true
RepeatedHealthFailures: false
CriticalErrors: false
CpuUsage: 0.01%
MemoryUsage: 32.76MiB

RuntimePortalCallsEnabled: false
RuntimeCouplingEnabled: false
PortalRoutesActivated: false
PortalNavigationActivated: false
PortalServicesInCompose: false
CommonDbRuntimeEnabled: false
UnexpectedOutboundDestinationDetected: false
UnexpectedDataChangesDetected: false

NonProductionRuntimeStable: true
