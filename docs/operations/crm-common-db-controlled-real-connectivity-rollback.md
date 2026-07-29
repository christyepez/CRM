# CRM Common DB Controlled Real Connectivity Rollback

Rollback inmediato:

1. Deshabilitar el flag explícito.
2. Mantener `DisabledCommonDbConnectivityProbe` como default.
3. Confirmar endpoint GET con `CommonDbControlledRealConnectivityEnabled=false`.
4. Confirmar probe POST con 423 Locked.

No se eliminan datos porque P3 no persiste ni cambia schema.
