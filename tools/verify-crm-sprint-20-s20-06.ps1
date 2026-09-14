$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$json=Get-Content (Join-Path $root 'docs\roadmap\crm-sprint-20-s20-06-note-local-integration-result.json') -Raw | ConvertFrom-Json
if($json.S2006Decision-ne'Implemented'){throw 'S20-06 decision invalid'}
foreach($p in @('CreateScenario','ReadAfterCreate','UpdateScenario','NoChangeScenario','InvalidCreateScenario','NotFoundScenario','ArchiveScenario','RepeatArchive','ArchivedUpdateConflict')){if($json.$p-ne'PASS'){throw "$p failed"}}
if($json.ProductiveNoteRouteAvailable -or $json.DeleteRouteAvailable){throw 'Unsafe Note route detected'}
if($json.PortalRuntimeObserved -or $json.CommonDbRuntimeObserved -or $json.ExternalConnectorRuntimeObserved -or $json.RealDataDetected -or $json.SimulatedProductionTouched){throw 'Unsafe runtime observation detected'}
Write-Output 'CRM Sprint 20 S20-06 verification passed.'