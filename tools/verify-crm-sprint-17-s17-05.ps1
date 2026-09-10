$ErrorActionPreference='Stop'
$required=@('tests/CRM.ArchitectureTests/SegmentCrossLayerGuardrailTests.cs','tests/CRM.UnitTests/SegmentFoundationApiHardeningTests.cs','docs/roadmap/crm-sprint-17-s17-05-segment-test-guardrail-hardening.md','codex/prompts/sprint-17-segment-management-s17-06.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S17-05 artifact: $f"}}
$arch=Get-Content 'tests/CRM.ArchitectureTests/SegmentCrossLayerGuardrailTests.cs' -Raw
$front=Get-Content 'frontend/crm-web/src/main.ts' -Raw
$api=Get-Content 'tests/CRM.UnitTests/SegmentFoundationApiHardeningTests.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-17-s17-05-segment-test-guardrail-hardening.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('/api/crm/foundation/segments','SegmentManagementPolicy.Evaluate','ISegmentFoundationStore')){if(-not$arch.Contains($m)){throw "Missing S17-05 architecture marker: $m"}}
foreach($m in @('maxlength="160"','maxlength="1000"','activateSegment','deactivateSegment')){if(-not$front.Contains($m)){throw "Missing S17-05 frontend marker: $m"}}
foreach($m in @('Update_NoChange_ReturnsChangedFalse','Activate_Repeated_IsIdempotent','Deactivate_Draft_ReturnsConflict','Update_MissingSegment_ReturnsNotFound','InvalidStatusTransition')){if(-not$api.Contains($m)){throw "Missing S17-05 API hardening marker: $m"}}
foreach($m in @('S1705Decision: Implemented','ProductiveSegmentRouteEnabled: false','DeleteBehaviorAdded: false','ArbitraryCriteriaExecutionEnabled: false','CampaignTargetingEnabled: false','AccountAutoClassificationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S17-05 doc marker: $m"}}
if(-not(($next -match 'CRM Sprint 17 S17-0[6-7]') -and ($next -match 'codex/prompts/sprint-17-segment-management-s17-0[6-7]\.md'))){throw 'Invalid Sprint 17 S17-05 handoff.'}
Write-Host 'CRM Sprint 17 S17-05 verification passed.'