$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$program=Get-Content (Join-Path $root 'src\CRM.Api\Program.cs') -Raw
$doc=Get-Content (Join-Path $root 'docs\roadmap\crm-sprint-22-s22-03-crm-document-metadata-foundation-api.md') -Raw
foreach($m in @('/api/crm/foundation/documents','S2203Decision: Implemented','Metadata/reference only','DELETE remains unavailable')){if(($program+$doc) -notmatch [regex]::Escape($m)){throw "Missing marker: $m"}}
if($program -match 'MapDelete\("/api/crm/foundation/documents'){throw 'Document DELETE route must remain unavailable'}
if($program -match '"/api/crm/documents"'){throw 'Productive document route must remain unavailable'}
Write-Host 'CRM Sprint 22 S22-03 verification passed.'
