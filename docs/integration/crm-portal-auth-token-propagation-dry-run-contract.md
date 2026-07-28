# CRM Portal Auth Token Propagation Dry-Run Contract

Endpoint:

- `GET /api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run`

Response contract:

- `status = PortalAuthTokenPropagationDryRunContract`
- `foundationMode = true`
- `portalAuthTokenPropagationDryRunContractExists = true`
- `portalAuthDryRunApprovalGranted = false`
- `portalAuthDryRunEnabled = false`
- `portalAuthRuntimeConnected = false`
- `tokenReadAttempted = false`
- `headerReadAttempted = false`
- `portalHttpAttempted = false`
- `usesSyntheticTokenMetadata = true`
- `syntheticTokenReference = mock://crm/portal-auth-token`
- `syntheticUserReference = mock://crm/portal-user`
- `realTokenUsed = false`
- `realHeadersRead = false`
- `loginImplementedByCrm = false`
- `identityImplementedByCrm = false`
- `permissionsPersistedInCrm = false`
- `productiveAuthorizationEnabled = false`
- `nonProductionOnly = true`
- `rollbackRequired = true`
- `observabilityRequired = true`
- `nextGate = Sprint6P5LockedStubRuntimeRegistrationTrial`
- `warning = Portal Auth token propagation dry-run contract only; no real tokens or headers are read`

This contract must remain read-only and must not mutate state.
