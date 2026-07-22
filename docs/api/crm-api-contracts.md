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

## P4 read model preview endpoints

P4 allows GET endpoints only under `/api/crm/foundation/...`.

- `GET /api/crm/foundation/leads/read-model-preview`
- `GET /api/crm/foundation/accounts/read-model-preview`
- `GET /api/crm/foundation/contacts/read-model-preview`
- `GET /api/crm/foundation/read-model-status`

These endpoints return `source: FoundationMock`, `persistence: None` and `warning: Read model preview only, not persisted`.
# CRM API Contracts - P5 Portal Integration

Sprint 1 P5 adds foundation-only Portal adapter contract endpoints:

- `GET /api/crm/foundation/portal-integration/status`
- `GET /api/crm/foundation/portal-integration/contracts`
- `GET /api/crm/foundation/portal-integration/required-capabilities`

All responses are NonProduction readiness contracts with `integrationMode=Planned`, `connected=false` and `capabilityOwner=PortalCorporativo`. They do not call Portal at runtime.

# CRM API Contracts - P6 Financial Integration

Sprint 1 P6 adds foundation-only Financial adapter contract endpoints:

- `GET /api/crm/foundation/financial-integration/status`
- `GET /api/crm/foundation/financial-integration/contracts`
- `GET /api/crm/foundation/financial-integration/required-capabilities`
- `GET /api/crm/foundation/financial-integration/events`

All responses are NonProduction readiness contracts with `integrationMode=Planned`, `connected=false`, `capabilityOwner=Financiero` and `integrationPatterns=API, Events, NoSharedDatabase`. They do not call Financiero at runtime.

# CRM API Contracts - P7 Reporting/BI

Sprint 1 P7 adds foundation-only Reporting/BI contract endpoints:

- `GET /api/crm/foundation/reporting/status`
- `GET /api/crm/foundation/reporting/kpis`
- `GET /api/crm/foundation/reporting/dashboards`
- `GET /api/crm/foundation/reporting/analytics-read-models`

All responses are NonProduction readiness contracts with `analyticsMode=Planned`, `connected=false`, `source=FoundationMock` and no analytics runtime configured.

# CRM API Contracts - P8 Closure

Sprint 1 P8 adds a foundation-only closure endpoint:

- `GET /api/crm/foundation/sprint-1/closure-status`

It returns `FoundationClosed`, `ProductizationStatus=NotReady`, `NextGate=Sprint2Planning` and `Foundation closure only; no productive activation`.

# CRM API Contracts - Sprint 2 P1 Persistence Readiness

Sprint 2 P1 adds a foundation-only persistence readiness endpoint:

- `GET /api/crm/foundation/persistence/readiness`

It returns `persistenceMode=DesignOnly`, `databaseConfigured=false`, `migrationReady=false`, `dbContextConfigured=false`, `sqlServerOwnedByCrm=false` and `Persistence design review only; no database configured`.
## Sprint 2 P2 foundation persistence seam

New non-productive endpoints:

- `GET /api/crm/foundation/persistence/seam-status`
- `GET /api/crm/foundation/persistence/feature-flags`
- `GET /api/crm/foundation/persistence/stores/status`
- `POST /api/crm/foundation/persistence/stores/clear-preview`

All return `foundationMode=true` and keep `productiveCrudEnabled=false`. No productive `/api/crm/leads`, `/api/crm/accounts` or `/api/crm/contacts` endpoints are active.

## Sprint 2 P3 Portal authorization simulation

New foundation-only endpoints:

- `GET /api/crm/foundation/portal-authorization/simulation-status`
- `GET /api/crm/foundation/portal-authorization/scenarios`
- `GET /api/crm/foundation/portal-authorization/permissions`
- `GET /api/crm/foundation/portal-authorization/sample-user-context`
- `POST /api/crm/foundation/portal-authorization/check-permission`

Responses include `foundationMode=true`, `simulationMode=FoundationSimulation`, `portalRuntimeConnected=false`, `authOwnedBy=PortalCorporativo`, `crmOwnsAuth=false` and the warning `Portal authorization simulation only; no real Portal runtime configured`.

## Sprint 2 P4 controlled foundation CRUD

P4 adds GET/POST/PUT preview routes for Lead, Account and Contact under `/api/crm/foundation/...` plus `GET /api/crm/foundation/crud/status`.

These routes are not productive CRUD. They use in-memory foundation stores, Portal permission simulation and return `Foundation CRUD only; no productive endpoint or database configured`.
