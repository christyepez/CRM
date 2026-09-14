$ErrorActionPreference='Stop'
$required=@(
 'docs/roadmap/crm-sprint-18-case-management-functional-baseline.md',
 'codex/prompts/sprint-18-case-management-s18-01.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing Sprint 18 P1 artifact: $f"}}
$baseline=Get-Content 'docs/roadmap/crm-sprint-18-case-management-functional-baseline.md' -Raw
$prompt=Get-Content 'codex/prompts/sprint-18-case-management-s18-01.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($m in @('SelectedSliceId: S18-CASE','Sprint18P1Decision: ReadyForS1801CaseContractsAndDomainRules','CanonicalCaseFields: Id, CustomerId, Title, Summary, Priority, Status','ProductiveCaseRouteEnabled: false','FoundationCaseRouteEnabledByP1: false','DeleteBehaviorAdded: false','CustomerMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false','Port8094Touched: false')){if(-not$baseline.Contains($m)){throw "Missing Sprint 18 P1 marker: $m"}}
foreach($f in @('MapGet("/api/crm/cases','MapPost("/api/crm/cases','MapPut("/api/crm/cases','MapDelete("/api/crm/cases','MapDelete("/api/crm/foundation/cases')){if($program.Contains($f)){throw "Forbidden Case route detected: $f"}}
foreach($m in @('CRM Sprint 18 S18-01 - Case Contracts and Domain Rules','No DELETE','Customer creation or mutation','/api/crm/cases','Common DB')){if(-not$prompt.Contains($m)){throw "S18-01 prompt missing $m"}}
if(-not(($next -match 'CRM Sprint 18 S18-0[1-7]') -and ($next -match 'codex/prompts/sprint-18-case-management-s18-0[1-7]\.md'))){throw 'Invalid Sprint 18 forward handoff.'}
Write-Host 'CRM Sprint 18 P1 verification passed.'
