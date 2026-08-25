# CRM Sprint 10 P44D - Approval Drift Root Cause

| DriftId | Source | Expected | Actual | Material | Resolved | Evidence |
| --- | --- | --- | --- | --- | --- | --- |
| D1 NonProductionStability | P44C revalidation | NonProductionRuntimeStable: true | NonProductionRuntimeStable: false | true | true | P44D restarted scoped crm-api and revalidated running state, restart count 0, health/readiness 200 and smoke blocked routes 404. |
| D2 HumanApproval | P44C approval gate | HumanProductionApprovalRecorded: true | false | true | false | P44D is not allowed to request or record human approval. |
| D3 ResidualRiskAcceptance | P44C approval gate | residual risks accepted by human | not accepted | true | false | P44D preserves risk acceptance as human-only. |

ProductionApprovalDriftDetected: false
ApprovalDriftRootCause: P44C NonProduction stability drift plus absent human approval and unaccepted residual risks.
ApprovalDriftResolved: true

RuntimeTargetCommitDrift: false
ImageIdDrift: false
ScopeDrift: false
ScopeHashDrift: false
ConfigurationDrift: false
RunbookDrift: false
RollbackDrift: false
MonitoringDrift: false
SecurityConditionDrift: false
NonProductionStabilityDrift: false
