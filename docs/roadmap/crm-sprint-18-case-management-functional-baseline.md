# CRM Sprint 18 P1 - Case Management Functional Baseline

Sprint18P1BaseMainCommit: 039051a
SelectedSliceId: S18-CASE
SelectedSliceName: Case Management Foundation
Sprint18P1Decision: ReadyForS1801CaseContractsAndDomainRules
FirstImplementationStoryId: S18-01
FirstImplementationStoryName: Case Contracts and Domain Rules

## Repository inventory
CaseDomainStatus: ListedCapabilityOnly
CaseLifecyclePolicyExists: false
CaseApplicationStatus: NotStarted
CaseFoundationStoreStatus: NotStarted
CaseApiStatus: NotStarted
CaseFrontendStatus: NotStarted
CustomerMutationStatus: Deferred

## Canonical model
CanonicalCaseFields: Id, CustomerId, Title, Summary, Priority, Status
CaseCustomerIdRule: Required structural reference only; no Customer creation or mutation.
CaseTitleRule: Required, trimmed, max 160 characters.
CaseSummaryRule: Required, trimmed, max 1000 characters.
CasePriorityProposal: Low, Medium, High, Critical.
CaseStatusProposal: Open, InProgress, Resolved, Closed.
CaseCreateRule: Create starts Open.
CaseUpdateRule: Open and InProgress may update Title, Summary and Priority when valid.
CaseStartRule: Open may transition to InProgress explicitly; repeated InProgress is no-change success.
CaseResolveRule: Open or InProgress may transition to Resolved explicitly; repeated Resolved is no-change success.
CaseCloseRule: Resolved may transition to Closed explicitly; repeated Closed is no-change success.
CaseDeleteRule: NotSupported

## Explicit exclusions
ProductiveCaseRouteEnabled: false
FoundationCaseRouteEnabledByP1: false
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

## Finite backlog
S18-01 Case Contracts and Domain Rules
S18-02 Case Application Service and Foundation Store
S18-03 Case Foundation API
S18-04 Case Frontend Foundation Page
S18-05 Case Test and Guardrail Hardening
S18-06 Case Local Integration Validation
S18-07 Case Sprint Closure
