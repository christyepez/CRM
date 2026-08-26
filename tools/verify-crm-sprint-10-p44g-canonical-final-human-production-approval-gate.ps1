$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
& (Join-Path $root "tools/test-crm-sprint-10-p44f-canonical-hash.ps1")
& (Join-Path $root "tools/check-crm-sprint-10-p44g-canonical-final-human-production-approval-gate-guardrails.ps1")
& (Join-Path $root "tools/verify-crm-sprint-10-p44f-final-approval-packet-canonicalization.ps1")
Write-Host "PASS CRM P44G verifier confirmed technical approval passed, human approval not recorded, and P45 remains blocked."

