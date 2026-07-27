# CRM Runtime Preflight Checklist

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
