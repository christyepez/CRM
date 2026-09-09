# Next Task

CRM Sprint 16 S16-02 - Account Application Service and Foundation Store Modernization

Base Main Commit: Sprint 16 S16-01 merge commit required
Branch: crm-sprint-16-s16-02-account-application-store-modernization
Prompt: codex/prompts/sprint-16-account-management-s16-02.md
Suggested commit: feat(crm): modernize account application foundation store
PR title: CRM Sprint 16 S16-02 - Account Application Service and Foundation Store Modernization

## Intent
Introduce a dedicated Account Application service backed by the Account domain policy and modernize foundation-only in-memory persistence without changing Account API registrations yet.

## Guardrails
- Productive `/api/crm/accounts` and DELETE remain unavailable.
- No Lead conversion or automatic Account creation.
- No Contact relationship mutations.
- No Portal users/assignment, Common DB/EF/schema/SQL/real data, external connectors or Production changes.
