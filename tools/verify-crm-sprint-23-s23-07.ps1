$ErrorActionPreference='Stop'
$closure=Get-Content 'docs/releases/crm-sprint-23-crm-tag-closure.md' -Raw
if($closure -notmatch 'Status: Closed locally'){throw 'Missing Sprint 23 closure status'}
if($closure -notmatch 'CRM Assignment Reference Foundation'){throw 'Missing Sprint 24 evidence-based selection'}
Write-Host 'CRM Sprint 23 S23-07 verification passed.'
