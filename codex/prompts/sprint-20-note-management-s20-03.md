# CRM Sprint 20 S20-03 - Note Foundation API

## Objective
Expose Note Management through a foundation-only API over the S20-02 application service.

## Required
- Explicit foundation Note request/response DTOs.
- List/detail/create/update/archive endpoints under `/api/crm/foundation/notes`.
- Route all behavior through `INoteManagementService`.
- Validation 400, missing 404, archived update conflict 409.
- Preserve repeated archive/no-change as successful `Changed=false` responses.
- Add focused API/architecture tests.

## Guardrails
No productive `/api/crm/notes`, no DELETE, no Angular Note page yet, no cross-entity mutation, no Activity scheduling, no Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.
