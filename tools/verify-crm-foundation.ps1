param()

$ErrorActionPreference = "Continue"
$failures = @()

function Require-Path($Path) {
    if (-not (Test-Path $Path)) {
        $script:failures += "Missing required path: $Path"
    }
}

@(
    "README.md",
    "CRM.sln",
    "src/CRM.Api/CRM.Api.csproj",
    "src/CRM.Application/CRM.Application.csproj",
    "src/CRM.Domain/CRM.Domain.csproj",
    "src/CRM.Infrastructure/CRM.Infrastructure.csproj",
    "tests/CRM.UnitTests/CRM.UnitTests.csproj",
    "tests/CRM.ArchitectureTests/CRM.ArchitectureTests.csproj",
    "docs/architecture/crm-architecture-overview.md",
    "docs/architecture/crm-clean-architecture.md",
    "docs/architecture/crm-portal-integration-principles.md",
    "docs/architecture/crm-security-guardrails.md",
    "docs/domain/crm-domain-model.md",
    "docs/domain/crm-business-rules.md",
    "docs/domain/crm-leads-foundation.md",
    "docs/domain/crm-accounts-foundation.md",
    "docs/domain/crm-contacts-foundation.md",
    "docs/data/crm-persistence-strategy.md",
    "docs/data/crm-read-models.md",
    "docs/data/crm-data-ownership.md",
    "docs/data/crm-migration-readiness-checklist.md",
    "docs/api/crm-api-contracts.md",
    "docs/api/crm-api-index.md",
    "docs/api/crm-foundation-preview-api.md",
    "docs/api/crm-read-model-preview-api.md",
    "docs/integration/crm-portal-boundary.md",
    "docs/integration/crm-portal-adapter-contracts.md",
    "docs/integration/crm-portal-capability-map.md",
    "docs/integration/crm-portal-readiness-checklist.md",
    "docs/integration/crm-financial-boundary.md",
    "docs/integration/crm-financial-adapter-contracts.md",
    "docs/integration/crm-financial-capability-map.md",
    "docs/integration/crm-financial-readiness-checklist.md",
    "docs/integration/crm-financial-event-contracts.md",
    "docs/security/crm-portal-security-boundary.md",
    "docs/security/crm-financial-security-boundary.md",
    "docs/security/crm-reporting-security-boundary.md",
    "docs/reporting/crm-reporting-contracts.md",
    "docs/reporting/crm-kpi-catalog.md",
    "docs/reporting/crm-dashboard-catalog.md",
    "docs/reporting/crm-analytics-read-models.md",
    "docs/reporting/crm-powerbi-readiness-checklist.md",
    "docs/roadmap/crm-roadmap.md",
    "docs/roadmap/crm-sprint-plan.md",
    "docs/releases/crm-sprint-1-notes.md",
    "docs/releases/crm-sprint-1-foundation-closure.md",
    "docs/releases/crm-sprint-1-integrated-evidence.md",
    "docs/releases/crm-sprint-1-go-no-go.md",
    "docs/releases/crm-sprint-1-open-risks.md",
    "docs/releases/crm-sprint-1-next-sprint-options.md",
    "docs/architecture/crm-integrated-capability-matrix.md",
    "docs/architecture/crm-ownership-boundaries.md",
    "docs/architecture/crm-cross-module-dependency-map.md",
    "docs/api/crm-foundation-endpoint-inventory.md",
    "docs/security/crm-foundation-guardrail-register.md",
    "docs/security/crm-sensitive-data-policy.md",
    "docs/roadmap/crm-sprint-2-options.md",
    "docs/roadmap/crm-sprint-2-recommended-path.md",
    "docs/roadmap/crm-productization-gates.md",
    "frontend/crm-web/package.json",
    "src/CRM.Domain/Entities/Lead.cs",
    "src/CRM.Domain/Entities/Opportunity.cs",
    "src/CRM.Domain/Entities/Activity.cs",
    "src/CRM.Domain/ValueObjects/ContactValueObjects.cs",
    "src/CRM.Domain/ValueObjects/BusinessValueObjects.cs",
    "src/CRM.Domain/ValueObjects/FoundationValueObjects.cs",
    "src/CRM.Application/Contracts/CrmDomainCatalogService.cs",
    "src/CRM.Application/Foundation/CrmSprint1ClosureStatusService.cs",
    "src/CRM.Application/Foundation/LeadFoundationService.cs",
    "src/CRM.Application/Foundation/AccountFoundationService.cs",
    "src/CRM.Application/Foundation/ContactFoundationService.cs",
    "src/CRM.Application/Persistence/CrmPersistencePorts.cs",
    "src/CRM.Application/Portal/CrmPortalIntegrationStatusService.cs",
    "src/CRM.Application/Portal/PortalIntegrationContracts.cs",
    "src/CRM.Application/Ports/Portal/IPortalUserContextProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalPermissionProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalMenuRegistrationProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalAuditPublisher.cs",
    "src/CRM.Application/Ports/Portal/IPortalNotificationPublisher.cs",
    "src/CRM.Application/Ports/Portal/IPortalConfigurationProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalCorrelationContext.cs",
    "src/CRM.Infrastructure/Portal/PortalAdapterNotConfiguredException.cs",
    "src/CRM.Infrastructure/Portal/PortalIntegrationPlaceholder.cs",
    "src/CRM.Application/Financial/CrmFinancialIntegrationStatusService.cs",
    "src/CRM.Application/Financial/FinancialIntegrationContracts.cs",
    "src/CRM.Application/Financial/FinancialConceptualEvents.cs",
    "src/CRM.Application/Ports/Financial/IFinancialCustomerLookupPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialAccountStatusPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialInvoiceAwarenessPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialPaymentStatusPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialCollectionsSignalPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialEventPublisher.cs",
    "src/CRM.Infrastructure/Financial/FinancialAdapterNotConfiguredException.cs",
    "src/CRM.Infrastructure/Financial/FinancialIntegrationPlaceholder.cs",
    "src/CRM.Application/Reporting/CrmReportingIntegrationStatusService.cs",
    "src/CRM.Application/Reporting/ReportingContracts.cs",
    "src/CRM.Application/Ports/Reporting/ICrmKpiCatalogProvider.cs",
    "src/CRM.Application/Ports/Reporting/ICrmDashboardCatalogProvider.cs",
    "src/CRM.Application/Ports/Reporting/ICrmAnalyticsReadModelProvider.cs",
    "src/CRM.Application/Ports/Reporting/ICrmReportAuthorizationContext.cs",
    "src/CRM.Infrastructure/Reporting/ReportingAdapterNotConfiguredException.cs",
    "src/CRM.Infrastructure/Reporting/ReportingIntegrationPlaceholder.cs",
    "src/CRM.Application/ReadModels/ReadModelContracts.cs",
    "src/CRM.Application/ReadModels/ReadModelPreviewServices.cs"
) | ForEach-Object { Require-Path $_ }

