$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$gate = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate.md") -Raw
$summary = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-evidence-summary.md") -Raw
$checklist = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-activation-approval-gate-compliance-checklist.md") -Raw
$p17 = Get-Content (Join-Path $root "docs/integration/crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run.md") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

foreach ($expected in @(
    "RuntimePortalCouplingEnabled: false",
    "RuntimePortalCallsEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "PortalAuthDuplicated: false"
)) {
    if ($gate -notlike "*$expected*") {
        throw "P18 gate missing expected marker: $expected"
    }
}

foreach ($expected in @(
    "FirstSliceActivationApprovalGateEvidenceSummaryPrepared: true",
    "CrmSprint10P17DryRunReviewed: true",
    "NonProductionActivationExecuted: false"
)) {
    if ($summary -notlike "*$expected*") {
        throw "P18 evidence summary missing expected marker: $expected"
    }
}

foreach ($expected in @("ActivationApprovalGateOnly: true", "ConditionalFutureGoExecuted: false", "SecretsPresent: false")) {
    if ($checklist -notlike "*$expected*") {
        throw "P18 checklist missing expected marker: $expected"
    }
}

if ($p17 -notlike "*NonProductionActivationDryRunOnly: true*") {
    throw "P17 dry run evidence was not found."
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice activation approval gate verified."
