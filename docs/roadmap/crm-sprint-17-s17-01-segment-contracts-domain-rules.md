# CRM Sprint 17 S17-01 - Segment Contracts and Domain Rules

Base: `c1e851ed98808b1095579f2d5cd4b21ad31a716c`
S1701Decision: Implemented
SegmentDomainPolicyImplemented: true

## Capability
- SegmentStatus: Draft, Active, Inactive.
- SegmentManagementOperation: Create, Update, Activate, Deactivate.
- Name required/trim/max 160.
- CriteriaSummary required/trim/max 1000; descriptive only.
- Create starts Draft.
- Update allowed in Draft/Active/Inactive with no-change detection.
- Draft/Inactive -> Active; repeat Active idempotent.
- Active -> Inactive; repeat Inactive idempotent.

## Guardrails
ProductiveSegmentRouteEnabled: false
FoundationSegmentRouteEnabled: false
DeleteBehaviorAdded: false
ArbitraryCriteriaExecutionEnabled: false
CampaignTargetingEnabled: false
AccountAutoClassificationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataDetected: false
SimulatedProductionTouched: false

Next: CRM Sprint 17 S17-02 - Segment Application Service and Foundation Store.