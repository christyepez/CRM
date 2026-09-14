# CRM Sprint 24 S24-03 - Assignment Reference Foundation API

Status: Implemented locally

## Routes
- GET /api/crm/foundation/assignments
- GET /api/crm/foundation/assignments/{id}
- POST /api/crm/foundation/assignments
- PUT /api/crm/foundation/assignments/{id}
- POST /api/crm/foundation/assignments/{id}/archive

## Guardrails
- `/api/crm/assignments` remains unavailable.
- DELETE remains unavailable.
- PortalIdentityRuntimeEnabled=false.
- CrossEntityMutationEnabled=false.
- AssigneeReferenceId stays opaque; no Portal Security lookup.