# Next Task

CRM Sprint 20 S20-01 - Note Contracts and Domain Rules

Branch: crm-sprint-20-s20-01-note-contracts-domain-rules
Prompt: codex/prompts/sprint-20-note-management-s20-01.md
Suggested commit: feat(crm): add note contracts and domain rules

## Guardrails
- Domain-only Note slice.
- Related entity ids are structural references only.
- No productive Note route or DELETE.
- No cross-entity mutation or Activity scheduling.
- Portal-owned audit, notifications, files/content, users/roles and configuration are not duplicated.
- No Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, 8094 or Production.