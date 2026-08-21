$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution-preparation-guardrails.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-final-go-no-go-gate-guardrails.ps1"
Write-Host "PASS CRM P37 verifier confirmed controlled execution preparation is prepared and NoGo remains active."
