$ErrorActionPreference='Stop'
$required=@('frontend/crm-web/src/main.ts','docs/roadmap/crm-sprint-15-s15-04-campaign-frontend-foundation.md','codex/prompts/sprint-15-campaign-management-s15-05.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S15-04 artifact: $f"}}
$frontend=Get-Content 'frontend/crm-web/src/main.ts' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-15-s15-04-campaign-frontend-foundation.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
$start=$frontend.IndexOf("type CampaignStatus")
$end=$frontend.IndexOf("type ActivityType")
if($start -lt 0 -or $end -le $start){throw 'Campaign frontend source block not found.'}
$campaign=$frontend.Substring($start,$end-$start)
foreach($m in @('CampaignManagementPageComponent','CampaignManagementApiService','foundation/campaigns','Activate campaign','Complete campaign','Cancel campaign','Terminal Campaigns are read-only','No changes were necessary')){if(-not$campaign.Contains($m)){throw "Missing S15-04 frontend marker: $m"}}
foreach($m in @('ProductiveCampaignRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S15-04 guardrail marker: $m"}}
foreach($f in @('/api/crm/campaigns','deleteCampaign','localStorage','sessionStorage','Bearer ','Authorization')){if($campaign.Contains($f)){throw "Forbidden S15-04 Campaign frontend marker: $f"}}
if(-not($frontend.Contains("{ path: 'foundation/campaigns', component: CampaignManagementPageComponent }"))){throw 'Missing Campaign frontend route.'}
if(-not(($next -match 'CRM Sprint 15 S15-0[5-7]') -and ($next -match 'codex/prompts/sprint-15-campaign-management-s15-0[5-7]\.md'))){throw 'Invalid Sprint 15 forward handoff'}
Write-Host 'CRM Sprint 15 S15-04 verification passed.'
