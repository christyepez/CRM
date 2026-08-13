# CRM Controlled Runtime Pilot First Slice Scaffold Security Decision

## Decision

Approve a disabled-by-default scaffold only.

## Security boundaries

- No SSO/OIDC production configuration.
- No real secret provider.
- No real notification provider.
- No real observability provider.
- No browser token storage.
- No real Portal private URL.
- No Portal capability duplication.

## Markers

- FirstSliceScaffoldSecurityDecisionPrepared: true.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- BrowserTokenStorageDetected: false.
