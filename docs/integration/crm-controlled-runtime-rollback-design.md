# CRM Controlled Runtime Rollback Design

P4 rollback is a branch/PR revert because it is design-only.

## Future pilot rollback

- Disable pilot feature flags.
- Remove CRM route registration from the future pilot environment.
- Stop metadata probes.
- Preserve CRM and Portal data boundaries.
- Do not drop shared platform SQL Server.
- Do not modify Portal databases or Portal Gateway state directly.

## Markers

- ControlledRuntimeRollbackDesignPrepared: true.
- PortalDatabaseDirectAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
- SharedPortalTablesAccessEnabled: false.
