# CRM Sprint 17 S17-03 - Segment Foundation API

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 17 S17-02 merge commit required
Branch: crm-sprint-17-s17-03-segment-foundation-api
Suggested commit: feat(crm): add segment foundation api
PR title: CRM Sprint 17 S17-03 - Segment Foundation API

## Objective
Expose the Segment Management application service through foundation-only HTTP endpoints.

## Required endpoints
- GET `/api/crm/foundation/segments`
- GET `/api/crm/foundation/segments/{id}`
- POST `/api/crm/foundation/segments`
- PUT `/api/crm/foundation/segments/{id}`
- POST `/api/crm/foundation/segments/{id}/activate`
- POST `/api/crm/foundation/segments/{id}/deactivate`

## Contracts and mapping
Use explicit API DTOs. Map validation to 400, missing Segment to 404 and invalid lifecycle transition to 409. Preserve Changed=false idempotent success as 200.

## Guardrails
No Productive `/api/crm/segments`, no DELETE, no criteria execution/targeting/classification, no Portal/Common DB/real data/connectors/Production.

## Handoff
Prepare S17-04 Segment Frontend Foundation Page and run full regression before PR/merge.