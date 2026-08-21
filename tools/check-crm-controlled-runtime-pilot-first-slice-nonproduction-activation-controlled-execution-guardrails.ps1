$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p40-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-pre-execution-baseline.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-approval-revalidation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-drift-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-execution-scope-validation.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-controlled-execution-log.md",
    "docs/testing/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-smoke-test-evidence.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-monitoring-evidence.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-security-runtime-evidence.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-rollback-evidence.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-controlled-execution-decision.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-risk-register.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-p41-entry-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p40-runbook.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P40 file: $path" }
}
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P40ControlledRuntimePilotFirstSliceNonProductionActivationControlledExecutionExists: true",
    "P40ControlledExecutionOnly: true",
    "P40ExecutionDecision: Successful",
    "Environment: NonProduction",
    "ProductionEnvironmentDetected: false",
    "ApprovalRevalidationPassed: true",
    "HumanApprovalRecorded: true",
    "HumanApprovalDecision: Go",
    "NonProductionExecutionDecision: Go",
    "P40Authorized: true",
    "ApprovalDriftDetected: false",
    "CriticalBlockers: 0",
    "ExecutionScopeValidated: true",
    "ExecutionScopeDriftDetected: false",
    "PreExecutionBaselineCaptured: true",
    "ControlledActivationExecuted: true",
    "NonProductionActivationControlledExecutionExecuted: true",
    "NonProductionActivationExecuted: true",
    "RuntimePortalCallsEnabled: false",
    "RuntimeCouplingEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "ExternalDependencyReached: false",
    "UnexpectedDestinationDetected: false",
    "DataChangesExecuted: false",
    "RoutesChanged: false",
    "PortalRoutesActivated: false",
    "PortalNavigationActivated: false",
    "PortalServicesInCompose: false",
    "PortalDuplicationDetected: false",
    "SmokeTestsPassed: true",
    "MonitoringPassed: true",
    "AbortCriteriaTriggered: false",
    "RollbackTriggered: false",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "P41EntryConditionsPrepared: true",
    "P41AuthorizedToStart: true"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P40 marker: $marker" }
}
foreach ($bad in @(
    "ProductionActivationDecision: Go",
    "CrmProductionReady: true",
    "ProductionActivated: true",
    "ProductionRuntimeEnabled: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true",
    "DataChangesExecuted: true",
    "RollbackTriggered: true"
)) {
    if ($docs -like "*$bad*") { throw "Forbidden P40 marker detected: $bad" }
}
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P40 content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}
Write-Host "PASS CRM P40 controlled NonProduction execution guardrails passed."
