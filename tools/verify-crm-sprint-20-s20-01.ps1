$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Domain/NoteManagement/NoteEnums.cs',
 'src/CRM.Domain/NoteManagement/NoteManagementContracts.cs',
 'src/CRM.Domain/NoteManagement/NoteManagementPolicy.cs',
 'tests/CRM.UnitTests/NoteManagementPolicyTests.cs',
 'docs/roadmap/crm-sprint-20-s20-01-note-contracts-domain-rules.md',
 'codex/prompts/sprint-20-note-management-s20-02.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S20-01 artifact: $f"}}
$doc=Get-Content 'docs/roadmap/crm-sprint-20-s20-01-note-contracts-domain-rules.md' -Raw
foreach($m in @('S2001Decision: Implemented','ProductiveNoteRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing S20-01 marker: $m"}}
$p=Get-Content 'src/CRM.Domain/NoteManagement/NoteManagementPolicy.cs' -Raw
foreach($m in @('MaxTextLength = 4000','NoteManagementOperation.Archive','ArchivedNoteCannotBeModified')){if(-not$p.Contains($m)){throw "Missing Note policy marker: $m"}}
Write-Host 'CRM Sprint 20 S20-01 verification passed.'