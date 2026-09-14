$ErrorActionPreference='Stop'
$e=Get-Content 'docs/roadmap/crm-sprint-23-s23-06-crm-tag-local-integration-result.json' -Raw
if($e -notmatch '"S2306Decision"\s*:\s*"Implemented"'){throw 'Missing S23-06 implemented evidence'}
if($e -notmatch '"ProductiveTagRouteAvailable"\s*:\s*false'){throw 'Productive Tag route guardrail failed'}
if($e -notmatch '"DeleteRouteAvailable"\s*:\s*false'){throw 'Delete route guardrail failed'}
Write-Host 'CRM Sprint 23 S23-06 verification passed.'
