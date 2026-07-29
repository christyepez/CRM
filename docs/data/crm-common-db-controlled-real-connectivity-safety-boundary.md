# CRM Common DB Controlled Real Connectivity Safety Boundary

P3 permite solo preparar el boundary de conectividad. No habilita persistencia de dominio.

## Permitido

- Metadata sanitizada de disponibilidad.
- Timeout corto.
- Fail closed.
- Futura apertura/cierre controlada o `SELECT 1` sin schema.

## Prohibido

- Connection strings en API/logs/repo.
- Persistencia/cache de connection strings.
- Migrations.
- Schema changes.
- EF runtime productivo.
- CRUD productivo.
- SQL Server propio.
