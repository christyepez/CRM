# CRM Sprint 13 S13-02 - Activity Application Service and Foundation Store

Repository: christyepez/CRM

S1301PullRequest: #173

S1301MergeCommit: 1fa6b5428dec562f387ecd0737f01ba3759ec2c1

S1302BaseMainCommit: 1fa6b5428dec562f387ecd0737f01ba3759ec2c1

## Implementation Status

ActivityManagementImplementationStatus: ApplicationAndFoundationStoreImplemented

ActivityManagementDomain: Implemented

ActivityManagementApplicationService: Implemented

ActivityManagementFoundationStore: Implemented

ActivityManagementApi: NotImplemented

ActivityManagementFrontend: NotImplemented

ProductiveActivityRouteEnabled: false

DeleteBehaviorAdded: false

## Application Architecture

ActivityManagementServiceInterface: `src/CRM.Application/ActivityManagement/IActivityManagementService.cs`

ActivityManagementServiceImplementation: `src/CRM.Application/ActivityManagement/ActivityManagementService.cs`

Application orchestrates create, update, complete, cancel, list and detail operations. Domain remains authoritative through `ActivityManagementPolicy`; Application handles lookup, target existence checks, persistence and safe result mapping.

DomainRulesDuplicatedInApplication: false

ActivityManagementPolicyInvoked: true

## Store Architecture

ActivityFoundationStoreInterface: `src/CRM.Application/Ports/Persistence/IActivityFoundationStore.cs`

ActivityFoundationStoreImplementation: `src/CRM.Infrastructure/Persistence/Foundation/InMemoryActivityFoundationStore.cs`

PersistenceClassification: FoundationOnly / NonProductionSeam

SyntheticActivitySeedEnabled: true

The store keeps existing `Activity` entities in memory and returns defensive copies to avoid silent mutation outside `SaveAsync`.

ReadMethodsIncluded: true

CancellationTokenSupported: true

## Target Existence Strategy

LeadExistenceValidation: ImplementedFoundationOnly

ContactExistenceValidation: ImplementedFoundationOnly

Target lookup uses only the exact foundation seam selected by the domain rule:

- Lead target -> `ILeadFoundationStore.GetPreviewByIdAsync`.
- Contact target -> `IContactFoundationStore.GetPreviewByIdAsync`.

No Account, Opportunity, Common DB, Portal Auth or external lookup is used.

## Behavior

CreateBehavior: policy evaluation, foundation target existence validation, Activity creation, one store write on valid request.

ValidCreateWriteCount: 1

InvalidCreateWriteCount: 0

UpdateBehavior: load Activity, evaluate policy, validate target, suppress no-change writes, persist changed scheduled Activity.

ChangedUpdateWriteCount: 1

NoChangeUpdateWriteCount: 0

InvalidUpdateWriteCount: 0

ActivityNotFoundBehavior: deterministic safe result with `ActivityNotFound` and zero writes.

CompleteBehavior: load Activity, evaluate completion, set UTC completion time in Application, persist only changed Scheduled activity.

ValidCompleteWriteCount: 1

RepeatCompleteWriteCount: 0

CancelledCompleteWriteCount: 0

CancelBehavior: load Activity, evaluate cancellation, persist only changed Scheduled activity.

ValidCancelWriteCount: 1

RepeatCancelWriteCount: 0

CompletedCancelWriteCount: 0

NoChangePersistenceSuppressed: true

## Write Matrix

| Operation | Scenario | Policy result | Lookup result | Writes | Application result |
| --- | --- | --- | --- | --- | --- |
| Create | valid Lead target | Allowed/Changed | Lead found | 1 | Success |
| Create | valid Contact target | Allowed/Changed | Contact found | 1 | Success |
| Create | invalid domain request | Rejected | skipped | 0 | Safe validation result |
| Create | missing Lead/Contact | Allowed/Changed | target missing | 0 | ActivityNotFound safe target result |
| Update | changed scheduled Activity | Allowed/Changed | target found | 1 | Success |
| Update | same normalized state | Allowed/NoChange | target found | 0 | Success no-change |
| Update | invalid update | Rejected | skipped or no write | 0 | Safe validation result |
| Update | Activity not found | NotFound | skipped | 0 | ActivityNotFound |
| Complete | scheduled Activity | Allowed/Changed | existing Activity found | 1 | Completed |
| Complete | already completed | Allowed/NoChange | existing Activity found | 0 | Success no-change |
| Complete | cancelled Activity | Rejected | existing Activity found | 0 | CancelledActivityCannotBeCompleted |
| Cancel | scheduled Activity | Allowed/Changed | existing Activity found | 1 | Cancelled |
| Cancel | already cancelled | Allowed/NoChange | existing Activity found | 0 | Success no-change |
| Cancel | completed Activity | Rejected | existing Activity found | 0 | CompletedActivityCannotBeCancelled |

## Clock Behavior

Completion uses a single Application-controlled `DateTimeOffset.UtcNow` call at completion mutation time. No infrastructure clock framework was introduced.

## Security

ActivityApplicationSecurityReview: PASS

- No Activity subject/body logging was added.
- No secrets, tokens, Authorization header reads or client credentials were added.
- No API DTO binding was added.
- No productive routes were added.
- No Common DB/SQL connection string was introduced.
- Synthetic seed data only.

## Out of Scope

- Activity foundation API routes.
- Angular Activity page.
- Productive `/api/crm/activities`.
- DELETE.
- Portal Auth runtime.
- Common DB/EF/migrations/schema.
- Account/Opportunity/Assignment runtime dependencies.
- Simulated Production or real Production changes.

## S13-03 Entry Criteria

S1302Decision: Implemented

S13-03 may add foundation API routes over `IActivityManagementService` with explicit DTO mapping and safe status mapping. It must not add productive routes or DELETE.
