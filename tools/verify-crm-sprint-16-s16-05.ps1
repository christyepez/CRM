$ErrorActionPreference='Stop'
$required=@(
 'tests/CRM.ArchitectureTests/AccountCrossLayerGuardrailTests.cs',
 'tests/CRM.UnitTests/AccountFoundationApiHardeningTests.cs',
 'docs/roadmap/crm-sprint-16-s16-05-account-test-guardrail-hardening.md',
 'codex/prompts/sprint-16-account-management-s16-06.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S16-05 artifact: $f"}}
$arch=Get-Content 'tests/CRM.ArchitectureTests/AccountCrossLayerGuardrailTests.cs' -Raw
$front=Get-Content 'frontend/crm-web/src/main.ts' -Raw
$api=Get-Content 'tests/CRM.UnitTests/AccountFoundationApiHardeningTests.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-16-s16-05-account-test-guardrail-hardening.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('/api/crm/foundation/accounts','AccountManagementPolicy.Evaluate','IAccountFoundationStore')){if(-not$arch.Contains($m)){throw "Missing S16-05 architecture marker: $m"}}
foreach($m in @('maxlength="160"','maxlength="64"','maxlength="120"','maxlength="80"','Activate account','Deactivate account')){if(-not$front.Contains($m)){throw "Missing S16-05 frontend marker: $m"}}
foreach($m in @('Update_NoChange_ReturnsChangedFalse','Activate_Repeated_IsIdempotent','Deactivate_Draft_ReturnsConflict','Update_MissingAccount_ReturnsNotFound','InvalidStatusTransition')){if(-not$api.Contains($m)){throw "Missing S16-05 API hardening marker: $m"}}
foreach($m in @('S1605Decision: Implemented','ProductiveAccountRouteEnabled: false','DeleteBehaviorAdded: false','LeadConversionEnabled: false','AutomaticAccountCreationEnabled: false','ContactRelationshipMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S16-05 doc marker: $m"}}
$s16=( ($next -match 'CRM Sprint 16 S16-0[6-7]') -and ($next -match 'codex/prompts/sprint-16-account-management-s16-0[6-7]\\.md') ); $s17=$next.Contains('CRM Sprint 17 P1 - Segment Management Functional Baseline and Backlog') -and $next.Contains('codex/prompts/sprint-17-segment-management-p1.md'); if(-not($s16 -or $s17)){throw 'Invalid Sprint 16 S16-05 forward handoff.'}
Write-Host 'CRM Sprint 16 S16-05 verification passed.'
