# CRM Controlled Runtime Pilot Conditional Enablement Security Decision

## Decision

Security approves preparation of a conditional enablement design only. Security does not approve production activation, real credentials, real endpoints, token storage or runtime Portal calls in P10.

## Security boundaries

- Portal Auth ownership remains in Portal.
- CRM does not create login, SSO/OIDC production configuration or token storage.
- CRM does not duplicate Portal Permissions, Audit, Notification or Configuration.
- Secrets remain logical names only.
- No browser storage is introduced for tokens.

## Markers

- ConditionalEnablementSecurityDecisionPrepared: true.
- PortalAuthDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- BrowserTokenStorageDetected: false.
