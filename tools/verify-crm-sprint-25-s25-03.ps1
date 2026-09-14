$ErrorActionPreference='Stop';$r=Split-Path $PSScriptRoot -Parent;$p=Get-Content (Join-Path $r 'src\CRM.Api\Program.cs') -Raw
if($p-notmatch '/api/crm/foundation/customer360'){throw 'Missing Customer360 foundation route'}
if($p-match 'Map(Post|Put|Delete|Patch)\("/api/crm/foundation/customer360'){throw 'Customer360 mutation route detected'}
Write-Host 'CRM Sprint 25 S25-03 verification passed.'
