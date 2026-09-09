$ErrorActionPreference='Stop'
$required=@(
 'docs/roadmap/crm-sprint-15-campaign-management-closure.md',
 'docs/roadmap/crm-sprint-16-account-management-functional-baseline.md',
 'codex/prompts/sprint-16-account-management-s16-01.md',
 'codex/next-task.md',
 'src/CRM.Domain/Entities/ConceptualEntities.cs',
 'src/CRM.Domain/Enums/CrmStatuses.cs',
 'src/CRM.Application/Foundation/FoundationAccountCrudService.cs',
 'src/CRM.Api/Program.cs')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing Sprint 16 P1 artifact: $f"}}
$closure=Get-Content 'docs/roadmap/crm-sprint-15-campaign-management-closure.md' -Raw
$baseline=Get-Content 'docs/roadmap/crm-sprint-16-account-management-functional-baseline.md' -Raw
$prompt=Get-Content 'codex/prompts/sprint-16-account-management-s16-01.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
$concept=Get-Content 'src/CRM.Domain/Entities/ConceptualEntities.cs' -Raw
$statuses=Get-Content 'src/CRM.Domain/Enums/CrmStatuses.cs' -Raw
$service=Get-Content 'src/CRM.Application/Foundation/FoundationAccountCrudService.cs' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($m in @('S1507Decision: ClosedSuccessfully','Sprint15CampaignManagementClosed: true','SelectedNextCapability: Account Management Foundation')){if(-not$closure.Contains($m)){throw "Sprint 15 closure missing $m"}}
foreach($m in @('Sprint16P1BaseMainCommit: 90feb498ab4254abe9a3a2a755fb179035147f9e','SelectedSliceId: S16-ACCOUNT-MGMT','Sprint16P1Decision: ReadyForS1601AccountContractsAndDomainRules','FirstImplementationStoryId: S16-01','ProductiveAccountRouteEnabled: false','DeleteBehaviorAdded: false','LeadConversionEnabled: false','AutomaticAccountCreationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$baseline.Contains($m)){throw "Sprint 16 baseline missing $m"}}
if(-not($concept.Contains('public sealed class Account') -and $concept.Contains('ContactReferences') -and $statuses.Contains('public enum AccountStatus'))){throw 'Account domain inventory evidence missing.'}
foreach($m in @('FoundationAccountCrudService','IAccountFoundationStore','PreviewOnly','NonProductionSeam')){if(-not$service.Contains($m)){throw "Existing Account foundation evidence missing $m"}}
foreach($m in @('MapGet("/api/crm/foundation/accounts"','MapGet("/api/crm/foundation/accounts/{id}"','MapPost("/api/crm/foundation/accounts"','MapPut("/api/crm/foundation/accounts/{id}"')){if(-not$program.Contains($m)){throw "Existing Account route missing $m"}}
foreach($f in @('MapGet("/api/crm/accounts','MapPost("/api/crm/accounts','MapPut("/api/crm/accounts','MapDelete("/api/crm/accounts','MapDelete("/api/crm/foundation/accounts')){if($program.Contains($f)){throw "Forbidden Account route detected: $f"}}
foreach($m in @('CRM Sprint 16 S16-01 - Account Contracts and Domain Rules','No DELETE','Lead conversion','Productive `/api/crm/accounts`','Common DB/EF/migrations/schema/SQL/real data')){if(-not$prompt.Contains($m)){throw "S16-01 prompt missing $m"}}
if(-not(($next -match 'CRM Sprint 16 S16-0[1-7]') -and ($next -match 'codex/prompts/sprint-16-account-management-s16-0[1-7]\.md'))){throw 'Invalid Sprint 16 forward handoff.'}
Write-Host 'CRM Sprint 16 P1 verification passed.'
