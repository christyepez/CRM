# CRM Sprint 11 Selected Slice Architecture

SelectedSprint11SliceId: S11-LEAD-QUAL
SelectedSprint11SliceName: Lead Intake and Qualification Foundation

ExpectedBackendFiles:

- src/CRM.Domain/Entities/Lead.cs
- src/CRM.Domain/Enums/CrmStatuses.cs
- src/CRM.Application/Foundation/FoundationLeadCrudContracts.cs
- src/CRM.Application/Foundation/FoundationLeadCrudService.cs
- optional: src/CRM.Application/Foundation/FoundationLeadQualificationContracts.cs
- optional: src/CRM.Application/Foundation/FoundationLeadQualificationService.cs
- src/CRM.Api/Program.cs foundation route registration

ExpectedTests:

- tests/CRM.UnitTests/*Lead* tests
- tests/CRM.ArchitectureTests/*Lead* tests

## API contract direction

| Method | Route | Purpose | Auth expectation | Persistence |
| --- | --- | --- | --- | --- |
| GET | `/api/crm/foundation/leads` | list foundation leads | foundation permission simulation | foundation store |
| POST | `/api/crm/foundation/leads` | create foundation lead | foundation permission simulation | foundation store |
| PUT | `/api/crm/foundation/leads/{id}` | update foundation lead | foundation permission simulation | foundation store |
| POST | `/api/crm/foundation/leads/{id}/qualify` | qualify lead | foundation permission simulation | foundation store |
| POST | `/api/crm/foundation/leads/{id}/disqualify` | disqualify lead | foundation permission simulation | foundation store |

Productive `/api/crm/leads` remains out of scope and locked.

## Data model direction

Minimum lead qualification fields:

- LeadId
- Status
- QualificationScore
- Source
- DisqualificationReason
- Owner/advisor reference as optional logical value
- Notes as optional sanitized text

No schema migration in S11-01.

## Security

- No CRM-owned Identity.
- No Portal Auth runtime activation.
- No Authorization header/token reads by default.
- No secrets.
- No DELETE.
- Input validation and output filtering required.
- PII fields should be minimal and sanitized in logs/tests.

Sprint11FrontendIncluded: true
FrontendScope: local development foundation page/service only after API readiness.
