$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p21-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-disabled-services.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-foundation-endpoint.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-test-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-rollback.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-security-decision.md",
    "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldContracts.cs",
    "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/NonProductionActivationOptions.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/NonProductionActivationFeatureFlags.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledNonProductionActivationService.cs",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P21 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$scaffoldOnly = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "NonProductionActivationScaffoldOnly: true",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimePortalCouplingEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldReadiness: NonProductionActivationScaffoldPreparedDisabledOnly"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P21 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"))) {
    if ($scaffoldOnly -like "*$pattern*") {
        throw "Forbidden P21 content detected: $pattern"
    }
}

$program = Get-Content (Join-Path $root "src/CRM.Api/Program.cs") -Raw
if ($program -notlike "*/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold*") {
    throw "P21 foundation endpoint is not registered."
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation scaffold guardrails passed."
