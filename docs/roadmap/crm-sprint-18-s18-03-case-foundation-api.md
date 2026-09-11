# CRM Sprint 18 S18-03 - Case Foundation API

Status: Implemented
BaseMainCommit: ddeffe7
Branch: crm-sprint-18-s18-03-case-foundation-api

S1803Decision: Implemented
CaseManagementImplementationStatus: FoundationApiImplemented
FoundationCaseApiRoute: /api/crm/foundation/cases
CaseApiUsesApplicationService: true
ExplicitApiDtos: true
ValidationFailuresStatus: 400
MissingCaseStatus: 404
InvalidLifecycleTransitionStatus: 409
ChangedFalseIdempotentSuccessStatus: 200

## Endpoint Scope

- GET `/api/crm/foundation/cases`
- GET `/api/crm/foundation/cases/{id}`
- POST `/api/crm/foundation/cases`
- PUT `/api/crm/foundation/cases/{id}`
- POST `/api/crm/foundation/cases/{id}/start`
- POST `/api/crm/foundation/cases/{id}/resolve`
- POST `/api/crm/foundation/cases/{id}/close`

## Guardrails

ProductiveCaseRouteEnabled: false
DeleteBehaviorAdded: false
AngularCasePageAdded: false
CustomerCreationEnabled: false
CustomerMutationEnabled: false
AssignmentRuntimeEnabled: false
SlaRuntimeEnabled: false
NotificationRuntimeEnabled: false
PortalRuntimeEnabled: false
PortalAuthRuntimeEnabled: false
PortalUsersRuntimeEnabled: false
CommonDbRuntimeEnabled: false
EfRuntimeEnabled: false
MigrationsCreated: false
SchemaChangesDetected: false
SqlAdded: false
RealDataDetected: false
ExternalConnectorRuntimeEnabled: false
SimulatedProductionTouched: false
CrmProdSimTouched: false
Port8094Touched: false

## Implementation Notes

- Added explicit foundation Case transport DTOs in `CRM.Api.Foundation`.
- Routed all create, update and lifecycle behavior through `ICaseManagementService`.
- Kept controllers/endpoints thin: HTTP binding and result/status mapping only.
- Preserved deterministic in-memory `NonProductionSeam` persistence from S18-02.
- Kept repeated start, resolve and close actions as 200 responses with `Changed=false`.

## Validation

- Added `CaseFoundationApiEndpointTests` for list/detail/create/update/lifecycle/not-found/conflict/idempotency/guardrails.
- Updated Case architecture guardrails to require foundation routes and forbid productive routes and DELETE.
- Added `tools/verify-crm-sprint-18-s18-03.ps1`.

NextTaskPhase: CRM Sprint 18 S18-04 - Case Frontend Foundation Page
NextTaskPromptFile: codex/prompts/sprint-18-case-management-s18-04.md
