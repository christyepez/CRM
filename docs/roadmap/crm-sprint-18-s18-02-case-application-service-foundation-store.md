# CRM Sprint 18 S18-02 - Case Application Service and Foundation Store

Status: Implemented
BaseMainCommit: d756147
Branch: crm-sprint-18-s18-02-case-application-service-foundation-store

S1802Decision: Implemented
CaseManagementImplementationStatus: ApplicationAndFoundationStoreImplemented
CaseManagementPolicyInvoked: true
DomainRulesDuplicatedInApplication: false
PersistenceClassification: FoundationOnly / NonProductionSeam
SyntheticSeedEnabled: true
ListAndDetailImplemented: true
CreateImplemented: true
UpdateImplemented: true
StartImplemented: true
ResolveImplemented: true
CloseImplemented: true
NoChangePersistenceSuppressed: true
InvalidPersistenceSuppressed: true
NotFoundPersistenceSuppressed: true
RejectedLifecyclePersistenceSuppressed: true
CancellationTokenSupported: true
FocusedUnitTests: 32 PASS
FocusedArchitectureTests: 5 PASS
UnitTestsAfter: 519
ArchitectureTestsAfter: 149
FullTestsAfter: 668
AngularBuild: PASS
AngularTests: PASS
FoundationVerifier: PASS
Guardrails: PASS
S1802Verifier: PASS

ProductiveCaseRouteEnabled: false
FoundationCaseRouteEnabled: false
DeleteBehaviorAdded: false
CustomerConversionEnabled: false
CustomerMutationEnabled: false
AssignmentRuntimeEnabled: false
SlaRuntimeEnabled: false
NotificationRuntimeEnabled: false
PortalRuntimeEnabled: false
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

## Implementation

- Added `ICaseManagementService` and explicit Case application contracts.
- Added `ICaseFoundationStore` as the application persistence port.
- Added `CaseManagementService` to orchestrate list, detail, create, update, start, resolve and close through `CaseManagementPolicy`.
- Added `InMemoryCaseFoundationStore` with one deterministic synthetic seed record.
- Registered the service and store in API composition only; no Case route mappings were added.

## Guardrails

- Case API and Angular workflow remain deferred to later stories.
- Productive `/api/crm/cases`, foundation `/api/crm/foundation/cases` and DELETE are unavailable.
- Customer creation/mutation, assignment, SLA and notifications runtime remain deferred.
- Portal runtime, Common DB, EF, migrations, schema, SQL, real data, external connectors, `crm-prod-sim`, port 8094 and Production remain untouched.

NextTaskPhase: CRM Sprint 18 S18-03 - Case Foundation API
NextTaskPromptFile: codex/prompts/sprint-18-case-management-s18-03.md
