# CRM Sprint 22 S22-04 - CRM Document Metadata Frontend Foundation Page

## Objective
Add a foundation-only Angular page for CRM Document metadata/reference management.

## Required
- Route `/foundation/documents`.
- API base `/api/crm/foundation/documents` only.
- List/detail/create/update/archive metadata.
- Fields: related entity type/id, display name, content type, file reference id, status.
- Archived metadata is read-only.
- Safe 400/404/409 messages.

## Guardrails
No file picker, upload/download, bytes/streams, blob/local storage, productive route, DELETE, Portal Content/File runtime, Common DB, real data or Production.
