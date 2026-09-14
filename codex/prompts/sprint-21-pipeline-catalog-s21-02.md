# CRM Sprint 21 S21-02 - Pipeline Catalog Application Read Model

## Objective
Add application-level read-only access over a deterministic synthetic Pipeline/PipelineStage catalog.

## Required
- IPipelineCatalogService with list/detail only.
- Typed deterministic synthetic catalog source behind an application port.
- Validate every catalog through PipelineCatalogPolicy before exposure.
- Preserve stage ordering and FoundationOnly metadata.
- Register service/source for later API use.

## Guardrails
No create/update/delete operations, no Opportunity mutation, no API/frontend yet, no Portal Catalog runtime, no Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.
