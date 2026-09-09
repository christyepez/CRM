$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Api/Foundation/AccountManagementApiContracts.cs',
 'tests/CRM.UnitTests/AccountFoundationApiEndpointTests.cs',
 'tests/CRM.ArchitectureTests/AccountApiArchitectureTests.cs',
 'docs/roadmap/crm-sprint-16-s16-03-account-foundation-api-modernization.md',
 'codex/prompts/sprint-16-account-management-s16-04.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S16-03 artifact: $f"}}
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$contracts=Get-Content 'src/CRM.Api/Foundation/AccountManagementApiContracts.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-16-s16-03-account-foundation-api-modernization.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('IAccountManagementService','MapGet("/api/crm/foundation/accounts"','MapGet("/api/crm/foundation/accounts/{id}"','MapPost("/api/crm/foundation/accounts"','MapPut("/api/crm/foundation/accounts/{id}"','/activate"','/deactivate"')){if(-not$program.Contains($m)){throw "Missing S16-03 program marker: $m"}}
foreach($m in @('FoundationAccountManagementCreateRequest','FoundationAccountManagementUpdateRequest','AccountManagementApiResponse','AccountNotFound','InvalidStatusTransition')){if(-not$contracts.Contains($m)){throw "Missing S16-03 contract marker: $m"}}
foreach($m in @('S1603Decision: Implemented','ProductiveAccountRouteEnabled: false','DeleteBehaviorAdded: false','LeadConversionEnabled: false','AutomaticAccountCreationEnabled: false','ContactRelationshipMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S16-03 doc marker: $m"}}
foreach($f in @('MapGet("/api/crm/accounts','MapPost("/api/crm/accounts','MapPut("/api/crm/accounts','MapDelete("/api/crm/accounts','MapDelete("/api/crm/foundation/accounts')){if($program.Contains($f)){throw "Forbidden Account route: $f"}}
if(-not(($next -match 'CRM Sprint 16 S16-0[4-7]') -and ($next -match 'codex/prompts/sprint-16-account-management-s16-0[4-7]\.md'))){throw 'Invalid Sprint 16 S16-03 forward handoff.'}
Write-Host 'CRM Sprint 16 S16-03 verification passed.'
