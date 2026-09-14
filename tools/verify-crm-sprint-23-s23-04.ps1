$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs/roadmap/crm-sprint-23-s23-04-crm-tag-frontend-foundation-page.md') -Raw
foreach($t in @('S2304Decision: Implemented','/foundation/tags','No DELETE')){if($doc -notmatch [regex]::Escape($t)){throw "Missing $t"}}
Write-Output 'CRM Sprint 23 S23-04 verification passed.'
