# CRM Sprint 15 S15-05 - Campaign Test and Guardrail Hardening

Repository: https://github.com/christyepez/CRM
Base Main Commit: S15-04 merge commit required
Branch: crm-sprint-15-s15-05-campaign-test-guardrail-hardening
Suggested commit: test(crm): harden campaign tests and guardrails
PR title: CRM Sprint 15 S15-05 - Campaign Test and Guardrail Hardening

## Objective
Harden Campaign Management across Domain, Application, Foundation API and Angular foundation UI without adding new runtime business scope.

## Required coverage
- lifecycle parity Draft/Active/Completed/Cancelled
- name/date validation and normalization
- no-change zero-write behavior
- API 400/404/409 mapping
- productive Campaign routes remain absent
- DELETE remains absent
- frontend uses only /api/crm/foundation/campaigns
- terminal states are read-only
- duplicate submission protection
- Portal/Common DB/external connectors remain disabled

Run full backend/frontend regressions, guardrails, Foundation and all Sprint 15 verifiers.
Prepare S15-06 Campaign Local Integration Validation and update next-task to S15-06.
Do not auto-merge.
