# CRM Sprint 4 P2 Common DB Runtime Probe

Status: `CommonDbRuntimeProbe`.

The probe exists, but it is disabled by default: `commonDbRuntimeProbeEnabled=false`.

The CRM API must continue starting without a database. P2 does not add a real DB, does not read secrets, does not require connection values, does not create migrations, does not activate EF runtime and does not add SQL Server to Docker Compose.

Default decision:

- `commonDbRuntimeProbeExists=true`
- `commonDbRuntimeProbeEnabled=false`
- `dbConnectionAttemptedByRuntime=false`
- `apiRequiresDatabase=false`
- `sqlServerOwnedByCrm=false`

Warning: `Common DB runtime probe exists but is disabled; no database connection is attempted`.

Next Gate: `Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag`.
