# CRM Controlled Runtime Pilot Scaffold Security Decision

## Decision

P5 is approved only as a disabled scaffold. It does not activate real Portal Auth, SSO/OIDC, secret provider, notification provider, observability provider, database runtime or productive routes.

## Security markers

- ControlledRuntimePilotSecurityDecisionPrepared: true.
- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- RealNotificationProviderConfigured: false.
- RealObservabilityProviderConfigured: false.
- BrowserTokenStorageDetected: false.
- SecretsPresent: false.
- EnvRealFileCommitted: false.
- PrivateUrlsPresent: false.
- RealDataPresent: false.
