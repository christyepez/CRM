$ErrorActionPreference='Stop'
$required=@(
 'tools/run-crm-sprint-18-s18-06-local-integration.ps1',
 'docs/roadmap/crm-sprint-18-s18-06-case-local-integration.md',
 'docs/roadmap/crm-sprint-18-s18-06-case-local-integration-result.json',
 'codex/prompts/sprint-18-case-management-s18-07.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S18-06 artifact: $f"}}
$e=Get-Content 'docs/roadmap/crm-sprint-18-s18-06-case-local-integration-result.json' -Raw | ConvertFrom-Json
if($e.S1806Decision-ne'Implemented'){throw 'S18-06 evidence not implemented'}
foreach($p in @('BackendHealth','FrontendToCaseApiConnectivity','CreateScenario','UpdateScenario','NoChangeScenario','InvalidCreateScenario','NotFoundScenario','InvalidTransitionScenario','StartScenario','RepeatStart','ResolveScenario','RepeatResolve','CloseScenario','RepeatClose')){if($e.$p-ne'PASS'){throw "S18-06 failed evidence: $p"}}
foreach($p in @('ReadAfterWriteConsistent')){if(-not $e.$p){throw "S18-06 false evidence: $p"}}
foreach($p in @('ProductiveCaseRouteAvailable','DeleteRouteAvailable','CustomerMutationObserved','AssignmentRuntimeObserved','SlaRuntimeObserved','NotificationRuntimeObserved','PortalRuntimeObserved','CommonDbRuntimeObserved','ExternalConnectorRuntimeObserved','RealDataDetected','SimulatedProductionTouched')){if($e.$p){throw "S18-06 unsafe evidence: $p"}}
$next=Get-Content 'codex/next-task.md' -Raw
if(-not($next.Contains('CRM Sprint 18 S18-07 - Case Management Sprint Closure') -and $next.Contains('codex/prompts/sprint-18-case-management-s18-07.md'))){throw 'Invalid S18-07 handoff'}
Write-Host 'CRM Sprint 18 S18-06 verification passed.'
