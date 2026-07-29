# CRM API Index

## Sprint 7 P1

- `GET /api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval`: Secret Provider real NonProduction approval package; no approval granted and no real secrets are read.

## Sprint 6 P6

- `GET /api/crm/foundation/sprint-6/gate-decision`: Sprint 6 gate decision only; closes Sprint 6 and recommends Sprint 7 controlled NonProduction activation planning without real activation.

## Sprint 5 P3

- `GET /api/crm/foundation/sprint-5/common-db-probe-optional-activation`: Common DB probe optional activation plan; no database connection is attempted.

## Sprint 5 P2

- `GET /api/crm/foundation/sprint-5/secret-provider-runtime-contract`: Secret Provider runtime contract validation; no secrets are read.

## Sprint 5 P1

- `GET /api/crm/foundation/sprint-5/runtime-probe-activation-plan`: controlled runtime probe activation plan; no runtime activation approved.

## Sprint 4 P6

- `GET /api/crm/foundation/sprint-4/gate-decision`: Sprint 4 foundation-only gate decision; no real activation and Sprint 5 P1 next gate.

## Sprint 4 P5

- `GET /api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness`: foundation-only E2E pilot readiness; no real activation and negative route validation required.

## Sprint 4 P4

- `GET /api/crm/foundation/sprint-4/productive-routes-locked-stub`: productive route locked stub validation; document-only preferred, no productive routes registered.

## Sprint 4 P3

- `GET /api/crm/foundation/sprint-4/portal-auth-runtime-probe`: controlled Portal Auth runtime probe status; disabled by default, no token read and no Portal runtime call.

## Sprint 4 P2

- `GET /api/crm/foundation/sprint-4/common-db-runtime-probe`: controlled common DB runtime probe status; disabled by default, no connection attempt, no CRM-owned SQL Server.

## Draft endpoints

- `GET /health`
- `GET /health/live`
- `GET /health/ready`
- `GET /api/crm/readiness`
- `GET /api/crm/domain-catalog`
- `GET /api/crm/contracts`
- `GET /api/crm/integration-boundaries`
- `POST /api/crm/foundation/leads/preview`
- `POST /api/crm/foundation/accounts/preview`
- `POST /api/crm/foundation/contacts/preview`
- `GET /api/crm/foundation/leads/read-model-preview`
- `GET /api/crm/foundation/accounts/read-model-preview`
- `GET /api/crm/foundation/contacts/read-model-preview`
- `GET /api/crm/foundation/read-model-status`

## Runtime

- Port: 8093.
- Mode: NonProduction.
- Contract status: Draft.
- Database: none.
- Foundation previews: enabled, not persisted.
- Read models: PreviewOnly.

## Next API work

Future sprints may add read models and controlled command endpoints only after Portal Security, Audit and Configuration boundaries are wired.
# CRM API Index - P5 Addendum

Portal foundation readiness endpoints:

- `GET /api/crm/foundation/portal-integration/status`
- `GET /api/crm/foundation/portal-integration/contracts`
- `GET /api/crm/foundation/portal-integration/required-capabilities`

These endpoints are contract/readiness only and must not be treated as productive CRM domain APIs.

# CRM API Index - P6 Addendum

Financial foundation readiness endpoints:

- `GET /api/crm/foundation/financial-integration/status`
- `GET /api/crm/foundation/financial-integration/contracts`
- `GET /api/crm/foundation/financial-integration/required-capabilities`
- `GET /api/crm/foundation/financial-integration/events`

These endpoints are contract/readiness only and must not be treated as productive CRM or financial APIs.

# CRM API Index - P7 Addendum

Reporting foundation readiness endpoints:

- `GET /api/crm/foundation/reporting/status`
- `GET /api/crm/foundation/reporting/kpis`
- `GET /api/crm/foundation/reporting/dashboards`
- `GET /api/crm/foundation/reporting/analytics-read-models`

These endpoints are metadata/readiness only and must not be treated as productive analytics APIs.

# CRM API Index - P8 Addendum

Closure readiness endpoint:

- `GET /api/crm/foundation/sprint-1/closure-status`

This endpoint is closure metadata only and must not be treated as productive CRM API activation.

# CRM API Index - Sprint 2 P1 Addendum

Persistence readiness endpoint:

- `GET /api/crm/foundation/persistence/readiness`

This endpoint is design-review metadata only and must not be treated as DB activation.
## Sprint 2 P2 additions

Persistence seam endpoints are foundation-only and documented in `crm-api-contracts.md` and `crm-foundation-endpoint-inventory.md`.
## Sprint 2 P3 additions

Portal authorization simulation endpoints are foundation-only and do not activate productive Auth, Portal runtime or CRUD.

P4 foundation CRUD endpoints are preview-only, in-memory and stay under `/api/crm/foundation/...`.

P5 integration readiness endpoint is read-only and exists only to summarize GO/NO-GO evidence before productization.

P6 productization gate endpoint is read-only and closes Sprint 2 without activation:

- `GET /api/crm/foundation/sprint-2/productization-gate`

Sprint 3 P1 durable persistence setup endpoint is read-only and design-only:

