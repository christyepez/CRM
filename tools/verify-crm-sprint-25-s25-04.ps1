$ErrorActionPreference='Stop';$r=Split-Path $PSScriptRoot -Parent;$m=Get-Content (Join-Path $r 'frontend\crm-web\src\main.ts') -Raw
if($m-notmatch "path: 'foundation/customer360'"){throw 'Missing frontend route'}
if($m-notmatch '/api/crm/foundation/customer360'){throw 'Missing foundation API usage'}
Write-Host 'CRM Sprint 25 S25-04 verification passed.'
