# CRM Secret Provider Safe Mock Synthetic Values

Allowed synthetic values:

- `mock://crm/common-db`
- `mock://crm/portal-auth-base-url`
- `mock-client-id`
- `mock-client-secret-not-real`
- `mock://crm/observability`

These values are safe placeholders. They are intentionally non-sensitive and not runtime usable. They must not be replaced with real values in source control, documentation examples, tests or logs.
