# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 11 S11-03 - Lead Qualification API Foundation Endpoints

Base Main Commit:
S11-02 merge commit required

Branch:
crm-sprint-11-s11-03-lead-qualification-api-foundation-endpoints

Commit sugerido:
feat(crm): add lead qualification foundation endpoints

PR title:
CRM Sprint 11 S11-03 - Lead Qualification API Foundation Endpoints

Objetivo:
Exponer endpoints foundation-only para Lead Qualification usando el servicio de aplicación de S11-02, sin desbloquear rutas productivas ni activar integraciones runtime.

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
codex/prompts/sprint-11-lead-qualification-s11-03.md

Acceptance Criteria:
- Lead qualification foundation endpoint exists.
- S11-02 application service is reused.
- Foundation scope remains explicit.
- Productive routes remain locked/404 by default.
- Existing and new tests remain green.
- Architecture/security guardrails pass.
