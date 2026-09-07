# CRM Sprint 13 S13-01 - Activity Contracts and Domain Rules

Repository: christyepez/CRM

Sprint13P1PullRequest: #171

Sprint13P1MergeCommit: c2229235595be70f899b8a891349d559ebf0a0ca

S1301BaseMainCommit: c2229235595be70f899b8a891349d559ebf0a0ca

## Existing Activity Baseline

- ExistingActivityEntity: `src/CRM.Domain/Entities/Activity.cs`
- ExistingActivityType: `Call`, `Email`, `Meeting`, `Task`
- ExistingActivityStatus: `Scheduled`, `Completed`, `Cancelled`
- Existing direct relationships before S13-01: none

The existing Activity entity was reused. No `ActivityV2`, `ActivityFoundation` or separate `FollowUp` aggregate was created.

## Contracts Added

- ActivityManagementCommand: `src/CRM.Domain/ActivityManagement/ActivityManagementCommand.cs`
- ActivityManagementSnapshot: `src/CRM.Domain/ActivityManagement/ActivityManagementCommand.cs`
- ActivityManagementRuleResult: `src/CRM.Domain/ActivityManagement/ActivityManagementRuleResult.cs`
- ActivityManagementErrorCode: `src/CRM.Domain/ActivityManagement/ActivityManagementErrorCode.cs`
- ActivityManagementOperation: `src/CRM.Domain/ActivityManagement/ActivityManagementOperation.cs`
- ActivityManagementPolicy: `src/CRM.Domain/ActivityManagement/ActivityManagementPolicy.cs`

## Target Model

ActivityTargetModel: Explicit LeadId or ContactId

LeadTargetSupported: true

ContactTargetSupported: true

ExactlyOneTargetRequired: true

Domain validates structural identifier format only. It does not query Lead or Contact existence; that belongs to S13-02 application orchestration.

AccountDependency: false

OpportunityDependency: false

AssignmentDependency: Deferred

## Follow-Up Semantics

FollowUpModel: Activity with scheduled/due date

Follow-Up is represented by Activity semantics, not by a new entity or ActivityType.

## Rules

SubjectRules: required, trimmed, max 160 characters

ScheduledAtRules: required DateTimeOffset; UTC expected by contract convention

PastScheduleDecision: AllowedForHistoricalRecording

NormalizationBehavior: subject and IDs are trimmed; IDs are not otherwise transformed

NoChangeBehavior: update with same normalized state returns Allowed=true and Changed=false

CompletedActivityMutationRule: completed activities cannot be normally updated; repeat completion is idempotent Changed=false

CancelledActivityMutationRule: cancelled activities cannot be normally updated or completed; repeat cancellation is idempotent Changed=false

DeleteBehaviorAdded: false

## Rule Matrix

| Operation | CurrentStatus | Condition | Allowed | Changed | ResultStatus | ErrorCode |
| --- | --- | --- | --- | --- | --- | --- |
| Create | none | valid type, subject, schedule and exactly one target | true | true | Scheduled | None |
| Create | none | no LeadId or ContactId | false | false | Scheduled | ActivityTargetRequired |
| Create | none | both LeadId and ContactId | false | false | Scheduled | MultipleActivityTargetsNotAllowed |
| Create | none | invalid LeadId | false | false | Scheduled | InvalidLeadId |
| Create | none | invalid ContactId | false | false | Scheduled | InvalidContactId |
| Create | none | missing subject | false | false | Scheduled | SubjectRequired |
| Create | none | subject too long | false | false | Scheduled | SubjectTooLong |
| Create | none | missing scheduled date | false | false | Scheduled | ScheduledAtRequired |
| Update | Scheduled | normalized state differs | true | true | Scheduled | None |
| Update | Scheduled | normalized state is same | true | false | Scheduled | None |
| Update | Completed | normal field mutation | false | false | Completed | CompletedActivityCannotBeModified |
| Update | Cancelled | normal field mutation | false | false | Cancelled | CancelledActivityCannotBeModified |
| Complete | Scheduled | valid existing snapshot | true | true | Completed | None |
| Complete | Completed | repeated completion | true | false | Completed | None |
| Complete | Cancelled | complete cancelled activity | false | false | Cancelled | CancelledActivityCannotBeCompleted |
| Cancel | Scheduled | valid existing snapshot | true | true | Cancelled | None |
| Cancel | Cancelled | repeated cancellation | true | false | Cancelled | None |
| Cancel | Completed | cancel completed activity | false | false | Completed | CompletedActivityCannotBeCancelled |

## Security

ActivitySecurityReview: PASS

- Domain rules centralize bounded input.
- No API DTO mass assignment was added.
- No HTML/rich text fields were added.
- No PII logging was introduced.
- No secrets, tokens, Portal Auth runtime or Common DB runtime were introduced.

## Out of Scope

- Activity application service.
- Activity foundation store.
- Activity API routes.
- Angular Activity page.
- Productive `/api/crm/activities`.
- DELETE.
- Portal Auth runtime.
- Common DB/SQL/EF/migrations/schema.
- Assignment using Portal users.
- Account Management or Opportunity Pipeline runtime dependency.
- Simulated Production or real Production changes.

## S13-02 Entry Criteria

S1301Decision: Implemented

S13-02 may implement `IActivityManagementService`, `ActivityManagementService`, `IActivityFoundationStore`, `InMemoryActivityFoundationStore` and foundation orchestration over the S13-01 policy.
