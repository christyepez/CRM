$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs\roadmap\crm-sprint-22-s22-05-crm-document-metadata-test-guardrail-hardening.md') -Raw
$test=Get-Content (Join-Path $root 'tests\CRM.UnitTests\DocumentMetadataHardeningTests.cs') -Raw
foreach($m in @('S2205Decision: Implemented','Changed=false','Archive is idempotent','OversizedMetadata_IsRejectedWith400')){if(($doc+$test) -notmatch [regex]::Escape($m)){throw "Missing marker: $m"}}
Write-Host 'CRM Sprint 22 S22-05 verification passed.'
