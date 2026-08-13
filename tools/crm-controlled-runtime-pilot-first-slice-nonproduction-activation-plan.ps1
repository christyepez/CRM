$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-validation.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-plan.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation plan passed. Activation remains unexecuted and ProductionActivationDecision remains NoGo."
