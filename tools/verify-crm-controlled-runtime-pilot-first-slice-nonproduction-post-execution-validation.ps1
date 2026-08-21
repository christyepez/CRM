$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-post-execution-validation-guardrails.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution-guardrails.ps1"
Write-Host "PASS CRM P41 verifier confirmed Healthy post-execution state and P42 entry conditions."
