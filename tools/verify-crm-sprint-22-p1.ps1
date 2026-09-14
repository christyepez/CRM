$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$baseline=Get-Content (Join-Path $root 'docs\roadmap\crm-sprint-22-crm-document-metadata-functional-baseline.md') -Raw
$prompt=Get-Content (Join-Path $root 'codex\prompts\sprint-22-crm-document-metadata-s22-01.md') -Raw
foreach($token in @('CRM Document Metadata Reference Foundation','Portal Content/File API','No DELETE','S22-07')){if(-not $baseline.Contains($token)){throw "Missing baseline token: $token"}}
foreach($token in @('FileReferenceId','Archived is read-only','No binary content fields')){if(-not $prompt.Contains($token)){throw "Missing prompt token: $token"}}
Write-Host 'CRM Sprint 22 P1 verification passed.'
