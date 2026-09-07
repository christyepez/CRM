# CRM Sprint 13 S13-04 - Activity / Follow-Up Frontend Foundation

S1303PullRequest: #177
S1303MergeCommit: cd3ff379de8dda88795f8a5964163265aa6756fa
S1304BaseMainCommit: cd3ff379de8dda88795f8a5964163265aa6756fa

## Decision

S1304Decision: Implemented
ActivityManagementImplementationStatus: FrontendFoundationImplemented
ActivityManagementFrontend: FoundationImplemented
FrontendActivityRoute: /foundation/activities

## Page architecture

The Activity / Follow-Up page is implemented as `ActivityManagementPageComponent` in the existing Angular standalone `main.ts` architecture. It uses `ActivityManagementApiService` and typed models for Activity type, status, read model and command payloads.

## Workflow

- List/detail: loads `GET /api/crm/foundation/activities`, supports selection and safe detail refresh.
- Create: posts to `POST /api/crm/foundation/activities`.
- Edit: updates only scheduled activities through `PUT /api/crm/foundation/activities/{id}`.
- Complete: posts to `POST /api/crm/foundation/activities/{id}/complete`.
- Cancel: posts to `POST /api/crm/foundation/activities/{id}/cancel`.

## Foundation scope and safety

FrontendUsesFoundationActivityApiOnly: true
FrontendUsesProductiveActivityRoute: false
DeleteBehaviorAdded: false
PortalRuntimeEnabled: false
TokenStorageAdded: false
CommonDbDependency: false
DuplicateSubmissionProtected: true
AccountDependency: false
OpportunityDependency: false

The UI uses Angular interpolation for user-controlled Activity subject text. It does not use `innerHTML`, browser token storage, auth interceptors, Portal SDKs, Common DB, EF, migrations or productive CRM endpoints.

## Date/time handling

`datetime-local` values are converted to ISO UTC payloads before calling the API. Past schedules are allowed and shown as historical/overdue when status remains `Scheduled`; no persisted `Overdue` status is introduced.

## Validation and error handling

The page handles required subject, max length, required schedule, exactly-one-target UI behavior, safe 400/404/409 messages and generic unexpected errors without exposing server internals or raw JSON payloads.

## S13-05 entry criteria

S13-05 should harden cross-layer coverage for Activity type/status parity, target rules, productive negative routes, DELETE absence, PII/XSS review and frontend/backend contract consistency.
