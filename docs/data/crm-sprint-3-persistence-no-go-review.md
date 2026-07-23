# CRM Sprint 3 Persistence No-Go Review

Persistence decision: `NoGo`.

CRM has design and contract evidence only. It must not activate durable persistence, EF runtime, migrations, connection strings or SQL Server owned by CRM.

Required before GO:

- Common SQL environment probe.
- Secret provider runtime outside repository.
- Migration governance.
- Backup/restore and rollback plan.
- Synthetic non-production data policy.
