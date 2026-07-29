# CRM Common DB Controlled Real Connectivity Policy

La conectividad real controlada a Common DB solo puede habilitarse en NonProduction y fuera del repositorio.

## Reglas

- No `.env`.
- No connection strings reales en appsettings ni Git.
- No SQL Server en Docker Compose de CRM.
- No migrations ni cambios de schema.
- No EF runtime productivo.
- No CRUD productivo ni DELETE.
- No Portal Auth runtime, Portal HTTP, tokens o headers.

Si falta una condición, el probe debe devolver Locked, Skipped o Blocked.
