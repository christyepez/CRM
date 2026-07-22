# CRM Sprint 2 P1 Persistence Design Review

This sprint reviews controlled persistence activation without enabling a database, migrations, DbContext, DbSet, SQL queries or productive CRUD.

## Decision summary

- Persistence mode: DesignOnly.
- Runtime mode: NonProduction.
- Database configured: false.
- Migration ready: false.
- CRM-owned SQL Server: false.
- Next gate: Sprint2P2PersistenceSeam.

## First aggregates

Persist first, only after the gate approves:

- Lead
- Account
- Contact

Opportunity is deferred until Lead, Account and Contact persistence seams are proven.

## Not persisted in CRM

- Portal user context.
- Portal permissions.
- Portal audit.
- Portal notifications.
- Financiero customer/account status.
- Reporting datasets.

CRM must not duplicate Portal master data and must not share a database with Financiero.
