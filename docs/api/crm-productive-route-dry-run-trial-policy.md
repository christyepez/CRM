# CRM Productive Route Dry Run Trial Policy

P5 is an explicit NonProduction-only dry-run. It prepares route decision metadata but does not enable productive CRM behavior.

Policy:
- Production activation is blocked.
- Productive routes remain 404 by default.
- Dry-run probe returns 423 by default.
- CRUD productivo real is not enabled.
- DELETE is not enabled.
- Side effects are not allowed.
- Database writes are not allowed.
- DB runtime, EF runtime, schema changes and migrations remain disabled.
- Portal Auth enforcement is not active.
- CRM does not create login/logout or Identity.
- CRM does not read Authorization headers or tokens by default.
- CRM does not persist or cache secrets/tokens.

P5 may consume only sanitized metadata from Sprint 9 P2, P3 and P4.
