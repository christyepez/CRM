$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution-guardrails.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-human-approval-reapproval-gate-guardrails.ps1"
Write-Host "PASS CRM P40 verifier confirmed controlled NonProduction execution evidence and P41 entry conditions."
