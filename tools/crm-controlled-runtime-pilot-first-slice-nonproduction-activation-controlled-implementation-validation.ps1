$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation controlled implementation validation passed. P24 remains validated disabled-only and activation is not executed."
