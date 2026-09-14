$ErrorActionPreference='Stop'
$c=Get-Content 'docs/roadmap/crm-sprint-24-crm-assignment-reference-functional-baseline.md' -Raw
if($c -notmatch 'Portal Security remains authoritative'){throw 'Portal ownership boundary missing'}
if($c -notmatch 'S24-07'){throw 'Sprint 24 backlog incomplete'}
Write-Host 'CRM Sprint 24 P1 verification passed.'
