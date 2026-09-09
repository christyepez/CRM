# Next Task

CRM Sprint 16 S16-03 - Account Foundation API Modernization

Base Main Commit: Sprint 16 S16-02 merge commit required
Branch: crm-sprint-16-s16-03-account-foundation-api-modernization
Prompt: codex/prompts/sprint-16-account-management-s16-03.md
Suggested commit: feat(crm): modernize account foundation api
PR title: CRM Sprint 16 S16-03 - Account Foundation API Modernization

## Intent
Replace generic Account foundation endpoint semantics with explicit Account Management DTOs and lifecycle actions while preserving the foundation route family.

## Guardrails
- Productive `/api/crm/accounts` and DELETE remain unavailable.
- No Lead conversion, automatic Account creation or Contact relationship mutations.
- No Portal users/assignment, Common DB/EF/schema/SQL/real data, external connectors or Production changes.
