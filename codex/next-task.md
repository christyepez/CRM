# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 11 S11-04 - Lead Intake Frontend Foundation Page

Base Main Commit:
S11-03 merge commit required

Branch:
crm-sprint-11-s11-04-lead-intake-frontend-foundation-page

Commit sugerido:
feat(crm): add lead qualification foundation frontend

PR title:
CRM Sprint 11 S11-04 - Lead Intake Frontend Foundation Page

Objetivo:
Implementar una página Angular 18 foundation-only para Lead Intake and Qualification usando el endpoint S11-03, sin desbloquear rutas productivas ni activar integraciones runtime.

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
codex/prompts/sprint-11-lead-qualification-s11-04.md

Acceptance Criteria:
- Lead intake/qualification foundation page exists.
- S11-03 foundation endpoint is consumed safely.
- Foundation scope remains explicit.
- Productive routes remain locked/404 by default.
- Existing and new tests remain green.
- Architecture/security guardrails pass.
