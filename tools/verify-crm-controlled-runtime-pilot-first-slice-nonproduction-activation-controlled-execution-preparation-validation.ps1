$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution-preparation-validation-guardrails.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution-preparation-guardrails.ps1"
Write-Host "PASS CRM P38 verifier confirmed preparation validation is complete and NoGo remains active."
