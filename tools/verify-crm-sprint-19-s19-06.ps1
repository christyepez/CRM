$ErrorActionPreference='Stop'
$required=@(
 'docs/roadmap/crm-sprint-19-s19-06-interaction-local-integration-validation.md',
 'docs/roadmap/crm-sprint-19-s19-06-interaction-local-integration-result.json',
 'tools/run-crm-sprint-19-s19-06-local-integration.ps1',
 'codex/prompts/sprint-19-interaction-management-s19-07.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19-06 artifact: $f"}}
$doc=Get-Content 'docs/roadmap/crm-sprint-19-s19-06-interaction-local-integration-validation.md' -Raw
$result=Get-Content 'docs/roadmap/crm-sprint-19-s19-06-interaction-local-integration-result.json' -Raw | ConvertFrom-Json
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('S1906Decision: Passed','ProductiveInteractionRouteEnabled: false','DeleteBehaviorAdded: false','ActivitySchedulingEnabled: false','CrossEntityMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S19-06 marker: $m"}}
foreach($p in @('BackendHealth','FrontendToInteractionApiConnectivity','CreateScenario','ReadAfterCreate','UpdateScenario','NoChangeScenario','InvalidCreateScenario','NotFoundScenario','VoidScenario','RepeatVoid','VoidedUpdateConflict')){if($result.$p-ne'PASS'){throw "S19-06 result not PASS: $p"}}
if($result.ProductiveInteractionRouteAvailable -or $result.DeleteRouteAvailable -or $result.PortalRuntimeObserved -or $result.CommonDbRuntimeObserved -or $result.RealDataDetected -or $result.SimulatedProductionTouched){throw 'S19-06 safety evidence failed.'}
if(-not($next.Contains('CRM Sprint 19 S19-07 - Interaction Management Sprint Closure') -and $next.Contains('codex/prompts/sprint-19-interaction-management-s19-07.md'))){throw 'Invalid S19-07 handoff.'}
Write-Host 'CRM Sprint 19 S19-06 verification passed.'
