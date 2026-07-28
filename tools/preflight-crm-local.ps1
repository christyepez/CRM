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


# Sprint 5 P4 Portal Auth Probe Optional Activation checks
$P4RequiredFiles = @(
    "docs/integration/crm-sprint-5-p4-portal-auth-probe-optional-activation.md",
    "docs/integration/crm-portal-auth-probe-optional-activation-policy.md",
    "docs/integration/crm-portal-auth-probe-activation-gates.md",
    "docs/integration/crm-portal-auth-probe-rollback-plan.md",
    "docs/operations/crm-portal-auth-probe-optional-activation-runbook.md",
    "docs/security/crm-portal-auth-probe-token-boundary.md",
    "src/CRM.Application/Foundation/CrmPortalAuthProbeOptionalActivationContracts.cs",
    "src/CRM.Application/Foundation/CrmPortalAuthProbeOptionalActivationStatusService.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthProbeOptionalActivationPlaceholder.cs"
)
foreach ($P4RequiredFile in $P4RequiredFiles) {
    if (-not (Test-Path $P4RequiredFile)) { Fail "Missing Sprint 5 P4 required file: $P4RequiredFile" } else { Pass "Required P4 file exists: $P4RequiredFile" }
}
$P4Program = Get-Content "src/CRM.Api/Program.cs" -Raw
if ($P4Program -notmatch "portal-auth-probe-optional-activation") { Fail "Missing Sprint 5 P4 foundation endpoint" } else { Pass "Sprint 5 P4 Portal Auth optional activation endpoint registered." }
if ($P4Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation") { Fail "Sprint 5 P4 endpoint must remain GET-only." }
$P4Text = ""
foreach ($P4File in @("src/CRM.Application/Foundation/CrmPortalAuthProbeOptionalActivationContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthProbeOptionalActivationStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthProbeOptionalActivationPlaceholder.cs")) {
    if (Test-Path $P4File) { $P4Text += "`n" + (Get-Content -Raw $P4File) }
}
foreach ($P4Marker in @("PortalAuthProbeOptionalActivation", "PortalAuthProbeEnabled", "PortalHttpAttempted", "TokenReadAttempted", "HeaderReadAttempted", "SecretProviderRuntimeRequired", "SecretReadsEnabled", "LoginImplementedByCrm", "IdentityImplementedByCrm", "PermissionsPersistedInCrm", "ProductiveAuthorizationEnabled", "Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted")) {
    if ($P4Text -notmatch [regex]::Escape($P4Marker)) { Fail "Missing Sprint 5 P4 marker: $P4Marker" }
}

# Sprint 5 P5 Locked Productive Route Stub Trial checks
$P5RequiredFiles = @(
    "docs/api/crm-sprint-5-p5-locked-productive-route-stub-trial.md",
    "docs/api/crm-locked-productive-route-stub-trial-policy.md",
    "docs/api/crm-locked-productive-route-stub-trial-contract.md",
    "docs/security/crm-locked-productive-route-stub-trial-safety-gates.md",
    "docs/operations/crm-locked-productive-route-stub-trial-runbook.md",
    "src/CRM.Application/Foundation/CrmLockedProductiveRouteStubTrialContracts.cs",
    "src/CRM.Application/Foundation/CrmLockedProductiveRouteStubTrialStatusService.cs"
)
foreach ($P5RequiredFile in $P5RequiredFiles) {
    if (-not (Test-Path $P5RequiredFile)) { Fail "Missing Sprint 5 P5 required file: $P5RequiredFile" } else { Pass "Required P5 file exists: $P5RequiredFile" }
}
if ($programText -like "*/api/crm/foundation/sprint-5/locked-productive-route-stub-trial*") { Pass "Sprint 5 P5 locked productive route stub trial endpoint registered." } else { Fail "Sprint 5 P5 endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial") { Fail "Sprint 5 P5 endpoint must remain GET-only." }
$P5Text = ""
foreach ($P5File in @("src/CRM.Application/Foundation/CrmLockedProductiveRouteStubTrialContracts.cs", "src/CRM.Application/Foundation/CrmLockedProductiveRouteStubTrialStatusService.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P5File) { $P5Text += "`n" + (Get-Content -Raw $P5File) }
}
foreach ($P5Marker in @("LockedProductiveRouteStubTrial", "DocumentOnlyPreferredWithNoRuntimeRegistration", "LockedProductiveRouteStubsRegistered", "ProductiveRoutesRegistered", "DeleteEndpointsEnabled", "RuntimeFlagDefaultEnabled", "Sprint5P6Sprint5GateDecision", "Locked productive route stub trial only; no productive routes are registered by default")) {
    if ($P5Text -notmatch [regex]::Escape($P5Marker)) { Fail "Missing Sprint 5 P5 marker: $P5Marker" }
}
foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($programText -like "*$productiveRoute*") { Fail "Productive CRM route is registered by default: $productiveRoute" }
}

# Sprint 5 P6 Gate Decision checks
$P6RequiredFiles = @(
    "docs/releases/crm-sprint-5-closure.md",
    "docs/releases/crm-sprint-5-integrated-evidence.md",
    "docs/releases/crm-sprint-5-gate-decision.md",
    "docs/releases/crm-sprint-5-go-no-go.md",
    "docs/releases/crm-sprint-5-open-risks.md",
    "docs/releases/crm-sprint-5-decision-record.md",
    "docs/architecture/crm-sprint-5-gate-matrix.md",
    "docs/security/crm-sprint-5-security-gate-review.md",
    "docs/data/crm-sprint-5-persistence-gate-review.md",
    "docs/api/crm-sprint-5-api-gate-review.md",
    "docs/testing/crm-sprint-5-e2e-gate-review.md",
    "docs/roadmap/crm-sprint-6-options.md",
    "docs/roadmap/crm-sprint-6-recommended-path.md",
    "docs/roadmap/crm-sprint-6-gates.md",
    "src/CRM.Application/Foundation/CrmSprint5GateDecisionContracts.cs",
    "src/CRM.Application/Foundation/CrmSprint5GateDecisionStatusService.cs"
)
foreach ($P6RequiredFile in $P6RequiredFiles) {
    if (-not (Test-Path $P6RequiredFile)) { Fail "Missing Sprint 5 P6 required file: $P6RequiredFile" } else { Pass "Required P6 file exists: $P6RequiredFile" }
}
if ($programText -like "*/api/crm/foundation/sprint-5/gate-decision*") { Pass "Sprint 5 P6 gate decision endpoint registered." } else { Fail "Sprint 5 P6 gate decision endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/gate-decision") { Fail "Sprint 5 P6 gate decision endpoint must remain GET-only." }
$P6Text = ""
foreach ($P6File in @("src/CRM.Application/Foundation/CrmSprint5GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint5GateDecisionStatusService.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P6File) { $P6Text += "`n" + (Get-Content -Raw $P6File) }
}
foreach ($P6Marker in @("Sprint5GateDecision", "GoForControlledNonProductionPreparation", "NoGoForRuntimeRead", "NoGoForConnectionAttempt", "NoGoForPortalHttpOrTokenRead", "NoGoForRuntimeRegistration", "Sprint6P1NonProductionRuntimeApprovalPackage", "Sprint 5 gate decision only; no real activation")) {
    if ($P6Text -notmatch [regex]::Escape($P6Marker)) { Fail "Missing Sprint 5 P6 marker: $P6Marker" }
}

# Sprint 6 P1 NonProduction Runtime Approval Package checks
$P1RequiredFiles = @(
    "docs/operations/crm-sprint-6-p1-nonproduction-runtime-approval-package.md",
    "docs/operations/crm-nonproduction-runtime-approval-matrix.md",
    "docs/operations/crm-nonproduction-runtime-entry-exit-criteria.md",
    "docs/operations/crm-nonproduction-runtime-rollback-approval.md",
    "docs/security/crm-nonproduction-runtime-security-approval.md",
    "docs/architecture/crm-nonproduction-runtime-architecture-approval.md",
    "src/CRM.Application/Foundation/CrmNonProductionRuntimeApprovalPackageContracts.cs",
    "src/CRM.Application/Foundation/CrmNonProductionRuntimeApprovalPackageStatusService.cs"
)
foreach ($P1RequiredFile in $P1RequiredFiles) {
    if (-not (Test-Path $P1RequiredFile)) { Fail "Missing Sprint 6 P1 required file: $P1RequiredFile" } else { Pass "Required Sprint 6 P1 file exists: $P1RequiredFile" }
}
if ($programText -like "*/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package*") { Pass "Sprint 6 P1 approval package endpoint registered." } else { Fail "Sprint 6 P1 approval package endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package") { Fail "Sprint 6 P1 approval package endpoint must remain GET-only." }
$P1Text = ""
foreach ($P1File in @("src/CRM.Application/Foundation/CrmNonProductionRuntimeApprovalPackageContracts.cs", "src/CRM.Application/Foundation/CrmNonProductionRuntimeApprovalPackageStatusService.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P1File) { $P1Text += "`n" + (Get-Content -Raw $P1File) }
}
foreach ($P1Marker in @("NonProductionRuntimeApprovalPackage", "NonProductionRuntimeApprovalPackageExists", "NonProductionRuntimeApprovalGranted", "SecretProviderMockApprovalGranted", "CommonDbDryRunApprovalGranted", "PortalAuthDryRunApprovalGranted", "LockedStubRuntimeTrialApprovalGranted", "RealActivationApprovalGranted", "ProductiveRoutesApprovalGranted", "DeleteApprovalGranted", "Sprint6P2SecretProviderSafeMockActivation", "NonProduction runtime approval package only; no runtime approval is granted")) {
    if ($P1Text -notmatch [regex]::Escape($P1Marker)) { Fail "Missing Sprint 6 P1 marker: $P1Marker" }
}

# Sprint 6 P2 Secret Provider Safe Mock Activation checks
$P2RequiredFiles = @(
    "docs/security/crm-sprint-6-p2-secret-provider-safe-mock-activation.md",
    "docs/security/crm-secret-provider-safe-mock-policy.md",
    "docs/security/crm-secret-provider-safe-mock-contract.md",
    "docs/security/crm-secret-provider-safe-mock-synthetic-values.md",
    "docs/operations/crm-secret-provider-safe-mock-runbook.md",
    "src/CRM.Application/Foundation/CrmSecretProviderSafeMockActivationContracts.cs",
    "src/CRM.Application/Foundation/CrmSecretProviderSafeMockActivationStatusService.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderSafeMock.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderSafeMockOptions.cs"
)
foreach ($P2RequiredFile in $P2RequiredFiles) {
    if (-not (Test-Path $P2RequiredFile)) { Fail "Missing Sprint 6 P2 required file: $P2RequiredFile" } else { Pass "Required Sprint 6 P2 file exists: $P2RequiredFile" }
}
if ($programText -like "*/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation*") { Pass "Sprint 6 P2 safe mock endpoint registered." } else { Fail "Sprint 6 P2 safe mock endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation") { Fail "Sprint 6 P2 safe mock endpoint must remain GET-only." }
$P2Text = ""
foreach ($P2File in @("src/CRM.Application/Foundation/CrmSecretProviderSafeMockActivationContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderSafeMockActivationStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderSafeMock.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P2File) { $P2Text += "`n" + (Get-Content -Raw $P2File) }
}
foreach ($P2Marker in @("SecretProviderSafeMockActivation", "SecretProviderSafeMockExists", "SecretProviderSafeMockEnabled", "SecretProviderRuntimeConnected", "SecretProviderReadsRealSecrets", "SecretProviderReadsSyntheticValues", "SecretProviderReadsEnabledForMockOnly", "RealSecretsConfigured", "EnvFileRequired", "KeyVaultClientConfigured", "AzureSdkForSecretsConfigured", "SecretValuesExposedInLogs", "Sprint6P3CommonDbConnectivityDryRunContract", "Secret Provider safe mock only; no real secrets are read", "mock://crm/common-db", "mock-client-secret-not-real")) {
    if ($P2Text -notmatch [regex]::Escape($P2Marker)) { Fail "Missing Sprint 6 P2 marker: $P2Marker" }
}

# Sprint 6 P3 Common DB Connectivity Dry-Run Contract checks
$P3RequiredFiles = @(
    "docs/data/crm-sprint-6-p3-common-db-connectivity-dry-run-contract.md",
    "docs/data/crm-common-db-connectivity-dry-run-policy.md",
    "docs/data/crm-common-db-connectivity-dry-run-contract.md",
    "docs/data/crm-common-db-connectivity-dry-run-observability.md",
    "docs/operations/crm-common-db-connectivity-dry-run-runbook.md",
    "docs/security/crm-common-db-connectivity-dry-run-secret-boundary.md",
    "src/CRM.Application/Foundation/CrmCommonDbConnectivityDryRunContracts.cs",
    "src/CRM.Application/Foundation/CrmCommonDbConnectivityDryRunStatusService.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityDryRun.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityDryRunOptions.cs"
)
foreach ($P3RequiredFile in $P3RequiredFiles) {
    if (-not (Test-Path $P3RequiredFile)) { Fail "Missing Sprint 6 P3 required file: $P3RequiredFile" } else { Pass "Required Sprint 6 P3 file exists: $P3RequiredFile" }
}
if ($programText -like "*/api/crm/foundation/sprint-6/common-db-connectivity-dry-run*") { Pass "Sprint 6 P3 common DB dry-run endpoint registered." } else { Fail "Sprint 6 P3 common DB dry-run endpoint missing." }
if ($programText -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/common-db-connectivity-dry-run") { Fail "Sprint 6 P3 common DB dry-run endpoint must remain GET-only." }
$P3Text = ""
foreach ($P3File in @("src/CRM.Application/Foundation/CrmCommonDbConnectivityDryRunContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbConnectivityDryRunStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityDryRun.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P3File) { $P3Text += "`n" + (Get-Content -Raw $P3File) }
}
foreach ($P3Marker in @("CommonDbConnectivityDryRunContract", "CommonDbConnectivityDryRunContractExists", "CommonDbDryRunApprovalGranted", "CommonDbDryRunEnabled", "CommonDbConnectionAttempted", "UsesSecretProviderSafeMockMetadata", "UsesSyntheticConnectionReference", "mock://crm/common-db", "RealConnectionStringUsed", "ConnectionStringResolved", "SqlConnectionCreated", "DbConnectionCreated", "EfRuntimeEnabled", "MigrationsCreated", "ApiRequiresDatabase", "Sprint6P4PortalAuthTokenPropagationDryRunContract", "Common DB connectivity dry-run contract only; no database connection is attempted")) {
    if ($P3Text -notmatch [regex]::Escape($P3Marker)) { Fail "Missing Sprint 6 P3 marker: $P3Marker" }
}

if ($failures.Count -gt 0) { exit 1 }
exit 0
