# CRM Controlled Runtime Pilot First Slice Activation Approval Gate Blockers

## Blocking conditions for future activation

- Missing required approver.
- Any feature flag changed to true without P19 approval.
- Any real Portal runtime URL or credential appears in repository content.
- Any CRM compose change adds Portal services or SQL Server ownership.
- Any shared table, cross-domain migration or direct Portal DB access appears.

## Markers

- FirstSliceActivationApprovalGateBlockersPrepared: true.
- ProductionActivationDecision: NoGo.