$composeText = if (Test-Path "docker-compose.yml") { Get-Content -Raw "docker-compose.yml" } else { "" }
$composeCrmText = if (Test-Path "docker-compose.crm.yml") { Get-Content -Raw "docker-compose.crm.yml" } else { "" }
if (($composeText + $composeCrmText) -match "mcr\.microsoft\.com/mssql|1433:1433|container_name:\s*.*sql") {
    $failures += "SQL Server container or default SQL port mapping found in compose."
}

if (Test-Path ".env") {
    $failures += ".env must not be committed or required."
}

$scanRoots = @("src", "tests", "frontend", "docker-compose.yml", "docker-compose.crm.yml")
$patterns = "BEGIN PRIVATE KEY|BEGIN CERTIFICATE|access_token=|id_token=|refresh_token=|localStorage|sessionStorage|Microsoft\.AspNetCore\.Identity|AddIdentity|HardcodedFinanciero|https://github\.com/christyepez/Financiero/.*/api|https?://.*Financiero|https?://.*PortalCorporativo"
foreach ($root in $scanRoots) {
    if (Test-Path $root) {
        $paths = if ((Get-Item $root).PSIsContainer) {
            Get-ChildItem -Path $root -Recurse -File -ErrorAction SilentlyContinue |
                Where-Object { $_.FullName -notmatch "\\(bin|obj|node_modules|dist|\.angular|tools)\\" } |
                Select-Object -ExpandProperty FullName
        } else {
            @($root)
        }
        $matches = if ($paths.Count -gt 0) { Select-String -Path $paths -Pattern $patterns -ErrorAction SilentlyContinue } else { @() }
        foreach ($match in $matches) {
            $failures += "Forbidden pattern in $($match.Path):$($match.LineNumber)"
        }
    }
}

$apiProgram = Get-Content -Raw "src/CRM.Api/Program.cs"
foreach ($route in @('/health', '/health/live', '/health/ready', '/api/crm/readiness', '/api/crm/domain-catalog', '/api/crm/contracts', '/api/crm/integration-boundaries', '/api/crm/foundation/leads/preview', '/api/crm/foundation/accounts/preview', '/api/crm/foundation/contacts/preview', '/api/crm/foundation/leads/read-model-preview', '/api/crm/foundation/accounts/read-model-preview', '/api/crm/foundation/contacts/read-model-preview', '/api/crm/foundation/read-model-status', '/api/crm/foundation/portal-integration/status', '/api/crm/foundation/portal-integration/contracts', '/api/crm/foundation/portal-integration/required-capabilities', '/api/crm/foundation/financial-integration/status', '/api/crm/foundation/financial-integration/contracts', '/api/crm/foundation/financial-integration/required-capabilities', '/api/crm/foundation/financial-integration/events', '/api/crm/foundation/reporting/status', '/api/crm/foundation/reporting/kpis', '/api/crm/foundation/reporting/dashboards', '/api/crm/foundation/reporting/analytics-read-models', '/api/crm/foundation/sprint-1/closure-status')) {
    if ($apiProgram -notlike "*$route*") {
        $failures += "Missing documented route $route"
    }
}

