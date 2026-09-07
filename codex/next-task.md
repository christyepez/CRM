# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 13 S13-04 - Activity / Follow-Up Frontend Foundation Page

Base Main Commit:
S13-03 merge commit required

Branch:
crm-sprint-13-s13-04-activity-follow-up-frontend-foundation-page

Commit sugerido:
feat(crm): add activity follow-up frontend foundation page

PR title:
CRM Sprint 13 S13-04 - Activity / Follow-Up Frontend Foundation Page

Objetivo:
Agregar página frontend foundation para Activity / Follow-Up consumiendo solo el API foundation S13-03, sin activar rutas productivas.

Guardrails:
- Activity UI must remain foundation-only.
- No productive Activity API activation.
- No DELETE.
- No Lead conversion.
- No Account Management activation.
- No DB runtime productivo ni Common DB activation.
- No EF runtime.
- No migrations.
- No schema changes.
- No Portal Auth runtime activation.
- No Authorization header/token reads by default.
- No CRM-owned Identity/login.
- No secrets, `.env`, tokens, certificates or real data.
- Keep simulated Production baseline untouched.
- Do not reopen Sprint 10 Production gates.

Prompt File:
codex/prompts/sprint-13-activity-follow-up-s13-04.md

Acceptance Criteria:
- Foundation Activity frontend page is implemented using existing Angular conventions.
- The page calls only `/api/crm/foundation/activities`.
- Productive Activity routes remain unavailable.
- DELETE is not added.
- Portal/Common DB remain disabled.
- Guardrails pass.
