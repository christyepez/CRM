$ErrorActionPreference = "Stop"
& (Join-Path $PSScriptRoot "verify-crm-sprint-10-p44-explicit-production-approval-gate.ps1")
Write-Host "PASS CRM Sprint 10 P44 explicit production approval gate completed with NoGo."
