# CRM Common DB Runtime Probe Runbook

Validate that the probe remains disabled:

```powershell
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\verify-crm-foundation.ps1
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/foundation/sprint-4/common-db-runtime-probe
```

Expected:

- `commonDbRuntimeProbeEnabled=false`
- `dbConnectionAttemptedByRuntime=false`
- `apiRequiresDatabase=false`
- `sqlServerOwnedByCrm=false`

If the API is down, start it:

```powershell
docker compose up -d --build
```

If MCR is unavailable, mark `BLOCKED_EXTERNAL_REGISTRY`. Do not add SQL Server or real configuration values to this repository.
