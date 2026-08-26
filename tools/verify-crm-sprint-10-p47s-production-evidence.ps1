$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-sprint-10-p47s-production-evidence-guardrails.ps1"
& "$PSScriptRoot\verify-crm-sprint-10-p47r-production-target-external-inputs.ps1"
Write-Host "PASS CRM P47S verifier confirmed real production evidence is still missing and production was not executed."

