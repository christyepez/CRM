$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-execution-approval.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice NonProduction activation dry-run controlled execution passed. Dry-run is local no-op/fail-closed only."
