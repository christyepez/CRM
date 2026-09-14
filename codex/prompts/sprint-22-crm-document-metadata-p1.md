# CRM Sprint 22 P1 - CRM Document Metadata Reference Functional Baseline

Define a CRM-owned metadata/reference slice for `CrmDocument`.

Portal boundary: generic binary/file management belongs to Portal Content/File API and must not be duplicated.

Required baseline:
- Foundation-only synthetic metadata/reference records.
- Structural RelatedEntity reference only; no cross-entity mutation.
- Portal ContentReferenceId is metadata only; no file upload/download/runtime call yet.
- No binary/blob/file storage in CRM.
- No productive routes or DELETE.
- No Portal runtime, Common DB/EF/SQL, real data or Production.
- Define S22-01..S22-07 backlog and first implementation prompt.
