$ErrorActionPreference='Stop'
$required=@('tests/CRM.UnitTests/CaseFoundationApiEndpointTests.cs','tests/CRM.ArchitectureTests/CaseCrossLayerGuardrailTests.cs','docs/roadmap/crm-sprint-18-s18-05-case-test-guardrail-hardening.md','codex/prompts/sprint-18-case-management-s18-06.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S18-05 artifact: $f"}}
$api=Get-Content 'tests/CRM.UnitTests/CaseFoundationApiEndpointTests.cs' -Raw
$arch=Get-Content 'tests/CRM.ArchitectureTests/CaseCrossLayerGuardrailTests.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-18-s18-05-case-test-guardrail-hardening.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('ResolvedAndClosedUpdates_ReturnConflict','ProductiveAndDeleteRoutes_RemainUnavailable','InvalidLifecycleTransition_ReturnsConflict')){if(-not$api.Contains($m)){throw "Missing S18-05 API test marker: $m"}}
foreach($m in @('Frontend_UsesFoundationCaseRoutesOnly_AndNoDelete','Frontend_CaseFieldsLifecycle_AndSubmissionGuard_AreAligned','Frontend_DoesNotAddPortalTokenCustomerLookupOrRuntimeAssignment')){if(-not$arch.Contains($m)){throw "Missing S18-05 architecture marker: $m"}}
foreach($m in @('S1805Decision: Implemented','ProductiveCaseRouteEnabled: false','DeleteBehaviorAdded: false','CustomerMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S18-05 doc marker: $m"}}
if(-not($next.Contains('CRM Sprint 18 S18-06 - Case Local Integration Validation') -and $next.Contains('codex/prompts/sprint-18-case-management-s18-06.md'))){throw 'Invalid S18-06 handoff.'}
Write-Host 'CRM Sprint 18 S18-05 verification passed.'
