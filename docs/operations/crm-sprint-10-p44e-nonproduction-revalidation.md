# CRM Sprint 10 P44E - NonProduction Revalidation

Environment: NonProduction
NonProductionRuntimeStable: true
ContainerRunning: true
ContainerStatus: running
RestartCount: 0

HealthPassed: true
LivenessPassed: true
ReadinessPassed: true
SmokeRegressionPassed: true
CriticalRuntimeErrors: 0

Health: /health 200
Liveness: /health/live 200
Readiness: /health/ready 200 and /api/crm/readiness 200
LockedProductiveRouteBoundary: /api/crm/productive-route-dry-run 404
InvalidRouteBoundary: /api/crm/does-not-exist 404

DockerComposeConfigValid: true
DockerComposePsRunning: true
