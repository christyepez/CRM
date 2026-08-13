# CRM NonProduction Activation Final Approval Gate Runbook

Steps:

1. Confirm main contains the P22 merge commit.
2. Run P22 validation wrapper.
3. Run P23 guardrail.
4. Run P23 verifier.
5. Render compose configuration.
6. Run CRM guardrails, foundation verifier, build and tests.
7. Confirm decision remains NoGo now and ConditionalGoFuture only.

Stop conditions:

- Any activation executed marker changes to true.
- Any feature flag is changed to true.
- Any Portal call, route, navigation or service registration is introduced.
- Any Common DB runtime or direct Portal DB access is introduced.
- Any secret, token, certificate, private endpoint or real data appears.

Marker: FirstSliceNonProductionActivationFinalApprovalGateRunbookPrepared: true.
