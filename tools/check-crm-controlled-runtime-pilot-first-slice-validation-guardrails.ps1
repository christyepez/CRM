$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p15-controlled-runtime-pilot-first-slice-scaffold-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-validation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-validation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-report.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-foundation-endpoint.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-disabled-client.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-health-smoke.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-test-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-compose.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-validation-security-checklist.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-validation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-validation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-validation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-validation.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-validation.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P15 file: $path"
    }
}

$scanPaths = $paths + @(
    "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceScaffoldStatusService.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledPortalRuntimeClient.cs",
    "src/CRM.Api/Program.cs",
    "docker-compose.yml"
)
$joined = ($scanPaths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "FirstSliceScaffoldValidatedDisabledOnly: true",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstSliceScaffoldValidationReadiness: FirstSliceScaffoldValidatedDisabledOnly"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P15 validation marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"))) {
    if ($joined -like "*$pattern*") {
        throw "Forbidden P15 content detected: $pattern"
    }
}

Write-Host "PASS CRM controlled runtime pilot first slice validation guardrails passed."
