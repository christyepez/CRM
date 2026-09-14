# CRM Sprint 18 S18-01 - Case Contracts and Domain Rules

S1801Decision: Implemented
CaseManagementDomain: Implemented
CaseManagementPolicy: Implemented
CaseApplicationService: NotImplemented
CaseFoundationStore: NotImplemented
CaseApi: NotImplemented
CaseFrontend: NotImplemented

## Implemented contract
- CasePriority: Low, Medium, High, Critical.
- CaseStatus: Open, InProgress, Resolved, Closed.
- CustomerId required, trimmed, valid non-empty GUID; structural reference only.
- Title required, trimmed, max 160.
- Summary required, trimmed, max 1000.
- Create starts Open.
- Update allowed only in Open/InProgress with no-change detection.
- Start transitions Open to InProgress; repeated InProgress is idempotent.
- Resolve transitions Open/InProgress to Resolved; repeated Resolved is idempotent.
- Close transitions Resolved to Closed; repeated Closed is idempotent.

## Guardrails
ProductiveCaseRouteEnabled: false
FoundationCaseRouteEnabled: false
DeleteBehaviorAdded: false
CustomerCreationEnabled: false
CustomerMutationEnabled: false
AssignmentRuntimeEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
EfRuntimeEnabled: false
MigrationsCreated: false
SchemaChangesDetected: false
RealDataDetected: false
ExternalConnectorRuntimeEnabled: false
SimulatedProductionTouched: false
CrmProdSimTouched: false
Port8094Touched: false

NextTaskPhase: CRM Sprint 18 S18-02 - Case Application Service and Foundation Store
NextTaskPromptFile: codex/prompts/sprint-18-case-management-s18-02.md
