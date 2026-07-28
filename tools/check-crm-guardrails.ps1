param()

$ErrorActionPreference = "Continue"
$failures = @()
function Pass($Message) { Write-Output "PASS $Message" }
function Fail($Message) { $script:failures += $Message; Write-Output "FAIL $Message" }

$program = Get-Content -Raw "src/CRM.Api/Program.cs"
$source = ""
foreach ($root in @("src", "frontend/crm-web/src")) {
    if (Test-Path $root) {
        Get-ChildItem -Path $root -Recurse -File -ErrorAction SilentlyContinue |
            Where-Object { $_.FullName -notmatch "\\(bin|obj|node_modules|dist|\.angular)\\" } |
            ForEach-Object { $source += "`n" + (Get-Content -Raw $_.FullName) }
    }
}

foreach ($route in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($program -like "*$route*") { Fail "Productive route active: $route" }
}
if ($program -match "MapDelete") { Fail "DELETE endpoint found." }
if ($program -notlike "*/api/crm/foundation/sprint-4/common-db-runtime-probe*") { Fail "Sprint 4 P2 common DB runtime probe route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/common-db-runtime-probe") { Fail "Sprint 4 P2 common DB runtime probe must remain GET-only." }
if ($program -notlike "*/api/crm/foundation/sprint-4/portal-auth-runtime-probe*") { Fail "Sprint 4 P3 Portal Auth runtime probe route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/portal-auth-runtime-probe") { Fail "Sprint 4 P3 Portal Auth runtime probe must remain GET-only." }
if ($program -notlike "*/api/crm/foundation/sprint-4/productive-routes-locked-stub*") { Fail "Sprint 4 P4 productive routes locked stub route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/productive-routes-locked-stub") { Fail "Sprint 4 P4 productive routes locked stub endpoint must remain GET-only." }
if ($program -notlike "*/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness*") { Fail "Sprint 4 P5 non-production E2E pilot readiness route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness") { Fail "Sprint 4 P5 non-production E2E pilot readiness endpoint must remain GET-only." }
if ($program -notlike "*/api/crm/foundation/sprint-4/gate-decision*") { Fail "Sprint 4 P6 gate decision route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/gate-decision") { Fail "Sprint 4 P6 gate decision endpoint must remain GET-only." }
if ($program -notlike "*/api/crm/foundation/sprint-5/runtime-probe-activation-plan*") { Fail "Sprint 5 P1 controlled runtime probe activation plan route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/runtime-probe-activation-plan") { Fail "Sprint 5 P1 controlled runtime probe activation plan endpoint must remain GET-only." }
if ($program -notlike "*/api/crm/foundation/sprint-5/secret-provider-runtime-contract*") { Fail "Sprint 5 P2 secret provider runtime contract route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/secret-provider-runtime-contract") { Fail "Sprint 5 P2 secret provider runtime contract endpoint must remain GET-only." }
if ($program -notlike "*/api/crm/foundation/sprint-5/common-db-probe-optional-activation*") { Fail "Sprint 5 P3 common DB probe optional activation route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/common-db-probe-optional-activation") { Fail "Sprint 5 P3 common DB probe optional activation endpoint must remain GET-only." }
if ($source -match "AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|HttpClient|PortalBaseUrl|PortalCorporativoUrl") { Fail "Auth, token storage or Portal runtime marker found." }

$allowed = $source.Replace("DbContextConfigured", "").Replace("dbContextConfigured", "").Replace("DbContext Configured", "").Replace("DbContextRuntimeActive", "").Replace("dbContextRuntimeActive", "").Replace("DbContext Runtime Active", "").Replace("CrmDbContextPrototypeContract", "").Replace("CrmDbContextPrototype", "").Replace("InheritsRealDbContext", "").Replace("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", "").Replace("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", "").Replace("EfDbContextPrototypeDisabled", "").Replace("EF/DbContext prototype only; runtime disabled and no database configured", "")
if ($allowed -match "DbSet<|MigrationBuilder|UseSqlServer\(|UseNpgsql|AddDbContext|ConnectionString=") { Fail "DB runtime, migration or real configuration marker found." }

