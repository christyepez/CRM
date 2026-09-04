# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 12 S12-06 - Contact Management Local Integration Validation

Base Main Commit:
S12-05 merge commit required

Branch:
crm-sprint-12-s12-06-contact-management-local-integration-validation

Commit sugerido:
test(crm): validate contact management local integration

PR title:
CRM Sprint 12 S12-06 - Contact Management Local Integration Validation

Objetivo:
Validar localmente el flujo end-to-end Angular -> API foundation de Contact Management después de S12-05.

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

Prompt File:
codex/prompts/sprint-12-contact-management-s12-06.md

Acceptance Criteria:
- CRM API runs locally for foundation Contact routes.
- Angular frontend runs locally and calls only `/api/crm/foundation/contacts`.
- Contact list/create/edit/no-change/invalid/not-found/read-after-write are validated with synthetic data.
- Proxy/CORS behavior is validated.
- Productive Contact route remains unavailable.
- Existing backend/frontend tests remain green.
- Guardrails pass.
