# P44A Approval Expiration Rules

A future P44A approval is invalidated by:

- TargetCommitChanged
- TargetReleaseChanged
- TargetImageChanged
- ScopeChanged
- ScopeHashChanged
- ConfigurationChanged
- RunbookChanged
- RollbackChanged
- MonitoringChanged
- TestPlanChanged
- CriticalVulnerabilityDetected
- CriticalBlockerDetected
- HighBlockingRiskDetected
- EnvironmentDriftDetected
- InfrastructureDriftDetected
- NonProductionRuntimeRegressionDetected

ProductionApprovalDriftDetected: false
ProductionApprovalValidUntilDrift: true
