# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 11 S11-01 - Lead Qualification Contracts and Domain Rules

Base Main Commit:
b2d09708ada9db76b6e125c22f1b976e3ec2ae4a

Branch:
crm-sprint-11-s11-01-lead-qualification-contracts-domain-rules

Commit sugerido:
feat(crm): add lead qualification foundation contracts

PR title:
CRM Sprint 11 S11-01 - Lead Qualification Contracts and Domain Rules

Objetivo:
Implementar contratos y reglas de dominio para Lead Qualification Foundation, primer slice funcional de Sprint 11, sin desbloquear rutas productivas ni activar integraciones runtime.

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
codex/prompts/sprint-11-lead-qualification-s11-01.md

Acceptance Criteria:
- Lead qualification contracts/domain rules exist.
- Foundation scope remains explicit.
- Productive routes remain locked/404 by default.
- Existing 281 tests remain green.
- Architecture/security guardrails pass.
