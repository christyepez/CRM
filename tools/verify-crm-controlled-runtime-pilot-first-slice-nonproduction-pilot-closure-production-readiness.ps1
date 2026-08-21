$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-pilot-closure-production-readiness-guardrails.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-post-execution-validation-guardrails.ps1"
Write-Host "PASS CRM P42 verifier confirmed pilot closure and ReadyWithConditions production assessment."
