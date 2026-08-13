$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$paths = @(
    "docs/roadmap/crm-sprint-10-p23-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-go-no-go.md",
    "docs/roadmap/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-risk-register.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-evidence-summary.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-approval-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-decision-matrix.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-compliance-checklist.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-blockers.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-residual-risks.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-raci.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-communication-plan.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-audit-evidence.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-rollback.md",
    "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-p24-conditions.md",
    "docs/operations/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-runbook.md",
    "docs/security/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-security-decision.md",
    "tools/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-guardrails.ps1",
    "tools/verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate.ps1",
    "tools/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate.ps1",
    "codex/TASKS.md"
)

foreach ($path in $paths) {
    if (-not (Test-Path (Join-Path $root $path))) {
        throw "Missing expected P23 file: $path"
    }
}

$joined = ($paths | ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"
$gateOnly = ($paths |
    Where-Object { $_ -ne "codex/TASKS.md" } |
    ForEach-Object { Get-Content (Join-Path $root $_) -Raw }) -join "`n"

foreach ($marker in @(
    "CrmSprint10P23ControlledRuntimePilotFirstSliceNonProductionActivationFinalApprovalGateExists: true",
    "CrmSprint10P22ScaffoldValidationReviewed: true",
    "PortalSprint21ContractAlignmentReviewed: true",
    "ProductizationStatus: PreparationOnly",
    "ProductionActivationDecision: NoGo",
    "CrmProductionReady: false",
    "FirstSliceNonProductionActivationFinalApprovalGateAttempted: true",
    "FirstSliceNonProductionActivationFinalApprovalGatePrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateEvidenceSummaryPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateApprovalMatrixPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateDecisionMatrixPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateComplianceChecklistPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateBlockersPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateResidualRisksPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateRaciPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateCommunicationPlanPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateAuditEvidencePrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateRollbackPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateP24ConditionsPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateRunbookPrepared: true",
    "FirstSliceNonProductionActivationFinalApprovalGateSecurityDecisionPrepared: true",
    "NonProductionActivationFinalApprovalGateOnly: true",
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
    "ControlledRuntimePilotFirstSliceNonProductionActivationFinalApprovalGateReadiness: FinalApprovalGatePreparedConditionalGoFutureNoGoNow",
    "NextGate: CrmSprint10P24ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementation"
)) {
    if ($joined -notlike "*$marker*") {
        throw "Missing required P23 marker: $marker"
    }
}

foreach ($pattern in @(("client" + "_secret="), ("BEGIN " + "CERTIFICATE"), ("PRIVATE " + "KEY"), ("local" + "Storage"), ("session" + "Storage"), ("http" + "://"), ("https" + "://"), ("Http" + "Client"), ("Use" + "SqlServer"))) {
    if ($gateOnly -like "*$pattern*") {
        throw "Forbidden P23 content detected: $pattern"
    }
}

$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw
if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation final approval gate guardrails passed."
