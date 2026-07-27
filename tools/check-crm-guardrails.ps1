param()

$ErrorActionPreference = "Continue"
$failures = @()
function Pass($Message) { Write-Output "PASS $Message" }
function Fail($Message) { $script:failures += $Message; Write-Output "FAIL $Message" }

$program = Get-Content -Raw "src/CRM.Api/Program.cs"
$source = ""
foreach ($root in @("src", "frontend/src")) {
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
if ($source -match "AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|HttpClient|PortalBaseUrl|PortalCorporativoUrl") { Fail "Auth, token storage or Portal runtime marker found." }

$allowed = $source.Replace("DbContextConfigured", "").Replace("dbContextConfigured", "").Replace("DbContext Configured", "").Replace("DbContextRuntimeActive", "").Replace("dbContextRuntimeActive", "").Replace("DbContext Runtime Active", "").Replace("CrmDbContextPrototypeContract", "").Replace("CrmDbContextPrototype", "").Replace("InheritsRealDbContext", "").Replace("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", "").Replace("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", "").Replace("EfDbContextPrototypeDisabled", "").Replace("EF/DbContext prototype only; runtime disabled and no database configured", "")
if ($allowed -match "DbSet<|MigrationBuilder|UseSqlServer\(|UseNpgsql|AddDbContext|ConnectionString=") { Fail "DB runtime, migration or real configuration marker found." }

$compose = ""
foreach ($file in @("docker-compose.yml", "docker-compose.crm.yml")) { if (Test-Path $file) { $compose += "`n" + (Get-Content -Raw $file) } }
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|container_name:\s*.*sql") { Fail "CRM-owned SQL Server found in compose." }
if (Test-Path ".env") { Fail ".env found." }
if (Test-Path "database") { Fail "database folder found." }

if ($failures.Count -gt 0) { exit 1 }
Pass "CRM guardrails passed."
exit 0
