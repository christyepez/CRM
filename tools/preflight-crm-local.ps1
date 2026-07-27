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

if ($programText -like "*/api/crm/foundation/sprint-4/gate-decision*") { Pass "Sprint 4 P6 gate decision endpoint registered." } else { Fail "Sprint 4 P6 gate decision endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/gate-decision") { Fail "Sprint 4 P6 gate decision endpoint must remain GET-only." }

$gateDecisionText = if (Test-Path "src/CRM.Application/Foundation/CrmSprint4GateDecisionStatusService.cs") { Get-Content -Raw "src/CRM.Application/Foundation/CrmSprint4GateDecisionStatusService.cs" } else { "" }
if ($gateDecisionText -like "*Sprint 4 gate decision only; no real activation*") { Pass "Sprint 4 P6 gate decision warning present." } else { Fail "Sprint 4 P6 gate decision warning missing." }
if ($gateDecisionText -like "*GoForNonProductionFoundationPilot*" -and $gateDecisionText -like "*Sprint5P1ControlledRuntimeProbeActivationPlan*") { Pass "Sprint 4 P6 decision and Sprint 5 P1 next gate present." } else { Fail "Sprint 4 P6 decision or Sprint 5 P1 next gate marker missing." }
foreach ($doc in @("docs/releases/crm-sprint-4-closure.md", "docs/architecture/crm-sprint-4-gate-matrix.md", "docs/roadmap/crm-sprint-5-recommended-path.md")) {
    if (Test-Path $doc) { Pass "Required P6 doc exists: $doc" } else { Fail "Required P6 doc missing: $doc" }
}

if ($programText -like "*/api/crm/foundation/sprint-5/runtime-probe-activation-plan*") { Pass "Sprint 5 P1 controlled runtime probe activation plan endpoint registered." } else { Fail "Sprint 5 P1 controlled runtime probe activation plan endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/runtime-probe-activation-plan") { Fail "Sprint 5 P1 controlled runtime probe activation plan endpoint must remain GET-only." }

$runtimeProbeActivationPlanText = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmControlledRuntimeProbeActivationPlanStatusService.cs", "src/CRM.Application/Foundation/CrmControlledRuntimeProbeActivationPlanContracts.cs")) {
    if (Test-Path $file) { $runtimeProbeActivationPlanText += "`n" + (Get-Content -Raw $file) }
}
if ($runtimeProbeActivationPlanText -like "*Runtime probe activation plan only; no runtime activation approved*") { Pass "Sprint 5 P1 controlled activation warning present." } else { Fail "Sprint 5 P1 controlled activation warning missing." }
if ($runtimeProbeActivationPlanText -like "*ControlledRuntimeProbeActivationPlan*" -and $runtimeProbeActivationPlanText -like "*Sprint5P2SecretProviderRuntimeContractValidation*") { Pass "Sprint 5 P1 status and P2 next gate present." } else { Fail "Sprint 5 P1 status or P2 next gate marker missing." }
foreach ($doc in @("docs/operations/crm-sprint-5-p1-controlled-runtime-probe-activation-plan.md", "docs/operations/crm-runtime-probe-activation-approval-matrix.md", "docs/security/crm-runtime-probe-secret-handling-policy.md")) {
    if (Test-Path $doc) { Pass "Required P1 doc exists: $doc" } else { Fail "Required P1 doc missing: $doc" }
}

if ($programText -like "*/api/crm/foundation/sprint-5/secret-provider-runtime-contract*") { Pass "Sprint 5 P2 secret provider runtime contract endpoint registered." } else { Fail "Sprint 5 P2 secret provider runtime contract endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/secret-provider-runtime-contract") { Fail "Sprint 5 P2 secret provider runtime contract endpoint must remain GET-only." }

$secretProviderContractText = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmSecretProviderRuntimeContractStatusService.cs", "src/CRM.Application/Foundation/CrmSecretProviderRuntimeContractContracts.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeContractPlaceholder.cs")) {
    if (Test-Path $file) { $secretProviderContractText += "`n" + (Get-Content -Raw $file) }
}
if ($secretProviderContractText -like "*Secret Provider contract validation only; no secrets are read*") { Pass "Sprint 5 P2 secret provider no-read warning present." } else { Fail "Sprint 5 P2 secret provider no-read warning missing." }
if ($secretProviderContractText -like "*SecretProviderRuntimeContractValidation*" -and $secretProviderContractText -like "*Sprint5P3CommonDbProbeOptionalActivationInNonProduction*") { Pass "Sprint 5 P2 status and P3 next gate present." } else { Fail "Sprint 5 P2 status or P3 next gate marker missing." }
foreach ($doc in @("docs/security/crm-sprint-5-p2-secret-provider-runtime-contract-validation.md", "docs/security/crm-secret-provider-runtime-contract.md", "docs/security/crm-secret-provider-no-secret-read-policy.md", "docs/security/crm-secret-provider-approval-gates.md", "docs/operations/crm-secret-provider-runtime-runbook.md")) {
    if (Test-Path $doc) { Pass "Required P2 doc exists: $doc" } else { Fail "Required P2 doc missing: $doc" }
}

if ($programText -like "*/api/crm/foundation/sprint-5/common-db-probe-optional-activation*") { Pass "Sprint 5 P3 common DB probe optional activation endpoint registered." } else { Fail "Sprint 5 P3 common DB probe optional activation endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/common-db-probe-optional-activation") { Fail "Sprint 5 P3 common DB probe optional activation endpoint must remain GET-only." }

$commonDbOptionalText = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmCommonDbProbeOptionalActivationStatusService.cs", "src/CRM.Application/Foundation/CrmCommonDbProbeOptionalActivationContracts.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbProbeOptionalActivationPlaceholder.cs")) {
    if (Test-Path $file) { $commonDbOptionalText += "`n" + (Get-Content -Raw $file) }
}
if ($commonDbOptionalText -like "*Common DB probe optional activation only; no database connection is attempted*") { Pass "Sprint 5 P3 common DB optional activation warning present." } else { Fail "Sprint 5 P3 common DB optional activation warning missing." }
if ($commonDbOptionalText -like "*CommonDbProbeOptionalActivation*" -and $commonDbOptionalText -like "*Sprint5P4PortalAuthProbeOptionalActivationInNonProduction*") { Pass "Sprint 5 P3 status and P4 next gate present." } else { Fail "Sprint 5 P3 status or P4 next gate marker missing." }
foreach ($doc in @("docs/data/crm-sprint-5-p3-common-db-probe-optional-activation.md", "docs/data/crm-common-db-probe-optional-activation-policy.md", "docs/data/crm-common-db-probe-activation-gates.md", "docs/data/crm-common-db-probe-rollback-plan.md", "docs/operations/crm-common-db-probe-optional-activation-runbook.md", "docs/security/crm-common-db-probe-secret-dependency.md")) {
    if (Test-Path $doc) { Pass "Required P3 doc exists: $doc" } else { Fail "Required P3 doc missing: $doc" }
}

powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
if ($LASTEXITCODE -ne 0) { Fail "Guardrail check failed." } else { Pass "Guardrail check passed." }

if ($failures.Count -gt 0) { exit 1 }
exit 0
