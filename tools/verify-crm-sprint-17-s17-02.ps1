$ErrorActionPreference='Stop'
$required=@('src/CRM.Application/SegmentManagement/SegmentManagementService.cs','src/CRM.Application/SegmentManagement/ISegmentManagementService.cs','src/CRM.Application/Ports/Persistence/ISegmentFoundationStore.cs','src/CRM.Infrastructure/Persistence/Foundation/InMemorySegmentFoundationStore.cs','tests/CRM.UnitTests/SegmentManagementServiceTests.cs','docs/roadmap/crm-sprint-17-s17-02-segment-application-store.md','codex/prompts/sprint-17-segment-management-s17-03.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S17-02 artifact: $f"}}
$service=Get-Content 'src/CRM.Application/SegmentManagement/SegmentManagementService.cs' -Raw
$store=Get-Content 'src/CRM.Infrastructure/Persistence/Foundation/InMemorySegmentFoundationStore.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-17-s17-02-segment-application-store.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('SegmentManagementPolicy.Evaluate','ISegmentFoundationStore','NonProductionSeam','ActivateAsync','DeactivateAsync')){if(-not$service.Contains($m)){throw "Missing S17-02 service marker: $m"}}
foreach($m in @('bbbbbbbb-2222-2222-2222-222222222222','SegmentStatus.Draft','SaveAsync')){if(-not$store.Contains($m)){throw "Missing S17-02 store marker: $m"}}
foreach($m in @('S1702Decision: Implemented','ProductiveSegmentRouteEnabled: false','FoundationSegmentRouteEnabled: false','DeleteBehaviorAdded: false','ArbitraryCriteriaExecutionEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S17-02 doc marker: $m"}}
if(-not(($next -match 'CRM Sprint 17 S17-0[3-7]') -and ($next -match 'codex/prompts/sprint-17-segment-management-s17-0[3-7]\.md'))){throw 'Invalid Sprint 17 S17-02 handoff.'}
Write-Host 'CRM Sprint 17 S17-02 verification passed.'