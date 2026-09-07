# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 13 S13-03 - Activity Foundation API

Base Main Commit:
S13-02 merge commit required

Branch:
crm-sprint-13-s13-03-activity-foundation-api

Commit sugerido:
feat(crm): add activity foundation api

PR title:
CRM Sprint 13 S13-03 - Activity Foundation API

Objetivo:
Exponer endpoints foundation para Activity / Follow-Up usando IActivityManagementService, sin activar rutas productivas.

Guardrails:
- No Angular Activity UI yet.
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
codex/prompts/sprint-13-activity-follow-up-s13-03.md

Acceptance Criteria:
- Foundation Activity routes are implemented under `/api/crm/foundation/activities`.
- Explicit API DTO mapping and safe status mapping are implemented.
- Productive Activity routes remain unavailable.
- DELETE is not added.
- Portal/Common DB remain disabled.
- Guardrails pass.
