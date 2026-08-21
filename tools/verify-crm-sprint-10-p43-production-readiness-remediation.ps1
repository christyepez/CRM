$ErrorActionPreference = "Stop"
& (Join-Path $PSScriptRoot "check-crm-sprint-10-p43-production-readiness-remediation-guardrails.ps1")
& (Join-Path $PSScriptRoot "check-crm-controlled-runtime-pilot-first-slice-nonproduction-pilot-closure-production-readiness-guardrails.ps1")
Write-Host "PASS CRM P43 verifier confirmed ReadyForApprovalGate preparation with production NoGo preserved."
