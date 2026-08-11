# CRM Controlled Runtime Pilot Conditional Implementation Client Enablement

## Future client plan

The future Portal client must be disabled-by-default and fail closed. It must not store tokens, expose secrets, or call Portal until a future explicit Go enables NonProduction runtime checks.

## Markers

- ConditionalImplementationClientEnablementPrepared: true.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- SsoOidcProductionConfigured: false.
- BrowserTokenStorageDetected: false.
