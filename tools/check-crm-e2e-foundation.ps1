param(
    [string] $BaseUrl = "http://localhost:8093"
)

$ErrorActionPreference = "Continue"
$failures = @()

function Pass($Message) { Write-Output "PASS $Message" }
function Fail($Message) { $script:failures += $Message; Write-Output "FAIL $Message" }

$positivePaths = @(
    "/health",
    "/health/live",
    "/health/ready",
    "/api/crm/readiness",
    "/api/crm/foundation/sprint-3/productization-review",
    "/api/crm/foundation/sprint-4/runtime-readiness",
    "/api/crm/foundation/sprint-4/common-db-runtime-probe",
    "/api/crm/foundation/sprint-4/portal-auth-runtime-probe",
    "/api/crm/foundation/sprint-4/productive-routes-locked-stub",
    "/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness",
    "/api/crm/foundation/sprint-4/gate-decision",
    "/api/crm/foundation/sprint-5/runtime-probe-activation-plan",
    "/api/crm/foundation/sprint-5/secret-provider-runtime-contract",
    "/api/crm/foundation/sprint-5/common-db-probe-optional-activation",
    "/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation",
    "/api/crm/foundation/sprint-5/locked-productive-route-stub-trial",
    "/api/crm/foundation/sprint-5/gate-decision",
    "/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package",
    "/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation",
    "/api/crm/foundation/sprint-6/common-db-connectivity-dry-run",
    "/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run",
    "/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial",
    "/api/crm/foundation/sprint-6/gate-decision",
    "/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval",
    "/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe",
    "/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe",
    "/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe",
    "/api/crm/foundation/sprint-7/locked-productive-route-runtime-registration",
    "/api/crm/foundation/sprint-7/gate-decision",
    "/api/crm/foundation/sprint-8/secret-provider-approval-decision",
    "/api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read",
    "/api/crm/foundation/sprint-8/common-db-controlled-real-connectivity",
    "/api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation",
    "/api/crm/foundation/sprint-8/locked-route-authorization-policy-integration",
    "/api/crm/foundation/sprint-8/gate-decision",
    "/api/crm/foundation/sprint-9/controlled-runtime-activation-decision",
    "/api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial",
    "/api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial"
)

foreach ($path in $positivePaths) {
    try {
        $response = Invoke-WebRequest -UseBasicParsing "$BaseUrl$path" -TimeoutSec 5
        if ($response.StatusCode -ge 200 -and $response.StatusCode -lt 300) {
            Pass "$path $($response.StatusCode)"
        } else {
            Fail "$path returned $($response.StatusCode)"
        }
    } catch {
        Fail "$path unavailable: $($_.Exception.Message)"
    }
}

$negativePaths = @(
    "/api/crm/leads",
    "/api/crm/accounts",
    "/api/crm/contacts"
)

foreach ($path in $negativePaths) {
    try {
        $response = Invoke-WebRequest -UseBasicParsing "$BaseUrl$path" -TimeoutSec 5
        if ($response.StatusCode -ge 200 -and $response.StatusCode -lt 300) {
            Fail "$path is active and returned $($response.StatusCode)"
        } elseif ($response.StatusCode -eq 404 -or $response.StatusCode -eq 405) {
            Pass "$path not active: $($response.StatusCode)"
        } else {
            Fail "$path returned unexpected status $($response.StatusCode)"
        }
    } catch {
        $statusCode = $_.Exception.Response.StatusCode.value__
        if ($statusCode -eq 404 -or $statusCode -eq 405) {
            Pass "$path not active: $statusCode"
        } else {
            Fail "$path negative check failed: $($_.Exception.Message)"
        }
    }
}

if ($failures.Count -gt 0) { exit 1 }
Pass "CRM non-production E2E foundation pilot checks passed."
exit 0
