$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
& (Join-Path $root "tools/verify-crm-sprint-10-p46-production-post-abort-validation.ps1")
Write-Host "PASS CRM Sprint 10 P46 production post-abort validation completed."

