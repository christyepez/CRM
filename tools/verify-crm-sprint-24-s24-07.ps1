$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$closure=Get-Content "$root\docs\releases\crm-sprint-24-crm-assignment-reference-closure.md" -Raw
if($closure -notmatch 'ClosedSuccessfully'){throw 'Sprint 24 closure missing'}
if($closure -notmatch 'Customer 360 Read Model Foundation'){throw 'Next slice missing'}
if($closure -notmatch 'Portal Identity/Security owns users'){throw 'Portal ownership boundary missing'}
Write-Host 'CRM Sprint 24 S24-07 verification passed.'