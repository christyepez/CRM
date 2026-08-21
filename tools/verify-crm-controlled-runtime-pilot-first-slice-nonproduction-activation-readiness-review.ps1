$ErrorActionPreference = "Stop"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-readiness-review-guardrails.ps1"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run-controlled-execution-validation-guardrails.ps1"
Write-Host "PASS CRM P32 verifier confirmed readiness review only and P31 validation remains NoGo-now."
