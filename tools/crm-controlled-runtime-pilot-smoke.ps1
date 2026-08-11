param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot scaffold smoke starting."

& .\tools\verify-crm-controlled-runtime-pilot-scaffold.ps1

$text = Get-Content -Raw "docs/roadmap/crm-sprint-10-p5-controlled-runtime-pilot-scaffold.md"

foreach ($marker in @(
    "ControlledRuntimePilotScaffoldPrepared: true.",
    "ControlledRuntimePilotScaffoldReadiness: ScaffoldPreparedDisabledOnly.",
    "RuntimePortalCouplingEnabled: false.",
    "RuntimePortalCallsEnabled: false.",
    "ProductionActivationDecision: NoGo.",
    "CrmProductionReady: false."
)) {
    if ($text -notmatch [regex]::Escape($marker)) {
        Write-Error "Missing smoke marker: $marker"
        exit 1
    }
}

Write-Output "PASS CRM controlled runtime pilot scaffold smoke passed. Runtime remains disabled."
exit 0
