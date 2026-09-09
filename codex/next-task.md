# Next Task

CRM Sprint 16 S16-01 - Account Contracts and Domain Rules

Base Main Commit: Sprint 16 P1 merge commit required
Branch: crm-sprint-16-s16-01-account-contracts-domain-rules
Prompt: codex/prompts/sprint-16-account-management-s16-01.md
Suggested commit: feat(crm): add account management domain rules
PR title: CRM Sprint 16 S16-01 - Account Contracts and Domain Rules

## Intent
Create dedicated Account Management contracts and deterministic domain policy for profile validation and Draft/Active/Inactive lifecycle without changing the existing foundation API yet.

## Guardrails
- Productive `/api/crm/accounts` remains unavailable.
- No DELETE or Lead conversion.
- No automatic Account creation from Lead/Contact/Opportunity.
- No Portal user assignment, Common DB, EF, migrations, schema, SQL or real data.
- No SimulatedProduction or real Production changes.
