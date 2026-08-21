$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p41-controlled-runtime-pilot-first-slice-nonproduction-post-execution-validation-and-stabilization.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-post-execution-baseline.md",
    "docs/testing/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-runtime-health-evidence.md",
    "docs/testing/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-smoke-regression-evidence.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-monitoring-evidence.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-log-review.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-baseline-comparison.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-configuration-drift-validation.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-security-post-execution-decision.md",
    "docs/architecture/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-runtime-boundary-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-stability-decision.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-issue-register.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-rollback-reassessment.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-risk-register.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p41-p42-entry-conditions.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-post-execution-validation-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-post-execution-validation.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-post-execution-validation.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P41 file: $path" }
}
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P41ControlledRuntimePilotFirstSliceNonProductionPostExecutionValidationExists: true",
    "P41PostExecutionValidationOnly: true",
    "P41StabilityDecision: Healthy",
    "Environment: NonProduction",
    "PostExecutionStateDriftDetected: false",
    "RuntimePresenceValidated: true",
    "ContainerStatus: running",
    "ContainerRestartCount: 0",
    "HealthPassed: true",
    "LivenessPassed: true",
    "ReadinessPassed: true",
    "SmokeTestsPassed: true",
    "RegressionTestsPassed: true",
    "SecurityValidationPassed: true",
    "MonitoringAcceptable: true",
    "ConfigurationDriftDetected: false",
    "UnexpectedDataChangesDetected: false",
    "UnexpectedDestinationDetected: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimeCouplingEnabled: false",
    "PortalRoutesActivated: false",
    "PortalNavigationActivated: false",
    "PortalServicesInCompose: false",
    "CommonDbRuntimeEnabled: false",
    "PortalDuplicationDetected: false",
    "CriticalIssues: 0",
    "HighBlockingIssues: 0",
    "RollbackReassessment: RollbackNotRequired",
    "RollbackTriggered: false",
    "RollbackResult: NotRequired",
    "NonProductionActivationExecuted: true",
    "NonProductionRuntimeStable: true",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "P42EntryConditionsPrepared: true",
    "P42AuthorizedToStart: true"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P41 marker: $marker" }
}
foreach ($bad in @(
    "ProductionActivationDecision: Go",
    "CrmProductionReady: true",
    "ProductionActivated: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true",
    "UnexpectedDestinationDetected: true",
    "UnexpectedDataChangesDetected: true"
)) {
    if ($docs -like "*$bad*") { throw "Forbidden P41 marker detected: $bad" }
}
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P41 content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}
Write-Host "PASS CRM P41 post-execution validation guardrails passed."
