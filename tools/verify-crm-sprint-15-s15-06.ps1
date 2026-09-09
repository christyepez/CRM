$ErrorActionPreference='Stop'
$required=@('tools/run-crm-sprint-15-s15-06-local-integration.ps1','docs/roadmap/crm-sprint-15-s15-06-campaign-local-integration.md','codex/prompts/sprint-15-campaign-management-s15-07.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S15-06 artifact: $f"}}
$doc=Get-Content 'docs/roadmap/crm-sprint-15-s15-06-campaign-local-integration.md' -Raw
$runner=Get-Content 'tools/run-crm-sprint-15-s15-06-local-integration.ps1' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('S1506Decision: Implemented','CampaignManagementLocalIntegration: Validated','ProductiveCampaignRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S15-06 doc marker: $m"}}
foreach($m in @('/api/crm/foundation/campaigns','/api/crm/campaigns','/foundation/campaigns','FoundationOnly')){if(-not$runner.Contains($m)){throw "Missing S15-06 runner marker: $m"}}
if(-not($next.Contains('CRM Sprint 15 S15-07 - Campaign Sprint Closure') -and $next.Contains('codex/prompts/sprint-15-campaign-management-s15-07.md'))){throw 'Invalid S15-07 handoff.'}
Write-Host 'CRM Sprint 15 S15-06 verification passed.'
