param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot scaffold preflight starting."

& .\tools\verify-crm-controlled-runtime-integration-design.ps1
& .\tools\check-crm-controlled-runtime-pilot-scaffold-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-scaffold.ps1

$compose = ""
foreach ($composeFile in @("docker-compose.yml", "docker-compose.crm.yml")) {
    if (Test-Path $composeFile) {
        $compose += "`n" + (Get-Content -Raw $composeFile)
    }
}

if ($compose -match "portal.*image:|portal.*build:|PortalCorporativo|mcr\.microsoft\.com/mssql|1433:1433|container_name:\s*.*sql") {
    Write-Error "CRM compose contains Portal services or CRM-owned SQL Server."
    exit 1
}

if ((Test-Path ".env") -or (Test-Path ".env.local")) {
    Write-Error "Real environment file detected."
    exit 1
}

Write-Output "PASS CRM controlled runtime pilot scaffold preflight passed."
exit 0
