$ErrorActionPreference = "Stop"
& (Join-Path $PSScriptRoot "check-crm-sprint-10-p44b-production-approval-preconditions-guardrails.ps1")
& (Join-Path $PSScriptRoot "check-crm-sprint-10-p44a-human-production-approval-reapproval-gate-guardrails.ps1")
Write-Host "PASS CRM P44B verifier confirmed NonProduction restored and candidate artifact frozen locally with conditions."
