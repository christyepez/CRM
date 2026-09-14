$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$closure=Join-Path $root 'docs/releases/crm-sprint-21-pipeline-catalog-closure.md'
if(-not(Test-Path $closure)){throw 'Closure document missing'}
$c=Get-Content $closure -Raw
foreach($x in @('ClosedSuccessfully','CRM Document Metadata Reference Foundation','Portal Content/File API')){if(-not $c.Contains($x)){throw "Missing $x"}}
Write-Host 'CRM Sprint 21 S21-07 verification passed.'
