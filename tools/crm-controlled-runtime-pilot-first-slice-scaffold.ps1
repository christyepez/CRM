$ErrorActionPreference = "Stop"

& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-scaffold-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-scaffold.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice scaffold passed. Runtime remains disabled and ProductionActivationDecision remains NoGo."
