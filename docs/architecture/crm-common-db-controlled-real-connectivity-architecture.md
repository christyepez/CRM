# CRM Common DB Controlled Real Connectivity Architecture

P3 introduce:

- `ICommonDbConnectivityProbe`
- `DisabledCommonDbConnectivityProbe`
- `ControlledNonProductionCommonDbConnectivityProbe`

La API foundation usa el probe deshabilitado por defecto. El probe controlado valida NonProduction, flag explícito, Secret Provider P2 aprobado y `crm-common-db-connection`.

## Límites

- No SQL Server propio.
- No EF runtime.
- No migrations.
- No schema changes.
- No Portal HTTP.
- No token/header reads.
- No Productive UI.
- No DELETE.

P4 puede avanzar a Portal Auth real runtime validation sin asumir persistencia productiva.
