# CRM Common DB Runtime Connectivity Trial Policy

Policy:

- NonProduction-only.
- Disabled by default.
- Explicit flag required.
- Fail closed.
- Metadata-only observability.
- Use only sanitized Secret Provider metadata from Sprint 9 P2.

Forbidden:

- Production DB runtime activation.
- Connection string values in API, logs, docs, persistence or cache.
- Schema creation.
- Migration execution.
- EF productive runtime.
- Productive CRUD, DELETE or default productive routes.
- CRM-owned SQL Server service.
