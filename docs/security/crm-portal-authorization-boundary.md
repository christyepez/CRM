# CRM Portal Authorization Boundary

PortalCorporativo owns Auth, SSO, user context, tenant context and permission decisions.

CRM may request authorization context through Application ports, but must not create:

- Login or logout flows.
- Productive authentication middleware.
- Local user or role persistence.
- Token/cookie storage.
- Portal runtime HTTP calls before approval.

P3 keeps `portalRuntimeConnected=false`, `crmOwnsAuth=false`, `tokenStorageEnabled=false` and `productiveAuthorizationEnabled=false`.
