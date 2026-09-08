# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 14 S14-04 - Opportunity Pipeline Frontend Foundation Page

Base Main Commit:
Sprint 14 S14-03 merge commit required

Branch:
crm-sprint-14-s14-04-opportunity-pipeline-frontend-foundation-page

Commit sugerido:
feat(crm): add opportunity pipeline frontend foundation page

PR title:
CRM Sprint 14 S14-04 - Opportunity Pipeline Frontend Foundation Page

Objetivo:
Implementar `/foundation/opportunities` consumiendo exclusivamente Opportunity foundation APIs para list/create/edit/progress/win/lose/cancel.

Guardrails:
- No productive `/api/crm/opportunities`.
- No DELETE.
- No Portal Auth/Common DB/EF/schema/real data.
- No Lead conversion, Account Management runtime or assignment.
- Do not touch `crm-prod-sim`.

Prompt File:
codex/prompts/sprint-14-opportunity-pipeline-s14-04.md
