# CRM Sprint 16 S16-03 - Account Foundation API Modernization

S1603Decision: Implemented
Sprint16S1602Base: 847fd9274482c8ecd6fa2bac912aa9567246a3fd

## Implemented
- Existing `/api/crm/foundation/accounts` route family now uses `IAccountManagementService`.
- GET list/detail, POST create and PUT update preserved.
- Added POST `{id}/activate` and POST `{id}/deactivate`.
- Added explicit Account API request/response contracts and Application mapping.
- AccountNotFound maps to 404; validation maps to 400; invalid lifecycle transition maps to 409.
- FoundationMode and NonProductionSeam metadata remain explicit.

## Guardrails
ProductiveAccountRouteEnabled: false
DeleteBehaviorAdded: false
LeadConversionEnabled: false
AutomaticAccountCreationEnabled: false
ContactRelationshipMutationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
ExternalConnectorRuntimeEnabled: false
SimulatedProductionTouched: false
