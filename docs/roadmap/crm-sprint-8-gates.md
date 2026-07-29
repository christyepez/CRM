# CRM Sprint 8 Gates

Gate order:

1. `Sprint8P1SecretProviderApprovalDecision`
2. `Sprint8P2SecretProviderControlledRealNonProductionRead`
3. `Sprint8P3CommonDbControlledRealConnectivity`
4. `Sprint8P4PortalAuthControlledRealRuntimeValidation`
5. `Sprint8P5LockedRouteAuthorizationPolicyIntegration`
6. `Sprint8P6Sprint8GateDecision`

All gates must preserve no real production activation, no secrets in repo, no CRM-owned Auth, no CRM-owned SQL Server and no productive UI until explicitly approved.
## Sprint 8 P1 Gate

Status: `SecretProviderApprovalDecision`.

Decision: `ApprovedForControlledNonProductionReadPlanning`.

P1 approves entering P2, not reading secrets in P1. Productive activation remains `NoGo`.

## Sprint 8 P2 Gate

Status: `SecretProviderControlledRealNonProductionRead`.

Decision: controlled NonProduction read scaffold implemented, disabled by default and metadata-only.

P2 permits P3 to use only sanitized availability metadata. Productive activation remains `NoGo`.

## Sprint 8 P3 Gate

Status: `CommonDbControlledRealConnectivity`.

## Sprint 8 P4 Gate

Status: `PortalAuthControlledRealRuntimeValidation`.

Must remain disabled by default, NonProduction-only and fail-closed. CRM must not read request tokens/headers, expose Portal URLs/secrets/tokens, implement login/logout/Identity, persist roles/permissions, enable auth middleware, enable `[Authorize]`, activate productive routes, enable DELETE, enable DB runtime or enable product UI.

## Sprint 8 P5 Gate

Status: `LockedRouteAuthorizationPolicyIntegration`.

Must remain disabled by default, NonProduction-only and fail-closed. Productive routes must remain 404 by default. Explicit locked routes may return 423 with sanitized policy metadata only. CRM must not activate CRUD, domain execution, persistence, DELETE, DB runtime, EF runtime, Portal HTTP, token/header reads, auth middleware, `[Authorize]`, product UI or real Portal Auth.

Next gate: `Sprint8P6Sprint8GateDecision`.

## Sprint 8 P6 Gate

Status: `Sprint8GateDecision`.

Overall decision: `GoForSprint9ControlledRuntimeActivationPlanning`.

Production activation remains `NoGo`; productization remains `NotReady`; Sprint 9 planning is `Go`.

Next gate: `Sprint9P1ControlledRuntimeActivationDecision`.

Decision: controlled NonProduction connectivity scaffold implemented, disabled by default and metadata-only.

P3 permits P4 Portal Auth validation planning without implying productive persistence. Productive activation remains `NoGo`.
