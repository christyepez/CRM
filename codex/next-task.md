# Next Task

CRM Sprint 20 S20-04 - Note Frontend Foundation Page

Branch: crm-sprint-20-s20-04-note-frontend-foundation-page
Prompt: codex/prompts/sprint-20-note-management-s20-04.md
Suggested commit: feat(crm): add note foundation page

## Guardrails
- Foundation-only Note frontend slice.
- Consume only `/api/crm/foundation/notes`.
- No productive Note route or DELETE.
- No cross-entity mutation or Activity scheduling.
- Portal-owned audit, notifications, files/content, users/roles and configuration are not duplicated.
- No Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, 8094 or Production.