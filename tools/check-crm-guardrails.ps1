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

if ($failures.Count -gt 0) { exit 1 }
Pass "CRM guardrails passed."
exit 0
