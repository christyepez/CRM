param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot first slice design starting."

& .\tools\crm-controlled-runtime-pilot-implementation-readiness-review.ps1
& .\tools\check-crm-controlled-runtime-pilot-first-slice-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-first-slice-design.ps1

Write-Output "PASS CRM controlled runtime pilot first slice design passed. FirstImplementationSliceDesignOnly remains true and runtime remains NoGo."
exit 0
