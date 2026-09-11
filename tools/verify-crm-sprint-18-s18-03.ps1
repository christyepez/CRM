$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Api/Foundation/CaseManagementApiContracts.cs',
 'tests/CRM.UnitTests/CaseFoundationApiEndpointTests.cs',
 'docs/roadmap/crm-sprint-18-s18-03-case-foundation-api.md',
 'codex/prompts/sprint-18-case-management-s18-04.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S18-03 artifact: $f"}}

$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
$contracts=Get-Content 'src/CRM.Api/Foundation/CaseManagementApiContracts.cs' -Raw
$tests=Get-Content 'tests/CRM.UnitTests/CaseFoundationApiEndpointTests.cs' -Raw
$doc=Get-Content 'docs/roadmap/crm-sprint-18-s18-03-case-foundation-api.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw

foreach($route in @(
 'MapGet("/api/crm/foundation/cases"',
 'MapGet("/api/crm/foundation/cases/{id}"',
 'MapPost("/api/crm/foundation/cases"',
 'MapPut("/api/crm/foundation/cases/{id}"',
 'MapPost("/api/crm/foundation/cases/{id}/start"',
 'MapPost("/api/crm/foundation/cases/{id}/resolve"',
 'MapPost("/api/crm/foundation/cases/{id}/close"')){
    if(-not$program.Contains($route)){throw "Missing S18-03 Case route: $route"}
}
foreach($marker in @('ICaseManagementService service','CaseManagementApiResponse.ToApplicationRequest','CaseManagementApiResponse.ToStatusCode','CaseManagementErrorCode.CaseNotFound')){
    if(-not$program.Contains($marker)){throw "Missing S18-03 Program marker: $marker"}
}
foreach($route in @('MapGet("/api/crm/cases','MapPost("/api/crm/cases','MapPut("/api/crm/cases','MapDelete("/api/crm/cases','MapDelete("/api/crm/foundation/cases')){
    if($program.Contains($route)){throw "Forbidden S18-03 Case route detected: $route"}
}
foreach($marker in @(
 'FoundationCaseCreateRequest','FoundationCaseUpdateRequest','CaseManagementApiResponse',
 'CaseNotFound) => StatusCodes.Status404NotFound',
 'InvalidStatusTransition) => StatusCodes.Status409Conflict',
 'ResolvedCaseCannotBeModified) => StatusCodes.Status409Conflict',
 'ClosedCaseCannotBeModified) => StatusCodes.Status409Conflict',
 'StatusCodes.Status400BadRequest','CustomerMutationEnabled','AssignmentRuntimeEnabled','SlaRuntimeEnabled','NotificationRuntimeEnabled')){
    if(-not$contracts.Contains($marker)){throw "Missing S18-03 contract marker: $marker"}
}
foreach($marker in @('Create_ValidRequest_ReturnsExplicitApiContract','Update_NoChange_PreservesIdempotentSuccessAsOk','MissingCases_ReturnNotFound','InvalidLifecycleTransition_ReturnsConflict','ProductiveAndDeleteRoutes_RemainUnavailable')){
    if(-not$tests.Contains($marker)){throw "Missing S18-03 test marker: $marker"}
}
foreach($marker in @('S1803Decision: Implemented','FoundationCaseApiRoute: /api/crm/foundation/cases','CaseApiUsesApplicationService: true','ExplicitApiDtos: true','ValidationFailuresStatus: 400','MissingCaseStatus: 404','InvalidLifecycleTransitionStatus: 409','ChangedFalseIdempotentSuccessStatus: 200','ProductiveCaseRouteEnabled: false','DeleteBehaviorAdded: false','AngularCasePageAdded: false','CustomerMutationEnabled: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false','SimulatedProductionTouched: false','Port8094Touched: false')){
    if(-not$doc.Contains($marker)){throw "Missing S18-03 doc marker: $marker"}
}
if(-not($next.Contains('CRM Sprint 18 S18-04 - Case Frontend Foundation Page') -and $next.Contains('codex/prompts/sprint-18-case-management-s18-04.md'))){throw 'Invalid S18-04 handoff.'}
Write-Host 'CRM Sprint 18 S18-03 verification passed.'
