# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 11 S11-06 - Lead Qualification Local Integration Validation

Base Main Commit:
S11-05 merge commit required

Branch:
crm-sprint-11-s11-06-lead-qualification-local-integration-validation

Commit sugerido:
test(crm): validate lead qualification local integration workflow

PR title:
CRM Sprint 11 S11-06 - Lead Qualification Local Integration Validation

Objetivo:
Validar localmente el flujo end-to-end de Lead Qualification Foundation entre Angular y CRM API, usando solo datos sintéticos y rutas foundation.

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
codex/prompts/sprint-11-lead-qualification-s11-06.md

Acceptance Criteria:
- Angular page and CRM foundation API run locally together.
- Synthetic qualify/disqualify/idempotent/error scenarios are validated.
- Frontend uses only foundation API routes.
- Productive routes remain locked/404 by default.
- Existing and new tests remain green.
- Architecture/security guardrails pass.
