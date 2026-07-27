# CRM Portal Auth Runtime Probe Disabled Flag Policy

The probe must remain disabled until a future controlled environment explicitly approves runtime integration.

CRM must not:

- Implement login/logout.
- Store or read tokens.
- Persist roles or permissions.
- Own Identity, SSO, user or tenant authority.
- Call Portal runtime.
- Register productive authorization middleware.

Future enablement requires an approved Portal endpoint, signed Auth contract, correlation id strategy, token propagation strategy without local token storage, audit/observability approval and rollback plan.
