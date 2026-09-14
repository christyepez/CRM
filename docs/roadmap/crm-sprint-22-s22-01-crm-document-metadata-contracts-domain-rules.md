# CRM Sprint 22 S22-01 - CRM Document Metadata Contracts and Domain Rules

Status: Implemented
Decision: FoundationOnly

Implemented deterministic domain contracts and policy for CRM document metadata/reference records.

Validated fields:
- RelatedEntityType / RelatedEntityId.
- FileReferenceId opaque reference, max 300.
- FileName required, max 255.
- ContentType optional, max 120.
- Description optional, max 1000.

Lifecycle:
- Create -> Active.
- Update only while Active.
- Archive -> Archived.
- Repeated archive is idempotent.
- Archived is read-only.

No binary content, upload/download, file storage, DELETE, Portal Content/File runtime, Common DB/EF/SQL, real data or Production was introduced.

Validation: Release build clean; 646 Unit + 165 Architecture = 811 tests PASS.
