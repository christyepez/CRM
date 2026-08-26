# CRM Sprint 10 P44F - NonProduction Revalidation

NonProductionRuntimeStable: true
DockerComposeConfigValid: true
DockerComposePsRunning: true

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
