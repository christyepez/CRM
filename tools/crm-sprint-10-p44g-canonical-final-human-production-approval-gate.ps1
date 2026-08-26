$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
& (Join-Path $root "tools/verify-crm-sprint-10-p44g-canonical-final-human-production-approval-gate.ps1")
Write-Host "PASS CRM Sprint 10 P44G canonical final human production approval gate completed as NoGo without human approval."

