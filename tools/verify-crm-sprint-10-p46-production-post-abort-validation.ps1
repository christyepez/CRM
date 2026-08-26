$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
& (Join-Path $root "tools/check-crm-sprint-10-p46-production-post-abort-validation-guardrails.ps1")
& (Join-Path $root "tools/verify-crm-sprint-10-p45-controlled-production-activation-execution.ps1")
Write-Host "PASS CRM P46 verifier confirmed production remained untouched and P47 remediation is required."

