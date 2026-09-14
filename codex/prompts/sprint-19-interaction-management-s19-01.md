# CRM Sprint 19 S19-01 - Interaction Contracts and Domain Rules

## Objective
Implement domain-only Interaction Management contracts and deterministic policy from the Sprint 19 P1 baseline.

## Required
- Enums/value contracts for RelatedEntityType, Channel, Direction, Status and operations.
- Create/update/void evaluation with normalization and explicit error codes.
- Subject max 160, Summary max 2000, valid related GUID, UTC occurred timestamp not in future.
- Create -> Recorded; Update only Recorded; Void Recorded -> Voided; repeated Void Changed=false.
- Unit tests for validation, normalization, lifecycle, no-change and terminal read-only behavior.

## Guardrails
Domain only. No store, API, frontend, productive route, DELETE, Activity scheduling, cross-entity mutation, Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.
