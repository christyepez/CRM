$ErrorActionPreference='Stop'
$required=@('frontend/crm-web/src/main.ts','docs/roadmap/crm-sprint-17-s17-04-segment-frontend-foundation-page.md','codex/prompts/sprint-17-segment-management-s17-05.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S17-04 artifact: $f"}}
$front=Get-Content 'frontend/crm-web/src/main.ts' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-17-s17-04-segment-frontend-foundation-page.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @("'/api/crm/foundation/segments'","path: 'foundation/segments'",'SegmentManagementPageComponent','maxlength="160"','maxlength="1000"','activateSegment','deactivateSegment')){if(-not$front.Contains($m)){throw "Missing S17-04 frontend marker: $m"}}
foreach($m in @('S1704Decision: Implemented','ProductiveSegmentRouteEnabled: false','DeleteBehaviorAdded: false','ArbitraryCriteriaExecutionEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S17-04 doc marker: $m"}}
if($front.Contains("'/api/crm/segments'")){throw 'Productive Segment API found in frontend.'}
if(-not(($next -match 'CRM Sprint 17 S17-0[5-7]') -and ($next -match 'codex/prompts/sprint-17-segment-management-s17-0[5-7]\.md'))){throw 'Invalid S17-04 forward handoff.'}
Write-Host 'CRM Sprint 17 S17-04 verification passed.'