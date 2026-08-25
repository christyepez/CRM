$ErrorActionPreference = "Stop"
& $PSScriptRoot\check-crm-sprint-10-p44c-final-human-production-approval-guardrails.ps1
& $PSScriptRoot\check-crm-sprint-10-p44b-production-approval-preconditions-guardrails.ps1
Write-Host "PASS CRM P44C verifier confirmed final human approval gate is NoGo and P45 remains blocked."
