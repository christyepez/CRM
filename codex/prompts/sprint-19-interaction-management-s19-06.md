# CRM Sprint 19 S19-06 - Interaction Local Integration Validation

## Objective
Validate Interaction Management end-to-end through real local HTTP using an isolated CRM API instance and Angular development proxy.

## Required scenarios
- Health and frontend `/foundation/interactions` availability.
- Foundation list/detail/create/update/no-change.
- Void and repeated idempotent void.
- Validation 400, missing 404 and voided-update conflict 409.
- Read-after-write consistency.
- Productive `/api/crm/interactions` unavailable.
- Foundation/productive DELETE unavailable.
- Capture latency samples and persisted JSON evidence.

## Guardrails
Synthetic local data only. No Activity scheduling, cross-entity mutation, Portal runtime/token, Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.

## Handoff
Prepare S19-07 Sprint Closure only after integration evidence passes.
