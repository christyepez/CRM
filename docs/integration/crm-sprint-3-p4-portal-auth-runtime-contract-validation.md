# CRM Sprint 3 P4 Portal Auth Runtime Contract Validation

Status: `PortalAuthRuntimeContractValidation`.

P4 validates the future Portal Auth runtime contract without activating real authentication. PortalCorporativo owns Auth, SSO, users, tenants, permissions and policy evaluation. CRM consumes contracts only.

Decision:
- Portal Runtime Connected: `false`.
- Auth Runtime Enabled: `false`.
- CRM Owns Auth: `false`.
- Auth Owned By: `PortalCorporativo`.
- Token Storage Enabled: `false`.
- Login Implemented By CRM: `false`.
- Identity Implemented By CRM: `false`.
- Permissions Persisted In CRM: `false`.
- Foundation Simulation Active: `true`.
- Productive Authorization Enabled: `false`.
- Next Gate: `Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag`.
- Warning: `Portal Auth runtime contract validation only; no real Auth runtime configured`.

No Portal HTTP, tokens, middleware, roles, databases, migrations or productive CRM routes are added in P4.
