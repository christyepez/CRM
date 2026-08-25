# CRM Sprint 10 P44D - Monitoring Revalidation

ProductionMonitoringReady: true

HealthSignalsAvailable: true
ReadinessSignalsAvailable: true
LogsAvailable: true
ResourceObservationAvailable: true
ErrorDetectionAvailable: true
RestartDetectionAvailable: true
DeploymentObservationAvailable: true

Health: /health 200
Liveness: /health/live 200
Readiness: /health/ready 200 and /api/crm/readiness 200
LockedRoutes: /api/crm/productive-route-dry-run 404
InvalidRoutes: 404

MonitoringReadyForP44E: true
