# CRM Auth No-Go Policy

No-go in P4:

- CRM login/logout.
- CRM Identity.
- CRM JWT/cookie auth.
- CRM credential storage.
- CRM token parsing.
- CRM persisted roles.
- CRM persisted permissions.
- Portal HTTP runtime calls.
- Portal URLs in configuration or source.
- Productive CRM routes.
- DELETE endpoints.

Warning: `Portal Auth runtime contract validation only; no real Auth runtime configured`.
