# CRM Sprint 18 S18-04 - Case Frontend Foundation Page

S1804Decision: Implemented
CaseFrontendFoundationPage: Enabled
FoundationCaseRoute: /foundation/cases
FoundationCaseApi: /api/crm/foundation/cases

## Scope
- Angular foundation route `/foundation/cases`.
- Typed Case foundation API service.
- List/select/create/update workflow.
- Lifecycle actions: start, resolve and close.
- Fields: CustomerId, Title, Summary and Priority.
- Safe handling of loading, empty, success, validation, not-found and conflict states.
- Backend-normalized responses and Changed=false idempotent success are preserved.

## Safety
ProductiveCaseRouteEnabled: false
DeleteBehaviorAdded: false
CustomerMutationEnabled: false
AssignmentRuntimeEnabled: false
SlaRuntimeEnabled: false
NotificationRuntimeEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataEnabled: false
ExternalConnectorsEnabled: false
SimulatedProductionTouched: false
Port8094Touched: false
