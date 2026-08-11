# CRM Controlled Runtime Crosscutting Boundary

Portal owns crosscutting capabilities. CRM must not duplicate them.

## Portal-owned capabilities

- Auth.
- Menu.
- Permissions.
- Audit.
- Configuration.
- Notification.

## CRM future adapter responsibility

CRM may implement adapters that call approved Portal contracts after a future gate, but P4 does not enable runtime calls or configure real providers.

## Markers

- CrosscuttingBoundaryPrepared: true.
- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
- RealSecretProviderConfigured: false.
- RealNotificationProviderConfigured: false.
