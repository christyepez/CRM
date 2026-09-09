$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot; Set-Location $root
$required=@('src/CRM.Domain/CampaignManagement/CampaignManagementOperation.cs','src/CRM.Domain/CampaignManagement/CampaignManagementCommand.cs','src/CRM.Domain/CampaignManagement/CampaignManagementErrorCode.cs','src/CRM.Domain/CampaignManagement/CampaignManagementRuleResult.cs','src/CRM.Domain/CampaignManagement/CampaignManagementPolicy.cs','tests/CRM.UnitTests/CampaignManagementPolicyTests.cs','tests/CRM.ArchitectureTests/CampaignManagementArchitectureTests.cs','docs/roadmap/crm-sprint-15-s15-01-campaign-contracts-domain-rules.md','codex/prompts/sprint-15-campaign-management-s15-02.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S15-01 artifact: $f"}}
$domain=Get-Content src/CRM.Domain/CampaignManagement/CampaignManagementPolicy.cs -Raw
$statuses=Get-Content src/CRM.Domain/Enums/CrmStatuses.cs -Raw
$doc=Get-Content docs/roadmap/crm-sprint-15-s15-01-campaign-contracts-domain-rules.md -Raw
$next=Get-Content codex/next-task.md -Raw
$program=Get-Content src/CRM.Api/Program.cs -Raw
foreach($m in @('CampaignManagementPolicy','MaxNameLength = 160','CampaignManagementOperation.Activate','CampaignManagementOperation.Complete','CampaignManagementOperation.Cancel','CampaignStatus.Draft','CampaignStatus.Active','CampaignStatus.Completed','CampaignStatus.Cancelled')){if(-not($domain+$statuses).Contains($m)){throw "Missing S15-01 domain marker: $m"}}
foreach($m in @('S1501Decision: Implemented','ProductiveCampaignRouteEnabled: false','FoundationCampaignRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S15-01 doc marker: $m"}}
foreach($m in @('MapGet("/api/crm/campaigns','MapPost("/api/crm/campaigns','MapPut("/api/crm/campaigns','MapDelete("/api/crm/campaigns','MapGet("/api/crm/foundation/campaigns','MapPost("/api/crm/foundation/campaigns','MapPut("/api/crm/foundation/campaigns','MapDelete("/api/crm/foundation/campaigns')){if($program.Contains($m)){throw "Forbidden Campaign runtime route: $m"}}
if(-not(($next -match 'CRM Sprint 15 S15-0[2-7]') -and ($next -match 'codex/prompts/sprint-15-campaign-management-s15-0[2-7]\.md'))){throw 'Invalid Sprint 15 forward handoff'}
Write-Host 'CRM Sprint 15 S15-01 verification passed.'
