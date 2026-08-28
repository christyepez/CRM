# CRM Sprint 10 P50 - Monitoring Postcheck

MonitoringPostcheckExists: true

MonitoringStillAvailable: true
MonitoringSufficientForPilotClosure: true

Signals:

- docker logs crm-api-prod-sim
- docker inspect crm-api-prod-sim
- docker stats / docker inspect local simulation
- HTTP health probe
- HTTP readiness probe
- Docker RestartCount
- controlled local HTTP probe timing

CriticalLogErrorsDetected: false
WarningPatternsDetected: none
UnexpectedDependencyAttemptsDetected: false
PortalCallsDetected: false
CommonDbAccessDetected: false
SqlServerConnectionAttemptsDetected: false
