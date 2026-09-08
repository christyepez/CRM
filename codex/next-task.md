# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation

Base Main Commit:
S13-05 merge commit required

Branch:
crm-sprint-13-s13-06-activity-local-integration-validation

Commit sugerido:
test(crm): validate activity follow-up local integration

PR title:
CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation

Objetivo:
Validar localmente Activity / Follow-Up de extremo a extremo dentro del stack foundation-only.

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
codex/prompts/sprint-13-activity-follow-up-s13-06.md

Acceptance Criteria:
- Activity local integration is validated with synthetic data.
- The frontend and API foundation Activity workflow remain aligned.
- Productive Activity routes remain unavailable.
- DELETE is not added.
- Portal/Common DB remain disabled.
- Guardrails pass.
