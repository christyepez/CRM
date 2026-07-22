# CRM Portal Authorization Boundary

Sprint 3 P4 confirms Portal Auth runtime remains contract-only. CRM still does not implement login, Identity, credential storage, Portal HTTP, persisted roles or persisted permissions.

PortalCorporativo owns Auth, SSO, user context, tenant context and permission decisions.

CRM may request authorization context through Application ports, but must not create:

- Login or logout flows.
- Productive authentication middleware.
- Local user or role persistence.
- Token/cookie storage.
- Portal runtime HTTP calls before approval.

P3 keeps `portalRuntimeConnected=false`, `crmOwnsAuth=false`, `tokenStorageEnabled=false` and `productiveAuthorizationEnabled=false`.
