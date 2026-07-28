# CRM Productive API Disabled Route Policy

## Sprint 4 P4 decision

Productive routes remain disabled and unregistered. Locked stubs are not registered by default. DELETE remains prohibited.

Productive routes remain disabled in P5.

Policy:
- Do not register productive CRM routes yet.
- Do not call foundation stores from productive routes.
- Do not use in-memory stores as productive persistence.
- Do not enable DELETE endpoints.
- Do not bypass Portal Auth runtime gates.
- Do not enable DB, EF runtime, migrations or connection strings.

If future disabled stubs are introduced, they must be behind `CRM_PRODUCTIVE_API_ROUTE_STUBS_ENABLED=false` and return 404 or 423 without business logic.
## Sprint 5 P5

Productive routes remain disabled and unregistered by default. Negative route checks for leads, accounts and contacts must continue returning 404.
## Sprint 6 P5 Runtime Registration Policy

Locked stub runtime registration is not enabled in P5. Productive CRM routes remain disabled and unregistered. Default negative route status remains 404. Future 423 Locked behavior is documented only and requires a later explicit approval gate.
