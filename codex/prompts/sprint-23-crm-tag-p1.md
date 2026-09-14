# CRM Sprint 23 P1 - CRM Tag Functional Baseline

## Objective
Define the CRM Tag Foundation slice from repository evidence.

## Constraints
- CRM-owned tagging metadata only.
- No Portal users/roles/auth runtime.
- No generic configuration/catalog duplication.
- No productive CRUD, DELETE, Common DB/EF/SQL, real data, connectors or Production.
- Foundation-only synthetic persistence.

## Candidate model
Tag: Id, Name, Description?, Status.
Tag assignment: structural RelatedEntityType + RelatedEntityId only; no mutation of related entity.
Lifecycle: Active -> Archived, no physical delete.

Deliver functional baseline/backlog S23-01 through S23-07 and next-task prompt.
