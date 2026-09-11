$ErrorActionPreference='Stop'
$required=@('src/CRM.Domain/InteractionManagement/InteractionEnums.cs','src/CRM.Domain/InteractionManagement/InteractionManagementContracts.cs','src/CRM.Domain/InteractionManagement/InteractionManagementPolicy.cs','tests/CRM.UnitTests/InteractionManagementPolicyTests.cs','docs/roadmap/crm-sprint-19-s19-01-interaction-contracts-domain-rules.md','codex/prompts/sprint-19-interaction-management-s19-02.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19-01 artifact: $f"}}
$p=Get-Content 'src/CRM.Domain/InteractionManagement/InteractionManagementPolicy.cs' -Raw
$d=Get-Content 'docs/roadmap/crm-sprint-19-s19-01-interaction-contracts-domain-rules.md' -Raw
$n=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('MaxSubjectLength = 160','MaxSummaryLength = 2000','InteractionManagementOperation.Create','InteractionManagementOperation.Update','InteractionManagementOperation.Void','OccurredAtUtcMustBeUtc','OccurredAtUtcInFuture','VoidedInteractionCannotBeModified')){if(-not$p.Contains($m)){throw "Missing S19-01 policy marker: $m"}}
foreach($m in @('S1901Decision: Implemented','ActivitySchedulingDuplicated: false','CrossEntityMutationEnabled: false','ProductiveInteractionRouteEnabled: false','FoundationInteractionRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$d.Contains($m)){throw "Missing S19-01 doc marker: $m"}}
if(-not($n.Contains('CRM Sprint 19 S19-02') -and $n.Contains('sprint-19-interaction-management-s19-02.md'))){throw 'Invalid S19-02 handoff'}
Write-Host 'CRM Sprint 19 S19-01 verification passed.'
