$ErrorActionPreference="Stop"
$root=Split-Path -Parent $PSScriptRoot
Set-Location $root
$required=@(
 "tools/run-crm-sprint-14-s14-06-local-integration.ps1",
 "docs/roadmap/crm-sprint-14-s14-06-opportunity-pipeline-local-integration.md",
 "codex/prompts/sprint-14-opportunity-pipeline-s14-07.md",
 "codex/next-task.md")
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S14-06 file: $f"}}
$doc=Get-Content docs/roadmap/crm-sprint-14-s14-06-opportunity-pipeline-local-integration.md -Raw
$runner=Get-Content tools/run-crm-sprint-14-s14-06-local-integration.ps1 -Raw
$next=Get-Content codex/next-task.md -Raw
$tasks=Get-Content codex/TASKS.md -Raw
foreach($m in @("S1406Decision: Implemented","OpportunityPipelineLocalIntegration: Validated","RuntimePersistenceClassification: FoundationOnly","ReadAfterWriteConsistent: true","ProductiveOpportunityRouteEnabled: false","DeleteBehaviorAdded: false","PortalRuntimeObserved: false","CommonDbRuntimeObserved: false","RealDataDetected: false","SimulatedProductionTouched: false","LatencyAverageMs: 18.78","LatencyP95Ms: 56")){if(-not($doc.Contains($m) -and $tasks.Contains($m))){throw "Missing S14-06 evidence marker: $m"}}
foreach($m in @("/api/crm/foundation/opportunities","/foundation/opportunities","ProgressNextStage","SameStageRejected","SkipStageRejected","RepeatWin","RepeatLose","RepeatCancel","TerminalUpdateRejected","TerminalProgressRejected","ProductiveOpportunityRouteAvailable","DeleteRouteAvailable")){if(-not$runner.Contains($m)){throw "Runner missing S14-06 marker: $m"}}
if(-not($next.Contains("CRM Sprint 14 S14-07 - Opportunity Pipeline Sprint Closure") -and $next.Contains("codex/prompts/sprint-14-opportunity-pipeline-s14-07.md") -and $next.Contains("S14-06 merge commit required"))){throw "next-task must point to S14-07 closure"}
$program=Get-Content src/CRM.Api/Program.cs -Raw
foreach($f in @('MapGet("/api/crm/opportunities','MapPost("/api/crm/opportunities','MapPut("/api/crm/opportunities','MapDelete("/api/crm/opportunities','MapDelete("/api/crm/foundation/opportunities')){if($program.Contains($f)){throw "Forbidden productive/delete Opportunity marker: $f"}}
Write-Host "CRM Sprint 14 S14-06 verification passed."
