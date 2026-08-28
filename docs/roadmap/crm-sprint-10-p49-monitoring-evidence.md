# CRM Sprint 10 P49 - Monitoring Evidence

MonitoringEvidenceExists: true
ProductionMonitoringReadyForRetry: true

ProductionLogSource: docker logs crm-api-prod-sim
ProductionMetricSource: docker stats / docker inspect local simulation
ProductionAvailabilitySource: HTTP health/readiness probes
ProductionRestartSignalSource: Docker RestartCount
ProductionErrorRateSource: HTTP responses + container/application logs
ProductionLatencySource: controlled HTTP probe timing

StabilitySamples: 5
HealthSuccessCount: 5
HealthFailureCount: 0
ReadinessSuccessCount: 1
RestartCountBefore: 0
RestartCountAfter: 0

LatencySamplesMs: 6.87,4.34,5.19,3.58,3.67
LatencyMinMs: 3.58
LatencyAverageMs: 4.73
LatencyP95Ms: 6.87

CriticalLogErrorsDetected: false
StartupFailuresDetected: false
RepeatedCrashesDetected: false
UnexpectedDependencyAttemptsDetected: false
CommonDbAccessDetected: false
PortalCallsDetected: false
SqlServerConnectionAttemptsDetected: false
