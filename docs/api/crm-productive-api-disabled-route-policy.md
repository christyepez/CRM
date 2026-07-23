# CRM Productive API Disabled Route Policy

Productive routes remain disabled in P5.

Policy:
- Do not register productive CRM routes yet.
- Do not call foundation stores from productive routes.
- Do not use in-memory stores as productive persistence.
- Do not enable DELETE endpoints.
- Do not bypass Portal Auth runtime gates.
- Do not enable DB, EF runtime, migrations or connection strings.

If future disabled stubs are introduced, they must be behind `CRM_PRODUCTIVE_API_ROUTE_STUBS_ENABLED=false` and return 404 or 423 without business logic.
