$ErrorActionPreference = "Stop"
& (Join-Path $PSScriptRoot "verify-crm-sprint-10-p44b-production-approval-preconditions.ps1")
Write-Host "PASS CRM Sprint 10 P44B production approval preconditions remediation completed with conditions."
