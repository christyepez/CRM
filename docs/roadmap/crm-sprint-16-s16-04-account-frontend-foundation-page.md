# CRM Sprint 16 S16-04 - Account Management Frontend Foundation Page

S1604Decision: Implemented
Sprint16S1603Base: a54e9fecb0e6e8a5667bb6edca3669ff4100f001

## Implemented
- Added Angular `AccountManagementApiService` using only `/api/crm/foundation/accounts`.
- Added standalone `AccountManagementPageComponent` at `/foundation/accounts`.
- Supports list, select, create, edit, activate and deactivate.
- Fields: Name, TaxId, Industry, Segment and Status.
- Backend-normalized Account responses are reflected after mutations.
- Duplicate submissions are prevented with `isSubmitting`.
- Loading, empty, validation, not-found, conflict and safe generic error states are handled.
- Uses existing responsive foundation workflow styles and accessible labels/live regions.

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
- Angular build PASS.
- Angular foundation verifier includes Account S16-04 parity and negative checks.
- Full backend regression and Sprint 16 verifiers are required before merge.

Next: S16-05 Account Test and Guardrail Hardening.
