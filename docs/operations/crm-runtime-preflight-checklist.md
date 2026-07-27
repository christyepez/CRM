# CRM Runtime Preflight Checklist

## Sprint 4 P2 common DB runtime probe checks

- [ ] `/api/crm/foundation/sprint-4/common-db-runtime-probe` is registered as GET-only.
- [ ] `Common DB runtime probe exists but is disabled; no database connection is attempted` is present in Application and Infrastructure placeholders.
- [ ] `commonDbRuntimeProbeEnabled=false`.
- [ ] `dbConnectionAttemptedByRuntime=false`.
- [ ] `connectionStringsConfigured=false`.
- [ ] `sqlServerOwnedByCrm=false`.
- [ ] No SQL Server service is defined by CRM Compose.
- [ ] No migration or database folder is introduced.

Before Sprint 4 runtime probes:

- [ ] GitHub `main` is current.
- [ ] Worktree is clean before branch creation.
- [ ] `dotnet restore`, build and tests pass.
- [ ] `docker compose config` passes.
- [ ] Port `8093` is available or intentionally owned by `crm-api`.
- [ ] No SQL Server in CRM Compose.
- [ ] No `.env` committed.
- [ ] No productive `/api/crm/leads`, `/api/crm/accounts`, `/api/crm/contacts`.
- [ ] No DELETE.
- [ ] No Auth runtime, token storage, Portal HTTP or real configuration values.
- [ ] Node PATH issue is documented or bundled Node verifier passes.

Use:

```powershell
powershell.exe -ExecutionPolicy Bypass -File tools\preflight-crm-local.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-health.ps1
```
