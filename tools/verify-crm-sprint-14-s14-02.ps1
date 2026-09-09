$ErrorActionPreference="Stop"
$root=Split-Path -Parent $PSScriptRoot
Set-Location $root
$req=@("src/CRM.Application/OpportunityManagement/IOpportunityManagementService.cs","src/CRM.Application/OpportunityManagement/OpportunityManagementService.cs","src/CRM.Application/OpportunityManagement/OpportunityManagementApplicationContracts.cs","src/CRM.Application/Ports/Persistence/IOpportunityFoundationStore.cs","src/CRM.Infrastructure/Persistence/Foundation/InMemoryOpportunityFoundationStore.cs","tests/CRM.UnitTests/OpportunityManagementServiceTests.cs","tests/CRM.ArchitectureTests/OpportunityApplicationArchitectureTests.cs","docs/roadmap/crm-sprint-14-s14-02-opportunity-application-service-foundation-store.md","codex/prompts/sprint-14-opportunity-pipeline-s14-03.md")
foreach($f in $req){if(-not(Test-Path $f)){throw "Missing S14-02 file $f"}}
$app=Get-Content src/CRM.Application/OpportunityManagement/OpportunityManagementService.cs -Raw
$store=Get-Content src/CRM.Infrastructure/Persistence/Foundation/InMemoryOpportunityFoundationStore.cs -Raw
$doc=Get-Content docs/roadmap/crm-sprint-14-s14-02-opportunity-application-service-foundation-store.md -Raw
$n=Get-Content codex/next-task.md -Raw
$program=Get-Content src/CRM.Api/Program.cs -Raw
foreach($m in @("IOpportunityFoundationStore","OpportunityPipelinePolicy","GetAllAsync","CreateAsync","UpdateAsync","ProgressAsync","WinAsync","LoseAsync","CancelAsync")){if(-not$app.Contains($m)){throw "Missing app marker $m"}}
foreach($m in @("InMemoryOpportunityFoundationStore","OpportunityFoundationRecord","Synthetic Account")){if(-not$store.Contains($m)){throw "Missing store marker $m"}}
foreach($m in @("S1402Decision: Implemented","PersistenceClassification: FoundationOnly / NonProductionSeam","DomainRulesDuplicatedInApplication: false","NoChangePersistenceSuppressed: true","ProductiveOpportunityRouteEnabled: false","PortalRuntimeEnabled: false","CommonDbRuntimeEnabled: false","SimulatedProductionTouched: false")){if(-not$doc.Contains($m)){throw "Missing S14-02 doc marker $m"}}
if(-not(($n -match "CRM Sprint 14 S14-0[3-7]") -or ($n.Contains("CRM Sprint 15 P1 - Campaign Management Functional Baseline and Backlog") -and $n.Contains("codex/prompts/sprint-15-campaign-management-p1.md") -and $n.Contains("S14-07 merge commit required")))){throw "Invalid forward handoff after S14-02"}
foreach($m in @('MapGet("/api/crm/opportunities','MapPost("/api/crm/opportunities','MapPut("/api/crm/opportunities','MapDelete("/api/crm/opportunities')){if($program.Contains($m)){throw "Forbidden productive Opportunity route: $m"}}
foreach($m in @("CRM.Infrastructure","SqlConnection","DbContext","UseSqlServer","Authorization")){if($app.Contains($m)){throw "Forbidden Application dependency $m"}}
foreach($m in @("SqlConnection","DbContext","UseSqlServer","ConnectionString")){if($store.Contains($m)){throw "Forbidden foundation store dependency $m"}}
Write-Host "CRM Sprint 14 S14-02 verification passed."
