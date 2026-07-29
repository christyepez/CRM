# CRM Sprint 8 P3 - Common DB Controlled Real Connectivity

Sprint 8 P3 prepara conectividad real controlada a Common DB solo para NonProduction. Por defecto queda deshabilitada y fail-closed.

## Decisión

- NonProduction only.
- Flag explícito requerido.
- Depende de metadata sanitizada de Secret Provider P2.
- Usa únicamente el nombre lógico `crm-common-db-connection`.
- No expone connection strings en API, logs, repo, cache ni persistencia.
- No activa EF runtime, migrations, schema changes, CRUD productivo ni SQL Server propio.

## Estado por defecto

`CommonDbControlledRealConnectivityEnabled=false`, `CommonDbConnectivityAttempted=false`, `CommonDbConnected=false`, `ConnectionStringResolved=false`, `ConnectionStringReturnedToApi=false`.

## Next gate

`Sprint8P4PortalAuthControlledRealRuntimeValidation`.
