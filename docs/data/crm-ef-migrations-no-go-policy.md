# CRM EF Migrations No-Go Policy

P3 explicitly forbids migrations.

No-go items:
- No migration files.
- No migration commands.
- No database folder.
- No baseline schema.
- No rollback script.
- No live database execution.

Migration activation requires:
- Common SQL Server environment confirmed.
- Logical database `CrmDb` approved.
- Secret provider approved.
- Backup/restore and rollback plan approved.
- Synthetic data plan approved.
- Portal authorization runtime validated.

Until those gates pass, migrations remain `NoGo`.
