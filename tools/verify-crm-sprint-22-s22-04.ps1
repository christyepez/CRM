$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$main=Get-Content (Join-Path $root 'frontend\crm-web\src\main.ts') -Raw
foreach($m in @('/foundation/documents','/api/crm/foundation/documents','No file picker, upload, download or binary storage')){if($main -notmatch [regex]::Escape($m)){throw "Missing marker: $m"}}
if($main -match 'type="file"'){throw 'File picker must not exist'}
if($main -match '/api/crm/documents'){throw 'Productive document route must not be used'}
Write-Host 'CRM Sprint 22 S22-04 verification passed.'
