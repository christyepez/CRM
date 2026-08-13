$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-plan.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry run passed. Activation remains unexecuted and all future flags remain false."
