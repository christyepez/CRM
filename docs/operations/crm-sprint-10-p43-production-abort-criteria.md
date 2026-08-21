# P43 Production Abort Criteria

HealthFailure, ReadinessFailure, HighErrorRate, LatencyExceeded, AuthenticationFailure, AuthorizationRegression, UnexpectedDependency, ConfigurationMismatch, UnexpectedDataChange, MonitoringLoss and RollbackUnavailable require documented threshold, action, owner role, rollback decision and evidence during P45.

Default action: abort execution and rollback when health/readiness/security/configuration/rollback criteria fail.
