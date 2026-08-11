# CRM Controlled Runtime Pilot Enablement Approval Gate Runbook

## Procedure

1. Sync CRM `main` from GitHub.
2. Verify the required base commit.
3. Run P8 dry run validation.
4. Run P9 guardrail, verifier and approval gate script.
5. Run build, tests and compose config.
6. Open a pull request to `main`.
7. Do not merge automatically.

## Markers

- ControlledRuntimePilotApprovalGateRunbookPrepared: true.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- CrmProductionReady: false.
- ApprovalGateOnly: true.
