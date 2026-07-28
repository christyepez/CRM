# CRM Portal Auth Token Propagation Dry-Run Security Boundary

Security boundary:

- CRM does not implement login, logout, identity, SSO, tenants, roles or permissions.
- CRM does not read Authorization headers or request headers.
- CRM does not read, parse, store or forward real tokens.
- CRM does not enable JWT, cookie auth, Auth middleware or `[Authorize]`.
- CRM does not call PortalCorporativo over HTTP in P4.

Synthetic metadata:

- Token reference: `mock://crm/portal-auth-token`.
- User reference: `mock://crm/portal-user`.

Evidence required before real propagation:

- Portal Auth contract approved by PortalCorporativo.
- Security review for token handling and redaction.
- Explicit approval for header/token access.
- Rollback plan and operational owner.
- Observability plan with no secret leakage.

P4 remains No-Go for productive authorization.
