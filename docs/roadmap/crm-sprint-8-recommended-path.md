# CRM Sprint 8 Recommended Path

Recommended packages:

- Sprint 8 P1: Secret Provider Approval Decision.
- Sprint 8 P2: Secret Provider Controlled Real NonProduction Read.
- Sprint 8 P3: Common DB Controlled Real Connectivity.
- Sprint 8 P4: Portal Auth Controlled Real Runtime Validation.
- Sprint 8 P5: Locked Route Authorization Policy Integration. Default disabled; productive routes 404 by default; explicit NonProduction locked routes 423 with sanitized policy metadata only.
- Sprint 8 P6: Sprint 8 Gate Decision. Close Sprint 8, keep production activation NoGo, productization NotReady, and approve Sprint 9 controlled runtime activation planning.

Do not implement Sprint 8 runtime behavior until each explicit approval gate is satisfied.
## P1 - Secret Provider Approval Decision

Sprint 8 starts with a planning-only approval decision. P1 approves moving to P2 controlled NonProduction read planning, but performs no real secret read and exposes no values.

Next gate: `Sprint8P2SecretProviderControlledRealNonProductionRead`.

## P2 - Secret Provider Controlled Real NonProduction Read

P2 introduces a fail-closed Secret Provider runtime abstraction and metadata-only foundation probe. Default state remains disabled: no read attempted, no secret values exposed, no DB/Auth/Portal runtime and productization remains `NotReady`.

Next gate: `Sprint8P3CommonDbControlledRealConnectivity`.

## P3 - Common DB Controlled Real Connectivity

P3 introduces a fail-closed Common DB connectivity abstraction and metadata-only foundation probe. Default state remains disabled: no connection attempted, no connection string exposed, no EF runtime, no migrations and no productive CRUD.

Next gate: `Sprint8P4PortalAuthControlledRealRuntimeValidation`.

## P4 - Portal Auth Controlled Real Runtime Validation

P4 introduces a fail-closed Portal Auth validation abstraction and metadata-only foundation probe. Default state remains disabled: no Portal HTTP attempted, no request token/header read, no Portal URL/secret/token exposed, no CRM-owned auth and no productive CRUD.

Next gate: `Sprint8P5LockedRouteAuthorizationPolicyIntegration`.
