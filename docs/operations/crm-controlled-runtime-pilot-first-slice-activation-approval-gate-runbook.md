# CRM Controlled Runtime Pilot First Slice Activation Approval Gate Runbook

## Steps

1. Confirm P17 dry run evidence.
2. Confirm all required approvers are documented.
3. Run P18 guardrail script.
4. Run P18 verifier script.
5. Run build, tests and compose config validation.
6. Publish PR for review.
7. Do not merge without human approval.

## Marker

- FirstSliceActivationApprovalGateRunbookPrepared: true.
