# CRM Sprint 23 S23-03 - CRM Tag Foundation API

Status: Implemented
S2303Decision: Implemented

Routes:
- GET /api/crm/foundation/tags
- GET /api/crm/foundation/tags/{id}
- POST /api/crm/foundation/tags
- PUT /api/crm/foundation/tags/{id}
- POST /api/crm/foundation/tags/{id}/archive

Mappings: validation 400, missing 404, archived update 409.
Safety flags keep productive CRUD, Portal identity runtime and cross-entity mutation disabled.
Productive /api/crm/tags and DELETE remain unavailable.

Validation: 674 Unit + 169 Architecture = 843 tests PASS; Release build clean.
