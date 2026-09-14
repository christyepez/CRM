$ErrorActionPreference='Stop'
$required=@(
 'docs/roadmap/crm-sprint-19-s19-05-interaction-test-guardrail-hardening.md',
 'tests/CRM.UnitTests/InteractionManagementPolicyTests.cs',
 'tests/CRM.UnitTests/InteractionManagementServiceTests.cs',
 'tests/CRM.UnitTests/InteractionFoundationApiEndpointTests.cs',
 'tests/CRM.ArchitectureTests/InteractionFrontendGuardrailTests.cs',
 'codex/prompts/sprint-19-interaction-management-s19-06.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19-05 artifact: $f"}}
$policy=Get-Content 'tests/CRM.UnitTests/InteractionManagementPolicyTests.cs' -Raw
$api=Get-Content 'tests/CRM.UnitTests/InteractionFoundationApiEndpointTests.cs' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-19-s19-05-interaction-test-guardrail-hardening.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('Create_AcceptsExactTextBoundaries','Create_AllowsOccurrenceExactlyAtEvaluationTimestamp')){if(-not$policy.Contains($m)){throw "Missing S19-05 policy test: $m"}}
foreach($m in @('Create_FutureOccurrence_ReturnsBadRequest','Update_VoidedInteraction_ReturnsConflict','ProductiveAndDeleteRoutes_RemainUnavailable')){if(-not$api.Contains($m)){throw "Missing S19-05 API test: $m"}}
foreach($m in @('S1905Decision: Implemented','Productive Interaction API remains unavailable','DELETE remains unavailable','Portal runtime and Common DB runtime remain disabled')){if(-not$doc.Contains($m)){throw "Missing S19-05 doc marker: $m"}}
foreach($m in @('MapGet("/api/crm/interactions','MapPost("/api/crm/interactions','MapPut("/api/crm/interactions','MapDelete("/api/crm/interactions','MapDelete("/api/crm/foundation/interactions')){if($program.Contains($m)){throw "Forbidden S19-05 route: $m"}}
if(-not($next.Contains('CRM Sprint 19 S19-06 - Interaction Local Integration Validation') -and $next.Contains('codex/prompts/sprint-19-interaction-management-s19-06.md'))){throw 'Invalid S19-06 handoff.'}
Write-Host 'CRM Sprint 19 S19-05 verification passed.'
