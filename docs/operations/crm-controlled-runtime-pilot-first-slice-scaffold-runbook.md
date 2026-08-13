# CRM Controlled Runtime Pilot First Slice Scaffold Runbook

## Validate locally

1. Run `tools/check-crm-controlled-runtime-pilot-first-slice-scaffold-guardrails.ps1`.
2. Run `tools/verify-crm-controlled-runtime-pilot-first-slice-scaffold.ps1`.
3. Run `tools/crm-controlled-runtime-pilot-first-slice-scaffold.ps1`.
4. Run build, tests and Docker compose config.

## Operating rule

Do not enable any future flag until a later PR explicitly approves the next gate.

## Markers

- FirstSliceScaffoldRunbookPrepared: true.
- ConditionalFutureGoExecuted: false.
