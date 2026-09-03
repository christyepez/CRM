# CRM Sprint 12 S12-02 - Contact Application Service

## Summary

S12-02 adds the Contact Management application orchestration layer over the existing foundation Contact store and the S12-01 deterministic domain policy.

ContactManagementImplementationStatus: ApplicationServiceImplemented

ContactManagementDomain: Implemented

ContactManagementApplicationService: Implemented

ContactManagementApi: ExistingFoundationAwaitingControlledWiring

ContactManagementFrontend: NotImplemented

ProductiveContactRouteEnabled: false

PortalRuntimeEnabled: false

CommonDbRuntimeEnabled: false

LeadContactRuntimeImplemented: false

NoChangePersistenceSuppressed: true

S1202Decision: Implemented

## Service architecture

Application interface:

- `src/CRM.Application/ContactManagement/IContactManagementService.cs`

Application implementation:

- `src/CRM.Application/ContactManagement/ContactManagementService.cs`

Application contracts:

- `ContactManagementCreateApplicationRequest`
- `ContactManagementUpdateApplicationRequest`
- `ContactManagementApplicationResult`
- `ContactManagementApplicationContact`

Store reuse:

- ContactStoreInterface: `IContactFoundationStore`
- ContactStoreImplementation: `InMemoryContactFoundationStore`
- PersistenceClassification: `FoundationOnly / NonProductionSeam`

## Create flow

1. Receive application create request.
2. Build `ContactManagementCommand`.
3. Evaluate through `ContactManagementPolicy`.
4. Reject invalid input with deterministic safe result.
5. Generate a foundation Contact id.
6. Persist exactly one preview record through `IContactFoundationStore.SavePreviewAsync`.
7. Return normalized Contact representation.

CreateWriteCount:

- Valid create: 1.
- Invalid create: 0.

## Update flow

1. Validate operation by loading current Contact preview using `IContactFoundationStore.GetPreviewByIdAsync`.
2. Return deterministic `ContactNotFound` if missing.
3. Convert existing preview into `ContactManagementSnapshot`.
4. Evaluate through `ContactManagementPolicy`.
5. Reject invalid update without writing.
6. Suppress persistence when `Changed=false`.
7. Save exactly one updated preview when `Changed=true`.

ChangedUpdateWriteCount: 1

NoChangeUpdateWriteCount: 0

InvalidUpdateWriteCount: 0

NotFoundUpdateWriteCount: 0

## Policy delegation

ContactManagementPolicyInvoked: true

DomainRulesDuplicatedInApplication: false

Application orchestration does not duplicate name, email, phone, preferred method or AccountId validation rules. It maps requests, invokes the domain policy, and uses only policy-normalized values for persistence.

## Normalization

NormalizationPropagation: true

Persisted preview records use normalized values returned by `ContactManagementPolicy`:

- trimmed Name
- lower-case Email
- trimmed Phone
- trimmed Role
- validated optional AccountId

## Not-found handling

Expected missing Contact update returns safe application result:

- `Allowed=false`
- `Changed=false`
- `ErrorCode=ContactNotFound`

No raw exception, stack trace, store name or PII is exposed.

## Security boundaries

SecurityReview: PASS

- No Contact Name/Email/Phone logging added.
- No Authorization header/token parsing added.
- No Portal Auth runtime activated.
- No Common DB runtime activated.
- No connection string or secret added.
- No schema, migration or SQL Server added.

## API compatibility

Existing foundation routes remain unchanged:

- `POST /api/crm/foundation/contacts/preview`
- `GET /api/crm/foundation/contacts`
- `GET /api/crm/foundation/contacts/{id}`
- `POST /api/crm/foundation/contacts`
- `PUT /api/crm/foundation/contacts/{id}`
- `GET /api/crm/foundation/contacts/read-model-preview`

S12-03 will decide controlled route wiring through `IContactManagementService`.

## S12-03 entry criteria

- Application service exists and is tested.
- Foundation store seam is reused.
- S12-01 policy remains authoritative.
- Write-count behavior is covered.
- Foundation CRUD compatibility remains green.
- Productive routes remain locked.
- Portal/Common DB runtime remains disabled.
