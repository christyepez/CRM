$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot; Set-Location $root
$req=@('tests/CRM.ArchitectureTests/OpportunityCrossLayerGuardrailTests.cs','docs/roadmap/crm-sprint-14-s14-05-opportunity-pipeline-test-guardrail-hardening.md','codex/prompts/sprint-14-opportunity-pipeline-s14-06.md')
foreach($f in $req){if(-not(Test-Path $f)){throw "Missing S14-05 artifact $f"}}
$program=Get-Content src/CRM.Api/Program.cs -Raw; $front=Get-Content frontend/crm-web/src/main.ts -Raw; $doc=Get-Content docs/roadmap/crm-sprint-14-s14-05-opportunity-pipeline-test-guardrail-hardening.md -Raw; $n=Get-Content codex/next-task.md -Raw
foreach($m in @('StageCatalogParityGuarded: true','NextStageOnlyGuarded: true','TerminalReadOnlyGuarded: true','FoundationRouteOnlyGuarded: true','ProductiveOpportunityRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S14-05 marker $m"}}
foreach($m in @('MapGet("/api/crm/opportunities','MapPost("/api/crm/opportunities','MapPut("/api/crm/opportunities','MapDelete("/api/crm/opportunities','MapDelete("/api/crm/foundation/opportunities')){if($program.Contains($m)){throw "Forbidden route $m"}}
if($front.Contains('/api/crm/opportunities')){throw 'Frontend productive Opportunity route detected'}
$s1406=$n.Contains('CRM Sprint 14 S14-06 - Opportunity Pipeline Local Integration Validation') -and $n.Contains('codex/prompts/sprint-14-opportunity-pipeline-s14-06.md') -and $n.Contains('S14-05 merge commit required'); $s1407=$n.Contains('CRM Sprint 14 S14-07 - Opportunity Pipeline Sprint Closure') -and $n.Contains('codex/prompts/sprint-14-opportunity-pipeline-s14-07.md') -and $n.Contains('S14-06 merge commit required'); $s15=$n.Contains('CRM Sprint 15 P1 - Campaign Management Functional Baseline and Backlog') -and $n.Contains('codex/prompts/sprint-15-campaign-management-p1.md') -and $n.Contains('S14-07 merge commit required'); if(-not($s1406 -or $s1407 -or $s15)){throw 'Invalid Sprint 14/15 forward handoff'}
Write-Host 'CRM Sprint 14 S14-05 verification passed.'

