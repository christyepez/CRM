$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs/roadmap/crm-sprint-23-s23-02-crm-tag-application-foundation-store.md') -Raw
foreach($t in @('S2302Decision: Implemented','Assignment-only changes persist correctly','840 tests PASS')){if($doc -notmatch [regex]::Escape($t)){throw "Missing $t"}}
Write-Output 'CRM Sprint 23 S23-02 verification passed.'
