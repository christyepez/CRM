$ErrorActionPreference='Stop'
$required=@(
 'docs/releases/crm-sprint-17-segment-management-closure.md',
 'docs/roadmap/crm-sprint-17-s17-06-segment-local-integration.md',
 'docs/roadmap/crm-sprint-17-s17-06-segment-local-integration-result.json',
 'codex/prompts/sprint-18-case-management-p1.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S17-07 artifact: $f"}}
$closure=Get-Content 'docs/releases/crm-sprint-17-segment-management-closure.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('Sprint17SegmentManagementClosed: true','S1707Decision: ClosedSuccessfully','ProductiveSegmentRouteEnabled: false','DeleteBehaviorAdded: false','CriteriaExecutionEnabled: false','CampaignTargetingEnabled: false','AccountAutoClassificationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false','RecommendedNextSliceId: S18-CASE','RecommendedNextSlice: Case Management Foundation')){if(-not$closure.Contains($m)){throw "Missing S17-07 closure marker: $m"}}
if(-not($next.Contains('CRM Sprint 18 P1 - Case Management Functional Baseline and Backlog') -and $next.Contains('codex/prompts/sprint-18-case-management-p1.md'))){throw 'Invalid Sprint 18 P1 handoff.'}
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($forbidden in @('MapGet("/api/crm/segments','MapPost("/api/crm/segments','MapPut("/api/crm/segments','MapDelete("/api/crm/segments','MapDelete("/api/crm/foundation/segments')){if($program.Contains($forbidden)){throw "Forbidden S17-07 productive/delete marker: $forbidden"}}
Write-Host 'CRM Sprint 17 S17-07 verification passed.'
