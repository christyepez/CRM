param()

$ErrorActionPreference = "Continue"
$failures = @()
$warnings = @()

function Pass($Message) { Write-Output "PASS $Message" }
function Warn($Message) { $script:warnings += $Message; Write-Output "WARN $Message" }
function Fail($Message) { $script:failures += $Message; Write-Output "FAIL $Message" }

if (-not (Test-Path "CRM.sln")) { Fail "Run from CRM root." } else { Pass "CRM root detected." }

$docker = Get-Command docker -ErrorAction SilentlyContinue
if ($docker) {
    Pass "Docker command found."
    docker compose config *> $null
    if ($LASTEXITCODE -eq 0) { Pass "docker compose config passed." } else { Fail "docker compose config failed." }
} else {
    Fail "Docker command not found."
}

$composeText = ""
foreach ($file in @("docker-compose.yml", "docker-compose.crm.yml")) {
    if (Test-Path $file) { $composeText += "`n" + (Get-Content -Raw $file) }
}
if ($composeText -match "mcr\.microsoft\.com/mssql|1433:1433|container_name:\s*.*sql") { Fail "CRM compose defines SQL Server or 1433 mapping." } else { Pass "No CRM-owned SQL Server in compose." }

$port = Get-NetTCPConnection -LocalPort 8093 -ErrorAction SilentlyContinue
if ($port) { Warn "Port 8093 is in use; verify it is crm-api before starting." } else { Pass "Port 8093 appears available." }

if (Get-Command node -ErrorAction SilentlyContinue) {
    Pass "node found in PATH."
} else {
    Warn "node is not in PATH; use bundled Node for frontend verifier if available."
}

if (Test-Path ".env") { Fail ".env exists and must not be used." } else { Pass ".env not present." }

powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
if ($LASTEXITCODE -ne 0) { Fail "Guardrail check failed." } else { Pass "Guardrail check passed." }

if ($failures.Count -gt 0) { exit 1 }
exit 0
