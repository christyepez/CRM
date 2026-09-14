$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$tests=Get-Content "$root\tests\CRM.UnitTests\AssignmentHardeningTests.cs" -Raw
$doc=Get-Content "$root\docs\roadmap\crm-sprint-24-s24-05-crm-assignment-reference-test-guardrail-hardening.md" -Raw
if($tests -notmatch 'Update_NoChange_ReturnsChangedFalse'){throw 'Missing no-change hardening test'}
if($tests -notmatch 'Archive_IsIdempotent'){throw 'Missing archive idempotency hardening test'}
if($tests -notmatch 'MetadataAboveMax_ReturnsSafeBadRequest'){throw 'Missing metadata max hardening test'}
if($doc -notmatch 'No Portal Security runtime calls'){throw 'Missing Portal Security boundary'}
Write-Host 'CRM Sprint 24 S24-05 verification passed.'