$ErrorActionPreference='Stop'
$required=@(
 'docs/releases/crm-sprint-16-account-management-closure.md',
 'docs/roadmap/crm-sprint-16-s16-06-account-local-integration.md',
 'docs/roadmap/crm-sprint-16-s16-06-account-local-integration-result.json',
 'codex/prompts/sprint-17-segment-management-p1.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S16-07 artifact: $f"}}
$closure=Get-Content 'docs/releases/crm-sprint-16-account-management-closure.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('Sprint16AccountManagementClosed: true','S1607Decision: ClosedSuccessfully','ProductiveAccountRouteEnabled: false','DeleteBehaviorAdded: false','LeadConversionEnabled: false','AutomaticAccountCreationEnabled: false','ContactRelationshipMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false','RecommendedNextSliceId: S17-SEGMENT','RecommendedNextSlice: Segment Management Foundation')){if(-not$closure.Contains($m)){throw "Missing S16-07 closure marker: $m"}}
if(-not($next.Contains('CRM Sprint 17 P1 - Segment Management Functional Baseline and Backlog') -and $next.Contains('codex/prompts/sprint-17-segment-management-p1.md'))){throw 'Invalid Sprint 17 P1 handoff.'}
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($forbidden in @('MapGet("/api/crm/accounts','MapPost("/api/crm/accounts','MapPut("/api/crm/accounts','MapDelete("/api/crm/accounts','MapDelete("/api/crm/foundation/accounts')){if($program.Contains($forbidden)){throw "Forbidden S16-07 productive/delete marker: $forbidden"}}
Write-Host 'CRM Sprint 16 S16-07 verification passed.'
