$ErrorActionPreference = "Stop"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-guardrails.ps1"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-readiness-review-guardrails.ps1"
Write-Host "PASS CRM P33 verifier confirmed approval gate is prepared-only and P32 readiness remains NoGo-now."
