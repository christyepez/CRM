# CRM Locked Stub Runtime Registration Trial Contract

Endpoint:

- `GET /api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial`

Response contract:

- `status = LockedStubRuntimeRegistrationTrial`
- `foundationMode = true`
- `lockedStubRuntimeRegistrationTrialExists = true`
- `lockedStubRuntimeRegistrationApprovalGranted = false`
- `lockedStubRuntimeRegistrationEnabled = false`
- `lockedStubsRegisteredAtRuntime = false`
- `productiveRoutesRegistered = false`
- `productiveCrudEnabled = false`
- `deleteEndpointsEnabled = false`
- `defaultNegativeRouteStatus = 404`
- `futureLockedResponseStatusIfExplicitlyEnabled = 423`
- `runtimeFlagDefaultEnabled = false`
- `usesDomainServices = false`
- `usesFoundationStores = false`
- `usesDatabase = false`
- `usesPortalAuth = false`
- `usesTokenOrHeaderReads = false`
- `nonProductionOnly = true`
- `rollbackRequired = true`
- `observabilityRequired = true`
- `nextGate = Sprint6P6Sprint6GateDecision`
- `warning = Locked stub runtime registration trial only; no productive routes are registered by default`

The endpoint must not register routes or modify runtime state.
