# CRM Explicit Approval Runbook

1. Confirm main contains P25 merge commit.
2. Review P25 validation evidence.
3. Run P26 guardrail and verifier scripts.
4. Confirm approval is prepared, not executed.
5. Keep runtime flags disabled.
6. If any guardrail fails, keep ProductionActivationDecision: NoGo and stop.

- FirstSliceNonProductionActivationExplicitApprovalRunbookPrepared: true
- ExplicitApprovalExecuted: false
- NonProductionActivationExecuted: false
