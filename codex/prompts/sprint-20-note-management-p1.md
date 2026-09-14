# CRM Sprint 20 P1 - Note Management Functional Baseline and Backlog

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 19 S19-07 merge commit required
Branch: crm-sprint-20-p1-note-management-functional-baseline
Suggested commit: docs(crm): define note management functional baseline
PR title: CRM Sprint 20 P1 - Note Management Functional Baseline and Backlog

## Objective
Inventory the existing Note concept and Portal ownership boundaries, then define a deterministic foundation-only Note Management backlog.

## Required analysis
- Inspect `docs/database/crm-model.md`, conceptual entities and all Note references.
- Confirm whether any Note domain/application/API/frontend runtime already exists.
- Define canonical Note fields, normalization, max lengths and lifecycle without duplicating Interaction or Activity.
- Treat RelatedEntityId as structural reference only; no cross-entity mutation.
- Keep audit, notifications, files, users/roles and configuration Portal-owned REUSE/ADAPT/EXTEND capabilities.
- Produce S20-01 through S20-07 backlog and S20-01 prompt.

## Guardrails
Planning/baseline only. No productive Note route, DELETE, Portal runtime, Common DB/EF/schema/SQL, real data, connectors, crm-prod-sim, port 8094 or Production activation.
