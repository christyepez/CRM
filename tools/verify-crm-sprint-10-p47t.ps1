$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-sprint-10-p47t-guardrails.ps1"
& "$PSScriptRoot\verify-crm-sprint-10-p47s-production-evidence.ps1"
Write-Host "PASS CRM P47T verifier confirmed ArchitectureTests are stable, production evidence remains missing, and production was not executed."

