$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-activation-approval-gate.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation implementation plan passed. Plan is prepared, activation is not executed and decision remains NoGo."
