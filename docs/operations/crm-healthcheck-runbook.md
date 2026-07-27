# CRM Healthcheck Runbook

Start API:

```powershell
docker compose up -d --build
```

Validate:

```powershell
Invoke-WebRequest -UseBasicParsing http://localhost:8093/health
Invoke-WebRequest -UseBasicParsing http://localhost:8093/health/live
Invoke-WebRequest -UseBasicParsing http://localhost:8093/health/ready
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/readiness
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/foundation/sprint-3/productization-review
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/foundation/sprint-4/runtime-readiness
```

Expected runtime readiness response includes `crmApiPort=8093`, `sqlServerOwnedByCrm=false` and `warning=Runtime readiness only; no real activation`.
