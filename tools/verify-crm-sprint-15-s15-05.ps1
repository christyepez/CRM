$ErrorActionPreference='Stop'
$required=@('tests/CRM.ArchitectureTests/CampaignCrossLayerGuardrailTests.cs','docs/roadmap/crm-sprint-15-s15-05-campaign-test-guardrail-hardening.md','codex/prompts/sprint-15-campaign-management-s15-06.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S15-05 artifact: $f"}}
$test=Get-Content 'tests/CRM.ArchitectureTests/CampaignCrossLayerGuardrailTests.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-15-s15-05-campaign-test-guardrail-hardening.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('/api/crm/foundation/campaigns','selectedCampaignReadonly','CampaignManagementPolicy.Evaluate','ICampaignFoundationStore')){if(-not$test.Contains($m)){throw "Missing S15-05 test marker: $m"}}
foreach($m in @('S1505Decision: Implemented','ProductiveCampaignRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','ExternalConnectorRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S15-05 doc marker: $m"}}
if(-not(($next -match 'CRM Sprint 15 S15-0[6-7]') -and ($next -match 'codex/prompts/sprint-15-campaign-management-s15-0[6-7]\.md'))){throw 'Invalid Sprint 15 forward handoff.'}
Write-Host 'CRM Sprint 15 S15-05 verification passed.'
