$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs/roadmap/crm-sprint-23-crm-tag-functional-baseline.md') -Raw
foreach($token in @('P1Decision: ApprovedForFoundationImplementation','No physical DELETE','S23-07 closure')){if($doc -notmatch [regex]::Escape($token)){throw "Missing $token"}}
Write-Output 'CRM Sprint 23 P1 verification passed.'
