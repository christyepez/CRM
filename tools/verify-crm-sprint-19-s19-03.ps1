$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Api/Foundation/InteractionManagementApiContracts.cs',
 'tests/CRM.UnitTests/InteractionFoundationApiEndpointTests.cs',
 'docs/roadmap/crm-sprint-19-s19-03-interaction-foundation-api.md',
 'codex/prompts/sprint-19-interaction-management-s19-04.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19-03 artifact: $f"}}
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($r in @('MapGet("/api/crm/foundation/interactions"','MapGet("/api/crm/foundation/interactions/{id}"','MapPost("/api/crm/foundation/interactions"','MapPut("/api/crm/foundation/interactions/{id}"','MapPost("/api/crm/foundation/interactions/{id}/void"')){if(-not$program.Contains($r)){throw "Missing Interaction foundation route: $r"}}
foreach($r in @('MapGet("/api/crm/interactions','MapPost("/api/crm/interactions','MapPut("/api/crm/interactions','MapDelete("/api/crm/interactions','MapDelete("/api/crm/foundation/interactions')){if($program.Contains($r)){throw "Forbidden S19-03 route: $r"}}
$doc=Get-Content 'docs/roadmap/crm-sprint-19-s19-03-interaction-foundation-api.md' -Raw
foreach($m in @('S1903Decision: Implemented','ValidationFailuresStatus: 400','MissingInteractionStatus: 404','VoidedInteractionModificationStatus: 409','ProductiveInteractionRouteEnabled: false','DeleteBehaviorAdded: false','AngularInteractionPageAdded: false')){if(-not$doc.Contains($m)){throw "Missing S19-03 doc marker: $m"}}
$next=Get-Content 'codex/next-task.md' -Raw
if(-not($next.Contains('CRM Sprint 19 S19-04 - Interaction Frontend Foundation Page') -and $next.Contains('codex/prompts/sprint-19-interaction-management-s19-04.md'))){throw 'Invalid S19-04 handoff.'}
Write-Host 'CRM Sprint 19 S19-03 verification passed.'
