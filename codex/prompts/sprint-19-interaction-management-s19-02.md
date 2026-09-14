# CRM Sprint 19 S19-02 - Interaction Application Service and Foundation Store

## Objective
Add application orchestration and a deterministic typed in-memory foundation store for Interaction Management.

## Required
- Explicit Application create/update contracts and result model.
- `IInteractionManagementService` for list/detail/create/update/void.
- `IInteractionFoundationStore` and `InMemoryInteractionFoundationStore` with synthetic seed only.
- Delegate all decisions to `InteractionManagementPolicy`.
- Do not persist invalid, missing or Changed=false evaluations.
- Register Application service/store for later foundation API use.

## Guardrails
No Interaction API route or Angular page yet. No productive route, DELETE, Activity scheduling, cross-entity mutation, Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, 8094 or Production.
