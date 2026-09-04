# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 12 S12-07 - Contact Management Sprint Closure

Base Main Commit:
S12-06 merge commit required

Branch:
crm-sprint-12-s12-07-contact-management-sprint-closure

Commit sugerido:
docs(crm): close contact management foundation sprint

PR title:
CRM Sprint 12 S12-07 - Contact Management Sprint Closure

Objetivo:
Cerrar Sprint 12 Contact Management revisando P1 y S12-01..S12-06, confirmando Definition of Done, riesgos residuales y siguiente capacidad CRM.

Guardrails:
- No productive `/api/crm/contacts` unlock.
- No DELETE.
- No Lead conversion.
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
codex/prompts/sprint-12-contact-management-s12-07.md

Acceptance Criteria:
- Sprint 12 Contact Management evidence is reviewed from P1 through S12-06.
- Definition of Done is confirmed or residual risks are documented.
- Productive Contact route remains unavailable.
- DELETE and Lead conversion remain deferred.
- Portal/Common DB remain disabled.
- Existing backend/frontend tests remain green.
- Guardrails pass.
