# CRM Sprint 15 P1 - Campaign Management Functional Baseline and Backlog

Repository: https://github.com/christyepez/CRM
Base Main Commit: S14-07 merge commit required
Branch: crm-sprint-15-p1-campaign-management-functional-baseline
Suggested commit: docs(crm): define campaign management functional baseline
PR title: CRM Sprint 15 P1 - Campaign Management Functional Baseline and Backlog

## Objective
Inventory the existing Campaign concept and define the smallest valuable Campaign Management foundation slice and Sprint 15 backlog before implementation.

## Required inventory
- Domain Campaign/DateRange and any campaign-related enums/value objects.
- Application, persistence, API, frontend, tests, docs and integration evidence.
- Lead/Opportunity attribution touchpoints, but do not activate them unless needed for a foundation contract.
- Portal capabilities that would eventually own global catalogs, menu, security, audit and notifications.

## Functional decisions to make
- Canonical Campaign fields and validation.
- Draft/Active/Completed/Cancelled lifecycle or another evidence-backed lifecycle.
- Start/end date rules and timezone semantics.
- Optional channel/source/type representation using synthetic foundation catalogs only if needed.
- Whether Lead attribution is included now or explicitly deferred.
- No DELETE in the foundation slice.

## Guardrails
- FoundationOnly / NonProductionSeam.
- No productive `/api/crm/campaigns` route.
- No DELETE.
- No Portal Auth/token/header runtime.
- No Common DB/EF/migrations/schema/SQL/real data.
- No Salesforce/Dynamics/external connector activation.
- Do not touch `crm-prod-sim`.

## Deliverables
Create the Sprint 15 functional baseline, dependency matrix, acceptance criteria, risks, S15-01 prompt, verifier, and update `codex/TASKS.md` / `codex/next-task.md`. Prefer a finite implementation path analogous to recent business slices: contracts/rules -> application/store -> API -> frontend -> hardening -> local integration -> closure.
