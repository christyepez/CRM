# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 11 S11-05 - Lead Qualification Test and Guardrail Hardening

Base Main Commit:
S11-04 merge commit required

Branch:
crm-sprint-11-s11-05-lead-qualification-test-guardrail-hardening

Commit sugerido:
test(crm): harden lead qualification foundation guardrails

PR title:
CRM Sprint 11 S11-05 - Lead Qualification Test and Guardrail Hardening

Objetivo:
Endurecer pruebas y guardrails cross-layer para Lead Qualification Foundation sin desbloquear rutas productivas ni activar integraciones runtime.

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
codex/prompts/sprint-11-lead-qualification-s11-05.md

Acceptance Criteria:
- Cross-layer lead qualification guardrails are hardened.
- Productive routes remain disabled.
- Foundation scope remains explicit.
- Productive routes remain locked/404 by default.
- Existing and new tests remain green.
- Architecture/security guardrails pass.
