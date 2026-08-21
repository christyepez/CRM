$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-execution-approval-gate-guardrails.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution-preparation-validation-guardrails.ps1"
Write-Host "PASS CRM P39 verifier confirmed explicit approval gate is NoGo without human approval and production remains NoGo."
