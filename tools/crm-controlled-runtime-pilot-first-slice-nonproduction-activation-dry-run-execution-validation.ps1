$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-plan.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-validation.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run execution validation passed. Dry-run plan is validated only and not executed."
