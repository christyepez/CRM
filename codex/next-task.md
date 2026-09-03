# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 12 S12-02 - Contact Application Service

Base Main Commit:
S12-01 merge commit required

Branch:
crm-sprint-12-s12-02-contact-application-service

Commit sugerido:
feat(crm): add contact application service

PR title:
CRM Sprint 12 S12-02 - Contact Application Service

Objetivo:
Implementar la orquestación application de Contact Management usando los contratos y reglas de dominio de S12-01, manteniendo el alcance foundation-only.

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
codex/prompts/sprint-12-contact-management-s12-02.md

Acceptance Criteria:
- Contact Management application service invokes S12-01 domain rules.
- Existing Contact foundation CRUD remains compatible.
- Productive Contact route remains unavailable.
- New domain tests pass.
- Existing backend/frontend tests remain green.
- Guardrails pass.
- S12-02 prompt is prepared.
