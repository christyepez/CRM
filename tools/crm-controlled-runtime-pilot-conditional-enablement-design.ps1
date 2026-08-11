param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot conditional enablement design starting."

& .\tools\crm-controlled-runtime-pilot-approval-gate.ps1
& .\tools\check-crm-controlled-runtime-pilot-conditional-enablement-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-conditional-enablement-design.ps1

Write-Output "PASS CRM controlled runtime pilot conditional enablement design passed. ConditionalFutureGoExecuted remains false and runtime remains NoGo."
exit 0
