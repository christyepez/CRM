# CRM Sprint 6 Gates

| Gate | Purpose | Default |
|---|---|---|
| Sprint6P1NonProductionRuntimeApprovalPackage | Approvals and evidence package | Exists; approvals not granted |
| Sprint6P2SecretProviderSafeMockActivation | Mock-only secret provider activation | Enabled for synthetic values only |
| Sprint6P3CommonDbConnectivityDryRunContract | DB dry-run contract without real activation | Exists; connection disabled |
| Sprint6P4PortalAuthTokenPropagationDryRunContract | Token propagation dry-run contract | Not started |
| Sprint6P5LockedStubRuntimeRegistrationTrial | Controlled locked stub registration trial | Not started |
| Sprint6P6GateDecision | Sprint 6 closure | Not started |
## P4 Gate - Portal Auth Token Propagation Dry-Run Contract

- Decision: Contract exists, activation not granted.
- Required false flags: tokenReadAttempted, headerReadAttempted, portalHttpAttempted, realTokenUsed, realHeadersRead, productiveAuthorizationEnabled.
- Next gate: `Sprint6P5LockedStubRuntimeRegistrationTrial`.
