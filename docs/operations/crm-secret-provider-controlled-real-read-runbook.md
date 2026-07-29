# CRM Secret Provider Controlled Real Read Runbook

## Default

No hacer nada: P2 queda apagado por defecto.

## Para prueba NonProduction futura

1. Confirmar ambiente NonProduction.
2. Configurar provider externo seguro fuera del repo.
3. Habilitar flag explícito fuera del repo.
4. Usar solo nombres del allow-list.
5. Validar que API retorna metadata sanitizada.
6. Confirmar que logs no contienen valores.

P2 no habilita DB/Auth/Portal runtime.
