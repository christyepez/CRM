# CRM Sprint 17 S17-03 - Segment Foundation API

Base: `3db119b395fe95486ee353ff4903e6f85e0ac819`
S1703Decision: Implemented
SegmentFoundationApi: Enabled
ProductiveSegmentRouteEnabled: false
DeleteBehaviorAdded: false
ArbitraryCriteriaExecutionEnabled: false
CampaignTargetingEnabled: false
AccountAutoClassificationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataEnabled: false
SimulatedProductionTouched: false

## Endpoints
- GET `/api/crm/foundation/segments`
- GET `/api/crm/foundation/segments/{id}`
- POST `/api/crm/foundation/segments`
- PUT `/api/crm/foundation/segments/{id}`
- POST `/api/crm/foundation/segments/{id}/activate`
- POST `/api/crm/foundation/segments/{id}/deactivate`

## Result mapping
Validation -> 400, not found -> 404, invalid lifecycle -> 409, idempotent/no-change -> 200.
