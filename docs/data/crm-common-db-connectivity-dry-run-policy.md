# CRM Common DB Connectivity Dry-Run Policy

The Common DB dry-run is contract-only in Sprint 6 P3.

Allowed:

- Referencing safe mock metadata from Sprint 6 P2.
- Publishing `mock://crm/common-db` as a synthetic connection reference.
- Reporting disabled flags and required approvals.

Forbidden:

- Real connection strings.
- Connection string resolution.
- Database connections.
- SQL provider activation.
- EF runtime.
- Migrations.
- CRM-owned SQL Server services.
- Real secret, file or environment reads.
- Productive CRM routes or DELETE.
