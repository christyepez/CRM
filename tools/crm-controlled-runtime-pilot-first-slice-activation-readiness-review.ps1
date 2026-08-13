$ErrorActionPreference = "Stop"

& "$PSScriptRoot\crm-controlled-runtime-pilot-first-slice-nonproduction-activation-implementation-plan.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-activation-readiness-guardrails.ps1"
& "$PSScriptRoot\verify-crm-controlled-runtime-pilot-first-slice-activation-readiness-review.ps1"

Write-Host "PASS CRM controlled runtime pilot first slice activation readiness review passed. Review is prepared, activation is not executed and decision remains NoGo."
