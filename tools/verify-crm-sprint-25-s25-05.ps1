$ErrorActionPreference='Stop';$r=Split-Path $PSScriptRoot -Parent;$p=Get-Content (Join-Path $r 'src\CRM.Api\Program.cs') -Raw
if($p-match 'Map(Post|Put|Patch|Delete)\("/api/crm/foundation/customer360'){throw 'Mutation route detected'}
if(!(Test-Path (Join-Path $r 'tests\CRM.UnitTests\Customer360HardeningTests.cs'))){throw 'Missing hardening tests'}
Write-Host 'CRM Sprint 25 S25-05 verification passed.'
