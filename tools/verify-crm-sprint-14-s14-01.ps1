$ErrorActionPreference="Stop"
$root=Split-Path -Parent $PSScriptRoot
Set-Location $root
$required=@("src/CRM.Domain/OpportunityManagement/OpportunityPipelineOperation.cs","src/CRM.Domain/OpportunityManagement/OpportunityPipelineCommand.cs","src/CRM.Domain/OpportunityManagement/OpportunityPipelineErrorCode.cs","src/CRM.Domain/OpportunityManagement/OpportunityPipelineRuleResult.cs","src/CRM.Domain/OpportunityManagement/OpportunityPipelinePolicy.cs","tests/CRM.UnitTests/OpportunityPipelinePolicyTests.cs","tests/CRM.ArchitectureTests/OpportunityPipelineArchitectureTests.cs","docs/roadmap/crm-sprint-14-s14-01-opportunity-pipeline-contracts-domain-rules.md","codex/prompts/sprint-14-opportunity-pipeline-s14-02.md")
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing $f"}}
$d=Get-Content src/CRM.Domain/OpportunityManagement/OpportunityPipelinePolicy.cs -Raw
$doc=Get-Content docs/roadmap/crm-sprint-14-s14-01-opportunity-pipeline-contracts-domain-rules.md -Raw
$n=Get-Content codex/next-task.md -Raw
$program=Get-Content src/CRM.Api/Program.cs -Raw
foreach($m in @("OpportunityPipelinePolicy","DuplicateStageOrder","DuplicateStageName","InvalidStageProgression","OpportunityStatus.Won","OpportunityStatus.Lost","OpportunityStatus.Cancelled")){if(-not$d.Contains($m)){throw "Missing domain marker $m"}}
foreach($m in @("S1401Decision: Implemented","ProductiveOpportunityRouteEnabled: false","PortalRuntimeEnabled: false","CommonDbRuntimeEnabled: false","SimulatedProductionTouched: false")){if(-not$doc.Contains($m)){throw "Missing doc marker $m"}}
if(-not($n -match "CRM Sprint 14 S14-0[2-7]")){throw "Invalid Sprint 14 forward handoff after S14-01"}
foreach($m in @('MapGet("/api/crm/opportunities','MapPost("/api/crm/opportunities','MapPut("/api/crm/opportunities','MapDelete("/api/crm/opportunities')){if($program.Contains($m)){throw "Forbidden productive route $m"}}
Write-Host "CRM Sprint 14 S14-01 verification passed."
