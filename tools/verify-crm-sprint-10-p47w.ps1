$ErrorActionPreference = "Stop"

& "$PSScriptRoot\check-crm-sprint-10-p47w-guardrails.ps1"
& "$PSScriptRoot\verify-crm-sprint-10-ops04.ps1"

Write-Host "PASS CRM P47W verifier confirmed local simulated Production target, rollback baseline, monitoring, and packet V5 are frozen."
