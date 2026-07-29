# CRM Portal Auth Controlled Runtime Validation Policy

Policy:

- NonProduction only.
- Explicit enable flag required.
- Disabled by default.
- Fail closed.
- Use approved logical secret names only.
- Use Secret Provider metadata only; do not expose secret values.
- Do not expose, log, persist or cache private Portal URLs.
- Do not read request headers or Authorization headers.
- Do not validate real user tokens in CRM.
- Do not implement CRM-owned login, logout, Identity, roles or permissions persistence.
- Do not enable productive auth middleware or productive routes.

Production activation is NoGo until a later architecture and security approval.
