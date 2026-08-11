param()

$ErrorActionPreference = "Stop"

Write-Output "CRM controlled runtime pilot aggregate validation starting."

& .\tools\check-crm-common-db-controlled-activation-guardrails.ps1
& .\tools\verify-crm-common-db-controlled-activation-plan.ps1
& .\tools\check-crm-portal-consumer-contract-alignment-guardrails.ps1
& .\tools\verify-crm-portal-consumer-contract-alignment.ps1
& .\tools\check-crm-controlled-runtime-integration-design-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-integration-design.ps1
& .\tools\check-crm-controlled-runtime-pilot-scaffold-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-scaffold.ps1
& .\tools\crm-controlled-runtime-pilot-preflight.ps1
& .\tools\crm-controlled-runtime-pilot-smoke.ps1
& .\tools\check-crm-controlled-runtime-pilot-validation-guardrails.ps1
& .\tools\verify-crm-controlled-runtime-pilot-validation.ps1

Write-Output "PASS CRM controlled runtime pilot aggregate validation passed. Result remains disabled-only."
exit 0
