$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$program=Get-Content "$root\src\CRM.Api\Program.cs" -Raw
$service=Get-Content "$root\src\CRM.Application\AssignmentManagement\AssignmentManagementService.cs" -Raw
if($program -notmatch 'IAssignmentManagementService, AssignmentManagementService'){throw 'Missing assignment service DI'}
if($program -match '/api/crm/foundation/assignments'){throw 'S24-02 must not expose assignment routes'}
if($service -notmatch 'PortalIdentityRuntimeEnabled: false'){throw 'Portal identity runtime guardrail missing'}
Write-Host 'CRM Sprint 24 S24-02 verification passed.'