$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
& (Join-Path $root "tools/check-crm-sprint-10-p45-controlled-production-activation-execution-guardrails.ps1")
& (Join-Path $root "tools/verify-crm-sprint-10-p44h-record-explicit-human-production-approval.ps1")
Write-Host "PASS CRM P45 verifier confirmed execution aborted before production because target/rollback preflight did not pass."

