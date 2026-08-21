$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p39a-controlled-runtime-pilot-first-slice-nonproduction-activation-human-approval-reapproval-gate.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39a-human-approval-record.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39a-approval-revalidation-matrix.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39a-drift-validation.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39a-security-decision.md",
    "docs/architecture/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39a-architecture-decision.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39a-p40-entry-conditions.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-p39a-risk-register.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-human-approval-reapproval-gate-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-human-approval-reapproval-gate.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-human-approval-reapproval-gate.ps1",
    "codex/TASKS.md"
)
foreach ($path in $paths) { if (-not (Test-Path (Join-Path $root $path))) { throw "Missing expected P39A file: $path" } }
$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$docs = ($paths | Where-Object { $_ -like "docs/*" } | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
foreach ($marker in @(
    "P39AHumanApprovalReApprovalGateOnly: true",
    "P39HistoricalStatePreserved: true",
    "TechnicalApprovalPassed: true",
    "HumanApprovalRequired: true",
    "HumanApprovalRecorded: false",
    "HumanApproverReference: not-recorded",
    "HumanApprovalDecision: NoGo",
    "ApprovalDriftDetected: false",
    "CriticalBlockers: HumanApprovalMissing",
    "ExplicitApprovalExecuted: false",
    "NonProductionActivationExecutionApprovalExecuted: false",
    "NonProductionActivationReadinessApprovedForExecution: false",
    "NonProductionActivationFinalGoApproved: false",
    "NonProductionActivationFinalGoNoGoDecision: NoGo",
    "NonProductionExecutionDecision: NoGo",
    "NonProductionActivationControlledExecutionExecuted: false",
    "NonProductionActivationExecuted: false",
    "DryRunActivationExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "RuntimeCouplingEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "PortalRoutesActivated: false",
    "PortalNavigationActivated: false",
    "PortalServicesInCompose: false",
    "PortalDuplicationDetected: false",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "ProductionExecutionApproved: false",
    "SecurityApprovalPassed: true",
    "ArchitectureApprovalPassed: true",
    "DevOpsValidationPassed: true",
    "QaValidationPassed: true",
    "MonitoringValidationPassed: true",
    "RollbackValidationPassed: true",
    "P40EntryConditionsPrepared: true",
    "P40Authorized: false",
    "SecretsPresent: false",
    "EnvRealFileCommitted: false",
    "PrivateUrlsPresent: false",
    "RealDataPresent: false"
)) { if ($joined -notlike "*$marker*") { throw "Missing required P39A marker: $marker" } }
foreach ($bad in @(
    "ProductionActivationDecision: Go",
    "CrmProductionReady: true",
    "ProductionExecutionApproved: true",
    "NonProductionActivationExecuted: true",
    "NonProductionActivationControlledExecutionExecuted: true",
    "RuntimePortalCallsEnabled: true",
    "CommonDbRuntimeEnabled: true",
    "PortalRoutesActivated: true",
    "PortalNavigationActivated: true"
)) { if ($docs -like "*$bad*") { throw "Forbidden P39A execution/production marker detected: $bad" } }
foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Password" + "="), ("User " + "ID="), ("Authorization" + ":"))) { if ($docs -like "*$pattern*") { throw "Forbidden P39A content detected: $pattern" } }
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") { throw "CRM compose appears to define SQL Server or Portal services." }
Write-Host "PASS CRM P39A human approval re-approval gate guardrails passed."
