# CRM Controlled Runtime Common DB Boundary

The future pilot may use the common environment SQL Server only through a CRM-owned logical database and after explicit approval. P4 does not activate any DB runtime.

## Rules

- CRM does not create a SQL Server container.
- CRM does not use Portal databases or Portal tables.
- CRM does not create shared tables with Portal.
- CRM does not run cross-domain migrations.
- CRM does not configure real connection strings.

## Markers

- CommonDbBoundaryPrepared: true.
- CommonDbRuntimeEnabled: false.
- RealCommonDbConnectionConfigured: false.
- SharedPortalTablesAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
- PortalDatabaseDirectAccessEnabled: false.
