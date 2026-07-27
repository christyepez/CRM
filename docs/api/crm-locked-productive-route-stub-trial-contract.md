# CRM Locked Productive Route Stub Trial Contract

Foundation endpoint:

- `GET /api/crm/foundation/sprint-5/locked-productive-route-stub-trial`

Future productive route stubs are documented only:

- `/api/crm/leads`
- `/api/crm/accounts`
- `/api/crm/contacts`

Default behavior: routes are not registered and return 404.

Future explicit non-production behavior: if approved and registered later, return 423 Locked. No DELETE endpoint is allowed.
