$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$closure=Get-Content (Join-Path $root 'docs\releases\crm-sprint-20-note-management-closure.md') -Raw
$result=Get-Content (Join-Path $root 'docs\roadmap\crm-sprint-20-s20-06-note-local-integration-result.json') -Raw | ConvertFrom-Json
foreach($m in @('Sprint20NoteManagementClosed: true','S2007Decision: ClosedSuccessfully','S21-PIPELINE-CATALOG','Pipeline Catalog Foundation','15.33 ms','P95 65 ms')){if($closure-notmatch [regex]::Escape($m)){throw "Missing closure marker: $m"}}
if($result.S2006Decision-ne'Implemented'){throw 'Canonical S20-06 evidence missing'}
if($result.ProductiveNoteRouteAvailable -or $result.DeleteRouteAvailable){throw 'Unsafe S20 evidence'}
Write-Output 'CRM Sprint 20 S20-07 verification passed.'