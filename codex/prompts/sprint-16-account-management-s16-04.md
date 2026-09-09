# CRM Sprint 16 S16-04 - Account Management Frontend Foundation Page

Repository: https://github.com/christyepez/CRM
Base Main Commit: S16-03 merge commit required
Branch: crm-sprint-16-s16-04-account-management-frontend-foundation-page
Suggested commit: feat(crm): add account management frontend foundation page
PR title: CRM Sprint 16 S16-04 - Account Management Frontend Foundation Page

## Objective
Add an Angular foundation-only Account Management page at `/foundation/accounts` consuming only `/api/crm/foundation/accounts`.

## Required UX
- List/select/create/edit Account.
- Fields: Name, TaxId, Industry, Segment, Status.
- Activate Draft/Inactive; deactivate Active.
- Reflect backend-normalized values after mutations.
- Loading, empty, validation, not-found and safe error states.
- Prevent duplicate submissions.
- Responsive and accessible basics.
- No raw JSON or developer-console UI.

## Guardrails
- Productive `/api/crm/accounts` remains unavailable.
- No DELETE, Lead conversion, automatic Account creation or Contact relationship mutation.
- No Portal Auth/users/assignment, Common DB/EF/schema/SQL/real data, external connectors or Production.

## Validation and handoff
Run Angular build/test plus full backend regression and Sprint 16 verifiers. Prepare S16-05 Account Test and Guardrail Hardening and update next-task. Do not auto-merge.
