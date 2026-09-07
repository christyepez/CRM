# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 13 S13-05 - Activity / Follow-Up Test and Guardrail Hardening

Base Main Commit:
S13-04 merge commit required

Branch:
crm-sprint-13-s13-05-activity-test-guardrail-hardening

Commit sugerido:
test(crm): harden activity follow-up guardrails

PR title:
CRM Sprint 13 S13-05 - Activity / Follow-Up Test and Guardrail Hardening

Objetivo:
Endurecer pruebas y guardrails cross-layer para Activity / Follow-Up, validando contrato frontend/backend y límites foundation.

Guardrails:
- Activity UI and API must remain foundation-only.
- No productive Activity API activation.
- No DELETE.
- No Lead conversion.
- No Account Management activation.
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
codex/prompts/sprint-13-activity-follow-up-s13-05.md

Acceptance Criteria:
- Activity cross-layer tests and guardrails are hardened.
- Frontend/backend Activity type and status parity is verified.
- The frontend calls only `/api/crm/foundation/activities`.
- Productive Activity routes remain unavailable.
- DELETE is not added.
- Portal/Common DB remain disabled.
- Guardrails pass.
