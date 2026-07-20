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
    "docs/api/crm-api-contracts.md",
    "docs/api/crm-api-index.md",
    "docs/integration/crm-portal-boundary.md",
    "docs/integration/crm-financial-boundary.md",
    "docs/roadmap/crm-roadmap.md",
    "docs/roadmap/crm-sprint-plan.md",
    "docs/releases/crm-sprint-1-notes.md",
    "frontend/crm-web/package.json",
    "src/CRM.Domain/Entities/Lead.cs",
    "src/CRM.Domain/Entities/Opportunity.cs",
    "src/CRM.Domain/Entities/Activity.cs",
    "src/CRM.Domain/ValueObjects/ContactValueObjects.cs",
    "src/CRM.Domain/ValueObjects/BusinessValueObjects.cs",
    "src/CRM.Application/Contracts/CrmDomainCatalogService.cs"
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
foreach ($route in @('/health', '/health/live', '/health/ready', '/api/crm/readiness', '/api/crm/domain-catalog', '/api/crm/contracts', '/api/crm/integration-boundaries')) {
    if ($apiProgram -notlike "*$route*") {
        $failures += "Missing documented route $route"
    }
}

$crudPatterns = "MapPost|MapPut|MapDelete|CreateLead|CreateCustomer|CreateOpportunity"
if ($apiProgram -match $crudPatterns) {
    $failures += "Premature CRM mutating endpoint found."
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

if (Test-Path "database") {
    $failures += "Database directory or migration baseline must not exist in P2."
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM foundation verification passed."
exit 0
