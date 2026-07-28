# CRM Sprint 6 P5 - Locked Stub Runtime Registration Trial

Sprint 6 P5 adds a foundation-only trial contract for future locked stub runtime registration.

Decision: `DocumentOnlyPreferredWithNoRuntimeRegistration`.

No productive routes are registered by default:

- `/api/crm/leads` must return 404.
- `/api/crm/accounts` must return 404.
- `/api/crm/contacts` must return 404.

Default status:

- LockedStubRuntimeRegistrationTrialExists: true
- LockedStubRuntimeRegistrationApprovalGranted: false
- LockedStubRuntimeRegistrationEnabled: false
- LockedStubsRegisteredAtRuntime: false
- ProductiveRoutesRegistered: false
- ProductiveCrudEnabled: false
- DeleteEndpointsEnabled: false
- DefaultNegativeRouteStatus: 404
- FutureLockedResponseStatusIfExplicitlyEnabled: 423
- RuntimeFlagDefaultEnabled: false
- UsesDomainServices: false
- UsesFoundationStores: false
- UsesDatabase: false
- UsesPortalAuth: false
- UsesTokenOrHeaderReads: false
- NextGate: Sprint6P6Sprint6GateDecision

Future explicit enablement must be NonProduction-only and must return 423 Locked without domain, store, DB, Auth, Portal or DELETE behavior.
