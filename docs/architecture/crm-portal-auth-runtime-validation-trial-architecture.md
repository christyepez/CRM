# CRM Portal Auth Runtime Validation Trial Architecture

P4 adds a metadata-only foundation endpoint and a locked probe around the existing Portal Auth runtime validation abstraction.

Architecture boundaries:
- PortalCorporativo remains owner of Auth/Security.
- CRM does not implement login, logout, Identity, roles or permissions.
- CRM does not read Authorization headers or tokens by default.
- CRM does not call Portal HTTP by default.
- CRM does not expose Portal base URLs, client secrets, tokens or claims.
- CRM does not activate DB runtime, EF, migrations, productive routes, CRUD or DELETE.

The adapter depends on the existing disabled Portal Auth validation probe and returns sanitized metadata. The next gate is `Sprint9P5ProductiveRouteDryRunTrial`.
