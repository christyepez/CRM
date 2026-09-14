$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$baseline=Get-Content (Join-Path $root 'docs\roadmap\crm-sprint-21-pipeline-catalog-functional-baseline.md') -Raw
foreach($m in @('S21P1Decision: PipelineCatalogFoundationReadOnly','S21-01','S21-07','PipelineMutationEnabled: false','PortalCatalogRuntimeEnabled: false')){if($baseline-notmatch [regex]::Escape($m)){throw "Missing S21 P1 marker: $m"}}
Write-Output 'CRM Sprint 21 P1 verification passed.'