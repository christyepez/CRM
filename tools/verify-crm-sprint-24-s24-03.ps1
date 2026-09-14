$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$program=Get-Content "$root\src\CRM.Api\Program.cs" -Raw
if($program -notmatch '/api/crm/foundation/assignments'){throw 'Assignment foundation routes missing'}
if($program -match 'MapDelete\("/api/crm/foundation/assignments'){throw 'Assignment DELETE route forbidden'}
if($program -match '"/api/crm/assignments"'){throw 'Productive assignment route forbidden'}
Write-Host 'CRM Sprint 24 S24-03 verification passed.'