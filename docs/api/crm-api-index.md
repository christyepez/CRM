# CRM API Index

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
