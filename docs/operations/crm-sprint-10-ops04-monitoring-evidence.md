# CRM OPS-04 Monitoring Evidence

EnvironmentClassification: SimulatedProduction
RealProduction: false
LocalSimulation: true

ProductionMonitoringTargetResolved: true
ProductionMonitoringReadyForRetry: true

ProductionLogSource: docker logs crm-api-prod-sim
ProductionMetricSource: docker stats / docker inspect local simulation
ProductionAvailabilitySource: HTTP health/readiness probes
ProductionRestartSignalSource: Docker RestartCount
ProductionErrorRateSource: application/container logs + HTTP validation
ProductionLatencySource: controlled local HTTP probe timing

DockerHealthcheckConfigured: true
DockerHealth: healthy
RestartCount: 0

HealthEndpoint: http://127.0.0.1:8094/health
LivenessEndpoint: http://127.0.0.1:8094/health/live
ReadinessEndpoint: http://127.0.0.1:8094/health/ready
CRMReadinessEndpoint: http://127.0.0.1:8094/api/crm/readiness
