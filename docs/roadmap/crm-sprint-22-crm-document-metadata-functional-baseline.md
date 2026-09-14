# CRM Sprint 22 - CRM Document Metadata Reference Foundation

Status: FunctionalBaselineDefined
SelectedSliceId: S22-CRM-DOCUMENT-METADATA
SelectedSliceName: CRM Document Metadata Reference Foundation

## Capability boundary
CRM owns document metadata/reference records only. Generic file storage, upload/download, content scanning, retention and binary lifecycle remain owned by Portal Content/File API.

## P1 contract
- Id: foundation-generated GUID.
- RelatedEntityType: Customer, Contact, Lead, Opportunity, Case.
- RelatedEntityId: required non-empty GUID structural reference.
- FileReferenceId: required non-empty opaque Portal content/file reference.
- FileName: required, trimmed, max 255.
- ContentType: optional, trimmed, max 120.
- Description: optional, trimmed, max 1000.
- Status: Active or Archived.

## Lifecycle
- Create starts Active.
- Update allowed only while Active.
- Archive Active -> Archived.
- Repeat archive succeeds with Changed=false.
- Archived records are read-only.
- No DELETE.
## Portal ownership boundary
- Portal Content/File API is ADAPT/REUSE for binary content.
- S22 does not call Portal runtime yet.
- No binary bytes, blob paths, local filesystem storage, upload streams or download endpoints in CRM.
- No audit, notifications, users, roles or assignments runtime.

## Foundation routes planned
- `/api/crm/foundation/documents`
- `/api/crm/foundation/documents/{id}`
- `/api/crm/foundation/documents/{id}/archive`
- Productive `/api/crm/documents` remains unavailable.

## Backlog
- S22-01 Contracts and Domain Rules.
- S22-02 Application Service and Foundation Store.
- S22-03 Foundation API.
- S22-04 Angular Foundation Page.
- S22-05 Test and Guardrail Hardening.
- S22-06 Local HTTP Integration Validation.
- S22-07 Sprint Closure and next capability selection.

## Safety
FoundationOnly; synthetic metadata only; no DELETE; no Portal runtime; no Common DB/EF/SQL; no external connectors; no real data; no crm-prod-sim; no port 8094; no Production.
