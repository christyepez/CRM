# CRM Sprint 3 P2 Common DB Connection and Secret Strategy

Status: `CommonDbConnectionStrategy`.

Decision:
- Real Database Configured: `false`.
- Connection Strings Configured: `false`.
- Secret Provider Configured: `false`.
- Secret Provider Runtime Connected: `false`.
- SQL Server Owned By CRM: `false`.
- EF Runtime Enabled: `false`.
- DbContext Configured: `false`.
- Migrations Created: `false`.
- Logical Database Name: `CrmDb`.
- Logical DB Placeholder: `true`.
- Secret Strategy: `ContractOnly`.
- Connection String Policy: `NoRealValuesInRepository`.
- Next Gate: `Sprint3P3EfDbContextPrototypeBehindDisabledFlag`.

Warning: `Common DB connection contract only; no real database or secrets configured`.

This sprint defines contracts and placeholders only. It does not configure a real database, secret provider, EF runtime, migrations, connection values, SQL Server container, Portal runtime or productive API.
