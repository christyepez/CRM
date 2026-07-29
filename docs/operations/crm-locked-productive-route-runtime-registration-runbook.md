# CRM Locked Productive Route Runtime Registration Runbook

Default local run:

1. Keep `Crm:ProductiveRoutes:LockedRegistrationEnabled=false`.
2. Start CRM API.
3. Verify `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` return `404`.
4. Verify `/api/crm/foundation/sprint-7/locked-productive-route-runtime-registration` returns the P5 contract.

NonProduction locked-route validation:

1. Enable only `Crm:ProductiveRoutes:LockedRegistrationEnabled=true` in a controlled NonProduction test fixture.
2. Verify `GET`, `POST`, `PUT` and `PATCH` return `423`.
3. Verify `DELETE` remains unavailable.
4. Verify no DB, Portal Auth, token/header read or domain execution occurs.

Do not enable this flag in Production.
