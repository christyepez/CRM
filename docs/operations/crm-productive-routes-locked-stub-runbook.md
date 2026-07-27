# CRM Productive Routes Locked Stub Runbook

Validate productive routes remain inactive:

```powershell
docker compose config
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\verify-crm-foundation.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-health.ps1
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/foundation/sprint-4/productive-routes-locked-stub
```

Expected:

- `lockedStubsStrategy=DocumentOnlyPreferred`.
- `productiveRoutesRegistered=false`.
- `lockedStubsRegistered=false`.
- `productiveCrudEnabled=false`.
- `deleteEndpointsEnabled=false`.
- `dbRequired=false`.
- `authRuntimeRequired=false`.
- `foundationCrudStillSeparate=true`.
