$ErrorActionPreference = "Stop"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-guardrails.ps1"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-guardrails.ps1"
Write-Host "PASS CRM P31 verifier confirmed P30 dry-run evidence remains local/no-op/fail-closed and P31 validation is NoGo-now."
