$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$tests=Get-Content (Join-Path $root 'tests\CRM.UnitTests\NoteFoundationApiEndpointTests.cs') -Raw
$program=Get-Content (Join-Path $root 'src\CRM.Api\Program.cs') -Raw
$frontend=Get-Content (Join-Path $root 'frontend\crm-web\src\main.ts') -Raw
foreach($m in @('Create_TextAboveMax_ReturnsSafeBadRequest','Update_NoChange_ReturnsChangedFalse','TextTooLong','/api/crm/foundation/notes')){if(($tests+$program+$frontend)-notmatch [regex]::Escape($m)){throw "Missing marker: $m"}}
if($program -match 'MapDelete\("/api/crm/(foundation/)?notes'){throw 'DELETE Note route detected'}
if($program -match 'Map(Get|Post|Put)\("/api/crm/notes'){throw 'Productive Note route detected'}
Write-Output 'CRM Sprint 20 S20-05 verification passed.'