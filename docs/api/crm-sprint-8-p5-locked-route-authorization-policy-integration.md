# CRM Sprint 8 P5 - Locked Route Authorization Policy Integration

Sprint 8 P5 prepares metadata-only authorization policy evaluation for locked productive routes.

Default behavior:

- Productive routes remain unregistered and return 404.
- Locked route registration remains disabled by default.
- Locked authorization policy remains disabled by default.
- No CRUD, domain execution, persistence, Portal HTTP, token reads, header reads, auth middleware, `[Authorize]`, DELETE, or productive UI is enabled.

With explicit NonProduction flags, GET/POST/PUT/PATCH locked routes can return 423 with sanitized policy metadata. DELETE remains unavailable.

Next gate: Sprint8P6Sprint8GateDecision.
