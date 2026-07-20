# CRM / Portal Boundary

PortalCorporativo remains the owner of cross-cutting platform capabilities.

## CRM must reuse or adapt

- Security and permissions.
- Menu and navigation.
- Audit.
- Notification.
- Configuration.
- Gateway.
- Content/File when documents are needed.

## P2 status

Integration is Planned only. No runtime calls to Portal are implemented in P2.

## Rules

- CRM does not create login, Identity, roles or token storage.
- CRM does not duplicate audit or notification services.
- CRM does not create a Portal shell or gateway.
- CRM exposes draft metadata so Portal integration can be planned safely.
