# P43 P44 Approval Record and Entry Conditions

HumanProductionApprovalRequired: true
HumanProductionApprovalRecorded: false

ApprovalRecordId, Repository, Environment, TargetCommit, TargetRelease, TargetImage, ExecutionScope, ExecutionScopeHash, DeploymentStrategy, RunbookVersion, RollbackVersion, MonitoringPlanVersion, TestPlanVersion, RiskAcceptance, HumanApproverReference, ApprovalTimestamp, Decision, Reason and Expiration must be recorded in P44.

Approval expires on commit, image, release, configuration, scope, runbook, rollback, monitoring, vulnerability, blocker, infrastructure or environment drift.

P44 requires P43 merged, ProductionReadinessRemediationDecision ReadyForApprovalGate, zero critical/high blockers, frozen scope, prepared target, and all approval readiness booleans true.
