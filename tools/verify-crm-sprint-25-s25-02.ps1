$ErrorActionPreference='Stop';$r=Split-Path $PSScriptRoot -Parent
foreach($f in @('src\CRM.Application\Customer360\Customer360ReadService.cs','src\CRM.Infrastructure\ReadModels\InMemoryCustomer360FoundationProvider.cs','tests\CRM.UnitTests\Customer360ReadServiceTests.cs')){if(!(Test-Path (Join-Path $r $f))){throw "Missing $f"}}
if(Select-String -Path (Join-Path $r 'src\CRM.Api\Program.cs') -Pattern '/api/crm/foundation/customer360' -Quiet){throw 'S25-02 must not expose routes yet'}
Write-Host 'CRM Sprint 25 S25-02 verification passed.'
