$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot; Set-Location $root
$required=@(
 'docs/roadmap/crm-sprint-14-opportunity-pipeline-functional-baseline.md',
 'docs/roadmap/crm-sprint-14-s14-01-opportunity-pipeline-contracts-domain-rules.md',
 'docs/roadmap/crm-sprint-14-s14-02-opportunity-application-service-foundation-store.md',
 'docs/roadmap/crm-sprint-14-s14-03-opportunity-foundation-api.md',
 'docs/roadmap/crm-sprint-14-s14-04-opportunity-pipeline-frontend-foundation.md',
 'docs/roadmap/crm-sprint-14-s14-05-opportunity-pipeline-test-guardrail-hardening.md',
 'docs/roadmap/crm-sprint-14-s14-06-opportunity-pipeline-local-integration.md',
 'docs/roadmap/crm-sprint-14-opportunity-pipeline-closure.md',
 'codex/prompts/sprint-15-campaign-management-p1.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing Sprint 14 closure evidence: $f"}}
$closure=Get-Content docs/roadmap/crm-sprint-14-opportunity-pipeline-closure.md -Raw
$tasks=Get-Content codex/TASKS.md -Raw
$next=Get-Content codex/next-task.md -Raw
foreach($m in @('S1407Decision: ClosedSuccessfully','Sprint14OpportunityPipelineClosed: true','ProductiveOpportunityRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false','RecommendedNextSliceId: S15-CAMPAIGN','RecommendedNextSlice: Campaign Management Foundation')){if(-not($closure.Contains($m) -and $tasks.Contains($m))){throw "Missing S14-07 marker: $m"}}
if(-not($next.Contains('CRM Sprint 15 P1 - Campaign Management Functional Baseline and Backlog') -and $next.Contains('codex/prompts/sprint-15-campaign-management-p1.md') -and $next.Contains('S14-07 merge commit required'))){throw 'Invalid Sprint 15 P1 handoff'}
$program=Get-Content src/CRM.Api/Program.cs -Raw
foreach($m in @('MapGet("/api/crm/opportunities','MapPost("/api/crm/opportunities','MapPut("/api/crm/opportunities','MapDelete("/api/crm/opportunities','MapDelete("/api/crm/foundation/opportunities')){if($program.Contains($m)){throw "Forbidden Opportunity route marker: $m"}}
Write-Host 'CRM Sprint 14 S14-07 verification passed.'
