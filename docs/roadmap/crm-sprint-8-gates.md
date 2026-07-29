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
