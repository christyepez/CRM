# CRM Sprint 21 S21-01 - Pipeline Catalog Contracts and Domain Validation

## Objective
Add deterministic read-only Pipeline/PipelineStage catalog contracts and validation in CRM.Domain.

## Required
- Pipeline catalog record: Id, Name, ordered Stages.
- Stage catalog record: Id, Name, Order.
- Validate non-empty GUID ids.
- Normalize/trim names; max 120 characters.
- Require at least one stage.
- Require positive unique stage order values.
- Return normalized stages sorted by Order.
- Add focused unit tests for valid and invalid catalogs.

## Guardrails
No application/API/frontend yet, no mutation operations, no productive routes, no Portal Catalog runtime, no Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.