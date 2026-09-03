# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 12 S12-03 - Contact Foundation API Integration

Base Main Commit:
S12-02 merge commit required

Branch:
crm-sprint-12-s12-03-contact-foundation-api-integration

Commit sugerido:
feat(crm): wire contact foundation api to application service

PR title:
CRM Sprint 12 S12-03 - Contact Foundation API Integration

Objetivo:
Integrar de forma controlada las rutas foundation de Contact con el servicio application de S12-02, manteniendo el alcance foundation-only.

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
codex/prompts/sprint-12-contact-management-s12-03.md

Acceptance Criteria:
- Existing foundation Contact API remains backward compatible.
- Contact application service is invoked where route semantics allow it.
- Existing Contact foundation CRUD remains compatible.
- Productive Contact route remains unavailable.
- New domain tests pass.
- Existing backend/frontend tests remain green.
- Guardrails pass.
- S12-02 prompt is prepared.
