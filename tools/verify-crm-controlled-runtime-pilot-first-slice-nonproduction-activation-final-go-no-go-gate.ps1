$ErrorActionPreference = "Stop"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-guardrails.ps1"
& "$PSScriptRoot/check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-execution-plan-validation-guardrails.ps1"
Write-Host "PASS CRM P36 verifier confirmed final GO/NO-GO is NoGo and P35 validation remains NoGo-now."
