$ErrorActionPreference="Stop"
$root=Split-Path -Parent $PSScriptRoot
Set-Location $root
$required=@("src/CRM.Api/Foundation/OpportunityManagementApiContracts.cs","tests/CRM.UnitTests/OpportunityFoundationApiEndpointTests.cs","tests/CRM.ArchitectureTests/OpportunityApiArchitectureTests.cs","docs/roadmap/crm-sprint-14-s14-03-opportunity-foundation-api.md","codex/prompts/sprint-14-opportunity-pipeline-s14-04.md")
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S14-03 file $f"}}
$program=Get-Content src/CRM.Api/Program.cs -Raw
$contracts=Get-Content src/CRM.Api/Foundation/OpportunityManagementApiContracts.cs -Raw
$doc=Get-Content docs/roadmap/crm-sprint-14-s14-03-opportunity-foundation-api.md -Raw
$n=Get-Content codex/next-task.md -Raw
foreach($route in @("/api/crm/foundation/opportunities","/api/crm/foundation/opportunities/{id}","/progress","/win","/lose","/cancel")){if(-not$program.Contains($route)){throw "Missing Opportunity foundation route marker $route"}}
foreach($m in @("IOpportunityManagementService","FoundationOpportunityCreateRequest","FoundationOpportunityUpdateRequest","FoundationOpportunityProgressRequest")){if(-not($program+$contracts).Contains($m)){throw "Missing S14-03 API marker $m"}}
foreach($m in @("S1403Decision: Implemented","ProductiveOpportunityRouteEnabled: false","DeleteBehaviorAdded: false","PortalRuntimeEnabled: false","CommonDbRuntimeEnabled: false","SimulatedProductionTouched: false")){if(-not$doc.Contains($m)){throw "Missing S14-03 doc marker $m"}}
foreach($m in @('MapGet("/api/crm/opportunities','MapPost("/api/crm/opportunities','MapPut("/api/crm/opportunities','MapDelete("/api/crm/opportunities','MapDelete("/api/crm/foundation/opportunities')){if($program.Contains($m)){throw "Forbidden Opportunity route marker $m"}}
if(-not($n -match "CRM Sprint 14 S14-0[4-7]")){throw "next-task must point to S14-04 or later legitimate Sprint 14 phase"}
Write-Host "CRM Sprint 14 S14-03 verification passed."
