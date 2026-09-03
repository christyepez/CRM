# CRM Sprint 12 P1 - Contact Management Functional Baseline

## Summary

Sprint 12 starts a new business capability: `S12-CONTACT-MGMT` Contact Management Foundation.

P1 inspected the current repository state and confirms Contact is already partially implemented as a foundation capability, but it does not yet have the explicit Contact Management domain contracts and deterministic rules needed for a focused Sprint 12 implementation sequence.

## Base

- Sprint 11 closure PR: #153
- Sprint 11 closure merge commit: `5097cfebf9042d5b2d531390ab42adc9c46842cb`
- Sprint 12 P1 base main commit: `5097cfebf9042d5b2d531390ab42adc9c46842cb`

## Contact source inventory

| Area | Evidence | Status |
| --- | --- | --- |
| Domain entity | `src/CRM.Domain/Entities/ConceptualEntities.cs` | Exists, conceptual/shared file |
| Domain enums | `src/CRM.Domain/Enums/CrmStatuses.cs` | Exists |
| Value objects | `src/CRM.Domain/ValueObjects/FoundationValueObjects.cs`, common value objects | Exists |
| Preview service | `src/CRM.Application/Foundation/ContactFoundationService.cs` | Exists |
| Foundation CRUD contracts | `src/CRM.Application/Foundation/FoundationContactCrudContracts.cs` | Exists |
| Foundation CRUD service | `src/CRM.Application/Foundation/FoundationContactCrudService.cs` | Exists |
| Foundation store port | `src/CRM.Application/Ports/Persistence/IContactFoundationStore.cs` | Exists |
| In-memory store | `src/CRM.Infrastructure/Persistence/Foundation/InMemoryContactFoundationStore.cs` | Exists |
| Read model preview | `src/CRM.Application/ReadModels/ReadModelPreviewServices.cs` | Exists |
| API routes | `src/CRM.Api/Program.cs` | Foundation routes exist |
| Frontend | `frontend/crm-web/src/main.ts` | Dashboard references only; no dedicated Contact page |
| Tests | `tests/CRM.UnitTests/FoundationContactCrudServiceTests.cs`, `LeadAccountContactFoundationTests.cs`, read-model and architecture tests | Basic/foundation coverage exists |

## Domain inventory

Contact entity/model exists: true.

Existing properties:

- `Id`
- `Name`
- `Email`
- `Phone`
- `Role`
- `AccountId`
- `PreferredContactMethod`
- `Status`

Existing identifiers:

- Uses `CrmId`.

Existing enums:

- `ContactStatus`: `Draft`, `Active`, `Inactive`
- `PreferredContactMethod`: `NotSpecified`, `Email`, `Phone`

Existing value objects:

- `PersonName`
- `EmailAddress`
- `PhoneNumber`
- `ContactRole`
- `CrmId`

Existing validation rules:

- Preferred method `Email` requires an email.
- Preferred method `Phone` requires a phone.
- `AssignToAccount` activates the contact.
- `UpdateContactPreferences` validates preferred method requirements.

Relationships:

- Contact -> Account exists through nullable `AccountId`.
- Account -> Contact exists through `Account.ContactReferences`.
- Contact -> Lead does not exist.
- Contact -> Opportunity does not exist.
- Contact -> Activity does not exist.

## Current Contact status

ContactDomainStatus: PartiallyImplemented

ContactApplicationStatus: FoundationOnly

ContactPersistenceArchitecture: Foundation/NonProduction seam

ContactApiStatus: FoundationImplemented

ContactFrontendStatus: DashboardReferenceOnly

ReadyForNextSlice: true

## Current data model

Existing fields:

- ContactId through `Id` / `CrmId`
- FirstName and LastName through `PersonName` and foundation DTOs
- Email
- Phone
- Title/Role through foundation DTO metadata and `ContactRole`
- Status
- AccountId in domain only
- PreferredContactMethod in domain/preview

Missing fields required for foundation:

- Explicit Contact Management command/result contracts.
- Explicit validation/error code model.
- Optional `SourceLeadId` contract decision for later lead linkage.
- Dedicated domain policy for create/update/preference changes.

Fields not needed yet:

- Mobile as a separate phone type.
- Consent/marketing preferences.
- Owner/user assignment.
- Segmentation metadata.
- External IDs.
- Audit/storage IDs.
- CreatedAt/UpdatedAt durable fields.

