$ErrorActionPreference = "Stop"
& $PSScriptRoot\check-crm-sprint-10-p44d-nonproduction-stability-remediation-guardrails.ps1
& $PSScriptRoot\check-crm-sprint-10-p44c-final-human-production-approval-guardrails.ps1
& $PSScriptRoot\check-crm-sprint-10-p44b-production-approval-preconditions-guardrails.ps1
Write-Host "PASS CRM P44D verifier confirmed NonProduction restored and P44E package prepared with conditions."
