# CRM Controlled Implementation Validation Runbook

Steps:

1. Confirm main contains P24 merge commit.
2. Run P24 controlled implementation wrapper.
3. Run P25 guardrail.
4. Run P25 verifier.
5. Render compose configuration.
6. Run global guardrails, foundation verification, build and tests.
7. Confirm NoGo remains in place.

Stop conditions:

- Any activation executed marker becomes true.
- Any Portal runtime call or coupling is enabled.
- Any feature flag is true.
- Any private endpoint, secret, token, certificate or real data appears.
- Any Common DB runtime or Portal direct database access appears.

Marker: FirstSliceNonProductionActivationControlledImplementationValidationRunbookPrepared: true.
