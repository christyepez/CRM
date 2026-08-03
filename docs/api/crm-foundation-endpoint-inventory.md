# CRM Foundation Endpoint Inventory

| Sprint 9 P1 | GET | `/api/crm/foundation/sprint-9/controlled-runtime-activation-decision` | Controlled runtime activation decision; NonProduction trials approved for planning only; runtime enabled now is false. |
| Sprint 9 P2 | GET | `/api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial` | Secret Provider runtime enablement trial status; disabled by default. |
| Sprint 9 P2 | POST | `/api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial/probe` | Metadata-only Secret Provider probe; 423 by default and no secret values returned. |
| Sprint 9 P3 | GET | `/api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial` | Common DB runtime connectivity trial status; disabled by default. |
| Sprint 9 P3 | POST | `/api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial/probe` | Metadata-only Common DB probe; 423 by default and no connection strings returned. |

## Sprint 7 P1 secret provider real NonProduction approval

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval` | Reports approval-package-only state for future real Secret Provider NonProduction probe. |

## Sprint 6 P6 gate decision

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-6/gate-decision` | Reports Sprint 6 closure, Go for Sprint 7 planning and NoGo for real activation/productization. |

## Sprint 5 P3 common DB probe optional activation

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-5/common-db-probe-optional-activation` | Reports Common DB probe optional activation status without connecting to a database. |

## Sprint 5 P2 secret provider runtime contract

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-5/secret-provider-runtime-contract` | Reports Secret Provider runtime contract status without reading secrets or connecting to a provider. |

## Sprint 5 P1 controlled runtime probe activation plan

| Method | Endpoint | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-5/runtime-probe-activation-plan` | Reports controlled runtime probe activation plan status without approving or activating any runtime probe. |

## Sprint 4 P6 gate decision

| Method | Endpoint | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-4/gate-decision` | Reports Sprint 4 gate decision and Sprint 5 recommended next gate without real activation. |

## Sprint 4 P5 non-production E2E pilot readiness

| Method | Endpoint | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness` | Reports foundation-only E2E pilot readiness without running real integrations or productive routes. |

## Sprint 4 P4 productive routes locked stub validation

| Method | Endpoint | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-4/productive-routes-locked-stub` | Reports document-only productive route strategy without registering productive route stubs or activating CRUD. |

## Sprint 4 P3 Portal Auth runtime probe

| Method | Endpoint | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-4/portal-auth-runtime-probe` | Reports disabled Portal Auth runtime probe status without reading credentials or calling Portal runtime. |

## Sprint 4 P2 common DB runtime probe

| Method | Endpoint | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-4/common-db-runtime-probe` | Reports disabled common DB runtime probe status without opening a DB connection or requiring a connection value. |

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

## Sprint 3 P1 durable persistence setup

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-3/durable-persistence-setup` | Describes design-only durable persistence setup gates without DB, EF runtime, migrations or connection strings. |

## Sprint 3 P2 common DB connection strategy

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-3/common-db-connection-strategy` | Describes contract-only common DB and secret strategy without real DB, secrets or connection values. |

## Sprint 3 P3 EF prototype disabled flag

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-3/ef-prototype-status` | Shows disabled EF/DbContext prototype status with no runtime context, no provider, no migrations, no real DB and no productive CRUD. |

## Sprint 3 P4 Portal Auth runtime contract validation

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-3/portal-auth-runtime-contract` | Shows Portal Auth runtime contract-only status with no real Auth runtime, no token storage, no Portal HTTP and no productive authorization. |

## Sprint 3 P5 Productive API route draft

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-3/productive-api-route-draft` | Shows productive API route draft status while productive routes remain unregistered and disabled. |

## Sprint 3 P6 productization review

| Method | Endpoint | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-3/productization-review` | Closes Sprint 3 as NoGoForRealActivation and recommends Sprint 4 runtime gate preparation. |

## Sprint 4 P1 runtime readiness

| Method | Endpoint | Purpose |
| --- | --- | --- |
| GET | `/api/crm/foundation/sprint-4/runtime-readiness` | Reports local tooling and runtime readiness without activating DB, Auth, Portal runtime or productive routes. |

