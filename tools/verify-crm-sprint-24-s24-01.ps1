$ErrorActionPreference='Stop'
$d=Get-Content 'src/CRM.Domain/AssignmentManagement/AssignmentPolicy.cs' -Raw
if($d -notmatch 'MaxAssigneeReferenceIdLength = 200'){throw 'Assignee reference boundary missing'}
if($d -notmatch 'ArchivedAssignmentCannotBeModified'){throw 'Archived guardrail missing'}
if($d -match 'Security API|UserRepository|RoleRepository'){throw 'Portal identity runtime leaked into domain'}
Write-Host 'CRM Sprint 24 S24-01 verification passed.'
