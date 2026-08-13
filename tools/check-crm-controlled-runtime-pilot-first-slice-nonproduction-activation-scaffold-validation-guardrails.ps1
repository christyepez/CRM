$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p22-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-report.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-foundation-endpoint.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-disabled-service.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-safe-configuration.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-test-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-compose.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-security-checklist.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P22 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$validationOnly = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P22ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldValidationExists: true",
    "CrmSprint10P21NonProductionActivationScaffoldReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationScaffoldValidationAttempted: true",
    "FirstSliceNonProductionActivationScaffoldValidationPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationEvidenceMatrixPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationFoundationEndpointPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationDisabledServicePrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationFeatureFlagsPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationSafeConfigurationPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationTestEvidencePrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationComposePrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationSecurityChecklistPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationRunbookPrepared: true",
    "FirstSliceNonProductionActivationScaffoldValidationSecurityDecisionPrepared: true",
    "NonProductionActivationScaffoldValidatedDisabledOnly: true",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldValidationReadiness: NonProductionActivationScaffoldValidatedDisabledOnly",
    "NextGate: CrmSprint10P23ControlledRuntimePilotFirstSliceNonProductionActivationFinalApprovalGate"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P22 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"))) {
    if ($validationOnly -like "*$pattern*") {
        throw "Forbidden P22 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation scaffold validation guardrails passed."
