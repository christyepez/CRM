# CRM Sprint 22 S22-03 - CRM Document Metadata Foundation API

## Objective
Expose foundation-only CRM document metadata/reference endpoints.

## Routes
- GET `/api/crm/foundation/documents`
- GET `/api/crm/foundation/documents/{id}`
- POST `/api/crm/foundation/documents`
- PUT `/api/crm/foundation/documents/{id}`
- POST `/api/crm/foundation/documents/{id}/archive`

## Required
- Explicit request/response DTOs.
- 400 validation, 404 missing, 409 archived update.
- Responses expose FoundationOnly and binary/Portal runtime flags as false.
- Productive `/api/crm/documents` remains unavailable.
- No DELETE route.

## Guardrails
Metadata/reference only; no binary payload, upload/download, filesystem/blob storage, Portal Content/File runtime, Common DB/EF/SQL, real data, port 8094 or Production.
