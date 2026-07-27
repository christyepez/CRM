# CRM Sprint 4 P4 - Productive Routes Locked Stub Validation

Status: `ProductiveRoutesLockedStubValidation`.

Decision: `DocumentOnlyPreferred`. Productive routes remain documented only and are not registered by default.

Default decisions:

- `lockedStubsStrategy=DocumentOnlyPreferred`.
- `productiveRoutesRegistered=false`.
- `lockedStubsRegistered=false`.
- `productiveCrudEnabled=false`.
- `productiveAuthorizationEnabled=false`.
- `deleteEndpointsEnabled=false`.
- `dbRequired=false`.
- `authRuntimeRequired=false`.
- `foundationCrudStillSeparate=true`.
- `nextGate=Sprint4P5NonProductionE2EPilotReadiness`.

Warning: `Productive routes locked stub validation only; no productive routes are active`.

Foundation CRUD is not productive CRUD and remains under `/api/crm/foundation/...`.
