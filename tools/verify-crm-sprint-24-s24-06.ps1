$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$result=Get-Content "$root\docs\roadmap\crm-sprint-24-s24-06-crm-assignment-reference-local-integration-result.json" -Raw | ConvertFrom-Json
if($result.S2406Decision -ne 'Implemented'){throw 'S24-06 decision mismatch'}
if($result.ProductiveAssignmentRouteAvailable){throw 'Productive assignment route must remain unavailable'}
if($result.DeleteRouteAvailable){throw 'DELETE must remain unavailable'}
if($result.PortalIdentityRuntimeObserved -or $result.PortalSecurityRuntimeObserved){throw 'Portal runtime must remain disabled'}
if($result.SimulatedProductionTouched){throw 'Simulated production must remain untouched'}
Write-Host 'CRM Sprint 24 S24-06 verification passed.'