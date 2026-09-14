$ErrorActionPreference='Stop'
$required=@(
 'frontend/crm-web/src/main.ts',
 'tests/CRM.ArchitectureTests/CaseCrossLayerGuardrailTests.cs',
 'docs/roadmap/crm-sprint-18-s18-04-case-frontend-foundation-page.md',
 'codex/prompts/sprint-18-case-management-s18-05.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S18-04 artifact: $f"}}
$front=Get-Content 'frontend/crm-web/src/main.ts' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-18-s18-04-case-frontend-foundation-page.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @("'/api/crm/foundation/cases'","path: 'foundation/cases'",'CaseManagementPageComponent','maxlength="160"','maxlength="1000"','startCase','resolveCase','closeCase','CustomerId')){if(-not$front.Contains($m)){throw "Missing S18-04 frontend marker: $m"}}
foreach($m in @('S1804Decision: Implemented','ProductiveCaseRouteEnabled: false','DeleteBehaviorAdded: false','CustomerMutationEnabled: false','AssignmentRuntimeEnabled: false','SlaRuntimeEnabled: false','NotificationRuntimeEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S18-04 doc marker: $m"}}
if($front.Contains("'/api/crm/cases'")){throw 'Productive Case API found in frontend.'}
if($front.Contains('deleteCase')){throw 'Case DELETE behavior found in frontend.'}
if(-not(($next -match 'CRM Sprint 18 S18-0[5-7]') -and ($next -match 'codex/prompts/sprint-18-case-management-s18-0[5-7]\.md'))){throw 'Invalid S18-04 forward handoff.'}
Write-Host 'CRM Sprint 18 S18-04 verification passed.'
