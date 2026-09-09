$ErrorActionPreference='Stop'
$required=@(
 'frontend/crm-web/src/main.ts',
 'frontend/crm-web/tools/verify-crm-foundation.mjs',
 'docs/roadmap/crm-sprint-16-s16-04-account-frontend-foundation-page.md',
 'codex/prompts/sprint-16-account-management-s16-05.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S16-04 artifact: $f"}}
$main=Get-Content 'frontend/crm-web/src/main.ts' -Raw
$test=Get-Content 'frontend/crm-web/tools/verify-crm-foundation.mjs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-16-s16-04-account-frontend-foundation-page.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('AccountManagementApiService','AccountManagementPageComponent','/api/crm/foundation/accounts','foundation/accounts','Activate account','Deactivate account','formControlName="taxId"','formControlName="industry"','formControlName="segment"')){if(-not$main.Contains($m)){throw "Missing S16-04 frontend marker: $m"}}
foreach($m in @('type AccountStatus','Missing S16-04 Account frontend marker','Missing S16-04 Account duplicate submission protection')){if(-not$test.Contains($m)){throw "Missing S16-04 frontend verifier marker: $m"}}
foreach($m in @('S1604Decision: Implemented','ProductiveAccountRouteEnabled: false','DeleteBehaviorAdded: false','LeadConversionEnabled: false','AutomaticAccountCreationEnabled: false','ContactRelationshipMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S16-04 doc marker: $m"}}
foreach($f in @("'/api/crm/accounts'",'deleteAccount','Delete account')){if($main.Contains($f)){throw "Forbidden S16-04 Account frontend marker: $f"}}
if(-not(($next -match 'CRM Sprint 16 S16-0[5-7]') -and ($next -match 'codex/prompts/sprint-16-account-management-s16-0[5-7]\.md'))){throw 'Invalid Sprint 16 S16-04 forward handoff.'}
Write-Host 'CRM Sprint 16 S16-04 verification passed.'
