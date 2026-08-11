# CRM Controlled Runtime Pilot Validation Runbook

## Procedure

1. Sync CRM `main` from GitHub.
2. Verify the required base commit.
3. Run P2, P3, P4 and P5 guardrails and verifiers.
4. Run P6 guardrail and verifier.
5. Run the aggregate P6 validation script.
6. Run build, tests and compose config.
7. Open a pull request to `main`.
8. Do not merge automatically.

## Markers

- ControlledRuntimePilotValidationRunbookPrepared: true.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- CrmProductionReady: false.
