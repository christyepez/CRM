# CRM Sprint 16 S16-05 - Account Test and Guardrail Hardening

Repository: https://github.com/christyepez/CRM
Base Main Commit: S16-04 merge commit required
Branch: crm-sprint-16-s16-05-account-test-guardrail-hardening
Suggested commit: test(crm): harden account management foundation guardrails
PR title: CRM Sprint 16 S16-05 - Account Test and Guardrail Hardening

## Objective
Harden Account Management cross-layer contract parity and negative guardrails without expanding runtime scope.

## Required hardening
- Verify Domain/Application/API/frontend parity for Name, TaxId, Industry, Segment and Draft/Active/Inactive.
- Verify normalization and max lengths remain aligned across layers.
- Verify create/update/activate/deactivate lifecycle behavior and idempotency.
- Verify no-change operations suppress writes.
- Verify 400/404/409 API behavior and backend-normalized response reflection.
- Strengthen frontend tests for `/foundation/accounts`, loading/error states and duplicate-submit protection.

## Guardrails
- Productive `/api/crm/accounts` remains unavailable.
- No DELETE.
- No Lead conversion or automatic Account creation.
- No Contact relationship mutation.
- No Portal Auth/users/assignment, Common DB/EF/migrations/schema/SQL/real data.
- No external connectors, `crm-prod-sim`, or real Production changes.

## Validation and handoff
Run full .NET and Angular regressions, CRM guardrails, Foundation and Sprint 16 P1/S16-01..S16-05 verifiers. Prepare S16-06 Account Local Integration Validation and update next-task. Do not auto-merge.
