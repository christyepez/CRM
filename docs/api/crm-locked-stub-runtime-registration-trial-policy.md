# CRM Locked Stub Runtime Registration Trial Policy

P5 is a contract and governance evidence package only. Runtime registration is not approved.

Allowed:

- Foundation GET endpoint.
- Documentation of future locked route behavior.
- Status flags proving routes are not registered by default.
- Negative route checks expecting 404.

Forbidden:

- Registering `/api/crm/leads`, `/api/crm/accounts` or `/api/crm/contacts`.
- DELETE endpoints.
- Productive CRUD.
- Domain service execution from stubs.
- Foundation store access from stubs.
- DB runtime, EF runtime, migrations or connection strings.
- Auth runtime, Portal HTTP, token/header reads, login/logout, Identity or token storage.
- Productive UI.

If a future sprint explicitly enables locked stubs, the only allowed response is 423 Locked in NonProduction, with no side effects and no domain execution.
