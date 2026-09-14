$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Domain/CaseManagement/CaseManagementPolicy.cs',
 'src/CRM.Domain/CaseManagement/CaseManagementCommand.cs',
 'src/CRM.Domain/CaseManagement/CaseManagementRuleResult.cs',
 'tests/CRM.UnitTests/CaseManagementPolicyTests.cs',
 'tests/CRM.ArchitectureTests/CaseManagementArchitectureTests.cs',
 'docs/roadmap/crm-sprint-18-s18-01-case-contracts-domain-rules.md',
 'codex/prompts/sprint-18-case-management-s18-02.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S18-01 artifact: $f"}}
$policy=Get-Content 'src/CRM.Domain/CaseManagement/CaseManagementPolicy.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-18-s18-01-case-contracts-domain-rules.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($m in @('MaxTitleLength = 160','MaxSummaryLength = 1000','CaseStatus.Open','CaseStatus.InProgress','CaseStatus.Resolved','CaseStatus.Closed','EvaluateStart','EvaluateResolve','EvaluateClose')){if(-not$policy.Contains($m)){throw "Missing S18-01 policy marker: $m"}}
foreach($m in @('S1801Decision: Implemented','CaseManagementDomain: Implemented','CaseApplicationService: NotImplemented','ProductiveCaseRouteEnabled: false','FoundationCaseRouteEnabled: false','DeleteBehaviorAdded: false','CustomerMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false','Port8094Touched: false')){if(-not$doc.Contains($m)){throw "Missing S18-01 doc marker: $m"}}
foreach($f in @('MapGet("/api/crm/cases','MapPost("/api/crm/cases','MapPut("/api/crm/cases','MapDelete("/api/crm/cases','MapDelete("/api/crm/foundation/cases')){if($program.Contains($f)){throw "Forbidden S18-01 Case route detected: $f"}}
if(-not(($next -match 'CRM Sprint 18 S18-0[2-7]') -and ($next -match 'codex/prompts/sprint-18-case-management-s18-0[2-7]\.md'))){throw 'Invalid Sprint 18 S18-01 handoff.'}
Write-Host 'CRM Sprint 18 S18-01 verification passed.'
