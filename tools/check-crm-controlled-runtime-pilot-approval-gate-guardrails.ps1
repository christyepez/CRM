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
$changedPaths = git diff --name-only origin/main 2>$null
if (-not $changedPaths) { $changedPaths = git diff --name-only HEAD 2>$null }

$changedFiles = @()
foreach ($changedPath in $changedPaths) {
    if ((Test-Path $changedPath) -and ((Get-Item $changedPath) -is [System.IO.FileInfo])) {
        $changedFiles += Get-Item $changedPath
    }
}

foreach ($file in $changedFiles) {
    if ($file.FullName -notmatch "\\(docs|tools|codex)\\" -and $file.Name -ne "TASKS.md") {
        Fail "P9 must only modify docs, tools or codex: $($file.FullName)"
    }
}

foreach ($path in @(".env", ".env.local")) {
    if (Test-Path $path) { Fail "Unsafe environment file found: $path" }
}

foreach ($pattern in @("*.p12", "*.pfx", "*.key", "*.cer", "*.crt", "*.pem")) {
    $matches = Get-ChildItem -Recurse -File -Filter $pattern -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -notmatch $excluded }
    if ($matches) { Fail "Certificate/key file found: $pattern" }
}

$contentFiles = $changedFiles | Where-Object { $_.Name -notin @(
    "check-crm-controlled-runtime-pilot-approval-gate-guardrails.ps1"
) }

$changedText = ""
foreach ($file in $contentFiles) {
    $changedText += "`nFILE:$($file.FullName)`n"
    $changedText += Get-Content -Raw $file.FullName -ErrorAction SilentlyContinue
}

if ($changedText -match "BEGIN (RSA |EC |OPENSSH |)PRIVATE KEY|BEGIN CERTIFICATE") {
    Fail "Private key or certificate block found."
}

$unsafeSecretLines = $contentFiles | Select-String -Pattern "(client_secret|token|secret|Password|Pwd)\s*[:=]\s*['""][^'""]{8,}" -ErrorAction SilentlyContinue |
    Where-Object { $_.Line -notmatch "(false|true|placeholder|logical|metadata|redacted|example|no real|not configured|SecretProvider|Secret Provider|secret-provider|SecretsPresent|RealSecretProviderConfigured)" }
if ($unsafeSecretLines) { Fail "Possible real secret/token value found." }

if ($changedText -match "https://|http://") {
    Fail "URL found in P9 changed content."
}

if ($changedText -match "localStorage|sessionStorage") {
    Fail "Browser token storage marker found in P9 changed content."
}

$compose = ""
foreach ($composeFile in @("docker-compose.yml", "docker-compose.crm.yml")) {
    if (Test-Path $composeFile) { $compose += "`n" + (Get-Content -Raw $composeFile) }
}
if ($compose -match "portal.*image:|portal.*build:|PortalCorporativo|mcr\.microsoft\.com/mssql|1433:1433|container_name:\s*.*sql") {
    Fail "Portal service or CRM-owned SQL Server found in CRM compose."
}

if ($changedText -match "AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|SqlConnection\(|UseSqlServer\(|AddDbContext\(|MigrationBuilder") {
    Fail "Runtime Auth, DB or migration marker found in P9 changed content."
}

if ($changedText -notmatch "ApprovalGateOnly") {
    Fail "ApprovalGateOnly evidence marker is required in P9 changed content."
}

if ($changedText -match "ConditionalFutureGoExecuted:\s*true") {
    Fail "ConditionalFutureGo must not be executed in P9."
}

if ($failures.Count -gt 0) { exit 1 }

Pass "CRM controlled runtime pilot approval gate guardrails passed."
exit 0
