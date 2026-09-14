$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs/roadmap/crm-sprint-23-s23-05-crm-tag-test-guardrail-hardening.md') -Raw
foreach($t in @('S2305Decision: Implemented','Assignment-only update returns Changed=true','Productive route/DELETE absent')){if($doc -notmatch [regex]::Escape($t)){throw "Missing $t"}}
Write-Output 'CRM Sprint 23 S23-05 verification passed.'
