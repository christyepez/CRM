$ErrorActionPreference = "Stop"
& (Join-Path $PSScriptRoot "check-crm-sprint-10-p44a-human-production-approval-reapproval-gate-guardrails.ps1")
& (Join-Path $PSScriptRoot "check-crm-sprint-10-p44-explicit-production-approval-gate-guardrails.ps1")
Write-Host "PASS CRM P44A verifier confirmed NoGo because explicit human approval is absent and NonProduction runtime is not stable."
