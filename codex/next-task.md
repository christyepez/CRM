# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 12 S12-04 - Contact Management Frontend Foundation Page

Base Main Commit:
S12-03 merge commit required

Branch:
crm-sprint-12-s12-04-contact-management-frontend-foundation-page

Commit sugerido:
feat(crm): add contact management foundation frontend

PR title:
CRM Sprint 12 S12-04 - Contact Management Frontend Foundation Page

Objetivo:
Implementar la página Angular foundation de Contact Management consumiendo solo la API foundation.

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
codex/prompts/sprint-12-contact-management-s12-04.md

Acceptance Criteria:
- Contact foundation frontend consumes only foundation API routes.
- Existing Contact foundation CRUD remains compatible.
- Productive Contact route remains unavailable.
- New domain tests pass.
- Existing backend/frontend tests remain green.
- Guardrails pass.
- S12-02 prompt is prepared.
