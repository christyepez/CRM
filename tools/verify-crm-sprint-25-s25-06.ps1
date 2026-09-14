$ErrorActionPreference='Stop'
$r=Split-Path $PSScriptRoot -Parent
$j=Get-Content (Join-Path $r 'docs\roadmap\crm-sprint-25-s25-06-customer-360-local-integration-result.json')|ConvertFrom-Json
if($j.S2506Decision-ne'Implemented'-or$j.MutationRoutesAvailable-or$j.ProductiveRouteAvailable){throw 'Invalid S25-06 evidence'}
Write-Host 'CRM Sprint 25 S25-06 verification passed.'
