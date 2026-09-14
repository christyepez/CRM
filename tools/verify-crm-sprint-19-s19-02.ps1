$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Application/InteractionManagement/InteractionManagementService.cs',
 'src/CRM.Application/InteractionManagement/IInteractionManagementService.cs',
 'src/CRM.Application/Ports/Persistence/IInteractionFoundationStore.cs',
 'src/CRM.Infrastructure/Persistence/Foundation/InMemoryInteractionFoundationStore.cs',
 'tests/CRM.UnitTests/InteractionManagementServiceTests.cs',
 'docs/roadmap/crm-sprint-19-s19-02-interaction-application-foundation-store.md',
 'codex/prompts/sprint-19-interaction-management-s19-03.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19-02 artifact: $f"}}
$service=Get-Content 'src/CRM.Application/InteractionManagement/InteractionManagementService.cs' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-19-s19-02-interaction-application-foundation-store.md' -Raw
foreach($m in @('InteractionManagementPolicy.Evaluate','IInteractionFoundationStore','NonProductionSeam','VoidAsync')){if(-not$service.Contains($m)){throw "Missing S19-02 service marker: $m"}}
foreach($m in @('IInteractionManagementService, InteractionManagementService','IInteractionFoundationStore, InMemoryInteractionFoundationStore')){if(-not$program.Contains($m)){throw "Missing S19-02 DI marker: $m"}}
if($program.Contains('/api/crm/foundation/interactions')){throw 'Foundation Interaction route added too early.'}
foreach($m in @('S1902Decision: Implemented','ProductiveInteractionRouteEnabled: false','FoundationInteractionRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S19-02 doc marker: $m"}}
Write-Host 'CRM Sprint 19 S19-02 verification passed.'
foreach($route in @('/api/crm/foundation/interactions','/api/crm/interactions')){
 if($program.Contains($route)){throw "Interaction route must not exist in S19-02: $route"}
}
$next=Get-Content 'codex/next-task.md' -Raw
if(-not($next.Contains('CRM Sprint 19 S19-03 - Interaction Foundation API') -and $next.Contains('codex/prompts/sprint-19-interaction-management-s19-03.md'))){throw 'Invalid S19-03 handoff.'}
Write-Host 'CRM Sprint 19 S19-02 verification passed.'
