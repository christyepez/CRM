$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$required=@('src\CRM.Domain\Customer360\Customer360Contracts.cs','src\CRM.Domain\Customer360\Customer360Policy.cs','tests\CRM.UnitTests\Customer360PolicyTests.cs','docs\roadmap\crm-sprint-25-s25-01-customer-360-read-model-contracts-validation-rules.md')
foreach($f in $required){if(!(Test-Path (Join-Path $root $f))){throw "Missing $f"}}
if(Select-String -Path (Join-Path $root 'src\CRM.Domain\Customer360\*.cs') -Pattern 'Create|Update|Delete|Archive' -Quiet){throw 'Customer360 domain must remain read-only'}
Write-Host 'CRM Sprint 25 S25-01 verification passed.'
