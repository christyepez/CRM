# CRM Foundation CRUD Contracts

Sprint 2 P4 exposes controlled foundation CRUD previews for Lead, Account and Contact only under `/api/crm/foundation/...`.

Allowed routes:

- `GET /api/crm/foundation/leads`
- `GET /api/crm/foundation/leads/{id}`
- `POST /api/crm/foundation/leads`
- `PUT /api/crm/foundation/leads/{id}`
- `GET /api/crm/foundation/accounts`
- `GET /api/crm/foundation/accounts/{id}`
- `POST /api/crm/foundation/accounts`
- `PUT /api/crm/foundation/accounts/{id}`
- `GET /api/crm/foundation/contacts`
- `GET /api/crm/foundation/contacts/{id}`
- `POST /api/crm/foundation/contacts`
- `PUT /api/crm/foundation/contacts/{id}`
- `GET /api/crm/foundation/crud/status`

Every response includes `foundationMode=true`, `persistenceMode=NonProductionSeam`, `durablePersistence=false`, `productiveCrudEnabled=false`, `databaseConfigured=false`, `authorizationMode=FoundationSimulation` and `Foundation CRUD only; no productive endpoint or database configured`.

No DELETE and no productive `/api/crm/leads`, `/api/crm/accounts` or `/api/crm/contacts` routes are active.
