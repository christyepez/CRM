$ErrorActionPreference='Stop'
$required=@('src/CRM.Domain/SegmentManagement/SegmentManagementPolicy.cs','src/CRM.Domain/SegmentManagement/SegmentManagementCommand.cs','tests/CRM.UnitTests/SegmentManagementPolicyTests.cs','tests/CRM.ArchitectureTests/SegmentManagementArchitectureTests.cs','docs/roadmap/crm-sprint-17-s17-01-segment-contracts-domain-rules.md','codex/prompts/sprint-17-segment-management-s17-02.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S17-01 artifact: $f"}}
$policy=Get-Content 'src/CRM.Domain/SegmentManagement/SegmentManagementPolicy.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-17-s17-01-segment-contracts-domain-rules.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('MaxNameLength = 160','MaxCriteriaSummaryLength = 1000','EvaluateActivate','EvaluateDeactivate','SegmentStatus.Draft','SegmentStatus.Active','SegmentStatus.Inactive')){if(-not$policy.Contains($m)){throw "Missing S17-01 policy marker: $m"}}
foreach($m in @('S1701Decision: Implemented','ProductiveSegmentRouteEnabled: false','FoundationSegmentRouteEnabled: false','DeleteBehaviorAdded: false','ArbitraryCriteriaExecutionEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S17-01 doc marker: $m"}}
if(-not(($next -match 'CRM Sprint 17 S17-0[2-7]') -and ($next -match 'codex/prompts/sprint-17-segment-management-s17-0[2-7]\.md'))){throw 'Invalid Sprint 17 forward handoff.'}
Write-Host 'CRM Sprint 17 S17-01 verification passed.'
