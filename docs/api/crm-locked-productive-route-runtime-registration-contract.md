# CRM Locked Productive Route Runtime Registration Contract

Foundation endpoint:

- `GET /api/crm/foundation/sprint-7/locked-productive-route-runtime-registration`

Expected response markers:

- `status=LockedProductiveRouteRuntimeRegistrationWith423`
- `lockedProductiveRouteRuntimeRegistrationExists=true`
- `lockedProductiveRouteRuntimeRegistrationApprovalGranted=false`
- `lockedProductiveRouteRuntimeRegistrationEnabled=false`
- `productiveRoutesRegisteredByDefault=false`
- `productiveRoutesRegisteredWhenExplicitlyEnabled=true`
- `defaultNegativeRouteStatus=404`
- `explicitlyEnabledLockedRouteStatus=423`
- `productiveCrudEnabled=false`
- `productiveDomainExecutionEnabled=false`
- `productivePersistenceEnabled=false`
- `deleteEndpointsEnabled=false`
- `portalAuthRuntimeRequired=false`
- `portalAuthRuntimeEnabled=false`
- `tokenReadAttempted=false`
- `headerReadAttempted=false`
- `dbRuntimeEnabled=false`
- `efRuntimeEnabled=false`
- `migrationsCreated=false`
- `sideEffectsAllowed=false`
- `nonProductionOnly=true`
- `nextGate=Sprint7P6Sprint7GateDecision`

Locked route response:

- `status=Locked`
- `code=CRM_PRODUCTIVE_ROUTE_LOCKED`
- `message=Productive CRM route is registered only as a locked NonProduction stub`
- `sideEffectsAllowed=false`
- `productiveCrudEnabled=false`
- `domainExecutionEnabled=false`
- `persistenceEnabled=false`
- `portalAuthRuntimeEnabled=false`
