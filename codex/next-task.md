# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 12 S12-05 - Contact Management Test and Guardrail Hardening

Base Main Commit:
S12-04 merge commit required

Branch:
crm-sprint-12-s12-05-contact-management-test-guardrail-hardening

Commit sugerido:
test(crm): harden contact management guardrails

PR title:
CRM Sprint 12 S12-05 - Contact Management Test and Guardrail Hardening

Objetivo:
Endurecer pruebas y guardrails cross-layer de Contact Management después de S12-01 a S12-04.

Guardrails:
- No productive `/api/crm/contacts` unlock.
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
codex/prompts/sprint-12-contact-management-s12-05.md

Acceptance Criteria:
- Contact Management cross-layer tests and guardrails are hardened.
- Existing Contact foundation CRUD remains compatible.
- Productive Contact route remains unavailable.
- New domain tests pass.
- Existing backend/frontend tests remain green.
- Guardrails pass.
- S12-02 prompt is prepared.