foreach ($marker in @("Common DB runtime probe exists but is disabled; no database connection is attempted", "CommonDbRuntimeProbe", "CrmCommonDbRuntimeProbeStatusService", "CommonDbRuntimeProbePlaceholder", "Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P2 common DB runtime probe marker: $marker" }
}

foreach ($marker in @("commonDbRuntimeProbeEnabled: false", "dbConnectionAttemptedByRuntime: false", "commonDbSqlServerOwnedByCrm: false", "commonDbEfRuntimeEnabled: false", "commonDbContextRuntimeActive: false", "commonDbDurablePersistenceEnabled: false", "commonDbApiRequiresDatabase: false")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P2 frontend disabled marker: $marker" }
}

foreach ($marker in @("Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted", "PortalAuthRuntimeProbe", "CrmPortalAuthRuntimeProbeStatusService", "PortalAuthRuntimeProbePlaceholder", "Sprint4P4ProductiveRoutesLockedStubValidation")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P3 Portal Auth runtime probe marker: $marker" }
}

foreach ($marker in @("portalAuthRuntimeProbeEnabled: false", "tokenReadAttemptedByRuntime: false", "portalHttpAttemptedByRuntime: false", "portalAuthProbeLoginImplementedByCrm: false", "portalAuthProbeIdentityImplementedByCrm: false", "portalAuthProbePermissionsPersistedInCrm: false", "portalAuthProbeFoundationSimulationActive: true")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P3 frontend disabled marker: $marker" }
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"', 'MapGet("/api/crm/leads', 'MapGet("/api/crm/accounts', 'MapGet("/api/crm/contacts', 'MapPost("/api/crm/leads', 'MapPost("/api/crm/accounts', 'MapPost("/api/crm/contacts', 'MapPut("/api/crm/leads', 'MapPut("/api/crm/accounts', 'MapPut("/api/crm/contacts')) {
    if ($program -like "*$productiveRoute*") { Fail "Productive CRM route active in Program.cs: $productiveRoute" }
}

foreach ($marker in @("Productive routes locked stub validation only; no productive routes are active", "ProductiveRoutesLockedStubValidation", "CrmProductiveRoutesLockedStubStatusService", "DocumentOnlyPreferred", "Sprint4P5NonProductionE2EPilotReadiness")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P4 productive route locked stub marker: $marker" }
}

foreach ($marker in @("lockedStubsStrategy: 'DocumentOnlyPreferred'", "p4ProductiveRoutesRegistered: false", "lockedStubsRegistered: false", "p4ProductiveCrudEnabled: false", "p4DeleteEndpointsEnabled: false", "dbRequired: false", "authRuntimeRequired: false", "p4FoundationCrudStillSeparate: true")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P4 frontend disabled marker: $marker" }
}

foreach ($marker in @("Non-production E2E pilot readiness only; no real activation", "NonProductionE2EPilotReadiness", "CrmNonProductionE2EPilotReadinessStatusService", "Sprint4P6Sprint4GateDecision")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P5 non-production E2E marker: $marker" }
}

foreach ($marker in @("sprint4P5NonProductionE2EPilotReadiness: 'Prepared'", "e2ePilotCanRun: true", "e2ePilotScope: 'FoundationOnly'", "productiveRoutesUsed: false", "realDatabaseUsed: false", "portalAuthRuntimeUsed: false", "durablePersistenceUsed: false", "deleteOperationsUsed: false", "syntheticDataOnly: true", "foundationEndpointsOnly: true", "negativeRouteValidationRequired: true")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P5 frontend foundation-only marker: $marker" }
}

foreach ($marker in @("Sprint 4 gate decision only; no real activation", "Sprint4GateDecision", "CrmSprint4GateDecisionStatusService", "GoForNonProductionFoundationPilot", "Sprint5P1ControlledRuntimeProbeActivationPlan")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P6 gate marker: $marker" }
}

foreach ($marker in @("sprint4: 'Closed'", "sprint4GateDecision: 'Completed'", "sprint4OverallDecision: 'GoForNonProductionFoundationPilot'", "realActivationDecision: 'NoGo'", "commonDbRuntimeDecision: 'NoGoForRuntimeActivation'", "sprint4PortalAuthRuntimeDecision: 'NoGoForRuntimeActivation'", "productiveRoutesDecision: 'NoGo'", "deleteDecision: 'NoGo'", "productiveUiDecision: 'NoGo'", "nonProductionE2EPilotDecision: 'GoFoundationOnly'", "sprint5PlanningDecision: 'Go'")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 4 P6 frontend gate marker: $marker" }
}

foreach ($marker in @("Runtime probe activation plan only; no runtime activation approved", "ControlledRuntimeProbeActivationPlan", "CrmControlledRuntimeProbeActivationPlanStatusService", "Sprint5P2SecretProviderRuntimeContractValidation")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 5 P1 controlled runtime probe activation marker: $marker" }
}

foreach ($marker in @("sprint5P1ControlledRuntimeProbeActivationPlan: 'Exists'", "runtimeProbeActivationApproved: false", "commonDbProbeActivationApproved: false", "portalAuthProbeActivationApproved: false", "productiveRoutesActivationApproved: false", "realActivationApproved: false", "nonProductionOnly: true", "syntheticDataRequired: true", "rollbackPlanRequired: true", "observabilityRequired: true", "secretProviderRequired: true", "deleteStillNoGo: true")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 5 P1 frontend activation-plan marker: $marker" }
}

foreach ($marker in @("Secret Provider contract validation only; no secrets are read", "SecretProviderRuntimeContractValidation", "CrmSecretProviderRuntimeContractStatusService", "SecretProviderRuntimeContractPlaceholder", "Sprint5P3CommonDbProbeOptionalActivationInNonProduction")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 5 P2 secret provider contract marker: $marker" }
}

foreach ($marker in @("sprint5P2SecretProviderRuntimeContract: 'Exists'", "secretProviderContractExists: true", "p2SecretProviderRuntimeConnected: false", "secretProviderReadsEnabled: false", "secretReadAttemptedByRuntime: false", "realSecretsConfigured: false", "envFileRequired: false", "p2ConnectionStringsConfigured: false", "keyVaultClientConfigured: false", "secretValuesExposed: false", "p2RuntimeProbeActivationApproved: false", "p2CommonDbProbeActivationApproved: false", "p2PortalAuthProbeActivationApproved: false")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 5 P2 frontend secret-provider marker: $marker" }
}

foreach ($marker in @("Common DB probe optional activation only; no database connection is attempted", "CommonDbProbeOptionalActivation", "CrmCommonDbProbeOptionalActivationStatusService", "CommonDbProbeOptionalActivationPlaceholder", "Sprint5P4PortalAuthProbeOptionalActivationInNonProduction")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 5 P3 common DB optional activation marker: $marker" }
}

foreach ($marker in @("sprint5P3CommonDbProbeOptionalActivation: 'Exists'", "commonDbProbeOptionalActivationExists: true", "p3CommonDbProbeActivationApproved: false", "p3CommonDbProbeEnabled: false", "p3CommonDbConnectionAttempted: false", "p3SecretProviderRuntimeRequired: true", "p3SecretProviderRuntimeConnected: false", "secretReadsRequiredBeforeActivation: true", "p3SecretReadsEnabled: false", "p3RealDatabaseConfigured: false", "p3ConnectionStringsConfigured: false", "p3EfRuntimeEnabled: false", "p3MigrationsCreated: false", "p3ApiRequiresDatabase: false")) {
    if ($source -notlike "*$marker*") { Fail "Missing Sprint 5 P3 frontend common DB marker: $marker" }
}

$compose = ""
foreach ($file in @("docker-compose.yml", "docker-compose.crm.yml")) { if (Test-Path $file) { $compose += "`n" + (Get-Content -Raw $file) } }
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|container_name:\s*.*sql") { Fail "CRM-owned SQL Server found in compose." }
if (Test-Path ".env") { Fail ".env found." }
if (Test-Path "database") { Fail "database folder found." }


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
if ($program -notlike "*/api/crm/foundation/sprint-5/locked-productive-route-stub-trial*") { Fail "Sprint 5 P5 locked productive route stub trial route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial") { Fail "Sprint 5 P5 endpoint must remain GET-only." }
$P5Text = ""
foreach ($P5File in @("src/CRM.Application/Foundation/CrmLockedProductiveRouteStubTrialContracts.cs", "src/CRM.Application/Foundation/CrmLockedProductiveRouteStubTrialStatusService.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P5File) { $P5Text += "`n" + (Get-Content -Raw $P5File) }
}
foreach ($P5Marker in @("LockedProductiveRouteStubTrial", "DocumentOnlyPreferredWithNoRuntimeRegistration", "LockedProductiveRouteStubsRegistered", "ProductiveRoutesRegistered", "DeleteEndpointsEnabled", "RuntimeFlagDefaultEnabled", "Sprint5P6Sprint5GateDecision", "Locked productive route stub trial only; no productive routes are registered by default")) {
    if ($P5Text -notmatch [regex]::Escape($P5Marker)) { Fail "Missing Sprint 5 P5 marker: $P5Marker" }
}
foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($program -like "*$productiveRoute*") { Fail "Productive CRM route is registered by default: $productiveRoute" }
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
if ($program -notlike "*/api/crm/foundation/sprint-5/gate-decision*") { Fail "Sprint 5 P6 gate decision route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/gate-decision") { Fail "Sprint 5 P6 gate decision endpoint must remain GET-only." }
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
if ($program -notlike "*/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package*") { Fail "Sprint 6 P1 approval package route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package") { Fail "Sprint 6 P1 approval package endpoint must remain GET-only." }
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
if ($program -notlike "*/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation*") { Fail "Sprint 6 P2 safe mock route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation") { Fail "Sprint 6 P2 safe mock endpoint must remain GET-only." }
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
if ($program -notlike "*/api/crm/foundation/sprint-6/common-db-connectivity-dry-run*") { Fail "Sprint 6 P3 common DB dry-run route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/common-db-connectivity-dry-run") { Fail "Sprint 6 P3 common DB dry-run endpoint must remain GET-only." }
$P3Text = ""
foreach ($P3File in @("src/CRM.Application/Foundation/CrmCommonDbConnectivityDryRunContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbConnectivityDryRunStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityDryRun.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P3File) { $P3Text += "`n" + (Get-Content -Raw $P3File) }
}
foreach ($P3Marker in @("CommonDbConnectivityDryRunContract", "CommonDbConnectivityDryRunContractExists", "CommonDbDryRunApprovalGranted", "CommonDbDryRunEnabled", "CommonDbConnectionAttempted", "UsesSecretProviderSafeMockMetadata", "UsesSyntheticConnectionReference", "mock://crm/common-db", "RealConnectionStringUsed", "ConnectionStringResolved", "SqlConnectionCreated", "DbConnectionCreated", "EfRuntimeEnabled", "MigrationsCreated", "ApiRequiresDatabase", "Sprint6P4PortalAuthTokenPropagationDryRunContract", "Common DB connectivity dry-run contract only; no database connection is attempted")) {
    if ($P3Text -notmatch [regex]::Escape($P3Marker)) { Fail "Missing Sprint 6 P3 marker: $P3Marker" }
}

if ($failures.Count -gt 0) { exit 1 }
Pass "CRM guardrails passed."
exit 0
