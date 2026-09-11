$ErrorActionPreference='Stop'
$required=@(
 'frontend/crm-web/src/main.ts',
 'tests/CRM.ArchitectureTests/InteractionFrontendGuardrailTests.cs',
 'docs/roadmap/crm-sprint-19-s19-04-interaction-frontend-foundation-page.md',
 'codex/prompts/sprint-19-interaction-management-s19-05.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19-04 artifact: $f"}}
$front=Get-Content 'frontend/crm-web/src/main.ts' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-19-s19-04-interaction-frontend-foundation-page.md' -Raw
foreach($m in @("'/api/crm/foundation/interactions'","path: 'foundation/interactions'",'InteractionManagementPageComponent','maxlength="160"','maxlength="2000"','voidInteraction')){if(-not$front.Contains($m)){throw "Missing S19-04 frontend marker: $m"}}
if($front.Contains("'/api/crm/interactions'")){throw 'Productive Interaction API found in frontend.'}
foreach($m in @('S1904Decision: Implemented','ProductiveInteractionRouteEnabled: false','DeleteBehaviorAdded: false','ActivitySchedulingAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S19-04 doc marker: $m"}}
Write-Host 'CRM Sprint 19 S19-04 verification passed.'
