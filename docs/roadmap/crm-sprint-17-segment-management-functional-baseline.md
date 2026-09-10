# CRM Sprint 17 P1 - Segment Management Functional Baseline

Sprint17P1BaseMainCommit: 6340f0e724efee9d35ecd661215e86a46960a814
SelectedSliceId: S17-SEGMENT
SelectedSliceName: Segment Management Foundation
Sprint17P1Decision: ReadyForS1701SegmentContractsAndDomainRules
FirstImplementationStoryId: S17-01
FirstImplementationStoryName: Segment Contracts and Domain Rules

## Repository inventory
SegmentDomainStatus: ConceptualRecordOnly
SegmentCatalogStatus: ConceptualReadContractOnly
SegmentLifecyclePolicyExists: false
SegmentApplicationStatus: NotStarted
SegmentFoundationStoreStatus: NotStarted
SegmentApiStatus: NotStarted
SegmentFrontendStatus: NotStarted

## Canonical model
CanonicalSegmentFields: Id, Name, CriteriaSummary, Status
SegmentNameRule: Required, trimmed, max 160 characters.
SegmentCriteriaSummaryRule: Required, trimmed, max 1000 characters; descriptive text only.
SegmentStatusProposal: Draft, Active, Inactive.
SegmentCreateRule: Create starts in Draft.
SegmentUpdateRule: Draft, Active and Inactive may update Name and CriteriaSummary when valid.
SegmentActivateRule: Draft or Inactive may transition to Active explicitly.
SegmentDeactivateRule: Active may transition to Inactive explicitly.
SegmentIdempotencyRule: Repeating activate/deactivate on the same target state is no-change success.
SegmentDeleteRule: NotSupported
SegmentCriteriaExecutionRule: NotSupported

## Explicit exclusions
CampaignTargetingEnabled: false
AutomaticAudienceAssignmentEnabled: false
AccountAutoClassificationEnabled: false
ArbitraryCriteriaExecutionEnabled: false
ProductiveSegmentRouteEnabled: false
FoundationSegmentRouteEnabledByP1: false
DeleteBehaviorAdded: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
EfRuntimeEnabled: false
MigrationsCreated: false
SchemaChangesDetected: false
RealDataDetected: false
ExternalConnectorRuntimeEnabled: false
SimulatedProductionTouched: false
CrmProdSimTouched: false

## Finite backlog
S17-01 Segment Contracts and Domain Rules
S17-02 Segment Application Service and Foundation Store
S17-03 Segment Foundation API
S17-04 Segment Frontend Foundation Page
S17-05 Segment Test and Guardrail Hardening
S17-06 Segment Local Integration Validation
S17-07 Segment Sprint Closure
