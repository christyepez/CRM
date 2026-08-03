# CRM Common DB Logical Model

This is a logical model only. It does not create schema, migrations or runtime mappings.

## Candidate CRM-owned areas

- Leads.
- Accounts.
- Contacts.
- Opportunities.
- Activities.
- Customer interaction summaries.
- CRM read models approved by future gates.

## Non-owned areas

- Portal users, roles, permissions, menus, configuration, audit entries and notifications.
- Financial domain entities.
- Shared platform operational tables.

## Persistence rules

- CommonDbLogicalModelPrepared: true.
- CrossDomainMigrationsPresent: false.
- SharedPortalTablesAccessEnabled: false.
- PortalDatabaseDirectAccessEnabled: false.
- CommonDbRuntimeEnabled: false.
- RealDataPresent: false.
