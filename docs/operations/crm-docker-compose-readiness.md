# CRM Docker Compose Readiness

Required checks:

```powershell
docker version
docker compose config
docker compose up -d --build
docker compose ps
docker compose logs crm-api --tail 120
```

Expected:

- Service: `crm-api`.
- Published port: `8093`.
- No SQL Server service.
- No `1433:1433` mapping.
- No secrets or real connection values.

If image metadata cannot be pulled from MCR, mark `BLOCKED_EXTERNAL_REGISTRY` and retry when the registry/network is available.
