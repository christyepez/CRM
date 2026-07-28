# CRM Common DB Connectivity Dry-Run Runbook

Validation steps:

1. Confirm Sprint 6 P2 is merged.
2. Run `dotnet build` and `dotnet test`.
3. Run `tools/preflight-crm-local.ps1`.
4. Run `tools/check-crm-guardrails.ps1`.
5. Run `docker compose config` and confirm no SQL Server service exists.
6. Start `crm-api`.
7. Check `GET /api/crm/foundation/sprint-6/common-db-connectivity-dry-run`.
8. Confirm `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` remain 404.

Rollback:

- Revert the P3 branch/commit.
- No data cleanup is required because no connection, schema or external state is created.
