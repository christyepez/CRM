# CRM Controlled Runtime Pilot First Slice Security Checklist

## Checklist

- Portal Auth remains Portal-owned.
- Portal Menu and Permissions remain Portal-owned.
- Audit, Notification and Configuration are not duplicated in CRM.
- No SSO/OIDC production configuration.
- No real secret provider or notification provider.
- No browser token storage.

## Markers

- FirstSliceSecurityChecklistPrepared: true.
- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- RealNotificationProviderConfigured: false.
- BrowserTokenStorageDetected: false.
