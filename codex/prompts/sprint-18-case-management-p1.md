# CRM Sprint 18 P1 - Case Management Functional Baseline and Backlog

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 17 S17-07 merge commit required
Branch: crm-sprint-18-p1-case-management-functional-baseline
Suggested commit: docs(crm): define case management functional baseline
PR title: CRM Sprint 18 P1 - Case Management Functional Baseline and Backlog

## Objective
Define the smallest bounded Case Management foundation capability from existing repository evidence before implementation.

## Repository evidence
- CRM model documentation lists Case as a domain capability.
- Productive case CRUD is explicitly inactive behind productization gates.
- No dedicated Case lifecycle policy found.
- No Case Application service or typed foundation store found.
- No Case foundation API or Angular Case workflow found.

## Baseline decisions to make
Define canonical fields, normalization/max lengths, lifecycle/state semantics, create/update/no-change behavior, explicit exclusions, API/frontend route family, in-memory persistence seam and finite S18-01..S18-07 backlog.

## Guardrails
- Foundation routes only; no productive Case route.
- No DELETE.
- No automatic Customer conversion or mutation.
- No Portal Auth/users/assignment runtime.
- No Common DB/EF/migrations/schema/SQL/real data.
- No external connectors, `crm-prod-sim`, port 8094 or real Production changes.

## Validation and deliverables
Produce the P1 baseline document, P1 verifier, S18-01 prompt, TASKS/README/next-task updates, and run full .NET/Angular regressions plus existing guardrails. Do not implement Case runtime in P1. Do not auto-merge without exact base/head verification.
