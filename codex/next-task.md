# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 13 S13-01 - Activity Contracts and Domain Rules

Base Main Commit:
Sprint 13 P1 merge commit required

Branch:
crm-sprint-13-s13-01-activity-contracts-domain-rules

Commit sugerido:
feat(crm): add activity contracts and domain rules

PR title:
CRM Sprint 13 S13-01 - Activity Contracts and Domain Rules

Objetivo:
Implementar contratos y reglas determinísticas de dominio para Activity / Follow-Up foundation.

Guardrails:
- No Activity application service yet.
- No Activity API routes yet.
- No Angular Activity UI yet.
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
codex/prompts/sprint-13-activity-follow-up-s13-01.md

Acceptance Criteria:
- ActivityManagement contracts and domain policy are implemented.
- Activity requires one valid LeadId or ContactId target.
- Subject/details are bounded and normalized.
- Completion rules are deterministic.
- Portal/Common DB remain disabled.
- Productive Activity routes remain unavailable.
- Guardrails pass.
