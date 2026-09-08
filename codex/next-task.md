# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog

Base Main Commit:
S13-07 merge commit required

Branch:
crm-sprint-14-p1-opportunity-pipeline-functional-baseline

Commit sugerido:
docs(crm): baseline opportunity pipeline foundation sprint

PR title:
CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog

Objetivo:
Crear la linea base funcional y backlog de Sprint 14 para Opportunity Pipeline despues del cierre S13-07.

Guardrails:
- Opportunity UI and API must remain foundation-only until explicitly implemented in later Sprint 14 stories.
- No productive Opportunity API activation.
- No DELETE.
- No Lead conversion.
- No Account Management activation as part of Sprint 14 P1.
- No assignment, owner or Portal user feature activation.
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
codex/prompts/sprint-14-opportunity-pipeline-p1.md

Acceptance Criteria:
- Existing Opportunity, Lead, Contact and Activity evidence is inventoried.
- Opportunity Pipeline foundation terminology, lifecycle and backlog are defined.
- Exactly one first Sprint 14 implementation story is selected.
- Productive Opportunity routes remain unavailable.
- DELETE is not added.
- Portal/Common DB remain disabled.
- Lead conversion, Account activation and assignment remain deferred.
- Residual risks and dependencies are recorded.
- Guardrails pass.
