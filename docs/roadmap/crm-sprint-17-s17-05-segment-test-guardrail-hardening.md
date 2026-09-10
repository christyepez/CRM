# CRM Sprint 17 S17-05 - Segment Test and Guardrail Hardening

S1705Decision: Implemented
Sprint17S1704Base: 6b15324f4d98c254a9af0fa971fc61a7b14039f8

## Hardened coverage
- Cross-layer parity for Name, CriteriaSummary and Draft/Active/Inactive.
- Frontend remains bound only to `/api/crm/foundation/segments`.
- Frontend max lengths remain 160/1000 and duplicate-submit protection remains active.
- Application service remains policy-driven through `SegmentManagementPolicy` and `ISegmentFoundationStore`.
- No-change update behavior is explicitly covered.
- Repeated activation idempotency is explicitly covered.
- Draft deactivation conflict is explicitly covered.
- Missing Segment update returns 404.
- API mapping for 400/404/409 remains explicit.

## Guardrails
ProductiveSegmentRouteEnabled: false
DeleteBehaviorAdded: false
ArbitraryCriteriaExecutionEnabled: false
CampaignTargetingEnabled: false
AccountAutoClassificationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
ExternalConnectorRuntimeEnabled: false
RealDataEnabled: false
SimulatedProductionTouched: false