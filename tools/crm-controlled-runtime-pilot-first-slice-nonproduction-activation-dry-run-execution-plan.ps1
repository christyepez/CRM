$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution plan passed. Dry-run is prepared only and not executed."
