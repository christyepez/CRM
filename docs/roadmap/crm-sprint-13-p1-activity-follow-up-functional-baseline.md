# CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline

Repository: christyepez/CRM

Task: CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline and Backlog

Sprint12ClosurePullRequest: #169

Sprint12ClosureMergeCommit: 25871b9f43a58bfc8c8da785a5642af8c81ac84a

Sprint13P1BaseMainCommit: 25871b9f43a58bfc8c8da785a5642af8c81ac84a

## Decisions

CanonicalActivityTerm: Activity

CanonicalFollowUpTerm: Follow-Up

ActivityDomainStatus: FoundationOnly

ActivityApplicationStatus: NotStarted

ActivityPersistenceArchitecture: NotStarted; target FoundationOnly / NonProductionSeam

ActivityApiStatus: NotStarted

ActivityFrontendStatus: NotStarted

ActivityLeadRelationshipExists: false

ActivityLeadRelationshipDecision: RequiredForFoundation

ActivityContactRelationshipExists: false

ActivityContactRelationshipRequiredForFoundation: true

AccountRelationshipRequiredForFoundation: false

OpportunityRelationshipDecision: Deferred

ActivityTypeDecision: Reuse existing enum values Call, Email, Meeting, Task; model Follow-Up as business semantics over Activity instead of adding another type in P1.

ActivityStatusDecision: Reuse Scheduled, Completed, Cancelled; keep lifecycle deterministic and small.

FollowUpModelDecision: Follow-Up is an Activity with a scheduled/due date and Lead or Contact target, not a separate aggregate.

ActivityCompletionDecision: Completion is an explicit action; only Scheduled activities may complete; completion sets CompletedAtUtc. Outcome/note should be optional in the first foundation story and bounded when added.

ActivityAssignmentDecision: Deferred until Portal Auth/user context exists; no CRM Identity or token dependency.

ProductiveActivityRouteEnabled: false

Sprint13FrontendIncluded: true

DeleteBehaviorAllowed: false

PortalRuntimeEnabled: false

CommonDbRuntimeEnabled: false

SimulatedProductionTouched: false

Sprint13P1Decision: ReadyForS1301ActivityContractsAndDomainRules

## Source Inventory

### Domain

- `src/CRM.Domain/Entities/Activity.cs`: `Activity` aggregate-like entity with `Id`, `Type`, `Subject`, `ScheduledAtUtc`, `CompletedAtUtc`, `Status`, `Schedule(...)` and `Complete(...)`.
- `src/CRM.Domain/Enums/CrmStatuses.cs`: `ActivityType` values `Call`, `Email`, `Meeting`, `Task`; `ActivityStatus` values `Scheduled`, `Completed`, `Cancelled`.
- `src/CRM.Domain/Events/CrmDomainEvents.cs`: `FollowUpScheduledDomainEvent`.
- `src/CRM.Domain/Entities/ConceptualEntities.cs`: `Note` as conceptual related entity record; Contact/Account concepts already exist separately.
- `docs/domain/crm-domain-model.md`: Activity described as scheduled commercial follow-up; Opportunity may have Activities in older conceptual model.
- `docs/domain/crm-business-rules.md`: Activity can be completed only when scheduled.

### Application

- `src/CRM.Application/Contracts/CrmDomainCatalogService.cs`: Activity appears in catalog with fields `Id`, `Type`, `Subject`, `Status` and behavior `Complete`.
- Reporting services include Activity KPIs/read-model names.
- No `ActivityManagement` application contracts/service found.
- No `IActivityFoundationStore` found.

### Infrastructure

- Existing foundation stores exist for Lead, Account and Contact only.
- No Activity foundation store found.
- Common DB logical model lists Activities, but runtime is disabled and must stay disabled.

### API

FoundationActivityRoutes: none

No `/api/crm/foundation/activities` routes found.

No productive `/api/crm/activities` route found.

Expected conceptual foundation routes for later stories:

- `GET /api/crm/foundation/activities`
- `GET /api/crm/foundation/activities/{id}`
- `POST /api/crm/foundation/activities`
- `PUT /api/crm/foundation/activities/{id}`
- `POST /api/crm/foundation/activities/{id}/complete`

### Frontend

- Angular foundation pages exist for Lead and Contact.
- No `/foundation/activities` route or Activity service/model found.
- Sprint 13 should include a frontend foundation page once API exists.

### Tests

CurrentActivityTests:

- `tests/CRM.UnitTests/DomainCoreTests.cs`: `Activity_Complete_MarksActivityAsCompleted`.

ActivityTestGaps:

- ActivityManagement policy tests.
- Type/status validation tests.
- Lead/Contact target validation tests.
- bounded subject/details tests.
- no-change update tests.
- completion invalid-state tests.
- application service tests.
- API route tests.
- frontend verifier tests.
- guardrails for no productive `/api/crm/activities`, no DELETE, no Portal/Common DB runtime.

## Existing Activity Fields

ExistingActivityFields: Id, Type, Subject, ScheduledAtUtc, CompletedAtUtc, Status

MissingFieldsRequiredForFoundation: LeadId, ContactId, Details, Outcome, UpdatedAtUtc

FieldsDeferred: AccountId, OpportunityId, AssignedTo, Priority, ReminderAt, Channel, NextActionAt, attachments

ExistingActivityRelationships: none in `Activity`; older docs mention Opportunity->Activity conceptually only.

## Minimum Business Model

The foundation Activity answers:

- What needs to happen?
- For whom: Lead or Contact?
- What kind of interaction: Call, Email, Meeting or Task?
- When is it scheduled/due?
- Is it completed?
- What happened, when outcome is later added?

No workflow engine, scheduler, reminders, external notifications or calendar integration is part of Sprint 13.

## First Implementation Story

FirstImplementationStoryId: S13-01

FirstImplementationStoryName: Activity Contracts and Domain Rules

FirstImplementationStoryRationale: Existing Activity is useful but too thin for Lead/Contact follow-up. The first implementation must stabilize contracts, target association, bounded inputs and lifecycle before adding service/API/UI.

FirstImplementationStoryAcceptanceCriteria:

- ActivityManagement contracts and deterministic policy exist.
- Activity requires subject and one valid LeadId or ContactId target.
- Subject/details are bounded and normalized.
- Activity type/status validation is explicit.
- Completion rules are deterministic.
- No DELETE or productive route is added.
- Portal/Common DB remain disabled.
- Unit and architecture tests pass.

ExpectedArchitectureChanges: CRM.Domain ActivityManagement folder with command/result/error contracts and policy; optional Activity entity extension only if required by the policy.
