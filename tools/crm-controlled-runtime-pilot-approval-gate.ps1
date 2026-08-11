param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot approval gate starting."

& .\tools\crm-controlled-runtime-pilot-enablement-dry-run.ps1
& .\tools\check-crm-controlled-runtime-pilot-approval-gate-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-approval-gate.ps1

Write-Output "PASS CRM controlled runtime pilot approval gate passed. ApprovalGateOnly remains true and runtime remains NoGo."
exit 0
