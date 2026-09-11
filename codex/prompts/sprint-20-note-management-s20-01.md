# CRM Sprint 20 S20-01 - Note Contracts and Domain Rules

## Objective
Implement domain-only Note Management contracts and deterministic policy from Sprint 20 P1.

## Required
- Enums/contracts for RelatedEntityType, Status, operations and error codes.
- Create/update/archive evaluation with normalization.
- RelatedEntityId required valid non-empty GUID.
- Text required, trim, max 4000.
- Create -> Active; Update only Active; Archive Active -> Archived; repeated Archive Changed=false.
- Archived notes otherwise read-only.
- Unit tests for validation, normalization, lifecycle, no-change and terminal behavior.

## Guardrails
Domain only. No store/API/frontend/productive route/DELETE, no cross-entity mutation, no Activity scheduling, no Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.