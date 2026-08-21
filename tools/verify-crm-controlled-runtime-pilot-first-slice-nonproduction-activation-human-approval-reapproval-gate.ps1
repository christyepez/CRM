$ErrorActionPreference = "Stop"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-human-approval-reapproval-gate-guardrails.ps1"
& "$PSScriptRoot\check-crm-controlled-runtime-pilot-first-slice-nonproduction-activation-explicit-execution-approval-gate-guardrails.ps1"
Write-Host "PASS CRM P39A verifier confirmed human approval is not recorded and P40 remains blocked."
