# CRM Sprint 17 S17-04 - Segment Frontend Foundation Page

Base: `6804a60cc2778c7f6b29e8a75d30b83c4a290018`
S1704Decision: Implemented
SegmentFrontendFoundationPage: Enabled

## Scope
- Angular route `/foundation/segments`.
- Typed Segment foundation API service.
- List/detail/create/update workflow.
- Activate/deactivate lifecycle actions.
- Name validation: required, max 160.
- CriteriaSummary validation: required, max 1000.
- Safe handling of 400, 404 and 409 responses.
- Foundation-only presentation and messaging.

## Safety
ProductiveSegmentRouteEnabled: false
DeleteBehaviorAdded: false
ArbitraryCriteriaExecutionEnabled: false
CampaignTargetingEnabled: false
AccountAutoClassificationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataEnabled: false
SimulatedProductionTouched: false
