# CRM Sprint 19 S19-01 - Interaction Contracts and Domain Rules

S1901Decision: Implemented
InteractionDomainPolicy: Enabled

## Implemented
- Domain enums and value contracts for operation, related entity, channel, direction and status.
- Canonical create/update/void policy.
- RelatedEntityId is a structural GUID reference only.
- Subject required/max 160; Summary required/max 2000.
- OccurredAtUtc required, UTC and not in the future.
- Create starts Recorded; Update applies only to Recorded; Void produces Voided.
- No-change Update and repeated Void return Changed=false.
- Voided Interaction is read-only.

## Safety
ProductiveInteractionRouteEnabled: false
FoundationInteractionRouteEnabled: false
DeleteBehaviorAdded: false
ActivitySchedulingDuplicated: false
CrossEntityMutationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataEnabled: false
ExternalConnectorRuntimeEnabled: false
SimulatedProductionTouched: false
Port8094Touched: false