## Sprint 5 P4
- `GET /api/crm/foundation/sprint-5/portal-auth-probe-optional-activation` - contract-only, no Portal HTTP, no token/header reads.
## Sprint 5 P5

- `GET /api/crm/foundation/sprint-5/locked-productive-route-stub-trial` - contract-only, no productive routes registered by default.
## Sprint 5 P6

- `GET /api/crm/foundation/sprint-5/gate-decision` - Sprint 5 gate decision only; no real activation.

## Sprint 6 P1

- `GET /api/crm/foundation/sprint-6/nonproduction-runtime-approval-package` - approval package exists, but no non-production runtime approval is granted.

## Sprint 6 P2

- `GET /api/crm/foundation/sprint-6/secret-provider-safe-mock-activation` - safe mock exists and is enabled for synthetic values only; no real secrets are read.

## Sprint 6 P3

- `GET /api/crm/foundation/sprint-6/common-db-connectivity-dry-run` - Common DB dry-run contract exists, disabled, using only `mock://crm/common-db`.
## Sprint 6 P4

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run` | GET | Foundation-only contract; no token/header read; no Portal HTTP |
## Sprint 6 P5

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial` | GET | Foundation-only trial; no runtime route registration; productive routes remain 404 |

## Sprint 6 P6

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-6/gate-decision` | GET | Gate-decision only; no real activation; Sprint 7 planning allowed |

## Sprint 7 P1

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval` | GET | Approval package only; no real secret read; runtime disabled |

## Sprint 7 P2

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe` | GET | Runtime probe contract only; skipped because approval is not granted |

## Sprint 7 P3

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe` | GET | Common DB connectivity probe contract only; skipped because Secret Provider approval is not granted |

## Sprint 7 P4

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe` | GET | Portal Auth real runtime probe contract only; skipped because Portal Auth approval is not granted |
## Sprint 7 P5

- `/api/crm/foundation/sprint-7/locked-productive-route-runtime-registration` - GET only. Reports `LockedProductiveRouteRuntimeRegistrationWith423`, default productive route status `404`, explicit locked status `423`, no DELETE, no DB, no Portal Auth runtime and no side effects.
## Sprint 7 P6

- `/api/crm/foundation/sprint-7/gate-decision` - GET only. Reports Sprint 7 closure, NoGo real activation, NotReady productization and Sprint 8 planning Go.
## Sprint 8 P1

- `/api/crm/foundation/sprint-8/secret-provider-approval-decision` - GET only. Reports approved planning for P2 controlled NonProduction read, while real read remains disabled now.

## Sprint 8 P2

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read` | GET | Controlled real read status; disabled and fail-closed by default |
| `/api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read/probe` | POST | Foundation-only probe; locked by default and metadata-only |

## Sprint 8 P3

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-8/common-db-controlled-real-connectivity` | GET | Common DB connectivity status; disabled and fail-closed by default |
| `/api/crm/foundation/sprint-8/common-db-controlled-real-connectivity/probe` | POST | Foundation-only probe; locked by default and metadata-only |

## Sprint 8 P4

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation` | GET | Portal Auth controlled validation status; disabled and fail-closed by default |
| `/api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation/probe` | POST | Foundation-only probe; locked by default and metadata-only |

## Sprint 8 P5

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-8/locked-route-authorization-policy-integration` | GET | Locked route authorization policy status; disabled and fail-closed by default |
| `/api/crm/foundation/sprint-8/gate-decision` | GET | Sprint 8 gate decision; no production activation |

## Sprint 9 P4

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial` | GET | Portal Auth runtime validation trial status; disabled and fail-closed by default |
| `/api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial/probe` | POST | Foundation-only probe; locked by default and metadata-only |

## Sprint 9 P5

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-9/productive-route-dry-run-trial` | GET | Productive route dry-run trial status; productive routes remain 404 by default |
| `/api/crm/foundation/sprint-9/productive-route-dry-run-trial/probe` | POST | Foundation-only dry-run probe; 423 by default and metadata-only |

## Sprint 9 P6

| Endpoint | Method | Runtime |
| --- | --- | --- |
| `/api/crm/foundation/sprint-9/gate-decision` | GET | Sprint 9 closure and gate status only; no runtime activation |
