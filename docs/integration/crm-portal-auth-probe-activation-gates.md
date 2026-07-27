# CRM Sprint 5 P4 - Portal Auth Probe Optional Activation

## Decision
The Portal Auth probe optional activation exists as a contract-only foundation capability and remains disabled by default.

## Default state
- PortalAuthProbeOptionalActivationExists: true
- PortalAuthProbeActivationApproved: false
- PortalAuthProbeEnabled: false
- PortalAuthRuntimeConnected: false
- PortalHttpAttempted: false
- TokenReadAttempted: false
- HeaderReadAttempted: false
- SecretProviderRuntimeRequired: true
- SecretProviderRuntimeConnected: false
- SecretReadsEnabled: false
- LoginImplementedByCrm: false
- IdentityImplementedByCrm: false
- PermissionsPersistedInCrm: false
- ProductiveAuthorizationEnabled: false
- NonProductionOnly: true
- RollbackRequired: true
- NextGate: Sprint5P5LockedProductiveRouteStubTrialInNonProduction

## Boundary
PortalCorporativo remains owner of Auth, SSO, users, tenants, roles and permissions. CRM does not implement login, logout, Identity, JWT, cookies, token storage, header reads, token reads or productive authorization middleware.

## Warning
Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted.

## Gates
1. Security approval for token propagation.
2. Secret provider runtime approval.
3. Architecture approval for Portal ownership.
4. DevOps rollback approval.
5. QA negative route checks.
