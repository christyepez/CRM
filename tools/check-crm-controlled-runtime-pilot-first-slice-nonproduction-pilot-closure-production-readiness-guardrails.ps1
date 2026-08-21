$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p42-controlled-runtime-pilot-first-slice-nonproduction-pilot-closure-and-production-readiness-assessment.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-pilot-closure-decision.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-traceability-matrix.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-lessons-learned.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-production-prerequisites-matrix.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-production-readiness-assessment-decision.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-security-production-readiness-decision.md",
    "docs/architecture/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-architecture-production-readiness-decision.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-devops-production-readiness-decision.md",
    "docs/testing/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-qa-production-readiness-decision.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-observability-readiness.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-operations-support-readiness.md",
    "docs/testing/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-performance-readiness.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-backup-recovery-readiness.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-portal-common-db-readiness.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-residual-risk-register.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-p42-p43-entry-conditions.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-pilot-closure-production-readiness-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-pilot-closure-production-readiness.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-pilot-closure-production-readiness.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P42 file: $path" }
}
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "CrmSprint10P42ControlledRuntimePilotFirstSliceNonProductionPilotClosureAndProductionReadinessAssessmentExists: true",
    "P42PilotClosureAndReadinessAssessmentOnly: true",
    "P41StabilityRevalidationPassed: true",
    "P41StabilityDecision: Healthy",
    "NonProductionPilotClosureDecision: ClosedSuccessfully",
    "CrmProductionReadyAssessmentCompleted: true",
    "CrmProductionReadinessAssessment: ReadyWithConditions",
    "ProductionReadinessAssessment: ReadyWithConditions",
    "ProductionActivationDecision: NoGo",
    "ProductionApprovalExecuted: false",
    "CrmProductionReady: false",
    "ProductionActivated: false",
    "NonProductionActivationExecuted: true",
    "NonProductionRuntimeStable: true",
    "RuntimePortalCallsEnabled: false",
    "RuntimeCouplingEnabled: false",
    "PortalRoutesActivated: false",
    "PortalNavigationActivated: false",
    "PortalServicesInCompose: false",
    "CommonDbRuntimeEnabled: false",
    "PortalDuplicationDetected: false",
    "CriticalProductionBlockers: 0",
    "HighBlockingRisks: 0",
    "ResidualRisksRegistered: true",
    "P43EntryConditionsPrepared: true",
    "P43RecommendedMode: RemediateAndPrepareApprovalGate",
    "ProductionActivationAllowedInP43: false"
)) {
    if ($joined -notlike "*$marker*") { throw "Missing required P42 marker: $marker" }
}
foreach ($bad in @(
    "ProductionActivationDecision: Go",
    "ProductionApprovalExecuted: true",
    "ProductionActivated: true",
    "CrmProductionReady: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true"
)) {
    if ($docs -like "*$bad*") { throw "Forbidden P42 marker detected: $bad" }
}
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) {
    if ($docs -like "*$pattern*") { throw "Forbidden P42 content detected: $pattern" }
}
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}
Write-Host "PASS CRM P42 pilot closure and production readiness guardrails passed."
