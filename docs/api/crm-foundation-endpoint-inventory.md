# CRM Foundation Endpoint Inventory

| Method | Path | Purpose | Status | Persistence | Runtime integration | Production readiness | Security notes |
| --- | --- | --- | --- | --- | --- | --- | --- |
| GET | `/health` | Health | Active | None | None | Foundation | No auth runtime |
| GET | `/health/live` | Liveness | Active | None | None | Foundation | No auth runtime |
| GET | `/health/ready` | Readiness | Active | None | None | Foundation | No auth runtime |
| GET | `/api/crm/readiness` | CRM readiness | Foundation | None | None | NotReady | No secrets |
| GET | `/api/crm/domain-catalog` | Domain catalog | Foundation | None | None | NotReady | Metadata only |
| GET | `/api/crm/contracts` | Contract index | Foundation | None | None | NotReady | Metadata only |
| GET | `/api/crm/integration-boundaries` | Boundaries | Foundation | None | None | NotReady | Metadata only |
| POST | `/api/crm/foundation/leads/preview` | Lead preview | Foundation | None | None | NotReady | Preview only |
| POST | `/api/crm/foundation/accounts/preview` | Account preview | Foundation | None | None | NotReady | Preview only |
| POST | `/api/crm/foundation/contacts/preview` | Contact preview | Foundation | None | None | NotReady | Preview only |
| GET | `/api/crm/foundation/leads/read-model-preview` | Lead read model preview | Foundation | None | None | NotReady | Mock only |
| GET | `/api/crm/foundation/accounts/read-model-preview` | Account read model preview | Foundation | None | None | NotReady | Mock only |
| GET | `/api/crm/foundation/contacts/read-model-preview` | Contact read model preview | Foundation | None | None | NotReady | Mock only |
| GET | `/api/crm/foundation/read-model-status` | Read model status | Foundation | None | None | NotReady | Mock only |
| GET | `/api/crm/foundation/portal-integration/status` | Portal status | Planned | External | NotConnected | NotReady | Portal owned |
| GET | `/api/crm/foundation/portal-integration/contracts` | Portal contracts | Planned | External | NotConnected | NotReady | No tokens |
| GET | `/api/crm/foundation/portal-integration/required-capabilities` | Portal dependencies | Planned | External | NotConnected | NotReady | No runtime |
| GET | `/api/crm/foundation/financial-integration/status` | Financial status | Planned | External | NotConnected | NotReady | No shared DB |
| GET | `/api/crm/foundation/financial-integration/contracts` | Financial contracts | Planned | External | NotConnected | NotReady | No SRI |
| GET | `/api/crm/foundation/financial-integration/required-capabilities` | Financial dependencies | Planned | External | NotConnected | NotReady | No runtime |
| GET | `/api/crm/foundation/financial-integration/events` | Financial events | Conceptual | External | None | NotReady | No broker |
| GET | `/api/crm/foundation/reporting/status` | Reporting status | Planned | None | None | NotReady | No BI runtime |
| GET | `/api/crm/foundation/reporting/kpis` | KPI catalog | FoundationMock | None | None | NotReady | No real data |
| GET | `/api/crm/foundation/reporting/dashboards` | Dashboard catalog | FoundationMock | None | None | NotReady | No embed |
| GET | `/api/crm/foundation/reporting/analytics-read-models` | Analytics metadata | FoundationMock | None | None | NotReady | No ETL |
| GET | `/api/crm/foundation/sprint-1/closure-status` | Sprint 1 closure | FoundationClosed | None | None | NotReady | Closure only |
| GET | `/api/crm/foundation/persistence/readiness` | Persistence design review | DesignOnly | None | None | NotReady | No DB configured |
## Sprint 2 P2 persistence seam

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/persistence/seam-status` | Shows `NonProductionSeam` status. |
| GET | `/api/crm/foundation/persistence/feature-flags` | Shows safe persistence flags. |
| GET | `/api/crm/foundation/persistence/stores/status` | Shows in-memory foundation store status. |
| POST | `/api/crm/foundation/persistence/stores/clear-preview` | Clears in-memory preview state; not productive DELETE. |
## Sprint 2 P3 Portal authorization simulation

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/portal-authorization/simulation-status` | Shows foundation authorization simulation status. |
| GET | `/api/crm/foundation/portal-authorization/scenarios` | Lists fictitious permission scenarios. |
| GET | `/api/crm/foundation/portal-authorization/permissions` | Lists fictitious CRM foundation permissions. |
| GET | `/api/crm/foundation/portal-authorization/sample-user-context` | Shows sample simulated user and tenant context. |
| POST | `/api/crm/foundation/portal-authorization/check-permission` | Checks one simulated permission; not productive authorization. |

## Sprint 2 P4 controlled foundation CRUD

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/crud/status` | Shows foundation CRUD status and gates. |
| GET | `/api/crm/foundation/leads` | Lists in-memory lead previews. |
| GET | `/api/crm/foundation/leads/{id}` | Reads one in-memory lead preview. |
| POST | `/api/crm/foundation/leads` | Creates one lead preview. |
| PUT | `/api/crm/foundation/leads/{id}` | Updates one lead preview. |
| GET | `/api/crm/foundation/accounts` | Lists in-memory account previews. |
| GET | `/api/crm/foundation/accounts/{id}` | Reads one in-memory account preview. |
| POST | `/api/crm/foundation/accounts` | Creates one account preview. |
| PUT | `/api/crm/foundation/accounts/{id}` | Updates one account preview. |
| GET | `/api/crm/foundation/contacts` | Lists in-memory contact previews. |
| GET | `/api/crm/foundation/contacts/{id}` | Reads one in-memory contact preview. |
| POST | `/api/crm/foundation/contacts` | Creates one contact preview. |
| PUT | `/api/crm/foundation/contacts/{id}` | Updates one contact preview. |

## Sprint 2 P5 integration readiness

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-2/integration-readiness` | Summarizes P1-P4 evidence and P5 GO/NO-GO readiness. |

## Sprint 2 P6 productization gate

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-2/productization-gate` | Closes Sprint 2 with NoGoForProductiveActivation and Sprint 3 planning GO. |
