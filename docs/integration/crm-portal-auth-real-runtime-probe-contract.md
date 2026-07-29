# CRM Portal Auth Real Runtime Probe Contract

Endpoint: `GET /api/crm/foundation/sprint-7/portal-auth-real-runtime-probe`

The response is sanitized metadata only. It must expose false for approval, runtime, token, header and HTTP activity markers, and true for synthetic fallback and skipped-probe markers.

Required status:

- `status`: `PortalAuthRealRuntimeProbe`
- `foundationMode`: true
- `portalAuthRealRuntimeProbeExists`: true
- `portalAuthRealRuntimeApprovalGranted`: false
- `portalAuthRealRuntimeProbeEnabled`: false
- `portalAuthRealRuntimeProbeAttempted`: false
- `portalAuthRuntimeConnected`: false
- `portalHttpClientCreated`: false
- `portalHttpCallAttempted`: false
- `tokenReadAttempted`: false
- `headerReadAttempted`: false
- `authorizationHeaderReadAttempted`: false
- `usesSyntheticFallback`: true
- `syntheticPortalAuthReference`: `mock://crm/portal-auth`
- `syntheticUserReference`: `mock://crm/portal-user`
- `probeSkippedBecausePortalAuthApprovalNotGranted`: true
- `nextGate`: `Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423`
