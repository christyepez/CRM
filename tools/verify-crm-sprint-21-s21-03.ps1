$ErrorActionPreference='Stop';$root=Split-Path $PSScriptRoot -Parent;$p=Get-Content (Join-Path $root 'src/CRM.Api/Program.cs') -Raw
foreach($x in @('MapGet("/api/crm/foundation/pipelines"','MapGet("/api/crm/foundation/pipelines/{id}"')){if(-not $p.Contains($x)){throw "Missing $x"}}
if($p.Contains('MapPost("/api/crm/foundation/pipelines') -or $p.Contains('MapPut("/api/crm/foundation/pipelines') -or $p.Contains('MapDelete("/api/crm/foundation/pipelines')){throw 'Mutation route found'}
Write-Host 'CRM Sprint 21 S21-03 verification passed.'
