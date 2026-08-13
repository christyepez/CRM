# CRM Controlled Runtime Pilot First Slice Activation Approval Gate Risk Register

## Residual risks

| Risk | Control | Status |
| --- | --- | --- |
| Accidental Portal runtime call | Disabled client remains default | Controlled |
| Feature flag enabled too early | Approval gate requires explicit future GO | Controlled |
| Private URL committed | Placeholder-only configuration policy | Controlled |
| Secret leakage | Logical secret names only | Controlled |
| Cross-domain persistence | No shared DB, table or migration | Controlled |

## Markers

- FirstSliceActivationApprovalGateBlockersPrepared: true.
- FirstSliceActivationApprovalGatePrepared: true.
- ProductionActivationDecision: NoGo.
