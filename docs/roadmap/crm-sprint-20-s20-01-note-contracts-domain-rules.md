# CRM Sprint 20 S20-01 - Note Contracts and Domain Rules

S2001Decision: Implemented
DomainOnly: true

## Delivered
- Note operations: Create, Update, Archive.
- Related entity types: Customer, Contact, Lead, Opportunity, Case.
- Status: Active, Archived.
- RelatedEntityId validation and normalization.
- Text trim with max 4000.
- Create -> Active; Update only Active; Archive -> Archived; repeated Archive Changed=false.
- Archived notes are otherwise read-only.

## Safety
ProductiveNoteRouteEnabled: false
DeleteBehaviorAdded: false
CrossEntityMutationEnabled: false
ActivitySchedulingEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataEnabled: false
ExternalConnectorsEnabled: false
SimulatedProductionTouched: false
Port8094Touched: false