## Lead relationship decision

LeadContactRelationshipExists: false

LeadContactDecision: ContractOnlyLater

Sprint 12 should not become Lead Conversion. A future story may define a `SourceLeadId` or equivalent metadata contract, but the first implementation story should focus on Contact domain rules.

## Account relationship decision

AccountRelationshipRequiredForFoundation: false

The domain already supports assigning a contact to an account, but basic Contact Management Foundation can create, view and update contact identity/preferences without requiring Account Management as a dependency.

## API inventory

| Method | Route | Classification | Service | Persistence | Auth |
| --- | --- | --- | --- | --- | --- |
| POST | `/api/crm/foundation/contacts/preview` | Preview | `ContactFoundationService` | None | None |
| GET | `/api/crm/foundation/contacts` | Foundation | `FoundationContactCrudService` | In-memory foundation store | Foundation simulation |
| GET | `/api/crm/foundation/contacts/{id}` | Foundation | `FoundationContactCrudService` | In-memory foundation store | Foundation simulation |
| POST | `/api/crm/foundation/contacts` | Foundation | `FoundationContactCrudService` | In-memory foundation store | Foundation simulation |
| PUT | `/api/crm/foundation/contacts/{id}` | Foundation | `FoundationContactCrudService` | In-memory foundation store | Foundation simulation |
| GET | `/api/crm/foundation/contacts/read-model-preview` | Preview | `ContactReadModelPreviewService` | Mock/read-model preview | None |
| GET/POST/PUT | `/api/crm/contacts` | Productive | Not registered by default | None | Not active |

ProductiveContactRouteEnabled: false

Productive contact routes remain 404 by default and may only become 423 under explicit non-production locked-route flags from previous governance work.

## Frontend inventory

ContactPageExists: false

ContactComponentsExist: false

ContactApiServiceExists: false

ContactRouteExists: false

ContactMocksExist: dashboard/status references only.

CurrentFrontendCapability: DashboardReferenceOnly

Sprint12FrontendIncluded: true

Frontend inclusion is recommended because Sprint 11 proved the local Angular foundation workflow pattern. Contact Management can reuse that controlled development-only style without productive route activation.

## Functional boundary selected

Sprint12FunctionalScope:

- Contact identity/details contracts.
- Deterministic create/update/preference rules.
- Contact list/search foundation API.
- Contact detail foundation API.
- Angular foundation page `/foundation/contacts`.
- Synthetic/foundation persistence only.
- Backend, frontend, architecture and local integration tests.

Sprint12OutOfScope:

- Productive `/api/crm/contacts`.
- DELETE.
- Real DB persistence, EF runtime, migrations or schema changes.
- Portal Auth runtime, login/logout or CRM-owned Identity.
- Contact deduplication engine.
- Master Data Management.
- Consent management.
- Marketing automation.
- Bulk import/export.
- Lead conversion.
- Account Management dependency.
- SimulatedProduction, real Production or Azure activation.

## Security requirements

- Treat Contact data as PII even when synthetic.
- Do not log PII.
- Use explicit DTO mapping.
- Bound free-text fields.
- Return safe errors only.
- Do not allow mass assignment.
- Do not use token storage.
- Do not read Authorization headers by default.
- Use Angular interpolation/forms safely; no unsafe DOM injection.
- Use synthetic data only.

## Test inventory and gaps

CurrentContactTests:

- `LeadAccountContactFoundationTests` covers basic Contact domain behavior.
- `FoundationContactCrudServiceTests` covers foundation CRUD basics.
- `FoundationPreviewServiceTests` covers Contact preview.
- `ReadModelPreviewServiceTests` covers Contact read-model preview.
- Architecture tests guard foundation routes and productive-route absence.

ContactTestGaps:

- Dedicated Contact domain policy tests.
- Contact command/result/error code tests.
- Application service orchestration tests.
- API contract mapping tests for future Contact Management endpoint semantics.
- Dedicated Angular contact page tests.
- Local integration runner for Contact workflow.

## P1 decision

Sprint12P1Decision: ReadyForS1201ContactContractsAndDomainRules

FirstImplementationStoryId: S12-01

FirstImplementationStoryName: Contact Contracts and Domain Rules

FirstImplementationStoryRationale: Existing Contact capability is sufficient for foundation CRUD, but the next safe step is to make Contact behavior explicit and deterministic in Domain before adding new application/API/UI behavior.
