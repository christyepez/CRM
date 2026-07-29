# CRM Sprint 8 P2 - Secret Provider Controlled Real NonProduction Read

Sprint 8 P2 habilita la estructura para lectura real controlada de Secret Provider solo en NonProduction. Por defecto permanece deshabilitada y fail-closed.

## Decisión

- NonProduction only.
- Flag explícito requerido.
- Allow-list obligatoria de nombres lógicos.
- Redaction obligatoria.
- No se retorna, registra, persiste ni cachea ningún valor secreto.
- Production siempre NoGo.

## Allow-list

- `crm-common-db-connection`
- `crm-portal-auth-base-url`
- `crm-portal-auth-client-id`
- `crm-portal-auth-client-secret`
- `crm-observability-endpoint`

## Estado por defecto

`SecretProviderControlledRealNonProductionReadEnabled=false`, `RealSecretReadAttempted=false`, `SecretValueReturnedToApi=false`, `SecretValuePersisted=false`, `SecretValueCached=false`.

## Next gate

`Sprint8P3CommonDbControlledRealConnectivity`.
