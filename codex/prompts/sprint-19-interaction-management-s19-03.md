# CRM Sprint 19 S19-03 - Interaction Foundation API

## Objective
Expose Interaction Management through a foundation-only API over the S19-02 application service.

## Required
- Explicit foundation API request/response DTOs.
- List/detail/create/update/void endpoints under `/api/crm/foundation/interactions`.
- Route all behavior through `IInteractionManagementService`.
- Map validation failures to safe HTTP responses.
- Preserve `Changed=false` idempotent successes as successful responses.
- Add focused API and architecture tests.

## Guardrails
- No productive Interaction route.
- No DELETE endpoint.
- No Angular page yet.
- No Activity scheduling.
- No cross-entity mutation.
- No Portal runtime.
- No Common DB/EF/SQL.
- No real data, connectors, `crm-prod-sim`, port 8094 or Production changes.
