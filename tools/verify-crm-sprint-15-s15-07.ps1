$ErrorActionPreference='Stop'
$required=@(
 'docs/roadmap/crm-sprint-15-campaign-management-closure.md',
 'codex/prompts/sprint-16-account-management-p1.md',
 'codex/next-task.md',
 'src/CRM.Domain/Entities/ConceptualEntities.cs',
 'src/CRM.Api/Program.cs')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S15-07 artifact: $f"}}
$doc=Get-Content 'docs/roadmap/crm-sprint-15-campaign-management-closure.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
$prompt=Get-Content 'codex/prompts/sprint-16-account-management-p1.md' -Raw
$concept=Get-Content 'src/CRM.Domain/Entities/ConceptualEntities.cs' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($m in @('S1507Decision: ClosedSuccessfully','Sprint15CampaignManagementClosed: true','SelectedNextCapability: Account Management Foundation','SelectedNextSprint: CRM Sprint 16 P1 - Account Management Functional Baseline and Backlog','Productive Campaign route enabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S15-07 closure marker: $m"}}
foreach($m in @('CRM Sprint 16 P1 - Account Management Functional Baseline and Backlog','codex/prompts/sprint-16-account-management-p1.md')){if(-not$next.Contains($m)){throw "Invalid Sprint 16 handoff: $m"}}
foreach($m in @('Account Management Functional Baseline and Backlog','no Lead conversion','no Productive `/api/crm/accounts`','no DELETE','no Portal Auth/token/user assignment runtime','no Common DB/EF/migrations/schema/SQL/real data')){if(-not$prompt.Contains($m)){throw "Missing Sprint 16 P1 guardrail marker: $m"}}
if(-not($concept.Contains('public sealed class Account') -and $concept.Contains('AccountStatus') -and $concept.Contains('ContactReferences'))){throw 'Account domain evidence missing.'}
foreach($f in @('MapGet("/api/crm/accounts','MapPost("/api/crm/accounts','MapPut("/api/crm/accounts','MapDelete("/api/crm/accounts','MapDelete("/api/crm/foundation/campaigns')){if($program.Contains($f)){throw "Forbidden productive/delete runtime marker: $f"}}
Write-Host 'CRM Sprint 15 S15-07 verification passed.'
