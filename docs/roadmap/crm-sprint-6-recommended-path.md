# CRM Sprint 6 Recommended Path

Recommended sequence:

1. Sprint 6 P1: NonProduction Runtime Approval Package - created, approvals not granted.
2. Sprint 6 P2: Secret Provider Safe Mock Activation - enabled for synthetic values only.
3. Sprint 6 P3: Common DB Connectivity Dry-Run Contract - contract exists, connection attempt disabled.
4. Sprint 6 P4: Portal Auth Token Propagation Dry-Run Contract.
5. Sprint 6 P5: Locked Stub Runtime Registration Trial.
6. Sprint 6 P6: Sprint 6 Gate Decision.

Do not implement Sprint 6 runtime activation before explicit approvals. After P3, the only recommended next gate is `Sprint6P4PortalAuthTokenPropagationDryRunContract`, still without token/header reads or Portal HTTP.
## Sprint 6 P4 - Portal Auth Token Propagation Dry-Run Contract

P4 validates the Portal Auth token propagation contract with synthetic metadata only. It does not activate Auth runtime, does not read real tokens or headers, and does not call PortalCorporativo. Next gate: `Sprint6P5LockedStubRuntimeRegistrationTrial`.
## Sprint 6 P5 - Locked Stub Runtime Registration Trial

P5 validates the locked stub runtime registration contract without registering productive routes. The default remains 404 for `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts`. Future 423 Locked behavior is documented only. Next gate: `Sprint6P6Sprint6GateDecision`.
