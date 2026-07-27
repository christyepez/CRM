# CRM Sprint 4 Integrated Evidence

Required evidence for Sprint 4 gate:

- Build: `dotnet build CRM.sln --no-restore`.
- Tests: `DOTNET_ROLL_FORWARD=Major dotnet test CRM.sln --no-build`.
- Frontend: `pnpm run build`, `pnpm run test`, `node tools/verify-crm-foundation.mjs`.
- Tooling: `tools/preflight-crm-local.ps1`, `tools/check-crm-guardrails.ps1`, `tools/check-crm-health.ps1`, `tools/check-crm-e2e-foundation.ps1`, `tools/verify-crm-foundation.ps1`.
- Docker: `docker compose config`, `docker compose up -d --build`, `docker compose ps`.
- Health: `/health`, `/health/live`, `/health/ready`, `/api/crm/readiness`, `/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness`, `/api/crm/foundation/sprint-4/gate-decision`.
- Negative routes: `/api/crm/leads`, `/api/crm/accounts`, `/api/crm/contacts` must not be active.

Evidence is foundation-only. No real data, DB runtime, Portal Auth runtime, DELETE or productive UI is used.
