param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot enablement dry run starting."

& .\tools\crm-controlled-runtime-pilot-enablement-readiness.ps1
& .\tools\check-crm-controlled-runtime-pilot-enablement-dry-run-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-enablement-dry-run.ps1

Write-Output "PASS CRM controlled runtime pilot enablement dry run passed. DryRunOnly remains true and runtime remains disabled."
exit 0
