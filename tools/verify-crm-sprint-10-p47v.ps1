$ErrorActionPreference = "Stop"

& "$PSScriptRoot\check-crm-sprint-10-p47v-guardrails.ps1"
& "$PSScriptRoot\verify-crm-sprint-10-p47u.ps1"

Write-Host "PASS CRM P47V verifier confirmed Operations inputs are missing, packet V5 was not created, and production was not executed."
