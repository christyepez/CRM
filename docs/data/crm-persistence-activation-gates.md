# CRM Persistence Activation Gates

Required gates before Sprint 2 P2 implementation:

1. DatabaseApproval: confirm the approved common/local database strategy without CRM-owned SQL Server.
2. PortalAuthorization: confirm Portal authorization expectations before productive CRUD.
3. MigrationPlan: approve naming, rollback, idempotency and seed rules.
4. DataPolicy: confirm fictitious non-production data only.
5. Guardrails: verify no shared DB with Financiero and no Portal data duplication.

GO only when every gate is approved.
