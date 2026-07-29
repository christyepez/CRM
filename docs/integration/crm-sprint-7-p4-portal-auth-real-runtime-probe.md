# CRM Sprint 7 P4 - Portal Auth Real Runtime Probe

P4 introduces a Portal Auth real runtime probe contract for NonProduction, but the probe is skipped by default.

- `portalAuthRealRuntimeProbeExists`: true
- `portalAuthRealRuntimeApprovalGranted`: false
- `secretProviderRealNonProductionApprovalGranted`: false
- `portalAuthRealRuntimeProbeEnabled`: false
- `portalAuthRealRuntimeProbeAttempted`: false
- `portalAuthRuntimeConnected`: false
- `portalHttpCallAttempted`: false
- `tokenReadAttempted`: false
- `headerReadAttempted`: false
- `probeSkippedBecausePortalAuthApprovalNotGranted`: true

Only synthetic references are allowed:

- `mock://crm/portal-auth`
- `mock://crm/portal-user`

Next gate: `Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423`.
