# CRM EF Runtime Activation Gates

Current gate: `Sprint3P4PortalAuthRuntimeContractValidation`.

Required before EF runtime activation:

1. Common DB approval.
2. Secret provider approval.
3. Runtime provider package approval.
4. Disabled registration strategy.
5. Migration, rollback and backup/restore plan.
6. Synthetic data policy.
7. Portal authorization runtime validation.
8. Audit/outbox reuse confirmation.
9. Operational smoke tests.

Until all gates are green:
- EF Runtime Enabled: `false`.
- DbContext Runtime Active: `false`.
- Real Database Configured: `false`.
- Productive CRUD Enabled: `false`.

Sprint 3 P5 keeps EF Runtime Enabled: `false` while productive API route contracts are draft-only.