- `GET /api/crm/foundation/sprint-3/durable-persistence-setup`

Sprint 3 P2 common DB connection strategy endpoint is read-only and contract-only:

- `GET /api/crm/foundation/sprint-3/common-db-connection-strategy`
# Sprint 3 P3

- `GET /api/crm/foundation/sprint-3/ef-prototype-status`: EF/DbContext prototype status, disabled runtime.

# Sprint 3 P4

- `GET /api/crm/foundation/sprint-3/portal-auth-runtime-contract`: Portal Auth runtime contract status, no real Auth activation.

# Sprint 3 P5

- `GET /api/crm/foundation/sprint-3/productive-api-route-draft`: Productive API route draft status, no active productive routes.

# Sprint 3 P6

- `GET /api/crm/foundation/sprint-3/productization-review`: Sprint 3 productization review status, `NoGoForRealActivation`, no real activation.

# Sprint 4 P1

- `GET /api/crm/foundation/sprint-4/runtime-readiness`: Local runtime readiness and tooling status, no real activation.

# Sprint 4 P2

- `GET /api/crm/foundation/sprint-4/common-db-runtime-probe`: Common DB runtime probe contract, disabled by default.

# Sprint 4 P3

- `GET /api/crm/foundation/sprint-4/portal-auth-runtime-probe`: Portal Auth runtime probe contract, disabled by default.

# Sprint 4 P4

- `GET /api/crm/foundation/sprint-4/productive-routes-locked-stub`: Productive route locked stub validation, document-only by default.

# Sprint 4 P5

- `GET /api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness`: Non-production foundation-only E2E pilot readiness.

## Sprint 5 P4 Foundation Endpoint
- `/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation` documents Portal Auth probe optional activation as disabled by default.
## Sprint 5 P5 Foundation Endpoint

- `/api/crm/foundation/sprint-5/locked-productive-route-stub-trial` documents locked productive route stubs without runtime registration.
## Sprint 5 P6 Foundation Endpoint

- `/api/crm/foundation/sprint-5/gate-decision` documents Sprint 5 closure and Sprint 6 planning.

## Sprint 6 P1 Foundation Endpoint

- `GET /api/crm/foundation/sprint-6/nonproduction-runtime-approval-package`: documents the non-production runtime approval package; all runtime approvals remain false and the next gate is `Sprint6P2SecretProviderSafeMockActivation`.

## Sprint 6 P2 Foundation Endpoint

- `GET /api/crm/foundation/sprint-6/secret-provider-safe-mock-activation`: reports the safe synthetic Secret Provider mock status; no real secrets are read and the next gate is `Sprint6P3CommonDbConnectivityDryRunContract`.

## Sprint 6 P3 Foundation Endpoint

- `GET /api/crm/foundation/sprint-6/common-db-connectivity-dry-run`: reports the Common DB dry-run contract; no connection string is resolved and no database connection is attempted.
## Sprint 6 P4 Foundation Endpoint

- `GET /api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run` - Portal Auth token propagation dry-run contract. Contract-only, synthetic metadata, no token/header read and no Portal HTTP.
## Sprint 6 P5 Foundation Endpoint

- `GET /api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial` - Locked stub runtime registration trial. Runtime registration not approved; productive routes remain unregistered and return 404 by default.

## Sprint 6 P6 Foundation Endpoint

- `GET /api/crm/foundation/sprint-6/gate-decision` - Sprint 6 closure and gate decision. Real activation remains NoGo; Sprint 7 planning is Go.

## Sprint 7 P1 Foundation Endpoint

- `GET /api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval` - Secret Provider real NonProduction approval package; runtime remains disabled and disconnected.

## Sprint 7 P2 Foundation Endpoint

- `GET /api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe` - Secret Provider real NonProduction runtime probe contract; skipped because approval is not granted.

## Sprint 7 P3 Foundation Endpoint

- `GET /api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe` - Common DB real connectivity NonProduction probe contract; skipped because Secret Provider approval is not granted.

## Sprint 7 P4 Foundation Endpoint

- `GET /api/crm/foundation/sprint-7/portal-auth-real-runtime-probe` - Portal Auth real runtime NonProduction probe contract; skipped because Portal Auth approval is not granted.
## Sprint 7 P5

- `GET /api/crm/foundation/sprint-7/locked-productive-route-runtime-registration`: foundation status for locked productive route runtime registration with 423.
- Productive routes remain 404 by default; explicit NonProduction flag can register GET/POST/PUT/PATCH locked stubs returning 423.
- DELETE, DB, Portal Auth runtime, token/header reads and productive UI remain disabled.
## Sprint 7 P6

- `GET /api/crm/foundation/sprint-7/gate-decision`: Sprint 7 closure and gate decision.
- Real activation remains `NoGo`; Sprint 8 planning is `Go`.
- Next gate: `Sprint8P1SecretProviderApprovalDecision`.
## Sprint 8 P1

- `GET /api/crm/foundation/sprint-8/secret-provider-approval-decision`: planning-only decision for controlled Secret Provider read in P2.
- No real secret read occurs in P1.
- Next gate: `Sprint8P2SecretProviderControlledRealNonProductionRead`.