if ($apiProgram -match "MapPut|MapPatch|MapDelete|CreateLead|CreateCustomer|CreateOpportunity") {
    $failures += "Premature CRM mutating endpoint found."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/portal-integration") {
    $failures += "Portal integration endpoints must remain GET-only foundation endpoints."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/financial-integration") {
    $failures += "Financial integration endpoints must remain GET-only foundation endpoints."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/reporting") {
    $failures += "Reporting endpoints must remain GET-only foundation endpoints."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-1") {
    $failures += "Closure endpoint must remain GET-only foundation endpoint."
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($apiProgram -like "*$productiveRoute*") {
        $failures += "Productive CRM endpoint found: $productiveRoute"
    }
}

$foundationText = ""
Get-ChildItem -Path "src/CRM.Application/Foundation" -Filter "*.cs" -File | ForEach-Object { $foundationText += "`n" + (Get-Content -Raw $_.FullName) }
if ($apiProgram -notlike "*Preview only, not persisted*" -and $foundationText -notlike "*Preview only, not persisted*") {
    $failures += "Foundation preview warning is missing."
}

$readModelText = Get-Content -Raw "src/CRM.Application/ReadModels/ReadModelPreviewServices.cs"
if ($apiProgram -notlike "*Read model preview only, not persisted*" -and $readModelText -notlike "*Read model preview only, not persisted*") {
    $failures += "Read model preview warning is missing."
}

$portText = Get-Content -Raw "src/CRM.Application/Persistence/CrmPersistencePorts.cs"
foreach ($port in @("ILeadReadModelStore", "IAccountReadModelStore", "IContactReadModelStore", "ICrmUnitOfWork", "ICrmClock", "FuturePersistencePort")) {
    if ($portText -notlike "*$port*") {
        $failures += "Missing persistence port marker: $port"
    }
}

$sourceText = ""
foreach ($root in @("src")) {
    if (Test-Path $root) {
        Get-ChildItem -Path $root -Recurse -File -ErrorAction SilentlyContinue |
            Where-Object { $_.FullName -notmatch "\\(bin|obj)\\" } |
            ForEach-Object { $sourceText += "`n" + (Get-Content -Raw $_.FullName) }
    }
}

if ($sourceText -match "DbContext|DbSet<|MigrationBuilder|UseSqlServer") {
    $failures += "Productive persistence, migration or DbContext reference found."
}

foreach ($marker in @("Portal integration contracts only; no runtime calls configured", "FuturePortalAdapter", "NonProductionPlaceholder")) {
    if ($sourceText -notlike "*$marker*") {
        $failures += "Missing Portal integration guardrail marker: $marker"
    }
}

foreach ($marker in @("Financial integration contracts only; no runtime calls configured", "FutureFinancialAdapter", "NoSharedDatabase", "CustomerConvertedForFinancialIntegration", "CollectionsRiskRaisedFinancialSignal")) {
    if ($sourceText -notlike "*$marker*") {
        $failures += "Missing Financial integration guardrail marker: $marker"
    }
}

foreach ($marker in @("Reporting contracts only; no analytics runtime configured", "FutureReportingAdapter", "LeadConversionRate", "CRM Executive Overview", "FoundationMock")) {
    if ($sourceText -notlike "*$marker*") {
        $failures += "Missing Reporting guardrail marker: $marker"
    }
}

foreach ($marker in @("Foundation closure only; no productive activation", "FoundationClosed", "NotReady", "Sprint2Planning")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md")) -notlike "*$marker*") {
        $failures += "Missing closure guardrail marker: $marker"
    }
}

if ($sourceText -match "HttpClient|PortalCorporativoUrl|PortalBaseUrl|portalBaseUrl") {
    $failures += "Runtime Portal adapter, URL or HTTP client found before integration approval."
}

if ($sourceText -match "FinancieroDb|UseSqlServer|ConnectionString|FinancieroUrl|financialBaseUrl") {
    $failures += "Runtime Financial adapter, connection string, shared DB or URL found before integration approval."
}

if ($sourceText -cmatch "Microsoft\.PowerBI|embedToken|workspaceId|reportId|datasetId|embedUrl|powerbi\.com|ConnectionString") {
    $failures += "Runtime BI adapter, token, ID, URL or connection string found before analytics approval."
}

if (Test-Path "database") {
    $failures += "Database directory or migration baseline must not exist in foundation sprints."
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM foundation verification passed."
exit 0
