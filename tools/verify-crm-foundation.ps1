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
    "docs/security/crm-portal-security-boundary.md",
    "docs/roadmap/crm-roadmap.md",
    "docs/roadmap/crm-sprint-plan.md",
    "docs/releases/crm-sprint-1-notes.md",
    "frontend/crm-web/package.json",
    "src/CRM.Domain/Entities/Lead.cs",
    "src/CRM.Domain/Entities/Opportunity.cs",
    "src/CRM.Domain/Entities/Activity.cs",
    "src/CRM.Domain/ValueObjects/ContactValueObjects.cs",
    "src/CRM.Domain/ValueObjects/BusinessValueObjects.cs",
    "src/CRM.Domain/ValueObjects/FoundationValueObjects.cs",
    "src/CRM.Application/Contracts/CrmDomainCatalogService.cs",
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
foreach ($route in @('/health', '/health/live', '/health/ready', '/api/crm/readiness', '/api/crm/domain-catalog', '/api/crm/contracts', '/api/crm/integration-boundaries', '/api/crm/foundation/leads/preview', '/api/crm/foundation/accounts/preview', '/api/crm/foundation/contacts/preview', '/api/crm/foundation/leads/read-model-preview', '/api/crm/foundation/accounts/read-model-preview', '/api/crm/foundation/contacts/read-model-preview', '/api/crm/foundation/read-model-status', '/api/crm/foundation/portal-integration/status', '/api/crm/foundation/portal-integration/contracts', '/api/crm/foundation/portal-integration/required-capabilities')) {
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

if ($sourceText -match "HttpClient|PortalCorporativoUrl|PortalBaseUrl|portalBaseUrl") {
    $failures += "Runtime Portal adapter, URL or HTTP client found before integration approval."
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
