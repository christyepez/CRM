param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot enablement readiness starting."

& .\tools\crm-controlled-runtime-pilot-validate-all.ps1
& .\tools\check-crm-controlled-runtime-pilot-enablement-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-enablement-plan.ps1

Write-Output "PASS CRM controlled runtime pilot enablement readiness passed. Plan remains disabled-only."
exit 0
