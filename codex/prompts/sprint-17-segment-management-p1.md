# CRM Sprint 17 P1 - Segment Management Functional Baseline and Backlog

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 16 S16-07 merge commit required
Branch: crm-sprint-17-p1-segment-management-functional-baseline
Suggested commit: docs(crm): define segment management functional baseline
PR title: CRM Sprint 17 P1 - Segment Management Functional Baseline and Backlog

## Objective
Define the smallest bounded Segment Management foundation capability from existing repository evidence before implementation.

## Repository evidence
- Existing conceptual `Segment(CrmId Id, string Name, string CriteriaSummary)`.
- Existing CRM domain catalog exposes Segment as conceptual/read-only.
- No dedicated Segment lifecycle policy found.
- No Segment Application service or typed foundation store found.
- No Segment foundation API or Angular Segment workflow found.

## Baseline decisions to make
Define canonical fields, normalization/max lengths, lifecycle/state semantics if needed, create/update/no-change behavior, explicit exclusions, API/frontend route family, in-memory persistence seam and finite S17-01..S17-07 backlog.
## Guardrails
- Foundation routes only; no Productive Segment route.
- No DELETE.
- No Campaign targeting/linking or automatic audience assignment.
- No Account auto-classification or mutation.
- No arbitrary criteria/query execution.
- No Portal Auth/users/assignment.
- No Common DB/EF/migrations/schema/SQL/real data.
- No external connectors, `crm-prod-sim`, port 8094 or real Production changes.

## Validation and deliverables
Produce the P1 baseline document, P1 verifier, S17-01 prompt, TASKS/README/next-task updates, and run full .NET/Angular regressions plus existing guardrails. Do not implement Segment runtime in P1. Do not auto-merge without exact base/head verification.