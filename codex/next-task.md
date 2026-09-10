# Next Task

CRM Sprint 16 S16-06 - Account Local Integration Validation

Base Main Commit: Sprint 16 S16-05 merge commit required
Branch: crm-sprint-16-s16-06-account-local-integration-validation
Prompt: codex/prompts/sprint-16-account-management-s16-06.md
Suggested commit: test(crm): validate account management local integration
PR title: CRM Sprint 16 S16-06 - Account Local Integration Validation

## Intent
Validate the complete foundation-only Account workflow through Angular, proxy, API, Application, Domain and the in-memory Account store using real local HTTP.

## Guardrails
- Productive `/api/crm/accounts` and DELETE remain unavailable.
- No Lead conversion, automatic Account creation or Contact relationship mutation.
- No Portal runtime, Common DB/EF/schema/SQL/real data or external connectors.
- Do not touch `crm-prod-sim` or port 8094.
- Real Production remains unauthorized.
