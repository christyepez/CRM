# CRM Sprint 9 P5 - Productive Route Dry Run Trial

Status: implemented as a controlled foundation trial.

The Productive Route Dry Run Trial prepares the decision contract for future CRM productive routes without registering those routes by default. It is NonProduction-only, disabled/fail-closed by default, and returns metadata-only evidence.

Default behavior:
- ProductiveRouteDryRunTrialExists: true
- ProductiveRouteDryRunTrialApproved: true
- ProductiveRouteDryRunTrialEnabled: false
- ProductiveRoutesRegisteredByDefault: false
- ProductiveRoutesDryRunRegistered: false
- ProductiveRouteDryRunStatusCode: 423
- ProductiveCrudEnabled: false
- ProductiveDomainExecutionEnabled: false
- ProductivePersistenceEnabled: false
- DatabaseWriteAttempted: false
- SideEffectsAllowed: false
- DeleteEndpointsEnabled: false
- DbRuntimeEnabled: false
- EfRuntimeEnabled: false
- MigrationsEnabled: false
- SchemaChangeAllowed: false
- PortalAuthMetadataDependencyValidated: true
- CommonDbMetadataDependencyValidated: true
- SecretProviderMetadataDependencyValidated: true
- AuthHeaderRead: false
- TokenRead: false
- TokenStored: false
- AuthAttributeEnabled: false
- LoginEndpointCreated: false
- LogoutEndpointCreated: false
- IdentityRuntimeEnabled: false
- NonProductionOnly: true
- ProductionBlocked: true
- FailClosedByDefault: true
- RollbackAvailable: true
- ObservabilityMetadataOnly: true
- NextGate: Sprint9P6Sprint9GateDecision

Endpoints:
- `GET /api/crm/foundation/sprint-9/productive-route-dry-run-trial`
- `POST /api/crm/foundation/sprint-9/productive-route-dry-run-trial/probe`

The probe returns `423 Locked` by default and must not execute CRM domain logic, persistence, DB writes, Portal Auth enforcement, token/header reads, DELETE or side effects.
