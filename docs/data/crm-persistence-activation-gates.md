# CRM Persistence Activation Gates

Required gates before Sprint 2 P2 implementation:

1. DatabaseApproval: confirm the approved common/local database strategy without CRM-owned SQL Server.
2. PortalAuthorization: confirm Portal authorization expectations before productive CRUD.
3. MigrationPlan: approve naming, rollback, idempotency and seed rules.
4. DataPolicy: confirm fictitious non-production data only.
5. Guardrails: verify no shared DB with Financiero and no Portal data duplication.

GO only when every gate is approved.
## Sprint 2 P2 update

`NonProductionSeam` is active for foundation preview storage only. Productive persistence remains gated by Portal authorization simulation, migration approval and explicit database activation. Current values remain: `databaseConfigured=false`, `dbContextConfigured=false`, `migrationReady=false`, `durablePersistence=false`, `productiveCrudEnabled=false`.

P5 confirms durable persistence remains NO-GO until DB, secrets, migrations, rollback, retention and backup/restore gates are approved.
