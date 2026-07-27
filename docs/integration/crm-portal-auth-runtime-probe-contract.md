# CRM Portal Auth Runtime Probe Contract

Endpoint:

- `GET /api/crm/foundation/sprint-4/portal-auth-runtime-probe`

The response is metadata only and must include:

- `foundationMode=true`.
- `status=PortalAuthRuntimeProbe`.
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

The endpoint must not read credentials, headers, secrets, files, database state or Portal runtime state.
