$ErrorActionPreference = "Stop"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-guardrails.ps1"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-guardrails.ps1"
Write-Host "PASS CRM P35 verifier confirmed execution plan is validated-only and P34 execution remains NoGo-now."
