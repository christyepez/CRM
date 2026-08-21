$ErrorActionPreference = "Stop"
& (Join-Path $PSScriptRoot "check-crm-sprint-10-p44-explicit-production-approval-gate-guardrails.ps1")
& (Join-Path $PSScriptRoot "check-crm-sprint-10-p43-production-readiness-remediation-guardrails.ps1")
Write-Host "PASS CRM P44 verifier confirmed NoGo because explicit human approval is absent."
