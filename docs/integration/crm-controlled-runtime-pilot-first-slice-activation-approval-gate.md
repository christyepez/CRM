# CRM Controlled Runtime Pilot First Slice Activation Approval Gate

## Purpose

P18 consolidates P14 to P17 evidence and defines the approval gate required before a later sprint may implement a limited NonProduction activation plan.

## Boundaries

- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- ProductivePortalNavigationEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- PortalServicesInCrmCompose: false.
- CommonDbRuntimeEnabled: false.
- RealCommonDbConnectionConfigured: false.
- SharedPortalTablesAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
- PortalDatabaseDirectAccessEnabled: false.

## Portal capability boundaries

- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
