$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p14-controlled-runtime-pilot-first-implementation-slice-scaffold.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-scaffold-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-scaffold-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-scaffold.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-scaffold-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-scaffold-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-scaffold-disabled-client.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-scaffold-health-smoke.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-scaffold-test-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-scaffold-rollback.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-scaffold-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-scaffold-security-decision.md",
    "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceScaffoldStatusService.cs",
    "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceScaffoldContracts.cs",
    "src/CRM.Application/Ports/Portal/IPortalRuntimeClient.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/PortalRuntimeOptions.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/PortalRuntimeFeatureFlags.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledPortalRuntimeClient.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/PortalRuntimeHealthCheck.cs",
    "tools/check-crm-controlled-runtime-pilot-first-slice-scaffold-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-scaffold.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-scaffold.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P14 file: $path"
    }
}

$scanPaths = $paths + @(
    "src/CRM.Api/Program.cs",
    "tests/CRM.UnitTests/CrmControlledRuntimePilotFirstSliceScaffoldStatusServiceTests.cs",
    "tests/CRM.UnitTests/DisabledPortalRuntimeClientTests.cs",
    "tests/CRM.ArchitectureTests/Sprint10ControlledRuntimePilotFirstSliceScaffoldArchitectureTests.cs",
    "docker-compose.yml"
)
$text = foreach ($target in $scanPaths) {
    $full = Join-Path $root $target
    if (Test-Path $full) {
        Get-Content $full -Raw
    }
}
$joined = ($text -join "`n")

$required = @(
    "FirstImplementationSliceScaffoldOnly: true",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstImplementationSliceScaffoldReadiness: FirstSliceScaffoldPreparedDisabledOnly"
)

foreach ($marker in $required) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P14 guardrail marker: $marker"
    }
}

$forbidden = @(
    ("client" + "_secret="),
    ("BEGIN " + "CERTIFICATE"),
    ("PRIVATE " + "KEY"),
    ("local" + "Storage"),
    ("session" + "Storage")
)

foreach ($pattern in $forbidden) {
    if ($joined -like "*$pattern*") {
        throw "Forbidden P14 content detected: $pattern"
    }
}

Write-Host "PASS CRM controlled runtime pilot first slice scaffold guardrails passed."
