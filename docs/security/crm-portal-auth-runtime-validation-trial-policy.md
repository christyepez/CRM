# CRM Portal Auth Runtime Validation Trial Policy

PortalCorporativo owns authentication and authorization. CRM must not create login, logout, Identity, roles or permission persistence.

P4 is allowed only as a NonProduction trial and is disabled by default. It may consume sanitized metadata from Sprint 9 P2 Secret Provider and Sprint 9 P3 Common DB, but it must not consume secret values, connection strings, request tokens or Authorization headers by default.

Production activation remains blocked. Productive authorization, `[Authorize]`, auth middleware, Portal HTTP calls and CRM productive routes remain out of scope.
