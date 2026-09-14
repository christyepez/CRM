$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$required=@('src/CRM.Application/PipelineCatalog/PipelineCatalogService.cs','src/CRM.Application/Ports/PipelineCatalog/IPipelineCatalogSource.cs','src/CRM.Infrastructure/Persistence/Foundation/SyntheticPipelineCatalogSource.cs','tests/CRM.UnitTests/PipelineCatalogServiceTests.cs')
foreach($x in $required){if(-not(Test-Path (Join-Path $root $x))){throw "Missing $x"}}
Write-Host 'CRM Sprint 21 S21-02 verification passed.'
