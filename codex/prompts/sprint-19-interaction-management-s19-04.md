# CRM Sprint 19 S19-04 - Interaction Frontend Foundation Page

## Objective
Add an Angular foundation-only Interaction Management page consuming the S19-03 API.

## Required frontend scope
- Route `/foundation/interactions`.
- Use only `/api/crm/foundation/interactions`.
- List/select/create/update interactions.
- Void through explicit API action; repeated void remains successful no-change.
- Fields: RelatedEntityType, RelatedEntityId, Channel, Direction, Subject, Summary, OccurredAtUtc.
- Safe loading, empty, success, validation, not-found and conflict states.
- Voided interactions are read-only except repeated void.

## Guardrails
- No productive `/api/crm/interactions` and no DELETE.
- No Activity scheduling or cross-entity mutation.
- No lookup/runtime mutation of related CRM entities.
- No Portal runtime/token storage, Common DB/EF/SQL, real data, connectors, crm-prod-sim, 8094 or Production.

## Handoff
Add S19-04 docs/verifier/cross-layer guards, prepare S19-05 hardening prompt, and run .NET/Angular/foundation verifications before commit.