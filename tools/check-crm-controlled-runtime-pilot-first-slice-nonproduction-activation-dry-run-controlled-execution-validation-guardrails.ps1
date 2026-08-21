$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p31-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-report.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-evidence-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-no-external-call.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-no-portal-call.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-no-activation.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-feature-flags.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-compose.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-common-db.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-portal-duplication.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-security-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-p32-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P31 file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$p31Only = ($paths | Where-Object { $_ -ne "codex/TASKS.md" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P31ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecutionValidationExists: true",
    "CrmSprint10P30DryRunControlledExecutionReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "NonProductionActivationDryRunControlledExecutionValidationOnly: true",
    "DryRunControlledExecutionValidated: true",
    "DryRunControlledExecutionExecuted: true",
    "DryRunExecuted: true",
    "DryRunExternalCallExecuted: false",
    "DryRunPortalCallExecuted: false",
    "DryRunActivationExecuted: false",
    "DryRunExecutionApprovalExecuted: false",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationControlledImplementationExecuted: false",
    "ConditionalGoFutureExecuted: false",
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
    "SecretsPresent: false",
    "EnvRealFileCommitted: false",
    "PrivateUrlsPresent: false",
    "RealDataPresent: false",
    "ControlledRuntimePilotFirstSliceNonProductionActivationDryRunControlledExecutionValidationReadiness: DryRunControlledExecutionValidatedLocalNoOpNoGoNow",
    "NextGate: CrmSprint10P32ControlledRuntimePilotFirstSliceNonProductionActivationReadinessReview"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P31 marker: $marker" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"), ("Password" + "="), ("User " + "ID="))) { if ($p31Only -like "*$pattern*") { throw "Forbidden P31 content detected: $pattern" } }
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P31 controlled dry-run execution validation guardrails passed."
