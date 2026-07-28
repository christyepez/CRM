# CRM Sprint 6 P4 - Portal Auth Token Propagation Dry-Run Contract

Sprint 6 P4 introduces a contract-only dry-run for future Portal Auth token propagation.

The contract exists, but CRM does not read real tokens, does not read request headers, does not inspect the Authorization header, does not call PortalCorporativo over HTTP, and does not enable authentication or authorization middleware.

Default status:

- PortalAuthTokenPropagationDryRunContractExists: true
- PortalAuthDryRunApprovalGranted: false
- PortalAuthDryRunEnabled: false
- PortalAuthRuntimeConnected: false
- TokenReadAttempted: false
- HeaderReadAttempted: false
- PortalHttpAttempted: false
- UsesSyntheticTokenMetadata: true
- SyntheticTokenReference: mock://crm/portal-auth-token
- SyntheticUserReference: mock://crm/portal-user
- RealTokenUsed: false
- RealHeadersRead: false
- LoginImplementedByCrm: false
- IdentityImplementedByCrm: false
- PermissionsPersistedInCrm: false
- ProductiveAuthorizationEnabled: false
- NonProductionOnly: true
- NextGate: Sprint6P5LockedStubRuntimeRegistrationTrial

PortalCorporativo remains the owner of Auth, SSO, user, tenant, roles and permissions.
