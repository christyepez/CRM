# CRM Sprint 5 P5 - Locked Productive Route Stub Trial

## Decision

P5 uses `DocumentOnlyPreferredWithNoRuntimeRegistration`.

The locked productive route stub trial exists, but productive CRM routes are not registered by default.

## Default state

- LockedProductiveRouteStubTrialExists: true
- LockedProductiveRouteStubRegistrationApproved: false
- LockedProductiveRouteStubsRegistered: false
- ProductiveRoutesRegistered: false
- ProductiveCrudEnabled: false
- ProductiveAuthorizationEnabled: false
- DeleteEndpointsEnabled: false
- RuntimeFlagDefaultEnabled: false
- LockedResponseIfEnabled: 423
- DefaultNegativeRouteStatus: 404
- FoundationCrudStillSeparate: true
- DbRequired: false
- AuthRuntimeRequired: false
- PortalRuntimeRequired: false
- NextGate: Sprint5P6Sprint5GateDecision

## Boundary

`/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` must keep returning 404 by default. If a future non-production trial explicitly enables locked stubs, each route must return 423 Locked without executing domain logic, stores, database, Auth runtime, Portal runtime or DELETE.

## Warning

Locked productive route stub trial only; no productive routes are registered by default.
