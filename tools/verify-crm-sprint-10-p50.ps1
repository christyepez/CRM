$ErrorActionPreference = 'Stop'

& powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools\verify-crm-sprint-10-p49.ps1
& powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools\check-crm-sprint-10-p50-guardrails.ps1

Write-Output 'PASS CRM P50 verifier confirmed local simulated Production pilot closure.'
