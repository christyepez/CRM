# CRM Local Development Runbook - Windows

Run from the CRM root.

```powershell
git checkout main
git fetch origin
git pull origin main
dotnet restore CRM.sln
dotnet build CRM.sln --no-restore
$env:DOTNET_ROLL_FORWARD='Major'; dotnet test CRM.sln --no-build
docker compose config
docker compose up -d --build
docker compose ps
```

Health:

```powershell
Invoke-WebRequest -UseBasicParsing http://localhost:8093/health
Invoke-WebRequest -UseBasicParsing http://localhost:8093/health/live
Invoke-WebRequest -UseBasicParsing http://localhost:8093/health/ready
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/readiness
```

If PowerShell process control returns access denied, close the owning terminal or Docker container from Docker Desktop. Do not force-delete project files.
