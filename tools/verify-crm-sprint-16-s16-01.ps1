$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Domain/AccountManagement/AccountManagementOperation.cs',
 'src/CRM.Domain/AccountManagement/AccountManagementCommand.cs',
 'src/CRM.Domain/AccountManagement/AccountManagementErrorCode.cs',
 'src/CRM.Domain/AccountManagement/AccountManagementRuleResult.cs',
 'src/CRM.Domain/AccountManagement/AccountManagementPolicy.cs',
 'tests/CRM.UnitTests/AccountManagementPolicyTests.cs',
 'tests/CRM.ArchitectureTests/AccountManagementArchitectureTests.cs',
 'docs/roadmap/crm-sprint-16-s16-01-account-contracts-domain-rules.md',
 'codex/prompts/sprint-16-account-management-s16-02.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S16-01 artifact: $f"}}
$policy=Get-Content 'src/CRM.Domain/AccountManagement/AccountManagementPolicy.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-16-s16-01-account-contracts-domain-rules.md' -Raw
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('MaxNameLength = 160','MaxTaxIdLength = 64','MaxIndustryLength = 120','MaxSegmentLength = 80','AccountStatus.Draft','AccountStatus.Active','AccountStatus.Inactive','AccountManagementOperation.Activate','AccountManagementOperation.Deactivate')){if(-not$policy.Contains($m)){throw "Missing S16-01 policy marker: $m"}}
foreach($m in @('S1601Decision: Implemented','ProductiveAccountRouteEnabled: false','DeleteBehaviorAdded: false','LeadConversionEnabled: false','AutomaticAccountCreationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false')){if(-not$doc.Contains($m)){throw "Missing S16-01 doc marker: $m"}}
foreach($f in @('MapGet("/api/crm/accounts','MapPost("/api/crm/accounts','MapPut("/api/crm/accounts','MapDelete("/api/crm/accounts','MapDelete("/api/crm/foundation/accounts')){if($program.Contains($f)){throw "Forbidden Account route: $f"}}
$s16=( ($next -match 'CRM Sprint 16 S16-0[2-7]') -and ($next -match 'codex/prompts/sprint-16-account-management-s16-0[2-7]\\.md') ); $s17=$next.Contains('CRM Sprint 17 P1 - Segment Management Functional Baseline and Backlog') -and $next.Contains('codex/prompts/sprint-17-segment-management-p1.md'); if(-not($s16 -or $s17)){throw 'Invalid Sprint 16 S16-01 forward handoff.'}
Write-Host 'CRM Sprint 16 S16-01 verification passed.'
