# CRM Controlled Runtime Pilot Scaffold Overview

This scaffold prepares the next validation step after Sprint 10 P4 design. It defines how CRM will be checked before any real Portal runtime integration is enabled.

## Boundaries

- Portal remains owner of Auth, Menu, Permissions, Audit, Notification and Configuration.
- CRM remains consumer-only and does not duplicate Portal capabilities.
- The pilot scaffold has no runtime Portal calls.
- The pilot scaffold has no productive routes or navigation.
- The pilot scaffold has no Common DB runtime.

## Markers

- ControlledRuntimePilotScaffoldPrepared: true.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.
