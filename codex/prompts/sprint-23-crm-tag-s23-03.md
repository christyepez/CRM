# CRM Sprint 23 S23-03 - CRM Tag Foundation API

Implement explicit DTOs and Foundation routes:
- GET /api/crm/foundation/tags
- GET /api/crm/foundation/tags/{id}
- POST /api/crm/foundation/tags
- PUT /api/crm/foundation/tags/{id}
- POST /api/crm/foundation/tags/{id}/archive

Map validation to 400, missing to 404, archived update to 409.
Expose Foundation safety flags. No productive /api/crm/tags and no DELETE.
No Portal identity/config runtime, no related-entity mutation, no Common DB/EF/SQL/real data/connectors/Production.
