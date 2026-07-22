# CRM Sprint 1 Integrated Evidence

## Validation evidence

- `dotnet restore CRM.sln`: passed.
- `dotnet build CRM.sln --no-restore`: passed.
- `DOTNET_ROLL_FORWARD=Major dotnet test CRM.sln --no-build`: passed through Sprint 1.
- Frontend Angular build: passed with bundled Node runtime.
- `tools/verify-crm-foundation.ps1`: passed.
- `docker compose config`: passed.

## Runtime evidence

Local health/readiness checks passed for:

- `/health`
- `/health/live`
- `/health/ready`
- `/api/crm/readiness`
- `/api/crm/domain-catalog`
- `/api/crm/contracts`
- `/api/crm/integration-boundaries`
- `/api/crm/foundation/sprint-1/closure-status`

## Docker registry note

`docker compose up -d --build` may remain `BLOCKED_EXTERNAL_REGISTRY` when `mcr.microsoft.com/dotnet/aspnet:8.0` times out. This is external to CRM source code when local build, tests, compose config and local health pass.
