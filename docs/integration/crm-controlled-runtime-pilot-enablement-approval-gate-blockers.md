# CRM Controlled Runtime Pilot Enablement Approval Gate Blockers

## Blocking conditions for future Go

- Any production activation request.
- Any real Portal endpoint, credential, token, certificate or provider value.
- Any CRM-owned duplication of Portal Auth, Menu, Permissions, Audit, Notification or Configuration.
- Any CRM service added to Portal compose without a separate approved gate.
- Any Common DB runtime activation without approval.
- Any shared Portal table access, cross-domain migration or direct Portal DB access.

## Markers

- ControlledRuntimePilotApprovalGateBlockersPrepared: true.
- ConditionalFutureGoExecuted: false.
- ProductionActivationDecision: NoGo.
