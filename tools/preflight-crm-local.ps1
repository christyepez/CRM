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

$programText = if (Test-Path "src/CRM.Api/Program.cs") { Get-Content -Raw "src/CRM.Api/Program.cs" } else { "" }
if ($programText -like "*/api/crm/foundation/sprint-4/common-db-runtime-probe*") { Pass "Sprint 4 P2 common DB runtime probe endpoint registered." } else { Fail "Sprint 4 P2 common DB runtime probe endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/common-db-runtime-probe") { Fail "Sprint 4 P2 common DB runtime probe must remain GET-only." }

$probeText = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmCommonDbRuntimeProbeStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbRuntimeProbePlaceholder.cs")) {
    if (Test-Path $file) { $probeText += "`n" + (Get-Content -Raw $file) }
}
if ($probeText -like "*Common DB runtime probe exists but is disabled; no database connection is attempted*") { Pass "Sprint 4 P2 disabled probe warning present." } else { Fail "Sprint 4 P2 disabled probe warning missing." }
if ($probeText -like "*Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag*") { Pass "Sprint 4 P2 next gate points to P3 Portal Auth runtime probe." } else { Fail "Sprint 4 P2 next gate marker missing." }

if ($programText -like "*/api/crm/foundation/sprint-4/portal-auth-runtime-probe*") { Pass "Sprint 4 P3 Portal Auth runtime probe endpoint registered." } else { Fail "Sprint 4 P3 Portal Auth runtime probe endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/portal-auth-runtime-probe") { Fail "Sprint 4 P3 Portal Auth runtime probe must remain GET-only." }

$portalAuthProbeText = ""
foreach ($file in @("src/CRM.Application/Portal/CrmPortalAuthRuntimeProbeStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRuntimeProbePlaceholder.cs")) {
    if (Test-Path $file) { $portalAuthProbeText += "`n" + (Get-Content -Raw $file) }
}
if ($portalAuthProbeText -like "*Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted*") { Pass "Sprint 4 P3 disabled Portal Auth probe warning present." } else { Fail "Sprint 4 P3 disabled Portal Auth probe warning missing." }
if ($portalAuthProbeText -like "*Sprint4P4ProductiveRoutesLockedStubValidation*") { Pass "Sprint 4 P3 next gate points to P4 productive route locked validation." } else { Fail "Sprint 4 P3 next gate marker missing." }

if ($programText -like "*/api/crm/foundation/sprint-4/productive-routes-locked-stub*") { Pass "Sprint 4 P4 productive routes locked stub validation endpoint registered." } else { Fail "Sprint 4 P4 productive routes locked stub validation endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/productive-routes-locked-stub") { Fail "Sprint 4 P4 productive routes locked stub endpoint must remain GET-only." }
foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($programText -like "*$productiveRoute*") { Fail "Productive CRM route is registered: $productiveRoute" }
}

$productiveRouteStubText = if (Test-Path "src/CRM.Application/Foundation/CrmProductiveRoutesLockedStubStatusService.cs") { Get-Content -Raw "src/CRM.Application/Foundation/CrmProductiveRoutesLockedStubStatusService.cs" } else { "" }
if ($productiveRouteStubText -like "*Productive routes locked stub validation only; no productive routes are active*") { Pass "Sprint 4 P4 locked stub warning present." } else { Fail "Sprint 4 P4 locked stub warning missing." }
if ($productiveRouteStubText -like "*DocumentOnlyPreferred*" -and $productiveRouteStubText -like "*Sprint4P5NonProductionE2EPilotReadiness*") { Pass "Sprint 4 P4 document-only decision and P5 next gate present." } else { Fail "Sprint 4 P4 decision or next gate marker missing." }

if ($programText -like "*/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness*") { Pass "Sprint 4 P5 non-production E2E pilot readiness endpoint registered." } else { Fail "Sprint 4 P5 non-production E2E pilot readiness endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness") { Fail "Sprint 4 P5 non-production E2E pilot readiness endpoint must remain GET-only." }

$e2ePilotText = if (Test-Path "src/CRM.Application/Foundation/CrmNonProductionE2EPilotReadinessStatusService.cs") { Get-Content -Raw "src/CRM.Application/Foundation/CrmNonProductionE2EPilotReadinessStatusService.cs" } else { "" }
if ($e2ePilotText -like "*Non-production E2E pilot readiness only; no real activation*") { Pass "Sprint 4 P5 non-production E2E warning present." } else { Fail "Sprint 4 P5 non-production E2E warning missing." }
if ($e2ePilotText -like "*NonProductionE2EPilotReadiness*" -and $e2ePilotText -like "*Sprint4P6Sprint4GateDecision*") { Pass "Sprint 4 P5 status and P6 next gate present." } else { Fail "Sprint 4 P5 status or P6 next gate marker missing." }
if (Test-Path "tools/check-crm-e2e-foundation.ps1") { Pass "Sprint 4 P5 E2E foundation script exists." } else { Fail "Sprint 4 P5 E2E foundation script missing." }

powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
if ($LASTEXITCODE -ne 0) { Fail "Guardrail check failed." } else { Pass "Guardrail check passed." }

if ($failures.Count -gt 0) { exit 1 }
exit 0
