# CRM Common DB Controlled Real Connectivity Runbook

## Default

No hacer nada. El probe queda apagado y bloqueado por defecto.

## Activación futura NonProduction

1. Confirmar ambiente NonProduction.
2. Confirmar Secret Provider P2 aprobado.
3. Configurar provider externo fuera del repo.
4. Habilitar flag explícito fuera del repo.
5. Usar solo `crm-common-db-connection`.
6. Validar timeout corto.
7. Confirmar que no hay connection strings en logs ni API.

P3 no habilita EF, migrations, schema changes ni CRUD.
