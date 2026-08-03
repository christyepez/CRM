# CRM Sprint 9 GO / NO-GO

## GO

- Sprint 10 controlled productization readiness planning.
- Explicit NonProduction-only continuation of metadata-only trials.
- Foundation status observability.

## NO-GO

- Production activation.
- Productive route registration by default.
- Productive CRUD, DELETE and side effects.
- DB writes, EF runtime, migrations and schema changes.
- Portal Auth enforcement, Authorization header reads, token reads, login/logout and CRM Identity.
- Productive UI.

Productive routes must continue returning 404 by default. P2/P3/P4/P5 probes must continue returning 423 by default.
