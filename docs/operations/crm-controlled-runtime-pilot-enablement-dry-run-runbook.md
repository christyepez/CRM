# CRM Controlled Runtime Pilot Enablement Dry Run Runbook

## Procedure

1. Sync CRM `main` from GitHub.
2. Verify the required base commit.
3. Run P7 enablement readiness.
4. Run P8 guardrail, verifier and dry run script.
5. Run build, tests and compose config.
6. Open a pull request to `main`.
7. Do not merge automatically.

## Markers

- ControlledRuntimePilotEnablementDryRunRunbookPrepared: true.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- CrmProductionReady: false.
- DryRunOnly: true.
