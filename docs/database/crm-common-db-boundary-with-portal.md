# CRM Common DB Boundary With Portal

CRM must not depend on Portal database internals. Portal capabilities are reused through published contracts, not direct SQL access.

## Boundary rules

- CRM owns CRM tables and CRM migrations only after a future approval.
- Portal owns Security, Menu, Permissions, Audit, Notification and Configuration persistence.
- CRM must consume Portal capabilities through API/event/contract adapters.
- CRM must not query, join, migrate or write Portal tables.
- CRM must not create duplicate Portal Auth, Menu, Permissions, Audit, Notification or Configuration modules.

## Duplication status

- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
- PortalRuntimeCouplingEnabled: false.
- ProductivePortalNavigationEnabled: false.
- SharedPortalTablesAccessEnabled: false.
- PortalDatabaseDirectAccessEnabled: false.
