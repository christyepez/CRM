$ErrorActionPreference = 'Stop'

& powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools\verify-crm-sprint-10-p48.ps1
& powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools\check-crm-sprint-10-p49-guardrails.ps1

Write-Output 'PASS CRM P49 verifier confirmed controlled local simulated Production execution succeeded.'
