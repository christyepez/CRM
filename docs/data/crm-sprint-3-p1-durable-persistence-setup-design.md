# CRM Sprint 3 P1 Durable Persistence Setup Design

Status: `DurablePersistenceSetupDesign`.

Decision:
- Durable Persistence: `DesignOnly`.
- Real DB: `NotConfigured`.
- EF Runtime: `NotEnabled`.
- Migrations: `PlannedOnly`.
- Connection Strings: `NotConfigured`.
- Secrets: `StrategyOnly`.
- SQL Server Owned By CRM: `false`.
- Productive Activation: `NoGo`.

This package designs the future non-production durable persistence setup only. It does not create a database, EF runtime, DbContext, DbSet, migrations, secrets, `.env`, or productive routes.

Next gate: `Sprint3P2CommonDbConnectionContractAndSecretStrategy`.
