# CRM NonProduction Activation Scaffold Validation Runbook

Purpose: validate the existing disabled scaffold before the final approval gate.

Steps:

1. Confirm GitHub main is the base.
2. Run the P21 scaffold wrapper.
3. Run the P22 guardrail script.
4. Run the P22 verifier script.
5. Render compose configuration.
6. Run solution build and tests.
7. Confirm the decision remains NoGo.

Stop conditions:

- Any feature flag is true.
- Any Portal runtime call is introduced.
- Any private endpoint, secret, token or real data is found.
- Any CRM-owned SQL Server or direct Portal database access is added.

Markers:

- FirstSliceNonProductionActivationScaffoldValidationRunbookPrepared: true.
- NonProductionActivationExecuted: false.
- NextGate: CrmSprint10P23ControlledRuntimePilotFirstSliceNonProductionActivationFinalApprovalGate.
