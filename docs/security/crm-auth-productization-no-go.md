# CRM Auth Productization NO-GO

Decision: Portal Auth Runtime is `NoGo`.

Reasons:
- CRM must not create login, Identity, JWT, cookies or token storage.
- PortalCorporativo owns authentication and permission runtime.
- Sprint 2 uses `FoundationSimulation` only.

Required before GO:
- Portal runtime authorization contract.
- Audited permission checks.
- Tenant/user/correlation context.
- Security tests for denied and allowed flows.
