# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 11 S11-02 - Lead Qualification Application Service

Base Main Commit:
S11-01 merge commit required

Branch:
crm-sprint-11-s11-02-lead-qualification-application-service

Commit sugerido:
feat(crm): add lead qualification foundation service

PR title:
CRM Sprint 11 S11-02 - Lead Qualification Application Service

Objetivo:
Implementar el servicio de aplicación para Lead Qualification Foundation usando los contratos y reglas de dominio de S11-01, sin desbloquear rutas productivas ni activar integraciones runtime.

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
codex/prompts/sprint-11-lead-qualification-s11-02.md

Acceptance Criteria:
- Lead qualification application service exists.
- S11-01 contracts/domain rules are reused.
- Foundation scope remains explicit.
- Productive routes remain locked/404 by default.
- Existing and new tests remain green.
- Architecture/security guardrails pass.
