# P39A P40 Entry Conditions

P40EntryConditionsPrepared: true
P40Authorized: true

P40 may only start if P39A is merged, NonProductionExecutionDecision is Go, HumanApprovalRecorded is true, ExplicitApprovalExecuted is true, NonProductionActivationExecutionApprovalExecuted is true, NonProductionActivationReadinessApprovedForExecution is true, ApprovalDriftDetected is false, and CriticalBlockers equals 0.

Current result: P40 is authorized for controlled NonProduction execution only after P39A is merged and no drift or critical blocker appears.
