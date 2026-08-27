$ErrorActionPreference = "Stop"

& "$PSScriptRoot\check-crm-sprint-10-p47u-guardrails.ps1"
& "$PSScriptRoot\verify-crm-sprint-10-p47t.ps1"

Write-Host "PASS CRM P47U verifier confirmed production evidence is still missing, ArchitectureTests remain stable, and production was not executed."
