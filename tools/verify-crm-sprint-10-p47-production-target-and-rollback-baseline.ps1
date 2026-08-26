$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-sprint-10-p47-production-target-and-rollback-baseline-guardrails.ps1"
& "$PSScriptRoot\verify-crm-sprint-10-p46-production-post-abort-validation.ps1"
Write-Host "PASS CRM P47 verifier confirmed production target remains unresolved, rollback is not ready, and production was not executed."

