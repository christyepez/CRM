# CRM Portal Auth Controlled Runtime Redaction

P4 redaction rules:

- Private Portal URLs are never returned, logged, persisted or cached.
- Secret values are never returned, logged, persisted or cached.
- Tokens are never returned, logged, persisted or cached.
- Public contracts expose booleans and sanitized categories only.
- Error categories must remain generic, for example `Locked`, `Skipped`, `Blocked`, `Timeout` or `PortalAuthValidationFailure`.

Any future real provider must keep values inside the infrastructure boundary.
