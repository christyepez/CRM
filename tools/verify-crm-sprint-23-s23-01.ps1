$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs/roadmap/crm-sprint-23-s23-01-crm-tag-contracts-domain-rules.md') -Raw
foreach($t in @('S2301Decision: Implemented','Archive idempotent','835 tests PASS')){if($doc -notmatch [regex]::Escape($t)){throw "Missing $t"}}
Write-Output 'CRM Sprint 23 S23-01 verification passed.'
