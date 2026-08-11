# CRM Controlled Runtime Pilot Scaffold Runbook

## How to use

1. Sync `main` from GitHub.
2. Create a new branch from the verified base commit.
3. Run the inherited verification tools.
4. Run the P5 guardrail, verifier, preflight and smoke scripts.
5. Open a pull request to `main`.
6. Do not merge automatically.

## Rollback

Revert the documentation and tools change. No runtime rollback is required because P5 does not activate runtime behavior.

## Markers

- ControlledRuntimePilotRunbookPrepared: true.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- CrmProductionReady: false.
