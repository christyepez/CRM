$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-scaffold-validation.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-approval-gate.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation final approval gate passed. Decision remains NoGo now and ConditionalGoFuture remains unexecuted."
