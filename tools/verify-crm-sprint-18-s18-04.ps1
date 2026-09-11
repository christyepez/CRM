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
foreach($m in @("'/api/crm/foundation/cases'","path: 'foundation/cases'",'CaseManagementPageComponent','maxlength="160"','maxlength="1000"','startCase','resolveCase','closeCase')){if(-not$front.Contains($m)){throw "Missing S18-04 frontend marker: $m"}}
foreach($m in @('S1804Decision: Implemented','ProductiveCaseRouteEnabled: false','DeleteBehaviorAdded: false','CustomerMutationEnabled: false','AssignmentRuntimeEnabled: false','SlaRuntimeEnabled: false','NotificationRuntimeEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S18-04 doc marker: $m"}}
if($front.Contains("'/api/crm/cases'")){throw 'Productive Case API found in frontend.'}
if($front.Contains('.delete<')){throw 'DELETE client behavior detected in Case frontend block.'}
if(-not($next.Contains('CRM Sprint 18 S18-05 - Case Test and Guardrail Hardening') -and $next.Contains('codex/prompts/sprint-18-case-management-s18-05.md'))){throw 'Invalid S18-05 handoff.'}
Write-Host 'CRM Sprint 18 S18-04 verification passed.'
