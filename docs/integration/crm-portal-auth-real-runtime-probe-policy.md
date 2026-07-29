# CRM Portal Auth Real Runtime Probe Policy

The P4 probe is contract-only. CRM must not duplicate PortalCorporativo authentication, identity, roles, permissions, login, logout, token storage or token validation.

Forbidden in P4:

- real Portal URL resolution or materialization
- Portal HTTP calls
- real `HttpClient` toward Portal
- Authorization header reads
- token/header reads
- token materialization, logs or API return
- Auth middleware or productive `[Authorize]`
- login/logout endpoints
- CRM-owned Identity, roles or permissions persistence
- secret reads, Key Vault runtime calls or `.env` reads
- database runtime, migrations or productive routes

Future activation requires Security, Architecture, DevOps and Portal owner approval.
