# CRM Common DB Connectivity Dry-Run Observability

Sprint 6 P3 observability is metadata-only.

Required signals:

- Dry-run endpoint returns 200.
- `commonDbConnectionAttempted=false`.
- `syntheticConnectionReference=mock://crm/common-db`.
- `realConnectionStringUsed=false`.
- `connectionStringResolved=false`.
- `efRuntimeEnabled=false`.
- Negative productive routes remain 404.

No database telemetry is emitted because no database operation occurs.
