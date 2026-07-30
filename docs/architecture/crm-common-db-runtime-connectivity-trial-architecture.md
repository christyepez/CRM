# CRM Common DB Runtime Connectivity Trial Architecture

P3 wraps the existing `ICommonDbConnectivityProbe` boundary with `CommonDbRuntimeConnectivityTrialService`.

Boundary:

- Application exposes decision/status contracts.
- Infrastructure owns the runtime trial adapter.
- API exposes read-only status and a locked/sanitized probe.

The adapter enforces:

- NonProduction-only.
- Explicit flag.
- Approved logical secret name.
- Metadata-only response.
- Production blocked.
- No schema, migrations, EF productive runtime, SQL Server service, productive CRUD, DELETE or Portal Auth runtime.
