# CRM Common DB Controlled Activation Security Decision

The Common DB activation path remains fail-closed, NonProduction-only and metadata-only.

## Decision

- CommonDbSecurityDecisionPrepared: true.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- RealNotificationProviderConfigured: false.
- RealObservabilityProviderConfigured: false.
- BrowserTokenStorageDetected: false.
- SecretsPresent: false.
- PrivateUrlsPresent: false.
- RealDataPresent: false.

## Controls

- No real secret values in repo, logs, API responses or docs.
- No token, certificate or private URL materialization.
- No browser token storage.
- No production Auth or DB runtime.
- Any future connection must use approved secret-provider metadata and return sanitized probe status only.
