# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 14 S14-03 - Opportunity Foundation API

Base Main Commit:
Sprint 14 S14-02 merge commit required

Branch:
crm-sprint-14-s14-03-opportunity-foundation-api

Commit sugerido:
feat(crm): add opportunity foundation api

PR title:
CRM Sprint 14 S14-03 - Opportunity Foundation API

Objetivo:
Exponer Opportunity Pipeline únicamente bajo `/api/crm/foundation/opportunities` mediante `IOpportunityManagementService`, con DTO mapping explícito y errores HTTP seguros.

Guardrails:
- No productive `/api/crm/opportunities`.
- No DELETE.
- No Lead conversion / Account Management activation / assignment runtime.
- No Portal Auth/token/header runtime.
- No Common DB/EF/migrations/schema/SQL.
- No real data/secrets.
- Do not touch `crm-prod-sim`.

Prompt File:
codex/prompts/sprint-14-opportunity-pipeline-s14-03.md
