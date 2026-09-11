$ErrorActionPreference='Stop'
$required=@(
 'docs/releases/crm-sprint-18-case-management-closure.md',
 'codex/prompts/sprint-19-interaction-management-p1.md',
 'codex/next-task.md',
 'docs/roadmap/crm-sprint-18-s18-06-case-local-integration-result.json')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S18-07 artifact: $f"}}
$c=Get-Content 'docs/releases/crm-sprint-18-case-management-closure.md' -Raw
$n=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('Sprint18CaseManagementClosed: true','S1807Decision: ClosedSuccessfully','RecommendedNextSliceId: S19-INTERACTION','RecommendedNextSlice: Interaction Management Foundation','ProductiveCaseRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$c.Contains($m)){throw "Missing S18-07 closure marker: $m"}}
if(-not($n.Contains('CRM Sprint 19 P1 - Interaction Management Functional Baseline and Backlog') -and $n.Contains('codex/prompts/sprint-19-interaction-management-p1.md'))){throw 'Invalid Sprint 19 P1 handoff.'}
Write-Host 'CRM Sprint 18 S18-07 verification passed.'