# CRM Non-Production E2E Pilot Runbook

Run:

```powershell
docker compose up -d --build
docker compose ps
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-health.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\verify-crm-foundation.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-e2e-foundation.ps1
```

Expected:

- Foundation endpoints return 2xx.
- Productive routes are not active.
- No DELETE endpoint exists.
- No DB, Auth runtime or Portal runtime is required.
