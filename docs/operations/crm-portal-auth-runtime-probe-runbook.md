# CRM Portal Auth Runtime Probe Runbook

Validate the probe remains disabled:

```powershell
docker compose config
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\verify-crm-foundation.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-health.ps1
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/foundation/sprint-4/portal-auth-runtime-probe
```

Expected:

- `portalAuthRuntimeProbeEnabled=false`.
- `tokenReadAttemptedByRuntime=false`.
- `portalHttpAttemptedByRuntime=false`.
- `portalRuntimeConnected=false`.
- `authRuntimeEnabled=false`.
- `foundationSimulationActive=true`.

If any value changes, stop the sprint and review the Auth safety gates before continuing.
