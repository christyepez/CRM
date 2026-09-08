# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 14 S14-02 - Opportunity Application Service and Foundation Store

Base Main Commit:
Sprint 14 S14-01 merge commit required

Branch:
crm-sprint-14-s14-02-opportunity-application-service-foundation-store

Commit sugerido:
feat(crm): add opportunity application service and foundation store

PR title:
CRM Sprint 14 S14-02 - Opportunity Application Service and Foundation Store

Objetivo:
Implement foundation-only Opportunity application orchestration and in-memory persistence seam through `OpportunityPipelinePolicy`.

Guardrails:
- No Opportunity API routes yet.
- No Angular Opportunity UI yet.
- No productive `/api/crm/opportunities`.
- No DELETE.
- No Lead conversion or Account Management activation.
- No assignment/owner runtime.
- No Portal Auth runtime.
- No Common DB/EF/migrations/schema/SQL.
- No real data/secrets.
- Do not touch `crm-prod-sim`.

Prompt File:
codex/prompts/sprint-14-opportunity-pipeline-s14-02.md
