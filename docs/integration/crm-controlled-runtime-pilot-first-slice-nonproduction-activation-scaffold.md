# CRM Controlled Runtime Pilot First Slice NonProduction Activation Scaffold

## Scope

P21 introduces a foundation/status endpoint, disabled activation options, disabled activation flags and a no-op disabled service.

## Runtime boundaries

- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- ProductivePortalNavigationEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- RealPortalPrivateUrlsPresent: false.
- PortalServicesInCrmCompose: false.
- CommonDbRuntimeEnabled: false.
- RealCommonDbConnectionConfigured: false.
- SharedPortalTablesAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
- PortalDatabaseDirectAccessEnabled: false.

## Portal duplication boundaries

- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
