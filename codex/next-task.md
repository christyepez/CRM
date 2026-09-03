# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 12 P1 - Contact Management Functional Baseline and Backlog

Base Main Commit:
S11-07 merge commit required

Branch:
crm-sprint-12-p1-contact-management-functional-baseline-backlog

Commit sugerido:
docs(crm): plan contact management foundation slice

PR title:
CRM Sprint 12 P1 - Contact Management Functional Baseline and Backlog

Objetivo:
Planificar el slice Contact Management Foundation después del cierre de Sprint 11 Lead Qualification, sin implementar runtime nuevo todavía.

Guardrails:
- No productive `/api/crm/contacts` unlock.
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
codex/prompts/sprint-12-contact-management-p1.md

Acceptance Criteria:
- Sprint 11 Lead Qualification closure is used as input.
- Contact Management current state is reviewed.
- Sprint 12 backlog and first implementation story are defined.
- Productive routes remain locked/404 by default.
- Portal Auth, Common DB, SimulatedProduction and Production remain untouched.
