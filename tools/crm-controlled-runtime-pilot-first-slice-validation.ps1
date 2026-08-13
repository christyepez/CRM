$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-scaffold.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-validation-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-validation.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice validation passed. Scaffold remains disabled-only and ProductionActivationDecision remains NoGo."
