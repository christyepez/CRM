$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation-validation.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-approval.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation explicit approval passed. Approval is prepared only and not executed."
