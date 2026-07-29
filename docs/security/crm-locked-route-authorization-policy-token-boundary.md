# CRM Locked Route Authorization Policy Token Boundary

CRM P5 must not read, parse, cache, persist, log, or return tokens.

Forbidden:

- Authorization header access.
- Request header access for auth material.
- JWT parsing or CRM-owned token validation.
- Cookie auth or CRM-owned session auth.
- Token storage in browser, API, DB, logs, or telemetry.

Allowed:

- Safe booleans such as `tokenReadAttempted=false`.
- Safe policy decision labels such as `BlockedBecauseRouteLocked`.
- Safe Portal Auth readiness metadata inherited from P4.
