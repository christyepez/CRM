# CRM Sprint 6 Integrated Evidence

Evidence consolidated for Sprint 6:

- Build: `dotnet build CRM.sln --no-restore` passed.
- Tests: unit and architecture tests passed.
- Frontend: `pnpm run build`, `pnpm test`, and Node foundation verifier passed.
- Docker: compose config/up passed with `crm-api` on 8093.
- Health: `/health`, `/health/live`, `/health/ready`, `/api/crm/readiness` passed.
- Foundation endpoints: P1-P5 Sprint 6 endpoints returned 200.
- Negative routes: `/api/crm/leads`, `/api/crm/accounts`, `/api/crm/contacts` returned 404.
- Security: no real secrets, token/header reads, Auth middleware or Portal HTTP.
- Data: no DB runtime, EF runtime, migrations, SQL Server or connection strings.
