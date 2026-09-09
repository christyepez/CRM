$ErrorActionPreference='Stop'
$required=@(
'src/CRM.Application/CampaignManagement/ICampaignManagementService.cs','src/CRM.Application/CampaignManagement/CampaignManagementService.cs','src/CRM.Application/CampaignManagement/CampaignManagementApplicationContracts.cs','src/CRM.Application/Ports/Persistence/ICampaignFoundationStore.cs','src/CRM.Infrastructure/Persistence/Foundation/InMemoryCampaignFoundationStore.cs','tests/CRM.UnitTests/CampaignManagementServiceTests.cs','docs/roadmap/crm-sprint-15-s15-02-campaign-application-service-foundation-store.md','codex/prompts/sprint-15-campaign-management-s15-03.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S15-02 artifact: $f"}}
$service=Get-Content 'src/CRM.Application/CampaignManagement/CampaignManagementService.cs' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('CampaignManagementPolicy.Evaluate','ICampaignFoundationStore','CreateAsync','UpdateAsync','ActivateAsync','CompleteAsync','CancelAsync')){if(-not$service.Contains($m)){throw "S15-02 service missing $m"}}
foreach($forbidden in @('MapGet("/api/crm/campaigns','MapPost("/api/crm/campaigns','MapPut("/api/crm/campaigns','MapDelete("/api/crm/campaigns','MapDelete("/api/crm/foundation/campaigns')){if($program.Contains($forbidden)){throw "Forbidden Campaign route: $forbidden"}}
if(-not(($next -match 'CRM Sprint 15 S15-0[3-7]') -and ($next -match 'sprint-15-campaign-management-s15-0[3-7]\.md'))){throw 'Invalid Sprint 15 forward handoff.'}
Write-Host 'CRM Sprint 15 S15-02 verification passed.'
