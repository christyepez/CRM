$ErrorActionPreference='Stop'
$required=@(
 'tools/run-crm-sprint-17-s17-06-local-integration.ps1',
 'docs/roadmap/crm-sprint-17-s17-06-segment-local-integration.md',
 'docs/roadmap/crm-sprint-17-s17-06-segment-local-integration-result.json',
 'codex/prompts/sprint-17-segment-management-s17-07.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S17-06 artifact: $f"}}
$doc=Get-Content 'docs/roadmap/crm-sprint-17-s17-06-segment-local-integration.md' -Raw
$runner=Get-Content 'tools/run-crm-sprint-17-s17-06-local-integration.ps1' -Raw
$result=Get-Content 'docs/roadmap/crm-sprint-17-s17-06-segment-local-integration-result.json' -Raw | ConvertFrom-Json
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('S1706Decision: Implemented','SegmentManagementLocalIntegration: Validated','ProductiveSegmentRouteEnabled: false','DeleteBehaviorAdded: false','CriteriaExecutionEnabled: false','CampaignTargetingEnabled: false','AccountAutoClassificationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S17-06 doc marker: $m"}}
foreach($m in @('/api/crm/foundation/segments','/api/crm/segments','/foundation/segments','FoundationOnly')){if(-not$runner.Contains($m)){throw "Missing S17-06 runner marker: $m"}}
if($result.S1706Decision -ne 'Implemented'){throw 'S17-06 result decision mismatch.'}
if($result.BackendHealth -ne 'PASS' -or $result.FrontendToSegmentApiConnectivity -ne 'PASS'){throw 'S17-06 runtime connectivity evidence failed.'}
if(-not$result.ReadAfterWriteConsistent){throw 'S17-06 read-after-write evidence failed.'}
if($result.ProductiveSegmentRouteAvailable -or $result.DeleteRouteAvailable -or $result.CriteriaExecutionObserved -or $result.CampaignTargetingObserved -or $result.AccountAutoClassificationObserved -or $result.PortalRuntimeObserved -or $result.CommonDbRuntimeObserved -or $result.ExternalConnectorRuntimeObserved -or $result.RealDataDetected -or $result.SimulatedProductionTouched){throw 'S17-06 safety evidence failed.'}
if(-not($next.Contains('CRM Sprint 17 S17-07 - Segment Management Sprint Closure') -and $next.Contains('codex/prompts/sprint-17-segment-management-s17-07.md'))){throw 'Invalid S17-07 handoff.'}
Write-Host 'CRM Sprint 17 S17-06 verification passed.'
