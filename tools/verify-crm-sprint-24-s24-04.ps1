$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$main=Get-Content "$root\frontend\crm-web\src\main.ts" -Raw
if($main -notmatch "path: 'foundation/assignments'"){throw 'Assignment frontend route missing'}
if($main -notmatch "apiBaseUrl='/api/crm/foundation/assignments'"){throw 'Assignment frontend API base missing'}
if($main -notmatch 'AssigneeReferenceId is not resolved against Portal Security'){throw 'Opaque reference warning missing'}
Write-Host 'CRM Sprint 24 S24-04 verification passed.'