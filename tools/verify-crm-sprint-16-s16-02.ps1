$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Application/AccountManagement/AccountManagementApplicationContracts.cs',
 'src/CRM.Application/AccountManagement/IAccountManagementService.cs',
 'src/CRM.Application/AccountManagement/AccountManagementService.cs',
 'src/CRM.Application/Ports/Persistence/IAccountFoundationStore.cs',
 'src/CRM.Infrastructure/Persistence/Foundation/InMemoryAccountFoundationStore.cs',
 'tests/CRM.UnitTests/AccountManagementServiceTests.cs',
 'docs/roadmap/crm-sprint-16-s16-02-account-application-store-modernization.md',
 'codex/prompts/sprint-16-account-management-s16-03.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S16-02 artifact: $f"}}
$service=Get-Content 'src/CRM.Application/AccountManagement/AccountManagementService.cs' -Raw
$store=Get-Content 'src/CRM.Application/Ports/Persistence/IAccountFoundationStore.cs' -Raw
$infra=Get-Content 'src/CRM.Infrastructure/Persistence/Foundation/InMemoryAccountFoundationStore.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-16-s16-02-account-application-store-modernization.md' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('AccountManagementPolicy.Evaluate','GetAllAsync','GetByIdAsync','CreateAsync','UpdateAsync','ActivateAsync','DeactivateAsync','NonProductionSeam')){if(-not$service.Contains($m)){throw "Missing S16-02 service marker: $m"}}
foreach($m in @('AccountFoundationRecord','GetAllAsync','GetByIdAsync','SaveAsync')){if(-not$store.Contains($m)){throw "Missing S16-02 store marker: $m"}}
foreach($m in @('aaaaaaaa-1111-1111-1111-111111111111','Contoso Foundation','AccountStatus.Draft')){if(-not$infra.Contains($m)){throw "Missing S16-02 in-memory marker: $m"}}
foreach($m in @('S1602Decision: Implemented','ProductiveAccountRouteEnabled: false','DeleteBehaviorAdded: false','LeadConversionEnabled: false','AutomaticAccountCreationEnabled: false','ContactRelationshipMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S16-02 doc marker: $m"}}
foreach($f in @('MapGet("/api/crm/accounts','MapPost("/api/crm/accounts','MapPut("/api/crm/accounts','MapDelete("/api/crm/accounts','MapDelete("/api/crm/foundation/accounts')){if($program.Contains($f)){throw "Forbidden Account route: $f"}}
if(-not(($next -match 'CRM Sprint 16 S16-0[3-7]') -and ($next -match 'codex/prompts/sprint-16-account-management-s16-0[3-7]\.md'))){throw 'Invalid Sprint 16 S16-02 forward handoff.'}
Write-Host 'CRM Sprint 16 S16-02 verification passed.'
