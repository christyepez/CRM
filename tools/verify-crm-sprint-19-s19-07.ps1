$ErrorActionPreference='Stop'
$required=@(
 'docs/releases/crm-sprint-19-interaction-management-closure.md',
 'docs/roadmap/crm-sprint-19-s19-06-interaction-local-integration-result.json',
 'codex/prompts/sprint-20-note-management-p1.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19-07 artifact: $f"}}
$closure=Get-Content 'docs/releases/crm-sprint-19-interaction-management-closure.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($m in @('Sprint19InteractionManagementClosed: true','S1907Decision: ClosedSuccessfully','ProductiveInteractionRouteEnabled: false','DeleteBehaviorAdded: false','ActivitySchedulingEnabled: false','CrossEntityMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','RecommendedNextSliceId: S20-NOTE','RecommendedNextSlice: Note Management Foundation')){if(-not$closure.Contains($m)){throw "Missing S19-07 closure marker: $m"}}
foreach($m in @('MapGet("/api/crm/interactions','MapPost("/api/crm/interactions','MapPut("/api/crm/interactions','MapDelete("/api/crm/interactions','MapDelete("/api/crm/foundation/interactions')){if($program.Contains($m)){throw "Forbidden S19-07 route: $m"}}
if(-not($next.Contains('CRM Sprint 20 P1 - Note Management Functional Baseline and Backlog') -and $next.Contains('codex/prompts/sprint-20-note-management-p1.md'))){throw 'Invalid Sprint 20 P1 handoff.'}
Write-Host 'CRM Sprint 19 S19-07 verification passed.'
