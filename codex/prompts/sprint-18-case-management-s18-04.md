# CRM Sprint 18 S18-04 - Case Frontend Foundation Page

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 18 S18-03 merge commit required
Branch: crm-sprint-18-s18-04-case-frontend-foundation-page
Suggested commit: feat(crm): add case foundation page
PR title: CRM Sprint 18 S18-04 - Case Frontend Foundation Page

## Objective
Add an Angular foundation-only Case Management page that consumes the S18-03 Case Foundation API.

## Required frontend scope
- Route `/foundation/cases`.
- Use only `/api/crm/foundation/cases`.
- List and select cases.
- Create cases with CustomerId, Title, Summary and Priority.
- Update open or in-progress cases.
- Start, resolve and close cases through explicit API actions.
- Reflect backend-normalized responses and `Changed=false` idempotent success.
- Show safe loading, empty, success, validation, not-found and conflict states.

## Guardrails
- No productive `/api/crm/cases`.
- No DELETE.
- No Customer creation/mutation, assignment runtime, SLA runtime or notifications runtime.
- No Portal Auth/users runtime, token/header reads or storage.
- No Common DB/EF/migrations/schema/SQL/real data.
- No external connectors, `crm-prod-sim`, port 8094 or real Production changes.
- Do not add customer lookup/runtime assignment/SLA timers; use CustomerId input only.

## Validation and handoff
Add frontend/source guardrails, S18-04 docs/verifier/tests, prepare S18-05 prompt, and run .NET/Angular/foundation/S18 verifiers before committing.
