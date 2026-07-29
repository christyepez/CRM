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
$portalAuthScan = $source.
    Replace("PortalHttpClientCreated", "").
    Replace("portalHttpClientCreated", "").
    Replace("Portal HTTP Client Created", "").
    Replace("PortalAuthBaseUrlResolved", "").
    Replace("portalAuthBaseUrlResolved", "").
    Replace("Portal Auth Base URL Resolved", "").
    Replace("PortalAuthBaseUrlMaterialized", "").
    Replace("portalAuthBaseUrlMaterialized", "").
    Replace("Portal Auth Base URL Materialized", "").
    Replace("PortalAuthBaseUrlLogged", "").
    Replace("portalAuthBaseUrlLogged", "").
    Replace("Portal Auth Base URL Logged", "").
    Replace("PortalAuthBaseUrlReturnedToApi", "").
    Replace("portalAuthBaseUrlReturnedToApi", "").
    Replace("Portal Auth Base URL Returned To API", "")

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
if ($portalAuthScan -match "AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|HttpClient|PortalBaseUrl|PortalCorporativoUrl") { Fail "Auth, token storage or Portal runtime marker found." }

$allowed = $source.Replace("DbContextConfigured", "").Replace("dbContextConfigured", "").Replace("DbContext Configured", "").Replace("DbContextRuntimeActive", "").Replace("dbContextRuntimeActive", "").Replace("DbContext Runtime Active", "").Replace("AddDbContextRuntimeEnabled", "").Replace("addDbContextRuntimeEnabled", "").Replace("AddDbContext Runtime Enabled", "").Replace("CrmDbContextPrototypeContract", "").Replace("CrmDbContextPrototype", "").Replace("InheritsRealDbContext", "").Replace("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", "").Replace("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", "").Replace("EfDbContextPrototypeDisabled", "").Replace("EF/DbContext prototype only; runtime disabled and no database configured", "").Replace("UseSqlServerEnabled", "").Replace("useSqlServerEnabled", "").Replace("UseSqlServer Enabled", "")
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

