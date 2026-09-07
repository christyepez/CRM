# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline and Backlog

Base Main Commit:
S12-07 merge commit required

Branch:
crm-sprint-13-p1-activity-follow-up-functional-baseline

Commit sugerido:
docs(crm): add activity follow-up functional baseline

PR title:
CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline and Backlog

Objetivo:
Definir la capacidad Activity / Follow-Up como siguiente slice foundation-only posterior al cierre de Sprint 12 Contact Management.

Guardrails:
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
codex/prompts/sprint-13-activity-follow-up-p1.md

Acceptance Criteria:
- Activity / Follow-Up baseline and backlog are documented.
- Contracts, risks, guardrails and implementation sequence are defined.
- Productive Activity routes remain unavailable.
- Portal/Common DB remain disabled.
- No runtime business functionality is activated.
- Guardrails pass.
