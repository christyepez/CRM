# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 12 S12-01 - Contact Contracts and Domain Rules

Base Main Commit:
Sprint 12 P1 merge commit required

Branch:
crm-sprint-12-s12-01-contact-contracts-domain-rules

Commit sugerido:
feat(crm): add contact management domain rules

PR title:
CRM Sprint 12 S12-01 - Contact Contracts and Domain Rules

Objetivo:
Implementar contratos y reglas de dominio explícitas para Contact Management Foundation, manteniendo el alcance foundation-only.

Guardrails:
- No productive `/api/crm/contacts` unlock.
- No DELETE.
- No DB runtime productivo ni Common DB activation.
- No EF runtime.
- No migrations.
- No schema changes.
- No Portal Auth runtime activation.
- No Authorization header/token reads by default.
- No CRM-owned Identity/login.
- No secrets, `.env`, tokens, certificates or real data.
- Keep simulated Production baseline untouched.

Prompt File:
codex/prompts/sprint-12-contact-management-s12-01.md

Acceptance Criteria:
- Contact Management domain rules are explicit and deterministic.
- Existing Contact foundation CRUD remains compatible.
- Productive Contact route remains unavailable.
- New domain tests pass.
- Existing backend/frontend tests remain green.
- Guardrails pass.
- S12-02 prompt is prepared.
