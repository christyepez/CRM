# CRM Sprint 18 S18-03 - Case Foundation API

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 18 S18-02 merge commit required
Branch: crm-sprint-18-s18-03-case-foundation-api
Suggested commit: feat(crm): add case foundation api
PR title: CRM Sprint 18 S18-03 - Case Foundation API

## Objective
Expose the S18-02 Case Management application service through foundation-only HTTP endpoints.

## Required endpoints
- GET `/api/crm/foundation/cases`
- GET `/api/crm/foundation/cases/{id}`
- POST `/api/crm/foundation/cases`
- PUT `/api/crm/foundation/cases/{id}`
- POST `/api/crm/foundation/cases/{id}/start`
- POST `/api/crm/foundation/cases/{id}/resolve`
- POST `/api/crm/foundation/cases/{id}/close`

## Contracts and mapping
- Use explicit API DTOs; do not expose domain, application or store records directly as transport contracts.
- Route all behavior through `ICaseManagementService`.
- Map validation failures to 400, missing Case to 404 and invalid lifecycle transition to 409.
- Preserve Changed=false idempotent success as 200.

## Guardrails
- No productive `/api/crm/cases`.
- No DELETE.
- No Angular Case page yet.
- No Customer creation/mutation, assignment runtime, SLA runtime or notifications runtime.
- No Portal Auth/users runtime, Common DB/EF/migrations/schema/SQL/real data.
- No external connectors, `crm-prod-sim`, port 8094 or real Production changes.

## Validation and handoff
Add S18-03 docs/verifier/tests, prepare S18-04 prompt, and run .NET/Angular/foundation/S18 verifiers before committing.
