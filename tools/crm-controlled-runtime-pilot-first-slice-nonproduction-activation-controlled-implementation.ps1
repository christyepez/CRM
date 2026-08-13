$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation controlled implementation passed. Scaffold remains disabled-only and activation is not executed."
