# CRM Sprint 22 S22-02 - CRM Document Metadata Application Service and Foundation Store

## Objective
Add application orchestration and a deterministic in-memory foundation store for CRM document metadata/reference records.

## Required
- Application request/result contracts.
- `IDocumentMetadataService` and implementation.
- `IDocumentMetadataFoundationStore` persistence seam.
- Synthetic seed only; no real file/content access.
- List/detail/create/update/archive operations.
- Suppress persistence when `Changed=false`.
- Surface `FoundationOnly`, `ProductiveCrudEnabled=false`, `BinaryStorageEnabled=false`, `PortalContentRuntimeEnabled=false`.
- DI registration and focused Unit/Architecture tests.

## Guardrails
No binary bytes, upload/download, local filesystem/blob storage, Portal Content/File runtime, generic file management, DELETE, Common DB/EF/SQL, productive routes, real data, port 8094 or Production.
