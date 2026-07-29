# CRM Sprint 7 P5 - Locked Productive Route Runtime Registration With 423

Sprint 7 P5 prepares controlled runtime registration for future productive CRM routes without activating productization.

- Default behavior: `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` remain `404`.
- Explicit NonProduction flag: `Crm:ProductiveRoutes:LockedRegistrationEnabled=true`.
- If the flag is enabled outside Production, `GET`, `POST`, `PUT` and `PATCH` return `423 Locked`.
- `DELETE` is not registered.
- Locked handlers do not execute CRM domain logic, stores, database access, Portal Auth, token reads or header reads.
- Rollback is disabling the flag, returning the routes to `404`.

Next gate: `Sprint7P6Sprint7GateDecision`.
