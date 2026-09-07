# CRM Sprint 13 - Activity / Follow-Up Foundation Roadmap

Repository: christyepez/CRM

Sprint13SelectedSliceId: S13-ACTIVITY

Sprint13SelectedSliceName: Activity / Follow-Up Foundation

Sprint13P1BaseMainCommit: 25871b9f43a58bfc8c8da785a5642af8c81ac84a

## Goal

Extend the closed Lead Qualification and Contact Management foundation flow toward useful CRM work tracking:

Lead / Contact -> Activity -> Follow-Up -> Next Action.

Sprint 13 remains foundation-only and does not activate productive routes, Portal Auth runtime, Common DB runtime or simulated Production.

## Current Baseline

- Sprint 11 Lead Qualification Foundation is closed.
- Sprint 12 Contact Management Foundation is closed.
- `Activity` exists in `src/CRM.Domain/Entities/Activity.cs`.
- `ActivityType` and `ActivityStatus` exist in `src/CRM.Domain/Enums/CrmStatuses.cs`.
- `FollowUpScheduledDomainEvent` exists in `src/CRM.Domain/Events/CrmDomainEvents.cs`.
- Domain catalog references Activity as scheduled commercial follow-up.
- Reporting catalog references `ActivitiesCompleted`, `FollowUpsPending`, `Sales Activities` and `ActivityAnalyticsReadModel`.
- No Activity application service, persistence store, foundation API or Angular page exists yet.

## Functional Scope

- Activity identity and deterministic domain policy.
- Activity type: Call, Email, Meeting, Task.
- Activity status: Scheduled, Completed, Cancelled.
- Subject/details with bounded text.
- Scheduled/due date.
- Explicit Lead and Contact association model.
- Create/update foundation flow.
- Complete activity as explicit business action.
- List/detail foundation API.
- Angular foundation page for upcoming/overdue/completed activities.
- Foundation-only in-memory persistence.
- Tests, guardrails and local integration.

## Out of Scope

- Productive Activity API.
- DELETE.
- Real DB, EF runtime, migrations or schema changes.
- Portal Auth runtime, `[Authorize]`, token parsing or CRM-owned login.
- Account Management dependency.
- Opportunity Pipeline implementation.
- Calendar/email/Teams/Outlook integration.
- Notifications, reminders, background scheduling or SLA engine.
- Attachments and document storage.
- Simulated Production or real Production changes.

## Architecture

ActivityArchitectureTarget: FoundationOnly

ActivityPersistenceArchitecture: FoundationOnly / NonProductionSeam

PortalDependency: none

PortalRuntimeEnabled: false

CommonDbDependency: none

CommonDbRuntimeEnabled: false

SimulatedProductionTouched: false

## Story Backlog

### S13-01 - Activity Contracts and Domain Rules

- UserStory: As a CRM user, I need a clear Activity / Follow-Up contract so calls, emails, meetings and tasks can be planned safely.
- BusinessValue: Establishes stable business language and lifecycle before application/API/UI work.
- FunctionalScope: command/result contracts, validation policy, target association rules, completion rules.
- TechnicalScope: `CRM.Domain` ActivityManagement policy/contracts; no API/persistence activation.
- AcceptanceCriteria: deterministic rules, bounded text, Lead/Contact target decision, no DELETE, no runtime activation.
- ResponsibleAgent: Domain Modeling Agent + Security Agent.
- ReviewAgents: Architecture Governance Agent + QA Lead Agent.
- Complexity: M.

### S13-02 - Activity Application Service

- UserStory: As a CRM user, I need foundation services to create, update and complete activities.
- BusinessValue: Enables behavior through a reusable application seam.
- TechnicalScope: application contracts/service using a foundation in-memory store port.
- Dependencies: S13-01.
- Complexity: M.

### S13-03 - Activity Foundation API

- UserStory: As a CRM user, I need foundation API endpoints for activities without productive route activation.
- BusinessValue: Enables backend-to-frontend workflow validation.
- TechnicalScope: `/api/crm/foundation/activities` routes only.
- Dependencies: S13-02.
- Complexity: M.

### S13-04 - Activity / Follow-Up Frontend Foundation Page

- UserStory: As a CRM user, I need a foundation page to view, create and complete follow-ups.
- BusinessValue: Makes the slice usable and testable end-to-end.
- TechnicalScope: Angular `/foundation/activities`, safe rendering, no tokens/storage.
- Dependencies: S13-03.
- Complexity: M.

### S13-05 - Activity Test and Guardrail Hardening

- UserStory: As maintainers, we need Activity behavior and guardrails proven before local integration.
- BusinessValue: Prevents productive route, security and data boundary drift.
- Dependencies: S13-04.
- Complexity: M.

### S13-06 - Activity Local Integration Validation

- UserStory: As maintainers, we need local end-to-end validation of Activity foundation workflow.
- BusinessValue: Confirms API/frontend/service/store integration.
- Dependencies: S13-05.
- Complexity: M.

### S13-07 - Activity / Follow-Up Sprint Closure

- UserStory: As maintainers, we need Sprint 13 closed with evidence and the next business slice selected.
- BusinessValue: Keeps delivery controlled and traceable.
- Dependencies: S13-06.
- Complexity: S.

## Milestones

- M1: Activity Domain Ready.
- M2: Activity Application Ready.
- M3: Activity Foundation API Ready.
- M4: Activity Frontend Ready.
- M5: Activity Quality Hardened.
- M6: Activity Local Integration Validated.
- M7: Sprint 13 Closed.

## Exit Criteria

- Activity/Follow-Up lifecycle tested.
- Lead/Contact association validated.
- Productive Activity route unavailable.
- Portal/Common DB absent.
- Synthetic data only.
- No DELETE.
- Sprint closure complete.
