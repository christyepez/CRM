# CRM Portal Auth Controlled Runtime Token Boundary

CRM P4 must not read, validate, store, log, cache or return real request tokens.

Forbidden:

- Reading `Authorization` headers.
- Reading request headers for auth decisions.
- Creating token storage.
- Returning token values through API contracts.
- Logging tokens.
- Persisting tokens.
- Implementing login/logout in CRM.

PortalCorporativo remains the owner of Auth/Security.
