# CRM Sprint 16 S16-05 - Account Test and Guardrail Hardening

S1605Decision: Implemented
Sprint16S1604Base: 41e581ef12b4fb02a0f0cb12ae74a7797777b31e

## Hardened coverage
- Cross-layer parity for Name, TaxId, Industry, Segment and Draft/Active/Inactive.
- Frontend remains bound only to `/api/crm/foundation/accounts`.
- Frontend max lengths remain 160/64/120/80 and duplicate-submit protection remains active.
- Application service remains policy-driven through `AccountManagementPolicy` and `IAccountFoundationStore`.
- No-change update behavior is explicitly covered.
- Repeated activation idempotency is explicitly covered.
- Draft deactivation conflict is explicitly covered.
- Missing Account update returns 404.
- API mapping for 400/404/409 remains explicit.

## Guardrails
ProductiveAccountRouteEnabled: false
DeleteBehaviorAdded: false
LeadConversionEnabled: false
AutomaticAccountCreationEnabled: false
ContactRelationshipMutationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
ExternalConnectorRuntimeEnabled: false
RealDataEnabled: false
SimulatedProductionTouched: false

## Validation
Executable regression must pass on the connected workstation before this story is merged.

Next: S16-06 Account Local Integration Validation.
