param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot implementation readiness review starting."

& .\tools\crm-controlled-runtime-pilot-conditional-implementation-plan.ps1
& .\tools\check-crm-controlled-runtime-pilot-implementation-readiness-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-implementation-readiness-review.ps1

Write-Output "PASS CRM controlled runtime pilot implementation readiness review passed. ReadinessReviewOnly remains true and runtime remains NoGo."
exit 0
