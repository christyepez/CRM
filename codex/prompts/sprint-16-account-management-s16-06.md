# CRM Sprint 16 S16-06 - Account Local Integration Validation

Repository: https://github.com/christyepez/CRM
Base Main Commit: S16-05 merge commit required
Branch: crm-sprint-16-s16-06-account-local-integration-validation
Suggested commit: test(crm): validate account management local integration
PR title: CRM Sprint 16 S16-06 - Account Local Integration Validation

## Objective
Validate the complete foundation-only Account workflow locally through real HTTP:
Angular `/foundation/accounts` -> frontend proxy -> Account foundation API -> Application -> Domain -> InMemoryAccountFoundationStore.

## Required scenarios
- health/live/ready and frontend route.
- list and detail.
- create with backend normalization.
- update and no-change update.
- activate Draft -> Active.
- repeated activate idempotent.
- deactivate Active -> Inactive.
- repeated deactivate idempotent.
- reject deactivate from Draft with 409.
- invalid create/update with safe 400.
- missing Account with 404.
- read-after-write/list consistency.
- productive `/api/crm/accounts` unavailable.
- foundation/productive DELETE unavailable.

## Guardrails
- Synthetic data and in-memory foundation persistence only.
- No Lead conversion, automatic Account creation or Contact relationship mutation.
- No Portal runtime, Common DB/EF/schema/SQL/real data or external connectors.
- Do not touch `crm-prod-sim` or port 8094.
- Real Production remains unauthorized.

## Validation and handoff
Capture deterministic JSON evidence, local latency smoke, logs and cleanup. Run full backend/frontend regression, guardrails and Sprint 16 P1/S16-01..S16-06 verifiers. Prepare S16-07 Account Management Sprint Closure and update next-task. Do not auto-merge.
