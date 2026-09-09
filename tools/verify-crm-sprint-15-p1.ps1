$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
Set-Location $root

$required = @(
  'docs/roadmap/crm-sprint-14-opportunity-pipeline-closure.md',
  'docs/roadmap/crm-sprint-15-campaign-management-functional-baseline.md',
  'codex/prompts/sprint-15-campaign-management-s15-01.md',
  'codex/next-task.md',
  'src/CRM.Domain/Entities/ConceptualEntities.cs',
  'src/CRM.Domain/Entities/Lead.cs',
  'src/CRM.Domain/ValueObjects/BusinessValueObjects.cs',
  'src/CRM.Api/Program.cs',
  'frontend/crm-web/src/main.ts'
)
foreach($f in $required){ if(-not(Test-Path $f)){ throw "Missing Sprint 15 P1 artifact: $f" } }

$closure = Get-Content 'docs/roadmap/crm-sprint-14-opportunity-pipeline-closure.md' -Raw
$baseline = Get-Content 'docs/roadmap/crm-sprint-15-campaign-management-functional-baseline.md' -Raw
$prompt = Get-Content 'codex/prompts/sprint-15-campaign-management-s15-01.md' -Raw
$next = Get-Content 'codex/next-task.md' -Raw
$concept = Get-Content 'src/CRM.Domain/Entities/ConceptualEntities.cs' -Raw
$lead = Get-Content 'src/CRM.Domain/Entities/Lead.cs' -Raw
$valueObjects = Get-Content 'src/CRM.Domain/ValueObjects/BusinessValueObjects.cs' -Raw
$program = Get-Content 'src/CRM.Api/Program.cs' -Raw
$frontend = Get-Content 'frontend/crm-web/src/main.ts' -Raw

foreach($m in @('S1407Decision: ClosedSuccessfully','RecommendedNextSliceId: S15-CAMPAIGN','RecommendedNextSlice: Campaign Management Foundation')){ if(-not$closure.Contains($m)){ throw "Sprint 14 closure missing $m" } }
foreach($m in @('Sprint15P1BaseMainCommit: e0bad427f0cb482bd784399345b6f45e4f230d9d','SelectedSliceId: S15-CAMPAIGN','Sprint15P1Decision: ReadyForS1501CampaignContractsAndDomainRules','FirstImplementationStoryId: S15-01','CampaignDomainStatus: ConceptualRecordOnly','LeadCampaignAttributionStatus: StructuralFieldOnly','ProductiveCampaignRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false','SimulatedProductionTouched: false','FirstImplementationPrompt: codex/prompts/sprint-15-campaign-management-s15-01.md')){ if(-not$baseline.Contains($m)){ throw "Sprint 15 baseline missing $m" } }
foreach($m in @('CRM Sprint 15 S15-01 - Campaign Contracts and Domain Rules','CampaignManagementPolicy','Draft | Active | Completed | Cancelled','Do not add `/api/crm/campaigns`','Do not add DELETE','Do not activate Portal Auth/token/header runtime','Do not activate Common DB/SQL/EF')){ if(-not$prompt.Contains($m)){ throw "S15-01 prompt missing $m" } }
if(-not($concept.Contains('public sealed record Campaign') -and $concept.Contains('DateRange ActiveRange'))){ throw 'Existing Campaign concept not found.' }
if(-not($lead.Contains('CampaignId'))){ throw 'Existing Lead CampaignId evidence not found.' }
if(-not($valueObjects.Contains('public sealed record DateRange') -and $valueObjects.Contains('end < start'))){ throw 'DateRange invariant not found.' }
foreach($forbidden in @('MapGet("/api/crm/campaigns','MapPost("/api/crm/campaigns','MapPut("/api/crm/campaigns','MapDelete("/api/crm/campaigns','MapDelete("/api/crm/foundation/campaigns')){ if($program.Contains($forbidden)){ throw "Forbidden Campaign route: $forbidden" } }
if($frontend.Contains('/api/crm/campaigns')){ throw 'Productive Campaign frontend route detected.' }
if(-not($next.Contains('CRM Sprint 15 S15-01 - Campaign Contracts and Domain Rules') -and $next.Contains('codex/prompts/sprint-15-campaign-management-s15-01.md') -and $next.Contains('Sprint 15 P1 merge commit required'))){ throw 'Invalid S15-01 handoff.' }
Write-Host 'CRM Sprint 15 P1 verification passed.'
