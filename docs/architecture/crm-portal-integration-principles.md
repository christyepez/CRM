# CRM Portal Integration Principles

CRM must reuse PortalCorporativo platform capabilities instead of replacing them.

## Planned reuse

- Auth/SSO: Portal Security.
- Menu/permissions: Portal Menu and Security resources.
- Gateway: Portal Gateway.
- Audit: Portal Audit.
- Notifications: Portal Notification.
- Configuration: Portal Configuration.
- Content/File: Portal Content/File if CRM needs documents.
- Reporting: Portal Reporting/BI boundaries when available.

## Integration rules

- No login propio.
- No Identity propio.
- No local token storage.
- No roles persisted by CRM.
- No Gateway propio.
- No Portal Shell propio.
- No duplicated audit/notification/configuration engines.

## Current status

All Portal integrations are `Planned`; no real Portal adapter is implemented in P1.
