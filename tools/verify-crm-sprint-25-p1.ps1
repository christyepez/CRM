$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content "$root\docs\roadmap\crm-sprint-25-customer-360-read-model-functional-baseline.md" -Raw
if($doc -notmatch 'GoFoundationOnly'){throw 'Missing P1 decision'}
if($doc -notmatch 'Account remains the CRM organization/customer container'){throw 'Missing Account ownership boundary'}
if($doc -notmatch 'Read-only list/detail behavior'){throw 'Missing read-only boundary'}
if($doc -notmatch 'No cross-entity mutation'){throw 'Missing cross-entity mutation guardrail'}
Write-Host 'CRM Sprint 25 P1 verification passed.'