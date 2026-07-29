# CRM Secret Provider Controlled Real Read Rollback

Rollback inmediato:

1. Deshabilitar flag explícito.
2. Dejar `DisabledSecretProviderRuntime` como default.
3. Confirmar endpoint GET con `SecretProviderControlledRealNonProductionReadEnabled=false`.
4. Confirmar probe POST con 423/Locked.

No se eliminan datos porque P2 no persiste secretos.
