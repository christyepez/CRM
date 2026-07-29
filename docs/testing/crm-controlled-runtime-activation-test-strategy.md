# CRM Controlled Runtime Activation Test Strategy

Sprint 9 P1 tests verify the decision contract and guardrails.

Required assertions:
- ControlledRuntimeActivationDecision is ApprovedForNonProductionTrialsOnly.
- ProductionActivationDecision is NoGo.
- RuntimeTrialsEnabledNow is false.
- Secret Provider, Common DB, Portal Auth and Productive Route trials are approved for future NonProduction planning only.
- No DELETE, DB runtime, EF runtime, Auth runtime, Portal HTTP, token/header reads or productive UI are introduced.
