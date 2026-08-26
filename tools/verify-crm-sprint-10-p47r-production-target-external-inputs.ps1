$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-sprint-10-p47r-production-target-external-inputs-guardrails.ps1"
& "$PSScriptRoot\verify-crm-sprint-10-p47-production-target-and-rollback-baseline.ps1"
Write-Host "PASS CRM P47R verifier confirmed external inputs remain unresolved and production was not executed."

