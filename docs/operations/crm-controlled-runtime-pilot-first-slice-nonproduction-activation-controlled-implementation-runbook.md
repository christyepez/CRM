# CRM Controlled NonProduction Activation Implementation Runbook

Steps:

1. Confirm main contains P23 merge commit.
2. Run P23 final approval gate wrapper.
3. Run P24 guardrail.
4. Run P24 verifier.
5. Render compose configuration.
6. Run CRM guardrails, foundation verifier, build and tests.
7. Confirm activation remains unexecuted.

Stop conditions:

- Any activation executed marker changes to true.
- Any Portal runtime call or coupling is enabled.
- Any private endpoint, secret, token or real data appears.
- Any Common DB runtime or Portal direct database access appears.

Marker: FirstSliceNonProductionActivationControlledImplementationRunbookPrepared: true.
