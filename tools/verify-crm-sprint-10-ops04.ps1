$ErrorActionPreference = "Stop"

& "$PSScriptRoot\check-crm-sprint-10-ops04-guardrails.ps1"
& "$PSScriptRoot\verify-crm-sprint-10-p47v.ps1"

Write-Host "PASS CRM OPS-04 verifier confirmed local simulated Production is provisioned, isolated, rollback-ready, and Production remains untouched."
