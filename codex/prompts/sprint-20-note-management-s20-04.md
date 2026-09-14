# CRM Sprint 20 S20-04 - Note Frontend Foundation Page

## Objective
Add a foundation-only Angular Note Management page over the S20-03 API.

## Required
- Route `/foundation/notes`.
- List/select/create/update/archive.
- Consume only `/api/crm/foundation/notes`.
- Show foundation/synthetic warning.
- Preserve 400/404/409 feedback and `Changed=false` success.
- Add frontend/architecture guardrails.

## Guardrails
No productive Note route, DELETE, Activity scheduling, cross-entity mutation, Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.