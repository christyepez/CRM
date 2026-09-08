$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
Set-Location $root
$required=@("src/CRM.Domain/OpportunityManagement/OpportunityPipelineOperation.cs","src/CRM.Domain/OpportunityManagement/OpportunityPipelineCommand.cs","src/CRM.Domain/OpportunityManagement/OpportunityPipelineErrorCode.cs","src/CRM.Domain/OpportunityManagement/OpportunityPipelineRuleResult.cs","src/CRM.Domain/OpportunityManagement/OpportunityPipelinePolicy.cs","tests/CRM.UnitTests/OpportunityPipelinePolicyTests.cs","tests/CRM.ArchitectureTests/OpportunityPipelineArchitectureTests.cs","docs/roadmap/crm-sprint-14-s14-01-opportunity-pipeline-contracts-domain-rules.md","codex/prompts/sprint-14-opportunity-pipeline-s14-02.md")
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing $f"}}
$d=Get-Content src/CRM.Domain/OpportunityManagement/OpportunityPipelinePolicy.cs -Raw
$doc=Get-Content docs/roadmap/crm-sprint-14-s14-01-opportunity-pipeline-contracts-domain-rules.md -Raw
$n=Get-Content codex/next-task.md -Raw
$program=Get-Content src/CRM.Api/Program.cs -Raw
foreach($m in @("OpportunityPipelinePolicy","DuplicateStageOrder","DuplicateStageName","InvalidStageProgression","OpportunityStatus.Won","OpportunityStatus.Lost","OpportunityStatus.Cancelled")){if(-not$d.Contains($m)){throw "Missing domain marker $m"}}
foreach($m in @("S1401Decision: Implemented","ProductiveOpportunityRouteEnabled: false","FoundationOpportunityRouteEnabled: false","PortalRuntimeEnabled: false","CommonDbRuntimeEnabled: false","SimulatedProductionTouched: false")){if(-not$doc.Contains($m)){throw "Missing doc marker $m"}}
$nextS1402 = $n.Contains("CRM Sprint 14 S14-02 - Opportunity Application Service and Foundation Store") -and $n.Contains("codex/prompts/sprint-14-opportunity-pipeline-s14-02.md") -and $n.Contains("Sprint 14 S14-01 merge commit required")
$nextS1403 = $n.Contains("CRM Sprint 14 S14-03 - Opportunity Foundation API") -and $n.Contains("codex/prompts/sprint-14-opportunity-pipeline-s14-03.md") -and $n.Contains("Sprint 14 S14-02 merge commit required")
if(-not($nextS1402 -or $nextS1403)){throw "Invalid Sprint 14 forward handoff after S14-01"}
foreach($m in @("/api/crm/opportunities","/api/crm/foundation/opportunities")){if($program.Contains($m)){throw "Forbidden route $m"}}
Write-Host "CRM Sprint 14 S14-01 verification passed."
