# CRM Sprint 3 Gates

Required gates:
- Durable persistence design approved.
- Common DB contract approved.
- Secret strategy approved.
- Disabled EF prototype approved.
- Portal Auth runtime contract validated.
- Productive route draft remains disabled.
- Architecture guardrails pass.
- Security and data reviews pass.

Next gate: `Sprint3P1DurablePersistenceSetupDesign`.

Sprint 3 P1 result: durable persistence setup is `DesignOnly`; continue to `Sprint3P2CommonDbConnectionContractAndSecretStrategy`.

Sprint 3 P2 result: common DB connection strategy is `ContractOnly`; continue to `Sprint3P3EfDbContextPrototypeBehindDisabledFlag`.
# Sprint 3 P3 gates

- EF prototype exists: `true`.
- EF runtime enabled: `false`.
- DbContext runtime active: `false`.
- Migrations created: `false`.
- Real database configured: `false`.
- Provider configured: `false`.
- Next gate: `Sprint3P4PortalAuthRuntimeContractValidation`.

# Sprint 3 P4 gates

- Portal runtime connected: `false`.
- Auth runtime enabled: `false`.
- CRM owns Auth: `false`.
- Foundation simulation active: `true`.
- Productive authorization enabled: `false`.
- Next gate: `Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag`.
