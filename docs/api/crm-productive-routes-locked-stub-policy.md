# CRM Productive Routes Locked Stub Policy

Productive route stubs are not registered in P4. The conservative policy is document-only until Portal Auth runtime, common DB runtime, productive authorization and route registration gates are approved.

Future documented routes:

- `GET /api/crm/leads`
- `GET /api/crm/leads/{id}`
- `POST /api/crm/leads`
- `PUT /api/crm/leads/{id}`
- `GET /api/crm/accounts`
- `GET /api/crm/accounts/{id}`
- `POST /api/crm/accounts`
- `PUT /api/crm/accounts/{id}`
- `GET /api/crm/contacts`
- `GET /api/crm/contacts/{id}`
- `POST /api/crm/contacts`
- `PUT /api/crm/contacts/{id}`

DELETE is not allowed.

If a later sprint registers locked stubs, they must be behind a hardcoded disabled flag by default and must never call domain services, stores, DB, Auth runtime or Portal runtime.
