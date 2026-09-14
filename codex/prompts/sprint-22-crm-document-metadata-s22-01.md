# CRM Sprint 22 S22-01 - CRM Document Metadata Contracts and Domain Rules

## Objective
Implement deterministic foundation-only domain contracts for CRM document metadata/reference records.

## Required
- Operations: Create, Update, Archive.
- Status: Active, Archived.
- RelatedEntityType: Customer, Contact, Lead, Opportunity, Case.
- Validate non-empty related GUID.
- Validate required FileReferenceId as opaque trimmed reference, max 300.
- Validate FileName required, trimmed, max 255.
- ContentType optional max 120.
- Description optional max 1000.
- Create starts Active; Archived is read-only; archive is idempotent.
- No binary content fields and no DELETE semantics.

## Guardrails
No Portal Content/File runtime call, no upload/download, no filesystem/blob storage, no Common DB/EF/SQL, no productive routes, no real data, no port 8094 or Production.
