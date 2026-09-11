$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Application/CaseManagement/CaseManagementApplicationContracts.cs',
 'src/CRM.Application/CaseManagement/ICaseManagementService.cs',
 'src/CRM.Application/CaseManagement/CaseManagementService.cs',
 'src/CRM.Application/Ports/Persistence/ICaseFoundationStore.cs',
 'src/CRM.Infrastructure/Persistence/Foundation/InMemoryCaseFoundationStore.cs',
 'tests/CRM.UnitTests/CaseManagementServiceTests.cs',
 'docs/roadmap/crm-sprint-18-s18-02-case-application-service-foundation-store.md',
 'codex/prompts/sprint-18-case-management-s18-03.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S18-02 artifact: $f"}}

$service=Get-Content 'src/CRM.Application/CaseManagement/CaseManagementService.cs' -Raw
$store=Get-Content 'src/CRM.Infrastructure/Persistence/Foundation/InMemoryCaseFoundationStore.cs' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-18-s18-02-case-application-service-foundation-store.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw

foreach($m in @('CaseManagementPolicy.Evaluate','ICaseFoundationStore','NonProductionSeam','StartAsync','ResolveAsync','CloseAsync')){
    if(-not$service.Contains($m)){throw "Missing S18-02 service marker: $m"}
}
foreach($m in @('55555555-5555-5555-5555-555555555555','11111111-1111-1111-1111-111111111111','CasePriority.Medium','CaseStatus.Open','SaveAsync')){
    if(-not$store.Contains($m)){throw "Missing S18-02 store marker: $m"}
}
foreach($m in @('ICaseManagementService, CaseManagementService','ICaseFoundationStore, InMemoryCaseFoundationStore')){
    if(-not$program.Contains($m)){throw "Missing S18-02 DI marker: $m"}
}
foreach($route in @('MapGet("/api/crm/cases','MapPost("/api/crm/cases','MapPut("/api/crm/cases','MapDelete("/api/crm/cases','MapDelete("/api/crm/foundation/cases')){
    if($program.Contains($route)){throw "Forbidden S18-02 Case route detected: $route"}
}
foreach($m in @('S1802Decision: Implemented','CaseManagementImplementationStatus: ApplicationAndFoundationStoreImplemented','ProductiveCaseRouteEnabled: false','FoundationCaseRouteEnabled: false','DeleteBehaviorAdded: false','CustomerMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false','Port8094Touched: false')){
    if(-not$doc.Contains($m)){throw "Missing S18-02 doc marker: $m"}
}
if(-not(($next -match 'CRM Sprint 18 S18-0[3-7]') -and ($next -match 'codex/prompts/sprint-18-case-management-s18-0[3-7]\.md'))){throw 'Invalid S18 forward handoff.'}
Write-Host 'CRM Sprint 18 S18-02 verification passed.'
