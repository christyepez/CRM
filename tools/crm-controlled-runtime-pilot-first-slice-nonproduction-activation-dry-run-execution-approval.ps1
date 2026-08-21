$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution approval passed. Approval is prepared only and not executed."
