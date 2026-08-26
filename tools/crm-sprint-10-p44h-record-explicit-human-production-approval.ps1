$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
& (Join-Path $root "tools/verify-crm-sprint-10-p44h-record-explicit-human-production-approval.ps1")
Write-Host "PASS CRM Sprint 10 P44H recorded explicit human production approval for canonical packet V3."

