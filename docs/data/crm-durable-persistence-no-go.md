# CRM Durable Persistence NO-GO

Decision: Durable Persistence and Real Database are `NoGo`.

Reasons:
- `DatabaseConfigured=false`.
- `DurablePersistence=false`.
- No schema, migrations, rollback, retention, backup/restore or secret strategy is approved.
- CRM must not create its own SQL Server container.

Required before GO:
- Common DB connection contract.
- Logical CRM database ownership.
- Migration and rollback plan.
- Backup/restore and retention policy.
- Synthetic test data policy.
