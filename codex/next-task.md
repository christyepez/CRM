# Next Task

CRM Sprint 19 S19-01 - Interaction Contracts and Domain Rules

Branch: crm-sprint-19-s19-01-interaction-contracts-domain-rules
Prompt: codex/prompts/sprint-19-interaction-management-s19-01.md
Suggested commit: feat(crm): add interaction contracts and domain rules

## Guardrails
- Domain-only Interaction slice.
- No productive Interaction route or DELETE.
- Do not duplicate Activity scheduling/follow-up semantics.
- Related CRM ids are structural references only; no cross-entity mutation.
- Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, 8094 and Production remain disabled.
