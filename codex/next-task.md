# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 13 S13-07 - Activity / Follow-Up Sprint Closure

Base Main Commit:
S13-06 merge commit required

Branch:
crm-sprint-13-s13-07-activity-follow-up-sprint-closure

Commit sugerido:
docs(crm): close activity follow-up foundation sprint

PR title:
CRM Sprint 13 S13-07 - Activity / Follow-Up Sprint Closure

Objetivo:
Cerrar Sprint 13 Activity / Follow-Up despues de la validacion local S13-06.

Guardrails:
- Activity UI and API must remain foundation-only.
- No productive Activity API activation.
- No DELETE.
- No Lead conversion.
- No Account Management activation.
- No Opportunity or assignment feature activation.
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
codex/prompts/sprint-13-activity-follow-up-s13-07.md

Acceptance Criteria:
- Sprint 13 Activity / Follow-Up foundation Definition of Done is reviewed.
- S13-01 through S13-06 evidence is summarized.
- Productive Activity routes remain unavailable.
- DELETE is not added.
- Portal/Common DB remain disabled.
- Residual risks and next CRM business capability options are recorded.
- Guardrails pass.
