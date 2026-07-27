# CRM Sprint 4 P1 Runtime Environment Readiness

Status: `RuntimeEnvironmentReadiness`.

Purpose: harden local runtime checks before Sprint 4 P2/P3 probes. This package does not activate real database, EF runtime, Auth runtime, Portal runtime, productive routes, DELETE or productive UI.

Expected local shape:

- Docker Compose is available.
- `crm-api` runs on port `8093`.
- SQL Server is not defined by CRM Compose.
- Health endpoints answer when API is running.
- Frontend verifier can use Node from PATH or the bundled Node executable.

Warning: `Runtime readiness only; no real activation`.

Next Gate: `Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag`.
