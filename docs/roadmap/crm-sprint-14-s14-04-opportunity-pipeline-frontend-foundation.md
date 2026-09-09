# CRM Sprint 14 S14-04 - Opportunity Pipeline Frontend Foundation Page

S1403MergeCommit: Sprint 14 S14-03 merge commit required
S1404BaseMainCommit: Sprint 14 S14-03 merge commit required

## Decision
S1404Decision: Implemented
OpportunityPipelineImplementationStatus: FrontendFoundationImplemented
OpportunityPipelineFrontend: FoundationImplemented
FrontendOpportunityRoute: /foundation/opportunities
FrontendUsesFoundationOpportunityApiOnly: true
FrontendUsesProductiveOpportunityRoute: false

## Workflow
- Lists and selects foundation Opportunities.
- Creates and edits only Open Opportunities.
- Displays AccountName, ExpectedValue, Currency, Probability, Pipeline, Stage and Status.
- Uses a deterministic synthetic frontend pipeline catalog with stable GUID stage ids, unique positive order and bounded names.
- Progresses only to the next ordered stage.
- Marks Open Opportunities Won, Lost or Cancelled through explicit lifecycle endpoints.
- Keeps Won, Lost and Cancelled Opportunities read-only and hides repeat terminal actions.
- Refreshes after create, update, progress, win, lose and cancel so backend-normalized values are shown.

## Frontend API
- GET `/api/crm/foundation/opportunities`
- GET `/api/crm/foundation/opportunities/{id}`
- POST `/api/crm/foundation/opportunities`
- PUT `/api/crm/foundation/opportunities/{id}`
- POST `/api/crm/foundation/opportunities/{id}/progress`
- POST `/api/crm/foundation/opportunities/{id}/win`
- POST `/api/crm/foundation/opportunities/{id}/lose`
- POST `/api/crm/foundation/opportunities/{id}/cancel`

## UX and safety
DuplicateSubmissionProtected: true
LoadingState: Implemented
EmptyState: Implemented
ValidationState: Implemented
NotFoundState: Implemented
SafeGenericErrorState: Implemented
ResponsiveBasics: SourceVerified
AccessibleBasics: SourceVerified
RawJsonDeveloperConsoleUi: false

## Boundaries
ProductiveOpportunityRouteEnabled: false
DeleteBehaviorAdded: false
LeadConversionImplemented: false
AccountManagementRuntimeActivated: false
AssignmentOwnerRuntimeActivated: false
PortalRuntimeEnabled: false
TokenStorageAdded: false
AuthorizationHeaderStorageAdded: false
CommonDbRuntimeEnabled: false
EfRuntimeEnabled: false
MigrationsCreated: false
SchemaChangesDetected: false
SecretsAdded: false
RealDataDetected: false
SimulatedProductionTouched: false
CrmProdSimTouched: false

## Next task
NextTaskPhase: CRM Sprint 14 S14-05 - Opportunity Pipeline Test and Guardrail Hardening
NextTaskPromptFile: codex/prompts/sprint-14-opportunity-pipeline-s14-05.md
