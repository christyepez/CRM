# CRM Sprint 14 S14-03 - Opportunity Foundation API

Repository: https://github.com/christyepez/CRM
Task: Expose foundation-only Opportunity Pipeline HTTP endpoints through `IOpportunityManagementService`.
Base Main Commit: Sprint 14 S14-02 merge commit required
Branch: crm-sprint-14-s14-03-opportunity-foundation-api
Suggested commit: feat(crm): add opportunity foundation api
PR title: CRM Sprint 14 S14-03 - Opportunity Foundation API

## Objective
Add explicit API DTOs and foundation-only routes for Opportunity list/detail/create/update/progress/win/lose/cancel, using Application interfaces only.

## Foundation routes
- GET `/api/crm/foundation/opportunities`
- GET `/api/crm/foundation/opportunities/{id}`
- POST `/api/crm/foundation/opportunities`
- PUT `/api/crm/foundation/opportunities/{id}`
- POST `/api/crm/foundation/opportunities/{id}/progress`
- POST `/api/crm/foundation/opportunities/{id}/win`
- POST `/api/crm/foundation/opportunities/{id}/lose`
- POST `/api/crm/foundation/opportunities/{id}/cancel`

## Requirements
- Register `IOpportunityManagementService` and `IOpportunityFoundationStore` through DI.
- Explicit API DTO to Application mapping; no entity binding or mass assignment.
- Deterministic safe HTTP status mapping: valid success, validation 400, not found 404, no-change success.
- Preserve foundation-only in-memory persistence.
- Add endpoint and architecture tests.
- Prepare S14-04 frontend foundation prompt.

## Guardrails
- Productive `/api/crm/opportunities` remains unavailable.
- No DELETE.
- No Lead conversion or Account Management activation.
- No assignment/owner/Portal user runtime.
- No Portal Auth/token storage/header dependency.
- No Common DB, EF, migrations, schema, SQL, real data or secrets.
- Do not touch `crm-prod-sim` or real Production.
