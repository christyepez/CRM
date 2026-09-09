$ErrorActionPreference='Stop'
$required=@('src/CRM.Api/Foundation/CampaignManagementApiContracts.cs','tests/CRM.UnitTests/CampaignFoundationApiEndpointTests.cs','docs/roadmap/crm-sprint-15-s15-03-campaign-foundation-api.md','codex/prompts/sprint-15-campaign-management-s15-04.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S15-03 artifact: $f"}}
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-15-s15-03-campaign-foundation-api.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('MapGet("/api/crm/foundation/campaigns"','MapGet("/api/crm/foundation/campaigns/{id}"','MapPost("/api/crm/foundation/campaigns"','MapPut("/api/crm/foundation/campaigns/{id}"','/activate"','/complete"','/cancel"','ICampaignManagementService','ICampaignFoundationStore')){if(-not$program.Contains($m)){throw "Missing S15-03 marker: $m"}}
foreach($m in @('S1503Decision: Implemented','ProductiveCampaignRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S15-03 doc marker: $m"}}
foreach($m in @('MapGet("/api/crm/campaigns','MapPost("/api/crm/campaigns','MapPut("/api/crm/campaigns','MapDelete("/api/crm/campaigns','MapDelete("/api/crm/foundation/campaigns')){if($program.Contains($m)){throw "Forbidden Campaign route: $m"}}
if(-not(($next -match 'CRM Sprint 15 S15-0[4-7]') -and ($next -match 'sprint-15-campaign-management-s15-0[4-7]\.md'))){throw 'Invalid Sprint 15 forward handoff.'}
Write-Host 'CRM Sprint 15 S15-03 verification passed.'
