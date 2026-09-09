# Next Task

CRM Sprint 16 S16-05 - Account Test and Guardrail Hardening

Base Main Commit: Sprint 16 S16-04 merge commit required
Branch: crm-sprint-16-s16-05-account-test-guardrail-hardening
Prompt: codex/prompts/sprint-16-account-management-s16-05.md
Suggested commit: test(crm): harden account management foundation guardrails
PR title: CRM Sprint 16 S16-05 - Account Test and Guardrail Hardening

## Intent
Harden Account Management cross-layer parity, lifecycle/idempotency, API errors and frontend guardrails without expanding runtime scope.

## Guardrails
- Productive `/api/crm/accounts` and DELETE remain unavailable.
- No Lead conversion, automatic Account creation or Contact relationship mutation.
- No Portal runtime, Common DB/EF/schema/SQL/real data, connectors or Production changes.
