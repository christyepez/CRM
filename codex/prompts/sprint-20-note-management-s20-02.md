# CRM Sprint 20 S20-02 - Note Application Service and Foundation Store

## Objective
Add application orchestration and a deterministic typed in-memory foundation store for Note Management.

## Required
- Explicit create/update contracts and application result model.
- `INoteManagementService` for list/detail/create/update/archive.
- `INoteFoundationStore` and `InMemoryNoteFoundationStore` with synthetic seed only.
- Delegate all decisions to `NoteManagementPolicy`.
- Do not persist invalid, missing or Changed=false evaluations.
- Register service/store for later foundation API use.

## Guardrails
No Note API/frontend/productive route/DELETE, no cross-entity mutation or Activity scheduling, no Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.