# Sprint 6 P4 Portal Auth Token Propagation Dry-Run Contract checks
$P4RequiredFiles = @(
    "docs/integration/crm-sprint-6-p4-portal-auth-token-propagation-dry-run-contract.md",
    "docs/integration/crm-portal-auth-token-propagation-dry-run-policy.md",
    "docs/integration/crm-portal-auth-token-propagation-dry-run-contract.md",
    "docs/integration/crm-portal-auth-token-propagation-dry-run-observability.md",
    "docs/operations/crm-portal-auth-token-propagation-dry-run-runbook.md",
    "docs/security/crm-portal-auth-token-propagation-dry-run-boundary.md",
    "src/CRM.Application/Foundation/CrmPortalAuthTokenPropagationDryRunContracts.cs",
    "src/CRM.Application/Foundation/CrmPortalAuthTokenPropagationDryRunStatusService.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthTokenPropagationDryRun.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthTokenPropagationDryRunOptions.cs"
)
foreach ($P4RequiredFile in $P4RequiredFiles) {
    if (-not (Test-Path $P4RequiredFile)) { Fail "Missing Sprint 6 P4 required file: $P4RequiredFile" } else { Pass "Required Sprint 6 P4 file exists: $P4RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run*") { Fail "Sprint 6 P4 Portal Auth token propagation dry-run route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run") { Fail "Sprint 6 P4 Portal Auth token propagation dry-run endpoint must remain GET-only." }
$P4Text = ""
foreach ($P4File in @("src/CRM.Application/Foundation/CrmPortalAuthTokenPropagationDryRunContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthTokenPropagationDryRunStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthTokenPropagationDryRun.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P4File) { $P4Text += "`n" + (Get-Content -Raw $P4File) }
}
foreach ($P4Marker in @("PortalAuthTokenPropagationDryRunContract", "PortalAuthTokenPropagationDryRunContractExists", "PortalAuthDryRunApprovalGranted", "PortalAuthDryRunEnabled", "PortalAuthRuntimeConnected", "TokenReadAttempted", "HeaderReadAttempted", "PortalHttpAttempted", "UsesSyntheticTokenMetadata", "mock://crm/portal-auth-token", "mock://crm/portal-user", "RealTokenUsed", "RealHeadersRead", "LoginImplementedByCrm", "IdentityImplementedByCrm", "PermissionsPersistedInCrm", "ProductiveAuthorizationEnabled", "Sprint6P5LockedStubRuntimeRegistrationTrial", "Portal Auth token propagation dry-run contract only; no real tokens or headers are read")) {
    if ($P4Text -notmatch [regex]::Escape($P4Marker)) { Fail "Missing Sprint 6 P4 marker: $P4Marker" }
}
$P4AuthScanText = $P4Text.
    Replace("AuthorizationHeaderReadAttempted", "").
    Replace("authorizationHeaderReadAttempted", "").
    Replace("Authorization Header Read Attempted", "").
    Replace("PortalHttpClientCreated", "").
    Replace("portalHttpClientCreated", "").
    Replace("Portal HTTP Client Created", "").
    Replace("PortalAuthBaseUrlResolved", "").
    Replace("portalAuthBaseUrlResolved", "").
    Replace("Portal Auth Base URL Resolved", "").
    Replace("PortalAuthBaseUrlMaterialized", "").
    Replace("portalAuthBaseUrlMaterialized", "").
    Replace("Portal Auth Base URL Materialized", "").
    Replace("PortalAuthBaseUrlLogged", "").
    Replace("portalAuthBaseUrlLogged", "").
    Replace("Portal Auth Base URL Logged", "").
    Replace("PortalAuthBaseUrlReturnedToApi", "").
    Replace("portalAuthBaseUrlReturnedToApi", "").
    Replace("Portal Auth Base URL Returned To API", "")
if ($P4AuthScanText -match "HttpContext\.Request\.Headers|Request\.Headers|Headers\[|AuthorizationHeader|authorizationHeader|Bearer|HttpClient|PortalBaseUrl|PortalCorporativoUrl|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") { Fail "Sprint 6 P4 must not read headers/tokens, call Portal, store tokens or activate Auth runtime." }

# Sprint 6 P5 Locked Stub Runtime Registration Trial checks
$P5RequiredFiles = @(
    "docs/api/crm-sprint-6-p5-locked-stub-runtime-registration-trial.md",
    "docs/api/crm-locked-stub-runtime-registration-trial-policy.md",
    "docs/api/crm-locked-stub-runtime-registration-trial-contract.md",
    "docs/security/crm-locked-stub-runtime-registration-trial-safety-boundary.md",
    "docs/operations/crm-locked-stub-runtime-registration-trial-runbook.md",
    "src/CRM.Application/Foundation/CrmLockedStubRuntimeRegistrationTrialContracts.cs",
    "src/CRM.Application/Foundation/CrmLockedStubRuntimeRegistrationTrialStatusService.cs"
)
foreach ($P5RequiredFile in $P5RequiredFiles) {
    if (-not (Test-Path $P5RequiredFile)) { Fail "Missing Sprint 6 P5 required file: $P5RequiredFile" } else { Pass "Required Sprint 6 P5 file exists: $P5RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial*") { Fail "Sprint 6 P5 locked stub runtime registration trial route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial") { Fail "Sprint 6 P5 endpoint must remain GET-only." }
$P5Text = ""
foreach ($P5File in @("src/CRM.Application/Foundation/CrmLockedStubRuntimeRegistrationTrialContracts.cs", "src/CRM.Application/Foundation/CrmLockedStubRuntimeRegistrationTrialStatusService.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $P5File) { $P5Text += "`n" + (Get-Content -Raw $P5File) }
}
foreach ($P5Marker in @("LockedStubRuntimeRegistrationTrial", "LockedStubRuntimeRegistrationTrialExists", "LockedStubRuntimeRegistrationApprovalGranted", "LockedStubRuntimeRegistrationEnabled", "LockedStubsRegisteredAtRuntime", "ProductiveRoutesRegistered", "ProductiveCrudEnabled", "DeleteEndpointsEnabled", "DefaultNegativeRouteStatus", "FutureLockedResponseStatusIfExplicitlyEnabled", "RuntimeFlagDefaultEnabled", "UsesDomainServices", "UsesFoundationStores", "UsesDatabase", "UsesPortalAuth", "UsesTokenOrHeaderReads", "DocumentOnlyPreferredWithNoRuntimeRegistration", "Sprint6P6Sprint6GateDecision", "Locked stub runtime registration trial only; no productive routes are registered by default")) {
    if ($P5Text -notmatch [regex]::Escape($P5Marker)) { Fail "Missing Sprint 6 P5 marker: $P5Marker" }
}
if (Test-Path "src/CRM.Api/ProductiveRoutes/LockedStubRuntimeRegistrationTrial.cs") { Fail "P5 selected DocumentOnlyPreferredWithNoRuntimeRegistration; runtime registrar file must not exist." }

# Sprint 6 P6 Gate Decision checks
$Sprint6P6RequiredFiles = @(
    "docs/releases/crm-sprint-6-closure.md",
    "docs/releases/crm-sprint-6-integrated-evidence.md",
    "docs/releases/crm-sprint-6-gate-decision.md",
    "docs/releases/crm-sprint-6-go-no-go.md",
    "docs/releases/crm-sprint-6-open-risks.md",
    "docs/releases/crm-sprint-6-decision-record.md",
    "docs/architecture/crm-sprint-6-gate-matrix.md",
    "docs/security/crm-sprint-6-security-gate-review.md",
    "docs/data/crm-sprint-6-persistence-gate-review.md",
    "docs/api/crm-sprint-6-api-gate-review.md",
    "docs/testing/crm-sprint-6-e2e-gate-review.md",
    "docs/roadmap/crm-sprint-7-options.md",
    "docs/roadmap/crm-sprint-7-recommended-path.md",
    "docs/roadmap/crm-sprint-7-gates.md",
    "src/CRM.Application/Foundation/CrmSprint6GateDecisionContracts.cs",
    "src/CRM.Application/Foundation/CrmSprint6GateDecisionStatusService.cs"
)
foreach ($Sprint6P6RequiredFile in $Sprint6P6RequiredFiles) {
    if (-not (Test-Path $Sprint6P6RequiredFile)) { Fail "Missing Sprint 6 P6 required file: $Sprint6P6RequiredFile" } else { Pass "Required Sprint 6 P6 file exists: $Sprint6P6RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-6/gate-decision*") { Fail "Sprint 6 P6 gate decision route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/gate-decision") { Fail "Sprint 6 P6 gate decision endpoint must remain GET-only." }
$Sprint6P6Text = ""
foreach ($Sprint6P6File in @("src/CRM.Application/Foundation/CrmSprint6GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint6GateDecisionStatusService.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint6P6File) { $Sprint6P6Text += "`n" + (Get-Content -Raw $Sprint6P6File) }
}
foreach ($Sprint6P6Marker in @("Sprint6GateDecision", "GoForSprint7ControlledNonProductionActivationPlanning", "Sprint7P1SecretProviderRealNonProductionApproval", "Sprint 6 gate decision only; no real activation", "Sprint 6: Closed", "Sprint 6 Gate Decision: Completed", "Sprint 7 Planning: Go")) {
    if ($Sprint6P6Text -notmatch [regex]::Escape($Sprint6P6Marker)) { Fail "Missing Sprint 6 P6 marker: $Sprint6P6Marker" }
}

# Sprint 7 P1 Secret Provider Real NonProduction Approval checks
$Sprint7P1RequiredFiles = @(
    "docs/security/crm-sprint-7-p1-secret-provider-real-nonproduction-approval.md",
    "docs/security/crm-secret-provider-real-nonproduction-approval-policy.md",
    "docs/security/crm-secret-provider-real-nonproduction-secret-boundary.md",
    "docs/security/crm-secret-provider-real-nonproduction-approved-secret-names.md",
    "docs/operations/crm-secret-provider-real-nonproduction-approval-runbook.md",
    "docs/operations/crm-secret-provider-real-nonproduction-rollback-plan.md",
    "docs/architecture/crm-secret-provider-real-nonproduction-architecture-review.md",
    "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionApprovalContracts.cs",
    "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionApprovalStatusService.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionApprovalPlaceholder.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionApprovalOptions.cs"
)
foreach ($Sprint7P1RequiredFile in $Sprint7P1RequiredFiles) {
    if (-not (Test-Path $Sprint7P1RequiredFile)) { Fail "Missing Sprint 7 P1 required file: $Sprint7P1RequiredFile" } else { Pass "Required Sprint 7 P1 file exists: $Sprint7P1RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval*") { Fail "Sprint 7 P1 secret provider approval route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval") { Fail "Sprint 7 P1 endpoint must remain GET-only." }
$Sprint7P1Text = ""
foreach ($Sprint7P1File in @("src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionApprovalContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionApprovalStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionApprovalPlaceholder.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint7P1File) { $Sprint7P1Text += "`n" + (Get-Content -Raw $Sprint7P1File) }
}
foreach ($Sprint7P1Marker in @("SecretProviderRealNonProductionApproval", "SecretProviderRealNonProductionApprovalPackageExists", "SecretProviderRealNonProductionApprovalGranted", "SecretProviderRealRuntimeEnabled", "SecretProviderRealRuntimeConnected", "RealSecretReadAttempted", "KeyVaultRuntimeClientEnabled", "AzureSecretSdkRuntimeEnabled", "EnvFileRequired", "EnvSecretReadAllowed", "SecretsLogged", "SecretNamesApproved", "SecretValuesApproved", "Sprint7P2SecretProviderRealNonProductionRuntimeProbe", "Secret Provider real NonProduction approval package only; no real secrets are read")) {
    if ($Sprint7P1Text -notmatch [regex]::Escape($Sprint7P1Marker)) { Fail "Missing Sprint 7 P1 marker: $Sprint7P1Marker" }
}

# Sprint 7 P2 Secret Provider Real NonProduction Runtime Probe checks
$Sprint7P2RequiredFiles = @(
    "docs/security/crm-sprint-7-p2-secret-provider-real-nonproduction-runtime-probe.md",
    "docs/security/crm-secret-provider-real-nonproduction-runtime-probe-policy.md",
    "docs/security/crm-secret-provider-real-nonproduction-runtime-probe-contract.md",
    "docs/security/crm-secret-provider-real-nonproduction-runtime-probe-redaction.md",
    "docs/operations/crm-secret-provider-real-nonproduction-runtime-probe-runbook.md",
    "docs/operations/crm-secret-provider-real-nonproduction-runtime-probe-rollback.md",
    "docs/architecture/crm-secret-provider-real-nonproduction-runtime-probe-architecture.md",
    "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionRuntimeProbeContracts.cs",
    "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionRuntimeProbeStatusService.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionRuntimeProbe.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionRuntimeProbeOptions.cs"
)
foreach ($Sprint7P2RequiredFile in $Sprint7P2RequiredFiles) {
    if (-not (Test-Path $Sprint7P2RequiredFile)) { Fail "Missing Sprint 7 P2 required file: $Sprint7P2RequiredFile" } else { Pass "Required Sprint 7 P2 file exists: $Sprint7P2RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe*") { Fail "Sprint 7 P2 secret provider runtime probe route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe") { Fail "Sprint 7 P2 endpoint must remain GET-only." }
$Sprint7P2Text = ""
foreach ($Sprint7P2File in @("src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionRuntimeProbeContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionRuntimeProbeStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionRuntimeProbe.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint7P2File) { $Sprint7P2Text += "`n" + (Get-Content -Raw $Sprint7P2File) }
}
foreach ($Sprint7P2Marker in @("SecretProviderRealNonProductionRuntimeProbe", "SecretProviderRealNonProductionRuntimeProbeExists", "SecretProviderRealNonProductionApprovalGranted", "SecretProviderRealRuntimeProbeEnabled", "SecretProviderRealRuntimeProbeAttempted", "SecretProviderRealRuntimeConnected", "RealSecretValueMaterialized", "RealSecretValueLogged", "SecretValueReturnedToApi", "KeyVaultRuntimeClientCreated", "KeyVaultRuntimeCallAttempted", "AzureSecretSdkRuntimeEnabled", "EnvSecretReadAttempted", "EnvFileRequired", "LogicalSecretNamesValidated", "SecretValuesValidated", "ProbeSkippedBecauseApprovalNotGranted", "Sprint7P3CommonDbRealConnectivityNonProductionProbe", "Secret Provider real NonProduction runtime probe is prepared but skipped because approval is not granted")) {
    if ($Sprint7P2Text -notmatch [regex]::Escape($Sprint7P2Marker)) { Fail "Missing Sprint 7 P2 marker: $Sprint7P2Marker" }
}

# Sprint 7 P3 Common DB Real Connectivity NonProduction Probe checks
$Sprint7P3RequiredFiles = @(
    "docs/data/crm-sprint-7-p3-common-db-real-connectivity-nonproduction-probe.md",
    "docs/data/crm-common-db-real-connectivity-nonproduction-probe-policy.md",
    "docs/data/crm-common-db-real-connectivity-nonproduction-probe-contract.md",
    "docs/data/crm-common-db-real-connectivity-nonproduction-probe-safety-boundary.md",
    "docs/operations/crm-common-db-real-connectivity-nonproduction-probe-runbook.md",
    "docs/operations/crm-common-db-real-connectivity-nonproduction-probe-rollback.md",
    "docs/architecture/crm-common-db-real-connectivity-nonproduction-probe-architecture.md",
    "src/CRM.Application/Foundation/CrmCommonDbRealConnectivityNonProductionProbeContracts.cs",
    "src/CRM.Application/Foundation/CrmCommonDbRealConnectivityNonProductionProbeStatusService.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbRealConnectivityNonProductionProbe.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbRealConnectivityNonProductionProbeOptions.cs"
)
foreach ($Sprint7P3RequiredFile in $Sprint7P3RequiredFiles) {
    if (-not (Test-Path $Sprint7P3RequiredFile)) { Fail "Missing Sprint 7 P3 required file: $Sprint7P3RequiredFile" } else { Pass "Required Sprint 7 P3 file exists: $Sprint7P3RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe*") { Fail "Sprint 7 P3 Common DB route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe") { Fail "Sprint 7 P3 endpoint must remain GET-only." }
$Sprint7P3Text = ""
foreach ($Sprint7P3File in @("src/CRM.Application/Foundation/CrmCommonDbRealConnectivityNonProductionProbeContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbRealConnectivityNonProductionProbeStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbRealConnectivityNonProductionProbe.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint7P3File) { $Sprint7P3Text += "`n" + (Get-Content -Raw $Sprint7P3File) }
}
foreach ($Sprint7P3Marker in @("CommonDbRealConnectivityNonProductionProbe", "CommonDbRealConnectivityNonProductionProbeExists", "CommonDbRealConnectivityApprovalGranted", "SecretProviderRealNonProductionApprovalGranted", "ConnectionStringResolved", "ConnectionStringValueMaterialized", "ConnectionStringLogged", "ConnectionStringReturnedToApi", "CommonDbProbeEnabled", "CommonDbProbeAttempted", "CommonDbConnected", "SqlConnectionCreated", "DbConnectionCreated", "UseSqlServerEnabled", "EfRuntimeEnabled", "AddDbContextRuntimeEnabled", "MigrationsCreated", "DatabaseSchemaChanged", "ProductivePersistenceEnabled", "ApiRequiresDatabase", "UsesSecretProviderRuntime", "UsesSyntheticFallback", "mock://crm/common-db", "ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted", "Sprint7P4PortalAuthRealRuntimeProbe", "Common DB real connectivity NonProduction probe is prepared but skipped because Secret Provider approval is not granted")) {
    if ($Sprint7P3Text -notmatch [regex]::Escape($Sprint7P3Marker)) { Fail "Missing Sprint 7 P3 marker: $Sprint7P3Marker" }
}

# Sprint 7 P4 Portal Auth Real Runtime Probe checks
$Sprint7P4RequiredFiles = @(
    "docs/integration/crm-sprint-7-p4-portal-auth-real-runtime-probe.md",
    "docs/integration/crm-portal-auth-real-runtime-probe-policy.md",
    "docs/integration/crm-portal-auth-real-runtime-probe-contract.md",
    "docs/integration/crm-portal-auth-real-runtime-probe-safety-boundary.md",
    "docs/operations/crm-portal-auth-real-runtime-probe-runbook.md",
    "docs/operations/crm-portal-auth-real-runtime-probe-rollback.md",
    "docs/architecture/crm-portal-auth-real-runtime-probe-architecture.md",
    "docs/security/crm-portal-auth-real-runtime-probe-token-boundary.md",
    "src/CRM.Application/Foundation/CrmPortalAuthRealRuntimeProbeContracts.cs",
    "src/CRM.Application/Foundation/CrmPortalAuthRealRuntimeProbeStatusService.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRealRuntimeProbe.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRealRuntimeProbeOptions.cs"
)
foreach ($Sprint7P4RequiredFile in $Sprint7P4RequiredFiles) {
    if (-not (Test-Path $Sprint7P4RequiredFile)) { Fail "Missing Sprint 7 P4 required file: $Sprint7P4RequiredFile" } else { Pass "Required Sprint 7 P4 file exists: $Sprint7P4RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe*") { Fail "Sprint 7 P4 Portal Auth route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe") { Fail "Sprint 7 P4 endpoint must remain GET-only." }
$Sprint7P4Text = ""
foreach ($Sprint7P4File in @("src/CRM.Application/Foundation/CrmPortalAuthRealRuntimeProbeContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthRealRuntimeProbeStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRealRuntimeProbe.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint7P4File) { $Sprint7P4Text += "`n" + (Get-Content -Raw $Sprint7P4File) }
}
foreach ($Sprint7P4Marker in @("PortalAuthRealRuntimeProbe", "PortalAuthRealRuntimeProbeExists", "PortalAuthRealRuntimeApprovalGranted", "SecretProviderRealNonProductionApprovalGranted", "PortalAuthRealRuntimeProbeEnabled", "PortalAuthRealRuntimeProbeAttempted", "PortalAuthRuntimeConnected", "PortalAuthBaseUrlResolved", "PortalAuthBaseUrlMaterialized", "PortalAuthBaseUrlLogged", "PortalAuthBaseUrlReturnedToApi", "PortalHttpClientCreated", "PortalHttpCallAttempted", "PortalAuthTokenValidationAttempted", "TokenReadAttempted", "HeaderReadAttempted", "AuthorizationHeaderReadAttempted", "RealTokenMaterialized", "RealTokenLogged", "TokenReturnedToApi", "LoginImplementedByCrm", "LogoutImplementedByCrm", "IdentityImplementedByCrm", "RolesPersistedInCrm", "PermissionsPersistedInCrm", "ProductiveAuthorizationEnabled", "ApiRequiresPortalAuth", "UsesSyntheticFallback", "mock://crm/portal-auth", "mock://crm/portal-user", "ProbeSkippedBecausePortalAuthApprovalNotGranted", "Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423", "Portal Auth real runtime probe is prepared but skipped because Portal Auth approval is not granted")) {
    if ($Sprint7P4Text -notmatch [regex]::Escape($Sprint7P4Marker)) { Fail "Missing Sprint 7 P4 marker: $Sprint7P4Marker" }
}
if ($Sprint7P4Text -match "HttpContext\.Request\.Headers|Request\.Headers|Headers\[|Bearer|HttpClient\(|new HttpClient|PortalCorporativoUrl|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(") { Fail "Sprint 7 P4 must not read headers/tokens, call Portal, activate Auth or connect DB." }

# Sprint 7 P5 Locked Productive Route Runtime Registration checks
$Sprint7P5RequiredFiles = @(
    "docs/api/crm-sprint-7-p5-locked-productive-route-runtime-registration-with-423.md",
    "docs/api/crm-locked-productive-route-runtime-registration-policy.md",
    "docs/api/crm-locked-productive-route-runtime-registration-contract.md",
    "docs/security/crm-locked-productive-route-runtime-registration-safety-boundary.md",
    "docs/operations/crm-locked-productive-route-runtime-registration-runbook.md",
    "docs/operations/crm-locked-productive-route-runtime-registration-rollback.md",
    "docs/architecture/crm-locked-productive-route-runtime-registration-architecture.md",
    "src/CRM.Application/Foundation/CrmLockedProductiveRouteRuntimeRegistrationContracts.cs",
    "src/CRM.Application/Foundation/CrmLockedProductiveRouteRuntimeRegistrationStatusService.cs",
    "src/CRM.Api/ProductiveRoutes/LockedProductiveRouteRuntimeRegistration.cs",
    "src/CRM.Api/ProductiveRoutes/LockedProductiveRouteRuntimeRegistrationOptions.cs"
)
foreach ($Sprint7P5RequiredFile in $Sprint7P5RequiredFiles) {
    if (-not (Test-Path $Sprint7P5RequiredFile)) { Fail "Missing Sprint 7 P5 required file: $Sprint7P5RequiredFile" } else { Pass "Required Sprint 7 P5 file exists: $Sprint7P5RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-7/locked-productive-route-runtime-registration*") { Fail "Sprint 7 P5 locked productive route runtime registration route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/locked-productive-route-runtime-registration") { Fail "Sprint 7 P5 foundation endpoint must remain GET-only." }
$Sprint7P5Text = ""
foreach ($Sprint7P5File in @("src/CRM.Application/Foundation/CrmLockedProductiveRouteRuntimeRegistrationContracts.cs", "src/CRM.Application/Foundation/CrmLockedProductiveRouteRuntimeRegistrationStatusService.cs", "src/CRM.Api/ProductiveRoutes/LockedProductiveRouteRuntimeRegistration.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint7P5File) { $Sprint7P5Text += "`n" + (Get-Content -Raw $Sprint7P5File) }
}
foreach ($Sprint7P5Marker in @("LockedProductiveRouteRuntimeRegistrationWith423", "LockedProductiveRouteRuntimeRegistrationExists", "LockedProductiveRouteRuntimeRegistrationApprovalGranted", "LockedProductiveRouteRuntimeRegistrationEnabled", "ProductiveRoutesRegisteredByDefault", "ProductiveRoutesRegisteredWhenExplicitlyEnabled", "DefaultNegativeRouteStatus", "ExplicitlyEnabledLockedRouteStatus", "ProductiveCrudEnabled", "ProductiveDomainExecutionEnabled", "ProductivePersistenceEnabled", "DeleteEndpointsEnabled", "PortalAuthRuntimeRequired", "PortalAuthRuntimeEnabled", "TokenReadAttempted", "HeaderReadAttempted", "DbRuntimeEnabled", "EfRuntimeEnabled", "MigrationsCreated", "SideEffectsAllowed", "Sprint7P6Sprint7GateDecision", "Locked productive routes are not registered by default; explicit NonProduction flag returns 423 without side effects")) {
    if ($Sprint7P5Text -notmatch [regex]::Escape($Sprint7P5Marker)) { Fail "Missing Sprint 7 P5 marker: $Sprint7P5Marker" }
}
if ($Sprint7P5Text -notmatch "Crm:ProductiveRoutes:LockedRegistrationEnabled") { Fail "Sprint 7 P5 locked route flag missing." }
if ($Sprint7P5Text -match "MapDelete|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") { Fail "Sprint 7 P5 must not enable DELETE, DB, Portal/Auth runtime, token/header reads or token storage." }

# Sprint 7 P6 Gate Decision checks
$Sprint7P6RequiredFiles = @(
    "docs/releases/crm-sprint-7-closure.md",
    "docs/releases/crm-sprint-7-integrated-evidence.md",
    "docs/releases/crm-sprint-7-gate-decision.md",
    "docs/releases/crm-sprint-7-go-no-go.md",
    "docs/releases/crm-sprint-7-open-risks.md",
    "docs/releases/crm-sprint-7-decision-record.md",
    "docs/architecture/crm-sprint-7-gate-matrix.md",
    "docs/security/crm-sprint-7-security-gate-review.md",
    "docs/data/crm-sprint-7-persistence-gate-review.md",
    "docs/api/crm-sprint-7-api-gate-review.md",
    "docs/testing/crm-sprint-7-e2e-gate-review.md",
    "docs/roadmap/crm-sprint-8-options.md",
    "docs/roadmap/crm-sprint-8-recommended-path.md",
    "docs/roadmap/crm-sprint-8-gates.md",
    "src/CRM.Application/Foundation/CrmSprint7GateDecisionContracts.cs",
    "src/CRM.Application/Foundation/CrmSprint7GateDecisionStatusService.cs"
)
foreach ($Sprint7P6RequiredFile in $Sprint7P6RequiredFiles) {
    if (-not (Test-Path $Sprint7P6RequiredFile)) { Fail "Missing Sprint 7 P6 required file: $Sprint7P6RequiredFile" } else { Pass "Required Sprint 7 P6 file exists: $Sprint7P6RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-7/gate-decision*") { Fail "Sprint 7 P6 gate decision route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/gate-decision") { Fail "Sprint 7 P6 endpoint must remain GET-only." }
$Sprint7P6Text = ""
foreach ($Sprint7P6File in @("src/CRM.Application/Foundation/CrmSprint7GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint7GateDecisionStatusService.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint7P6File) { $Sprint7P6Text += "`n" + (Get-Content -Raw $Sprint7P6File) }
}
foreach ($Sprint7P6Marker in @("Sprint7GateDecision", "GoForSprint8ControlledRuntimeApprovalAndPilotPlanning", "RealActivationDecision", "SecretProviderRealRuntimeDecision", "CommonDbRealConnectionDecision", "PortalAuthRealRuntimeDecision", "GoOnlyAsExplicitNonProductionLocked423", "ProductiveRoutesDefaultDecision", "ProductiveCrudDecision", "DeleteDecision", "ProductiveUiDecision", "ProductizationStatus", "Sprint8PlanningDecision", "Sprint8P1SecretProviderApprovalDecision", "Sprint 7 gate decision only; no real activation")) {
    if ($Sprint7P6Text -notmatch [regex]::Escape($Sprint7P6Marker)) { Fail "Missing Sprint 7 P6 marker: $Sprint7P6Marker" }
}
if ($Sprint7P6Text -match "SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") { Fail "Sprint 7 P6 must not activate DB, Portal/Auth runtime, token/header reads or token storage." }

# Sprint 8 P1 Secret Provider Approval Decision checks
$Sprint8P1RequiredFiles = @(
    "docs/security/crm-sprint-8-p1-secret-provider-approval-decision.md",
    "docs/security/crm-secret-provider-approval-decision-policy.md",
    "docs/security/crm-secret-provider-controlled-read-approval-criteria.md",
    "docs/security/crm-secret-provider-approved-logical-secret-names.md",
    "docs/security/crm-secret-provider-redaction-approval.md",
    "docs/operations/crm-secret-provider-controlled-read-runbook.md",
    "docs/operations/crm-secret-provider-controlled-read-rollback.md",
    "docs/architecture/crm-secret-provider-controlled-read-architecture-decision.md",
    "src/CRM.Application/Foundation/CrmSecretProviderApprovalDecisionContracts.cs",
    "src/CRM.Application/Foundation/CrmSecretProviderApprovalDecisionStatusService.cs"
)
foreach ($Sprint8P1RequiredFile in $Sprint8P1RequiredFiles) {
    if (-not (Test-Path $Sprint8P1RequiredFile)) { Fail "Missing Sprint 8 P1 required file: $Sprint8P1RequiredFile" } else { Pass "Required Sprint 8 P1 file exists: $Sprint8P1RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-8/secret-provider-approval-decision*") { Fail "Sprint 8 P1 secret provider approval decision route missing." }
if ($program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-8/secret-provider-approval-decision") { Fail "Sprint 8 P1 endpoint must remain GET-only." }
$Sprint8P1Text = ""
foreach ($Sprint8P1File in @("src/CRM.Application/Foundation/CrmSecretProviderApprovalDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderApprovalDecisionStatusService.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint8P1File) { $Sprint8P1Text += "`n" + (Get-Content -Raw $Sprint8P1File) }
}
foreach ($Sprint8P1Marker in @("SecretProviderApprovalDecision", "ApprovedForControlledNonProductionReadPlanning", "SecretProviderRealReadApprovedForNextSprint", "SecretProviderRealReadEnabledNow", "RealSecretReadAttempted", "RealSecretValueMaterialized", "RealSecretValueLogged", "SecretValueReturnedToApi", "KeyVaultRuntimeClientCreated", "KeyVaultRuntimeCallAttempted", "AzureSecretSdkRuntimeEnabled", "EnvFileRequired", "EnvSecretReadAllowed", "ApprovedSecretNamesOnly", "ApprovedSecretValues", "ApprovedForNonProductionOnly", "SecurityApprovalRecorded", "ArchitectureApprovalRecorded", "DevOpsApprovalRecorded", "RollbackPlanApproved", "ObservabilityPlanApproved", "RedactionPlanApproved", "Sprint8P2SecretProviderControlledRealNonProductionRead", "Secret Provider approval decision only; no real secret read in Sprint 8 P1")) {
    if ($Sprint8P1Text -notmatch [regex]::Escape($Sprint8P1Marker)) { Fail "Missing Sprint 8 P1 marker: $Sprint8P1Marker" }
}
if ($Sprint8P1Text -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") { Fail "Sprint 8 P1 must not read secrets/env/files or activate DB, Portal/Auth runtime, token/header reads or token storage." }

# Sprint 8 P2 Secret Provider Controlled Real NonProduction Read checks
$Sprint8P2RequiredFiles = @(
    "docs/security/crm-sprint-8-p2-secret-provider-controlled-real-nonproduction-read.md",
    "docs/security/crm-secret-provider-controlled-real-read-policy.md",
    "docs/security/crm-secret-provider-controlled-real-read-contract.md",
    "docs/security/crm-secret-provider-controlled-real-read-redaction.md",
    "docs/operations/crm-secret-provider-controlled-real-read-runbook.md",
    "docs/operations/crm-secret-provider-controlled-real-read-rollback.md",
    "docs/architecture/crm-secret-provider-controlled-real-read-architecture.md",
    "src/CRM.Application/Foundation/CrmSecretProviderControlledRealReadContracts.cs",
    "src/CRM.Application/Foundation/CrmSecretProviderControlledRealReadStatusService.cs",
    "src/CRM.Infrastructure/Security/Secrets/ISecretProviderRuntime.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeOptions.cs",
    "src/CRM.Infrastructure/Security/Secrets/DisabledSecretProviderRuntime.cs",
    "src/CRM.Infrastructure/Security/Secrets/ControlledNonProductionSecretProviderRuntime.cs"
)
foreach ($Sprint8P2RequiredFile in $Sprint8P2RequiredFiles) {
    if (-not (Test-Path $Sprint8P2RequiredFile)) { Fail "Missing Sprint 8 P2 required file: $Sprint8P2RequiredFile" } else { Pass "Required Sprint 8 P2 file exists: $Sprint8P2RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read*") { Fail "Sprint 8 P2 controlled read route missing." }
$Sprint8P2Text = ""
foreach ($Sprint8P2File in @("src/CRM.Application/Foundation/CrmSecretProviderControlledRealReadContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderControlledRealReadStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/ISecretProviderRuntime.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeOptions.cs", "src/CRM.Infrastructure/Security/Secrets/DisabledSecretProviderRuntime.cs", "src/CRM.Infrastructure/Security/Secrets/ControlledNonProductionSecretProviderRuntime.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint8P2File) { $Sprint8P2Text += "`n" + (Get-Content -Raw $Sprint8P2File) }
}
foreach ($Sprint8P2Marker in @("SecretProviderControlledRealNonProductionRead", "SecretProviderControlledRealNonProductionReadEnabled: false", "SecretProviderControlledRealNonProductionReadAttempted: false", "RealSecretReadAttempted: false", "SecretValueReturnedToApi: false", "SecretValuePersisted: false", "SecretValueCached: false", "FailClosedByDefault: true", "Sprint8P3CommonDbControlledRealConnectivity", "Controlled real secret read is disabled by default and never returns secret values", "DisabledSecretProviderRuntime", "ControlledNonProductionSecretProviderRuntime", "ISecretProviderRuntime")) {
    if ($Sprint8P2Text -notmatch [regex]::Escape($Sprint8P2Marker)) { Fail "Missing Sprint 8 P2 marker: $Sprint8P2Marker" }
}
if ($Sprint8P2Text -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") { Fail "Sprint 8 P2 must not activate secret SDK, DB, Portal/Auth runtime, token/header reads or token storage." }

# Sprint 8 P3 Common DB Controlled Real Connectivity checks
$Sprint8P3RequiredFiles = @(
    "docs/data/crm-sprint-8-p3-common-db-controlled-real-connectivity.md",
    "docs/data/crm-common-db-controlled-real-connectivity-policy.md",
    "docs/data/crm-common-db-controlled-real-connectivity-contract.md",
    "docs/data/crm-common-db-controlled-real-connectivity-safety-boundary.md",
    "docs/operations/crm-common-db-controlled-real-connectivity-runbook.md",
    "docs/operations/crm-common-db-controlled-real-connectivity-rollback.md",
    "docs/architecture/crm-common-db-controlled-real-connectivity-architecture.md",
    "src/CRM.Application/Foundation/CrmCommonDbControlledRealConnectivityContracts.cs",
    "src/CRM.Application/Foundation/CrmCommonDbControlledRealConnectivityStatusService.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/ICommonDbConnectivityProbe.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityProbeOptions.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/DisabledCommonDbConnectivityProbe.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/ControlledNonProductionCommonDbConnectivityProbe.cs"
)
foreach ($Sprint8P3RequiredFile in $Sprint8P3RequiredFiles) {
    if (-not (Test-Path $Sprint8P3RequiredFile)) { Fail "Missing Sprint 8 P3 required file: $Sprint8P3RequiredFile" } else { Pass "Required Sprint 8 P3 file exists: $Sprint8P3RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-8/common-db-controlled-real-connectivity*") { Fail "Sprint 8 P3 common DB controlled connectivity route missing." }
$Sprint8P3Text = ""
foreach ($Sprint8P3File in @("src/CRM.Application/Foundation/CrmCommonDbControlledRealConnectivityContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbControlledRealConnectivityStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/ICommonDbConnectivityProbe.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityProbeOptions.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/DisabledCommonDbConnectivityProbe.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/ControlledNonProductionCommonDbConnectivityProbe.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint8P3File) { $Sprint8P3Text += "`n" + (Get-Content -Raw $Sprint8P3File) }
}
foreach ($Sprint8P3Marker in @("CommonDbControlledRealConnectivity", "CommonDbControlledRealConnectivityEnabled: false", "CommonDbConnectivityAttempted: false", "CommonDbConnected: false", "ConnectionStringReturnedToApi: false", "ConnectionStringLogged: false", "MigrationsCreated: false", "ProductiveCrudEnabled: false", "FailClosedByDefault: true", "Sprint8P4PortalAuthControlledRealRuntimeValidation", "Common DB controlled real connectivity is disabled by default and never exposes connection strings", "DisabledCommonDbConnectivityProbe", "ControlledNonProductionCommonDbConnectivityProbe", "ICommonDbConnectivityProbe")) {
    if ($Sprint8P3Text -notmatch [regex]::Escape($Sprint8P3Marker)) { Fail "Missing Sprint 8 P3 marker: $Sprint8P3Marker" }
}
if ($Sprint8P3Text -match "System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") { Fail "Sprint 8 P3 must not activate DB, EF, migrations, Portal/Auth runtime, token/header reads or token storage." }

# Sprint 8 P4 Portal Auth Controlled Real Runtime Validation checks
$Sprint8P4RequiredFiles = @(
    "docs/integration/crm-sprint-8-p4-portal-auth-controlled-real-runtime-validation.md",
    "docs/integration/crm-portal-auth-controlled-real-runtime-validation-policy.md",
    "docs/integration/crm-portal-auth-controlled-real-runtime-validation-contract.md",
    "docs/security/crm-portal-auth-controlled-runtime-token-boundary.md",
    "docs/security/crm-portal-auth-controlled-runtime-redaction.md",
    "docs/operations/crm-portal-auth-controlled-runtime-validation-runbook.md",
    "docs/operations/crm-portal-auth-controlled-runtime-validation-rollback.md",
    "docs/architecture/crm-portal-auth-controlled-runtime-validation-architecture.md",
    "src/CRM.Application/Foundation/CrmPortalAuthControlledRealRuntimeValidationContracts.cs",
    "src/CRM.Application/Foundation/CrmPortalAuthControlledRealRuntimeValidationStatusService.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/IPortalAuthRuntimeValidationProbe.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRuntimeValidationProbeOptions.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/DisabledPortalAuthRuntimeValidationProbe.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/ControlledNonProductionPortalAuthRuntimeValidationProbe.cs"
)
foreach ($Sprint8P4RequiredFile in $Sprint8P4RequiredFiles) {
    if (-not (Test-Path $Sprint8P4RequiredFile)) { Fail "Missing Sprint 8 P4 required file: $Sprint8P4RequiredFile" } else { Pass "Required Sprint 8 P4 file exists: $Sprint8P4RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation*") { Fail "Sprint 8 P4 Portal Auth controlled validation route missing." }
$Sprint8P4Text = ""
foreach ($Sprint8P4File in @("src/CRM.Application/Foundation/CrmPortalAuthControlledRealRuntimeValidationContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthControlledRealRuntimeValidationStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/IPortalAuthRuntimeValidationProbe.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRuntimeValidationProbeOptions.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/DisabledPortalAuthRuntimeValidationProbe.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/ControlledNonProductionPortalAuthRuntimeValidationProbe.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint8P4File) { $Sprint8P4Text += "`n" + (Get-Content -Raw $Sprint8P4File) }
}
foreach ($Sprint8P4Marker in @("PortalAuthControlledRealRuntimeValidation", "PortalAuthControlledRealRuntimeValidationEnabled: false", "PortalAuthRuntimeValidationAttempted: false", "PortalAuthRuntimeConnected: false", "PortalAuthBaseUrlReturnedToApi: false", "TokenReturnedToApi: false", "HeaderReadAttempted: false", "ProductiveAuthorizationEnabled: false", "FailClosedByDefault: true", "Sprint8P5LockedRouteAuthorizationPolicyIntegration", "Portal Auth controlled real runtime validation is disabled by default and never reads request tokens", "DisabledPortalAuthRuntimeValidationProbe", "ControlledNonProductionPortalAuthRuntimeValidationProbe", "IPortalAuthRuntimeValidationProbe")) {
    if ($Sprint8P4Text -notmatch [regex]::Escape($Sprint8P4Marker)) { Fail "Missing Sprint 8 P4 marker: $Sprint8P4Marker" }
}
if ($Sprint8P4Text -match "HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder") { Fail "Sprint 8 P4 must not activate Portal/Auth runtime, token/header reads, token storage, DB, EF or migrations." }

# Sprint 8 P5 Locked Route Authorization Policy Integration checks
$Sprint8P5RequiredFiles = @(
    "docs/api/crm-sprint-8-p5-locked-route-authorization-policy-integration.md",
    "docs/api/crm-locked-route-authorization-policy-contract.md",
    "docs/api/crm-locked-route-authorization-policy-boundary.md",
    "docs/security/crm-locked-route-authorization-policy-security-review.md",
    "docs/security/crm-locked-route-authorization-policy-token-boundary.md",
    "docs/operations/crm-locked-route-authorization-policy-runbook.md",
    "docs/operations/crm-locked-route-authorization-policy-rollback.md",
    "docs/architecture/crm-locked-route-authorization-policy-architecture.md",
    "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyIntegrationContracts.cs",
    "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyIntegrationStatusService.cs",
    "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyEvaluator.cs",
    "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyEvaluationRequest.cs",
    "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyEvaluationResult.cs"
)
foreach ($Sprint8P5RequiredFile in $Sprint8P5RequiredFiles) {
    if (-not (Test-Path $Sprint8P5RequiredFile)) { Fail "Missing Sprint 8 P5 required file: $Sprint8P5RequiredFile" } else { Pass "Required Sprint 8 P5 file exists: $Sprint8P5RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-8/locked-route-authorization-policy-integration*") { Fail "Sprint 8 P5 locked route authorization policy route missing." }
$Sprint8P5Text = ""
foreach ($Sprint8P5File in @("src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyIntegrationContracts.cs", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyIntegrationStatusService.cs", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyEvaluator.cs", "src/CRM.Api/ProductiveRoutes/LockedProductiveRouteRuntimeRegistration.cs", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint8P5File) { $Sprint8P5Text += "`n" + (Get-Content -Raw $Sprint8P5File) }
}
foreach ($Sprint8P5Marker in @("LockedRouteAuthorizationPolicyIntegration", "LockedRouteAuthorizationPolicyIntegrationEnabled: false", "AuthorizationPolicyEvaluated: false", "NotEvaluatedBecauseDisabled", "BlockedBecauseRouteLocked", "PortalAuthMetadataUsed: true", "PortalAuthRuntimeRequired: false", "TokenReadAttempted: false", "HeaderReadAttempted: false", "PortalHttpCallAttempted: false", "ProductiveRoutesRegisteredByDefault: false", "DefaultNegativeRouteStatus: 404", "LockedRoutesEnabledOnlyWithExplicitNonProductionFlag: true", "LockedRouteStatus: 423", "ProductiveCrudEnabled: false", "ProductiveDomainExecutionEnabled: false", "ProductivePersistenceEnabled: false", "DeleteEndpointsEnabled: false", "DbRuntimeEnabled: false", "EfRuntimeEnabled: false", "Sprint8P6Sprint8GateDecision", "Locked route authorization policy is disabled by default and never activates productive CRM routes")) {
    if ($Sprint8P5Text -notmatch [regex]::Escape($Sprint8P5Marker)) { Fail "Missing Sprint 8 P5 marker: $Sprint8P5Marker" }
}
if ($Sprint8P5Text -match "HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder|MapDelete") { Fail "Sprint 8 P5 must not activate Portal/Auth runtime, token/header reads, token storage, DB, EF, migrations or DELETE." }

# Sprint 8 P6 Gate Decision checks
$Sprint8P6RequiredFiles = @(
    "docs/releases/crm-sprint-8-closure.md",
    "docs/releases/crm-sprint-8-integrated-evidence.md",
    "docs/releases/crm-sprint-8-gate-decision.md",
    "docs/releases/crm-sprint-8-go-no-go.md",
    "docs/releases/crm-sprint-8-open-risks.md",
    "docs/releases/crm-sprint-8-decision-record.md",
    "docs/architecture/crm-sprint-8-gate-matrix.md",
    "docs/security/crm-sprint-8-security-gate-review.md",
    "docs/data/crm-sprint-8-persistence-gate-review.md",
    "docs/api/crm-sprint-8-api-gate-review.md",
    "docs/testing/crm-sprint-8-e2e-gate-review.md",
    "docs/roadmap/crm-sprint-9-options.md",
    "docs/roadmap/crm-sprint-9-recommended-path.md",
    "docs/roadmap/crm-sprint-9-gates.md",
    "src/CRM.Application/Foundation/CrmSprint8GateDecisionContracts.cs",
    "src/CRM.Application/Foundation/CrmSprint8GateDecisionStatusService.cs"
)
foreach ($Sprint8P6RequiredFile in $Sprint8P6RequiredFiles) {
    if (-not (Test-Path $Sprint8P6RequiredFile)) { Fail "Missing Sprint 8 P6 required file: $Sprint8P6RequiredFile" } else { Pass "Required Sprint 8 P6 file exists: $Sprint8P6RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-8/gate-decision*") { Fail "Sprint 8 P6 gate decision route missing." }
$Sprint8P6Text = ""
foreach ($Sprint8P6File in @("src/CRM.Application/Foundation/CrmSprint8GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint8GateDecisionStatusService.cs", "docs/releases/crm-sprint-8-gate-decision.md", "docs/roadmap/crm-sprint-9-recommended-path.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint8P6File) { $Sprint8P6Text += "`n" + (Get-Content -Raw $Sprint8P6File) }
}
foreach ($Sprint8P6Marker in @("Sprint8GateDecision", "GoForSprint9ControlledRuntimeActivationPlanning", "RealProductionActivationDecision: `"NoGo`"", "SecretProviderControlledReadDecision", "GoOnlyAsExplicitNonProductionFlag", "CommonDbControlledConnectivityDecision", "PortalAuthControlledValidationDecision", "LockedRouteAuthorizationPolicyDecision", "GoOnlyAsExplicitNonProductionLocked423", "ProductiveRoutesDefaultDecision", "ProductiveCrudDecision", "DeleteDecision", "ProductiveUiDecision", "ProductizationStatus", "NotReady", "Sprint9PlanningDecision", "Sprint9P1ControlledRuntimeActivationDecision", "Sprint 8 gate decision only; no production activation", "Sprint 8: Closed")) {
    if ($Sprint8P6Text -notmatch [regex]::Escape($Sprint8P6Marker)) { Fail "Missing Sprint 8 P6 marker: $Sprint8P6Marker" }
}
if ($Sprint8P6Text -match "HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder|MapDelete") { Fail "Sprint 8 P6 must not activate Portal/Auth runtime, token/header reads, token storage, DB, EF, migrations or DELETE." }

# Sprint 9 P1 Controlled Runtime Activation Decision checks
$Sprint9P1RequiredFiles = @(
    "docs/releases/crm-sprint-9-p1-controlled-runtime-activation-decision.md",
    "docs/architecture/crm-controlled-runtime-activation-decision.md",
    "docs/security/crm-controlled-runtime-activation-approval-policy.md",
    "docs/operations/crm-controlled-runtime-activation-runbook.md",
    "docs/operations/crm-controlled-runtime-activation-rollback.md",
    "docs/testing/crm-controlled-runtime-activation-test-strategy.md",
    "docs/architecture/crm-sprint-9-gate-matrix.md",
    "docs/security/crm-sprint-9-security-gate-review.md",
    "docs/data/crm-sprint-9-runtime-data-gate-review.md",
    "docs/api/crm-sprint-9-runtime-api-gate-review.md",
    "docs/testing/crm-sprint-9-runtime-e2e-gate-review.md",
    "src/CRM.Application/Foundation/CrmControlledRuntimeActivationDecisionContracts.cs",
    "src/CRM.Application/Foundation/CrmControlledRuntimeActivationDecisionStatusService.cs"
)
foreach ($Sprint9P1RequiredFile in $Sprint9P1RequiredFiles) {
    if (-not (Test-Path $Sprint9P1RequiredFile)) { Fail "Missing Sprint 9 P1 required file: $Sprint9P1RequiredFile" } else { Pass "Required Sprint 9 P1 file exists: $Sprint9P1RequiredFile" }
}
if ($program -notlike "*/api/crm/foundation/sprint-9/controlled-runtime-activation-decision*") { Fail "Sprint 9 P1 controlled runtime activation route missing." }
$Sprint9P1Text = ""
foreach ($Sprint9P1File in @("src/CRM.Application/Foundation/CrmControlledRuntimeActivationDecisionContracts.cs", "src/CRM.Application/Foundation/CrmControlledRuntimeActivationDecisionStatusService.cs", "docs/releases/crm-sprint-9-p1-controlled-runtime-activation-decision.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $Sprint9P1File) { $Sprint9P1Text += "`n" + (Get-Content -Raw $Sprint9P1File) }
}
foreach ($Sprint9P1Marker in @("ControlledRuntimeActivationDecision", "ApprovedForNonProductionTrialsOnly", "ProductionActivationDecision", "NoGo", "RuntimeTrialsEnabledNow: false", "ProductionRuntimeEnabledNow: false", "ProductiveRoutesEnabledNow: false", "ProductiveCrudEnabledNow: false", "DeleteEnabledNow: false", "Sprint9P2SecretProviderRuntimeEnablementTrial", "Sprint 9 P1 is an approval decision only; no runtime trial is enabled now", "Sprint 9 P1 Controlled Runtime Activation Decision: Exists")) {
    if ($Sprint9P1Text -notmatch [regex]::Escape($Sprint9P1Marker)) { Fail "Missing Sprint 9 P1 marker: $Sprint9P1Marker" }
}
if ($Sprint9P1Text -match "HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder|MapDelete") { Fail "Sprint 9 P1 must not activate Portal/Auth runtime, token/header reads, token storage, DB, EF, migrations or DELETE." }

if ($failures.Count -gt 0) { exit 1 }
Pass "CRM guardrails passed."
exit 0
