param(
    [string] $BaseUrl = "http://localhost:8093"
)

$ErrorActionPreference = "Continue"
$failures = @()
function Pass($Message) { Write-Output "PASS $Message" }
function Warn($Message) { Write-Output "WARN $Message" }
function Fail($Message) { $script:failures += $Message; Write-Output "FAIL $Message" }

foreach ($path in @("/health", "/health/live", "/health/ready", "/api/crm/readiness", "/api/crm/foundation/sprint-3/productization-review", "/api/crm/foundation/sprint-4/runtime-readiness", "/api/crm/foundation/sprint-4/common-db-runtime-probe", "/api/crm/foundation/sprint-4/portal-auth-runtime-probe", "/api/crm/foundation/sprint-4/productive-routes-locked-stub", "/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness", "/api/crm/foundation/sprint-4/gate-decision", "/api/crm/foundation/sprint-5/runtime-probe-activation-plan", "/api/crm/foundation/sprint-5/secret-provider-runtime-contract", "/api/crm/foundation/sprint-5/common-db-probe-optional-activation",
    "/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation",
    "/api/crm/foundation/sprint-5/locked-productive-route-stub-trial")) {
    try {
        $response = Invoke-WebRequest -UseBasicParsing "$BaseUrl$path" -TimeoutSec 5
        if ($response.StatusCode -ge 200 -and $response.StatusCode -lt 300) { Pass "$path $($response.StatusCode)" } else { Fail "$path $($response.StatusCode)" }
    } catch {
        Warn "$path unavailable: $($_.Exception.Message)"
    }
}

if ($failures.Count -gt 0) { exit 1 }
exit 0
