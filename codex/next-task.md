# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 14 S14-05 - Opportunity Pipeline Test and Guardrail Hardening

Base Main Commit:
Sprint 14 S14-04 merge commit required

Branch:
crm-sprint-14-s14-05-opportunity-pipeline-test-guardrail-hardening

Commit sugerido:
test(crm): harden opportunity pipeline foundation guardrails

PR title:
CRM Sprint 14 S14-05 - Opportunity Pipeline Test and Guardrail Hardening

Objetivo:
Endurecer pruebas y guardrails cross-layer para Opportunity Pipeline foundation en `/foundation/opportunities` y `/api/crm/foundation/opportunities`.

Guardrails:
- No productive `/api/crm/opportunities`.
- No DELETE.
- No Portal Auth/Common DB/EF/schema/real data.
- No Lead conversion, Account Management runtime or assignment.
- Do not touch `crm-prod-sim`.

Prompt File:
codex/prompts/sprint-14-opportunity-pipeline-s14-05.md
