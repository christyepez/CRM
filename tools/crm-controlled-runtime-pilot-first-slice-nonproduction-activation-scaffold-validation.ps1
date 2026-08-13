$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation scaffold validation passed. Activation remains disabled-only and unexecuted."
