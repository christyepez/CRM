# CRM Sprint 13 S13-05 - Activity / Follow-Up Test and Guardrail Hardening

Repository: `christyepez/CRM`

S1304PullRequest: #179
S1304FeatureCommit: e1bc808310246ec400cee8339b47ea29e6df4a1b
S1304MergeCommit: 85e4380fb8d192816a36c8c846c7e43abb57e6fe
S1305BaseMainCommit: 85e4380fb8d192816a36c8c846c7e43abb57e6fe

## Scope

S13-05 hardens Activity / Follow-Up foundation tests and guardrails only. It does not add new business capability, product routes, DELETE behavior, Portal Auth runtime, Common DB runtime, EF runtime, schema changes, migrations, secrets, real data or simulated Production changes.

## ActivityManagementCoverageMatrix

| Area | Coverage | Status |
| --- | --- | --- |
| Domain policy | type/status, exactly-one target, lifecycle transitions, historical schedules | PASS |
| Application service | Lead and Contact target validation through foundation seams only | PASS |
| Foundation API | create, read, update, complete, cancel, negative product routes, DELETE unavailable | PASS |
| Frontend contract | foundation route only, UTC conversion, duplicate submission protection, derived overdue label | PASS |
| Cross-layer parity | ActivityType and ActivityStatus shared semantics across backend and frontend | PASS |

## ActivityCrossLayerScenarioMatrix

| Scenario | Expected result | Status |
| --- | --- | --- |
| Valid Lead target | scheduled Activity created | PASS |
| Valid Contact target | scheduled Activity created with contact only | PASS |
| Both Lead and Contact targets | BadRequest / MultipleActivityTargetsNotAllowed | PASS |
| Missing target | BadRequest / ActivityTargetRequired | PASS |
| Unknown Contact target | NotFound / ActivityNotFound | PASS |
| Past scheduled date | accepted as historical recording | PASS |
| Completed activity update | BadRequest / CompletedActivityCannotBeModified | PASS |
| Completed activity cancel | BadRequest / CompletedActivityCannotBeCancelled | PASS |
| Cancelled activity complete | BadRequest / CancelledActivityCannotBeCompleted | PASS |
| Productive route | NotFound or Locked | PASS |
| Foundation DELETE | NotFound or MethodNotAllowed | PASS |

## Guardrail Results

ActivityTypeParity: PASS
ActivityStatusParity: PASS
ActivityTargetContractParity: PASS
TargetExistenceValidationIsolation: PASS
FoundationActivityApiVerified: true
FoundationActivityFrontendRouteVerified: true
FrontendUsesFoundationActivityApiOnly: true
ProductiveActivityRouteAvailable: false
DeleteBehaviorAdded: false
RouteIdAuthority: PASS
MassAssignmentRisk: Controlled
ScheduledDateContractConsistency: PASS
FrontendUtcConversionVerified: SourceVerified
HistoricalActivitySchedulingSupported: true
OverdueDerivedOnly: true
CompletedActivityReadOnlyUi: true
CancelledActivityReadOnlyUi: true
DuplicateSubmissionProtected: true
PiiLoggingDetected: false
ScopedSecretScan: PASS
RealDataDetected: false
XssReview: PASS
AccessibilityValidation: SourceVerified
ResponsiveValidation: SourceVerified

## Architecture Boundaries

PortalRuntimeEnabled: false
PortalAuthClientAdded: false
AuthorizationHeaderReadAdded: false
TokenStorageAdded: false
CommonDbRuntimeEnabled: false
CRMOwnedSqlServerDetected: false
SchemaChangesDetected: false
AccountDependency: false
OpportunityDependency: false
AssignmentDependency: Deferred

## Decision

S1305Decision: Implemented

NextTaskPhase: CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation
