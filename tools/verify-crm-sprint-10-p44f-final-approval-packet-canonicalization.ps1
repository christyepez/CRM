$ErrorActionPreference = "Stop"
& $PSScriptRoot\test-crm-sprint-10-p44f-canonical-hash.ps1
& $PSScriptRoot\check-crm-sprint-10-p44f-final-approval-packet-canonicalization-guardrails.ps1
& $PSScriptRoot\check-crm-sprint-10-p44e-final-human-production-approval-revalidation-guardrails.ps1
& $PSScriptRoot\check-crm-sprint-10-p44d-nonproduction-stability-remediation-guardrails.ps1
Write-Host "PASS CRM P44F verifier confirmed canonical packet V3 hash is stable and P45 remains blocked."
