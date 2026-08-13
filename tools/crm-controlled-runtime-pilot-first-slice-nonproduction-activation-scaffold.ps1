$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-activation-readiness-review.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation scaffold passed. Scaffold remains disabled-only and activation is not executed."
