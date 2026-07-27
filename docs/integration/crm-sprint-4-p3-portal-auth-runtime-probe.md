# CRM Sprint 4 P3 - Portal Auth Runtime Probe

Status: `PortalAuthRuntimeProbe`.

The Portal Auth runtime probe exists but is disabled by default. CRM does not implement login, logout, Identity, SSO, user, tenant or permission ownership. PortalCorporativo remains the owner of Auth/SSO/user/tenant/permissions.

Default decisions:

- `portalAuthRuntimeProbeExists=true`.
- `portalAuthRuntimeProbeEnabled=false`.
- `portalRuntimeConnected=false`.
- `authRuntimeEnabled=false`.
- `productiveAuthorizationEnabled=false`.
- `tokenReadAttemptedByRuntime=false`.
- `portalHttpAttemptedByRuntime=false`.
- `loginImplementedByCrm=false`.
- `identityImplementedByCrm=false`.
- `permissionsPersistedInCrm=false`.
- `foundationSimulationActive=true`.
- `nextGate=Sprint4P4ProductiveRoutesLockedStubValidation`.

Warning: `Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted`.
