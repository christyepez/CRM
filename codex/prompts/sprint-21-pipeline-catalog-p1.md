# CRM Sprint 21 P1 - Pipeline Catalog Functional Baseline and Backlog

## Objective
Define a bounded read-only foundation slice for CRM Pipeline and PipelineStage catalog data.

## Repository evidence
- `Pipeline`: Id, Name; operation `ReadContract`.
- `PipelineStage`: Id, Name, Order; operation `ReadContract`.
- Relationship: Pipeline contains ordered stages.
- Sprint 14 residual keeps the catalog deterministic/synthetic until Portal Catalog integration is explicitly authorized.

## Required baseline
- Define deterministic synthetic Pipeline catalog contracts and ordering rules.
- Foundation list/detail only; no create/update/delete lifecycle.
- Stages must have unique positive order within a Pipeline.
- No Opportunity mutation from this slice.
- Produce S21-01 through S21-07 backlog and S21-01 prompt.

## Guardrails
No productive Pipeline routes, no Portal Catalog runtime, no Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.