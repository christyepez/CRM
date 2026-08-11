param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot conditional implementation plan starting."

& .\tools\crm-controlled-runtime-pilot-conditional-enablement-design.ps1
& .\tools\check-crm-controlled-runtime-pilot-conditional-implementation-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-conditional-implementation-plan.ps1

Write-Output "PASS CRM controlled runtime pilot conditional implementation plan passed. ImplementationPlanOnly remains true and runtime remains NoGo."
exit 0
