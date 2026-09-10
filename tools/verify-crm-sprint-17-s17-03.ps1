$ErrorActionPreference='Stop'
$required=@('src/CRM.Api/Foundation/SegmentManagementApiContracts.cs','tests/CRM.UnitTests/SegmentFoundationApiTests.cs','tests/CRM.ArchitectureTests/SegmentFoundationApiArchitectureTests.cs','docs/roadmap/crm-sprint-17-s17-03-segment-foundation-api.md','codex/prompts/sprint-17-segment-management-s17-04.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S17-03 artifact: $f"}}
$p=Get-Content 'src/CRM.Api/Program.cs' -Raw
$d=Get-Content 'docs/roadmap/crm-sprint-17-s17-03-segment-foundation-api.md' -Raw
$n=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('/api/crm/foundation/segments','ISegmentManagementService','ISegmentFoundationStore','InMemorySegmentFoundationStore','ActivateCrmFoundationSegment','DeactivateCrmFoundationSegment')){if(-not$p.Contains($m)){throw "Missing S17-03 Program marker: $m"}}
foreach($f in @('MapGet("/api/crm/segments','MapPost("/api/crm/segments','MapPut("/api/crm/segments','MapDelete("/api/crm/segments','MapDelete("/api/crm/foundation/segments')){if($p.Contains($f)){throw "Forbidden Segment route: $f"}}
foreach($m in @('S1703Decision: Implemented','ProductiveSegmentRouteEnabled: false','DeleteBehaviorAdded: false','ArbitraryCriteriaExecutionEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$d.Contains($m)){throw "Missing S17-03 doc marker: $m"}}
if(-not(($n -match 'CRM Sprint 17 S17-0[4-7]') -and ($n -match 'codex/prompts/sprint-17-segment-management-s17-0[4-7]\.md'))){throw 'Invalid S17-03 forward handoff.'}
Write-Host 'CRM Sprint 17 S17-03 verification passed.'
