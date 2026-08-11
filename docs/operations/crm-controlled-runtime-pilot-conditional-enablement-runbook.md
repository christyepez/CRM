# CRM Controlled Runtime Pilot Conditional Enablement Runbook

## Current runbook

P10 is design-only. Operators must not enable runtime flags or provide real endpoints.

## Future operator checklist

1. Confirm approval ticket and signed Go decision exist.
2. Confirm the future implementation branch is based on current main.
3. Confirm safe configuration is supplied outside the repository.
4. Run guardrail and verifier scripts.
5. Execute future smoke only after explicit approval.
6. Roll back by disabling all pilot flags.

## Markers

- ConditionalEnablementRunbookPrepared: true.
- ConditionalFutureGoDefined: true.
- ConditionalFutureGoExecuted: false.
- RuntimePortalCallsEnabled: false.
