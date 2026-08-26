$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
& (Join-Path $root "tools/test-crm-sprint-10-p44f-canonical-hash.ps1")
& (Join-Path $root "tools/check-crm-sprint-10-p44h-record-explicit-human-production-approval-guardrails.ps1")
& (Join-Path $root "tools/verify-crm-sprint-10-p44g-canonical-final-human-production-approval-gate.ps1")
Write-Host "PASS CRM P44H verifier confirmed explicit human approval is recorded, drift is absent, and P45 is authorized without production execution."

