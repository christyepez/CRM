# CRM Sprint 9 Gate Decision Architecture

P6 is a foundation-only decision layer.

The API exposes a single GET endpoint backed by `CrmSprint9GateDecisionStatusService`. The service returns static, sanitized status metadata and does not depend on Infrastructure, DB, Portal Auth, headers, tokens or productive route registration.

Allowed dependency direction:

`CRM.Api` -> `CRM.Application`

Disallowed:

- `CRM.Application` -> `CRM.Infrastructure`.
- DB/EF runtime.
- HTTP clients.
- Auth middleware.
- Header/token reads.
- Productive route registration by default.
- DELETE and side effects.
