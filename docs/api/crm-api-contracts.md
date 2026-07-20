# CRM API Contracts

All Sprint 1 P2 endpoints are non-mutating draft contract endpoints.

## GET /api/crm/readiness

Returns module readiness:

- module: CRM.
- status: ReadyForFoundationOnly.
- runtimeMode: NonProduction.
- contractStatus: Draft.
- portalIntegration: Planned.
- financialIntegration: Planned.

## GET /api/crm/domain-catalog

Returns conceptual CRM entities, relationships and domain events.

## GET /api/crm/contracts

Returns draft API contract metadata and currently exposed read-only endpoints.

## GET /api/crm/integration-boundaries

Returns explicit boundaries:

- Portal owns cross-cutting capabilities.
- Financiero integration is planned and must remain contract/event based.
- CRM owns relationship-domain concepts only.

## Not allowed in P2

- POST/PUT/DELETE business endpoints.
- CreateLead/UpdateLead/DeleteLead.
- Database-backed queries.
- Own login, Identity or token storage.

## P3 foundation preview endpoints

P3 allows POST endpoints only under `/api/crm/foundation/.../preview`.

- `POST /api/crm/foundation/leads/preview`
- `POST /api/crm/foundation/accounts/preview`
- `POST /api/crm/foundation/contacts/preview`

These endpoints return `foundationMode: true`, `persistence: None` and `warning: Preview only, not persisted`.
