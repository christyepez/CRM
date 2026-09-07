# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 13 S13-02 - Activity Application Service and Foundation Store

Base Main Commit:
S13-01 merge commit required

Branch:
crm-sprint-13-s13-02-activity-application-service-foundation-store

Commit sugerido:
feat(crm): add activity application service and foundation store

PR title:
CRM Sprint 13 S13-02 - Activity Application Service and Foundation Store

Objetivo:
Implementar orquestación Application y store foundation in-memory para Activity / Follow-Up usando la policy de dominio S13-01.

Guardrails:
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
codex/prompts/sprint-13-activity-follow-up-s13-02.md

Acceptance Criteria:
- IActivityManagementService and ActivityManagementService are implemented.
- IActivityFoundationStore and InMemoryActivityFoundationStore are implemented.
- Create, update, complete and cancel orchestration reuse ActivityManagementPolicy.
- Changed=false suppresses writes.
- Portal/Common DB remain disabled.
- Productive Activity routes remain unavailable.
- Guardrails pass.
