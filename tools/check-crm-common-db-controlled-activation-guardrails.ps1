param()

$ErrorActionPreference = "Continue"
$failures = @()

function Fail($Message) {
    $script:failures += $Message
    Write-Output "FAIL $Message"
}

function Pass($Message) {
    Write-Output "PASS $Message"
}

$excluded = "\\(\.git|bin|obj|node_modules|dist|coverage|TestResults)\\"
$files = Get-ChildItem -Recurse -File -ErrorAction SilentlyContinue |
    Where-Object { $_.FullName -notmatch $excluded }

$changedPaths = git diff --name-only origin/main 2>$null
if (-not $changedPaths) {
    $changedPaths = git diff --name-only HEAD 2>$null
}
$changedFiles = @()
foreach ($changedPath in $changedPaths) {
    if ((Test-Path $changedPath) -and ((Get-Item $changedPath) -is [System.IO.FileInfo])) {
        $changedFiles += Get-Item $changedPath
    }
}

foreach ($path in @(
    ".env",
    ".env.local",
    "database",
    "migrations"
)) {
    if (Test-Path $path) { Fail "Unsafe runtime artifact found: $path" }
}

$forbiddenExtensions = @("*.p12", "*.pfx", "*.key", "*.cer", "*.crt", "*.pem")
foreach ($pattern in $forbiddenExtensions) {
    $matches = Get-ChildItem -Recurse -File -Filter $pattern -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -notmatch $excluded }
    if ($matches) { Fail "Certificate/key file found: $pattern" }
}

$runtimeFiles = $changedFiles | Where-Object {
    $_.FullName -match "\\(src|backend|frontend|tools)\\" -or
    $_.Name -like "docker-compose*.yml" -or
    $_.Name -eq ".env.example"
}

$runtimeText = ""
foreach ($file in $runtimeFiles) {
    $runtimeText += "`nFILE:$($file.FullName)`n"
    $runtimeText += Get-Content -Raw $file.FullName -ErrorAction SilentlyContinue
}

$safeRuntimeText = $runtimeText.
    Replace("ConnectionStringReturnedToApi", "").
    Replace("ConnectionStringLogged", "").
    Replace("ConnectionStringPersisted", "").
    Replace("ConnectionStringCached", "").
    Replace("RealConnectionStringsPresent", "").
    Replace("PortalDatabaseDirectAccessEnabled", "").
    Replace("SharedPortalTablesAccessEnabled", "").
    Replace("CrossDomainMigrationsPresent", "")

if ($safeRuntimeText -match "Server=.*;.*(Database|Initial Catalog)=.*;.*(User ID|User Id|Password|Pwd)=") {
    Fail "Real-looking connection string found in runtime files."
}

if ($safeRuntimeText -match "SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|MigrationBuilder|EnsureCreated\(|Migrate\(") {
    Fail "DB runtime, EF runtime, schema creation or migration marker found."
}

if ($safeRuntimeText -match "Portal.*(DbContext|SqlConnection|UseSqlServer|MigrationBuilder|SELECT|INSERT|UPDATE|DELETE)") {
    Fail "Possible direct Portal database access marker found in runtime files."
}

if ($safeRuntimeText -match "AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication") {
    Fail "Productive auth runtime marker found."
}

if ($safeRuntimeText -match "localStorage|sessionStorage") {
    Fail "Browser token storage marker found."
}

$compose = ""
foreach ($composeFile in @("docker-compose.yml", "docker-compose.crm.yml")) {
    if (Test-Path $composeFile) { $compose += "`n" + (Get-Content -Raw $composeFile) }
}
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|container_name:\s*.*sql") {
    Fail "CRM-owned SQL Server found in compose."
}

$changedText = ""
foreach ($file in $changedFiles) {
    $changedText += "`nFILE:$($file.FullName)`n"
    $changedText += Get-Content -Raw $file.FullName -ErrorAction SilentlyContinue
}

if ($changedText -match "BEGIN (RSA |EC |OPENSSH |)PRIVATE KEY|BEGIN CERTIFICATE") {
    Fail "Private key or certificate block found."
}

$unsafeSecretLines = $changedFiles | Select-String -Pattern "(client_secret|token|secret|Password|Pwd)\s*[:=]\s*['""][^'""]{8,}" -ErrorAction SilentlyContinue |
    Where-Object { $_.Line -notmatch "(false|true|placeholder|logical|metadata|redacted|example|no real|not configured|SecretProvider|Secret Provider|secret-provider|SecretsPresent|RealSecretProviderConfigured)" }
if ($unsafeSecretLines) { Fail "Possible real secret/token value found." }

if ($failures.Count -gt 0) { exit 1 }

Pass "CRM Common DB controlled activation guardrails passed."
exit 0
