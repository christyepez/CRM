# CRM Controlled Runtime Pilot Enablement Runbook

## Procedure

1. Sync CRM `main` from GitHub.
2. Verify the required base commit.
3. Run P2 through P6 checks.
4. Run P7 guardrail, verifier and readiness script.
5. Run build, tests and compose config.
6. Open a pull request to `main`.
7. Do not merge automatically.

## Markers

- ControlledRuntimePilotEnablementRunbookPrepared: true.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- CrmProductionReady: false.
