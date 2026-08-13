$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-dry-run.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-activation-approval-gate-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-activation-approval-gate.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice activation approval gate passed. Approval gate is prepared, activation is not executed and decision remains NoGo."
