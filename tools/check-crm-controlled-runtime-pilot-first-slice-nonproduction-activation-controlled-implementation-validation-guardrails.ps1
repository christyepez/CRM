$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p25-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-report.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-foundation-endpoint.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-dry-run.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-disabled-service.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-test-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-compose.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-security-checklist.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P25 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p25Only = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P25ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationValidationExists: true",
    "CrmSprint10P24ControlledImplementationReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationControlledImplementationValidationAttempted: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationEvidenceMatrixPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationFoundationEndpointPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationDryRunPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationDisabledServicePrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationFeatureFlagsPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationSafeConfigurationPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationTestEvidencePrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationComposePrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationSecurityChecklistPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationRunbookPrepared: true",
    "FirstSliceNonProductionActivationControlledImplementationValidationSecurityDecisionPrepared: true",
    "NonProductionActivationControlledImplementationValidatedDisabledOnly: true",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationValidationReadiness: ControlledImplementationValidatedDisabledOnly",
    "NextGate: CrmSprint10P26ControlledRuntimePilotFirstSliceNonProductionActivationExplicitApproval"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P25 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"))) {
    if ($p25Only -like "*$pattern*") {
        throw "Forbidden P25 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation controlled implementation validation guardrails passed."
