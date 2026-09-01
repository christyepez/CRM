# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 11 S11-07 - Lead Qualification Sprint Closure

Base Main Commit:
S11-06 merge commit required

Branch:
crm-sprint-11-s11-07-lead-qualification-sprint-closure

Commit sugerido:
docs(crm): close lead qualification foundation slice

PR title:
CRM Sprint 11 S11-07 - Lead Qualification Sprint Closure

Objetivo:
Cerrar formalmente el slice Lead Qualification Foundation de Sprint 11 después de validar contratos, dominio, aplicación, API foundation, frontend foundation, pruebas, guardrails e integración local S11-06.

Guardrails:
- No redeploy/restart/rollback de `crm-prod-sim`.
- No P51 ni nuevos gates de producción.
- No real Production/Azure activation.
- No productive `/api/crm/leads` unlock.
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
codex/prompts/sprint-11-lead-qualification-s11-07.md

Acceptance Criteria:
- S11-01 through S11-06 evidence is reviewed.
- Lead Qualification Foundation closure is documented.
- Productive routes remain locked/404 by default.
- Portal Auth, Common DB, SimulatedProduction and Production remain untouched.
- Existing verification suite remains green.
- Next functional slice candidates are proposed.
