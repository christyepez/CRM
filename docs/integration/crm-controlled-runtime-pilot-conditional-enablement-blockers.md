# CRM Controlled Runtime Pilot Conditional Enablement Blockers

## Activation blockers

- Missing explicit future Go approval.
- Any real secret, token, certificate, private endpoint or real data in repository content.
- Any productive Portal route or navigation enabled.
- Any Portal service added to CRM compose.
- Any Common DB runtime activation, shared table, cross-domain migration or direct Portal DB access.
- Any duplicated Portal Auth, Menu, Permissions, Audit, Notification or Configuration capability.

## Markers

- ConditionalEnablementBlockersPrepared: true.
- ConditionalFutureGoExecuted: false.
- ProductionActivationDecision: NoGo.
