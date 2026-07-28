# CRM Portal Auth Token Propagation Dry-Run Policy

The P4 dry-run is contract-only and non-production-only.

Allowed:

- Synthetic token metadata: `mock://crm/portal-auth-token`.
- Synthetic user metadata: `mock://crm/portal-user`.
- Foundation GET endpoint status.
- Documentation, tests, preflight and observability markers.

Forbidden:

- Reading real tokens or Authorization headers.
- Reading `HttpContext.Request.Headers` or any request header collection.
- Token storage, JWT parsing, cookie authentication or CRM-owned login/logout.
- Productive Auth middleware or `[Authorize]`.
- Portal HTTP calls, hardcoded Portal URLs or `HttpClient` adapters.
- Persisting users, roles, tenants or permissions in CRM.
- DB runtime, EF runtime, migrations, connection strings and productive CRM routes.

Any future real propagation requires explicit security, architecture, rollback, observability and PortalCorporativo contract approval.
