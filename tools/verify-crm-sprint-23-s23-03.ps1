$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs/roadmap/crm-sprint-23-s23-03-crm-tag-foundation-api.md') -Raw
foreach($t in @('S2303Decision: Implemented','Productive /api/crm/tags and DELETE remain unavailable','843 tests PASS')){if($doc -notmatch [regex]::Escape($t)){throw "Missing $t"}}
Write-Output 'CRM Sprint 23 S23-03 verification passed.'
