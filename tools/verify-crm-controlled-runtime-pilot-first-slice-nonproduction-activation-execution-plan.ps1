$ErrorActionPreference = "Stop"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-guardrails.ps1"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-approval-gate-guardrails.ps1"
Write-Host "PASS CRM P34 verifier confirmed execution plan is prepared-only and P33 approval remains NoGo-now."
