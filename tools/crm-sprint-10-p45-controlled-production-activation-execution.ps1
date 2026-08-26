$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
& (Join-Path $root "tools/verify-crm-sprint-10-p45-controlled-production-activation-execution.ps1")
Write-Host "PASS CRM Sprint 10 P45 recorded controlled production activation execution as AbortedBeforeExecution."

