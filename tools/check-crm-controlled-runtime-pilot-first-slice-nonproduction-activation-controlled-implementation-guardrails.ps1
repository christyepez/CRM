$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p24-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-boundaries.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-disabled-services.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-foundation-endpoint.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-dry-run.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-test-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-rollback.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-security-decision.md",
    "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationContracts.cs",
    "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/ControlledNonProductionActivationOptions.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/ControlledNonProductionActivationFeatureFlags.cs",
    "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledControlledNonProductionActivationService.cs",
    "tests/CRM.UnitTests/CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusServiceTests.cs",
    "tests/CRM.UnitTests/DisabledControlledNonProductionActivationServiceTests.cs",
    "tests/CRM.ArchitectureTests/Sprint10ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationArchitectureTests.cs",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P24 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p24Only = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P24ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationExists: true",
    "CrmSprint10P23FinalApprovalGateReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationControlledImplementationAttempted: true",
    "FirstSliceNonProductionActivationControlledImplementationPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationBoundariesPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationFeatureFlagsPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationSafeConfigurationPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationDisabledServicesPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationFoundationEndpointPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationDryRunPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationTestEvidencePrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationRollbackPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationRunbookPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationSecurityDecisionPrepared: true",
    "NonProductionActivationControlledImplementationPrepared: true",
    "NonProductionActivationControlledImplementationExecuted: false",
    "ConditionalGoFutureDefined: true",
    "ConditionalGoFutureExecuted: false",
    "NonProductionActivationExecuted: false",
    "ConditionalFutureGoDefined: true",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "ProductivePortalNavigationEnabled: false",
    "ProductivePortalGatewayRoutesEnabled: false",
    "PortalServicesInCrmCompose: false",
    "CommonDbRuntimeEnabled: false",
    "PortalAuthDuplicated: false",
    "PortalMenuDuplicated: false",
    "PortalPermissionsDuplicated: false",
    "PortalAuditDuplicated: false",
    "PortalNotificationDuplicated: false",
    "PortalConfigurationDuplicated: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationReadiness: ControlledImplementationPreparedDisabledOnly",
    "NextGate: CrmSprint10P25ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationValidation"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P24 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"))) {
    if ($p24Only -like "*$pattern*") {
        throw "Forbidden P24 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation controlled implementation guardrails passed."
