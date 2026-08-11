# CRM Controlled Runtime Pilot Conditional Implementation Security Decision

## Decision

Security accepts a future implementation plan only. P11 does not approve production activation, real credentials, real endpoints, runtime Portal calls or Common DB activation.

## Boundaries

- Portal Auth, Menu, Permissions, Audit, Notification and Configuration remain owned by Portal.
- CRM must not create login, SSO/OIDC production setup or browser token storage.
- CRM must not commit real secrets, tokens, certificates, private endpoints or real data.

## Markers

- ConditionalImplementationSecurityDecisionPrepared: true.
- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- BrowserTokenStorageDetected: false.
