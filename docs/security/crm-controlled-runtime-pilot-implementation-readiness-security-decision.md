# CRM Controlled Runtime Pilot Implementation Readiness Security Decision

## Decision

Security accepts readiness review only. P12 does not approve production activation, real endpoints, real credentials, runtime Portal calls, SSO/OIDC production setup, Common DB runtime, or browser token storage.

## Security boundaries

- Portal Auth, Menu, Permissions, Audit, Notification and Configuration remain Portal-owned.
- CRM must not duplicate Portal cross-cutting capabilities.
- Repository content must contain placeholders only.

## Markers

- ImplementationReadinessSecurityDecisionPrepared: true.
- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- BrowserTokenStorageDetected: false.
