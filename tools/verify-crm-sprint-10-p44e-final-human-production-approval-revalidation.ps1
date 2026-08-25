$ErrorActionPreference = "Stop"
& $PSScriptRoot\check-crm-sprint-10-p44e-final-human-production-approval-revalidation-guardrails.ps1
& $PSScriptRoot\check-crm-sprint-10-p44d-nonproduction-stability-remediation-guardrails.ps1
& $PSScriptRoot\check-crm-sprint-10-p44c-final-human-production-approval-guardrails.ps1
Write-Host "PASS CRM P44E verifier confirmed NoGo because approval packet identity and human approval are not valid."
