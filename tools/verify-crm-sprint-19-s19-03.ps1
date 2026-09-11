$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Api/Foundation/InteractionManagementApiContracts.cs',
 'tests/CRM.UnitTests/InteractionFoundationApiEndpointTests.cs',
 'docs/roadmap/crm-sprint-19-s19-03-interaction-foundation-api.md',
 'codex/prompts/sprint-19-interaction-management-s19-04.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19-03 artifact: $f"}}
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$contracts=Get-Content 'src/CRM.Api/Foundation/InteractionManagementApiContracts.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-19-s19-03-interaction-foundation-api.md' -Raw
foreach($m in @('/api/crm/foundation/interactions','IInteractionManagementService service','InteractionManagementApiResponse.ToApplicationRequest','InteractionManagementApiResponse.ToStatusCode')){if(-not$program.Contains($m)){throw "Missing S19-03 API marker: $m"}}
foreach($m in @('FoundationInteractionCreateRequest','FoundationInteractionUpdateRequest','Status404NotFound','Status409Conflict','Status400BadRequest')){if(-not$contracts.Contains($m)){throw "Missing S19-03 contract marker: $m"}}
foreach($m in @('MapGet("/api/crm/interactions','MapPost("/api/crm/interactions','MapPut("/api/crm/interactions','MapDelete("/api/crm/interactions','MapDelete("/api/crm/foundation/interactions')){if($program.Contains($m)){throw "Forbidden S19-03 route: $m"}}
foreach($m in @('S1903Decision: Implemented','ProductiveInteractionRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S19-03 doc marker: $m"}}
Write-Host 'CRM Sprint 19 S19-03 verification passed.'
