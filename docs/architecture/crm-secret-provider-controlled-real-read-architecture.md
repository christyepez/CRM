# CRM Secret Provider Controlled Real Read Architecture

P2 introduce una abstracción de runtime:

- `ISecretProviderRuntime`
- `DisabledSecretProviderRuntime`
- `ControlledNonProductionSecretProviderRuntime`

La API foundation consume el runtime deshabilitado por defecto. La implementación controlada valida NonProduction, flag explícito, allow-list y redaction antes de intentar una lectura mediante puerto seguro.

## Límites

- No SQL Server.
- No EF runtime.
- No Portal HTTP.
- No token/header reads.
- No Productive UI.
- No DELETE.
- No Production.

P3 puede usar solo metadata de disponibilidad, nunca valores secretos.
