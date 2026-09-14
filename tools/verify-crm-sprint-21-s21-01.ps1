$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$policy=Get-Content (Join-Path $root 'src/CRM.Domain/PipelineCatalog/PipelineCatalogPolicy.cs') -Raw
$contracts=Get-Content (Join-Path $root 'src/CRM.Domain/PipelineCatalog/PipelineCatalogContracts.cs') -Raw
if($policy-notmatch'PipelineCatalogPolicy' -or $policy-notmatch'DuplicateStageOrder'){throw 'Pipeline catalog policy incomplete'}
if($contracts-notmatch'PipelineCatalogRecord' -or $contracts-notmatch'PipelineStageCatalogRecord'){throw 'Pipeline catalog contracts incomplete'}
Write-Host 'CRM Sprint 21 S21-01 verification passed.'
