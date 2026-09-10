$ErrorActionPreference='Stop'
$required=@(
 'docs/roadmap/crm-sprint-17-segment-management-functional-baseline.md',
 'codex/prompts/sprint-17-segment-management-s17-01.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing Sprint 17 P1 artifact: $f"}}
$baseline=Get-Content 'docs/roadmap/crm-sprint-17-segment-management-functional-baseline.md' -Raw
$prompt=Get-Content 'codex/prompts/sprint-17-segment-management-s17-01.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
$concept=Get-Content 'src/CRM.Domain/Entities/ConceptualEntities.cs' -Raw
$catalog=Get-Content 'src/CRM.Application/Contracts/CrmDomainCatalogService.cs' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($m in @('Sprint17P1BaseMainCommit: 6340f0e724efee9d35ecd661215e86a46960a814','SelectedSliceId: S17-SEGMENT','Sprint17P1Decision: ReadyForS1701SegmentContractsAndDomainRules','CanonicalSegmentFields: Id, Name, CriteriaSummary, Status','ProductiveSegmentRouteEnabled: false','FoundationSegmentRouteEnabledByP1: false','DeleteBehaviorAdded: false','ArbitraryCriteriaExecutionEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$baseline.Contains($m)){throw "Missing Sprint 17 P1 marker: $m"}}
if(-not($concept.Contains('public sealed record Segment') -and $concept.Contains('CriteriaSummary'))){throw 'Segment conceptual inventory missing.'}
if(-not($catalog.Contains('new("Segment"'))){throw 'Segment catalog evidence missing.'}
foreach($f in @('MapGet("/api/crm/segments','MapPost("/api/crm/segments','MapPut("/api/crm/segments','MapDelete("/api/crm/segments','MapDelete("/api/crm/foundation/segments')){if($program.Contains($f)){throw "Forbidden Segment route detected: $f"}}
foreach($m in @('CRM Sprint 17 S17-01 - Segment Contracts and Domain Rules','No DELETE','arbitrary criteria','/api/crm/segments','Common DB')){if(-not$prompt.Contains($m)){throw "S17-01 prompt missing $m"}}
if(-not(($next -match 'CRM Sprint 17 S17-0[1-7]') -and ($next -match 'codex/prompts/sprint-17-segment-management-s17-0[1-7]\.md'))){throw 'Invalid Sprint 17 forward handoff.'}
Write-Host 'CRM Sprint 17 P1 verification passed.'
