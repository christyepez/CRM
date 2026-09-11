$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Application/NoteManagement/NoteManagementApplicationContracts.cs',
 'src/CRM.Application/NoteManagement/INoteManagementService.cs',
 'src/CRM.Application/NoteManagement/NoteManagementService.cs',
 'src/CRM.Application/Ports/Persistence/INoteFoundationStore.cs',
 'src/CRM.Infrastructure/Persistence/Foundation/InMemoryNoteFoundationStore.cs',
 'tests/CRM.UnitTests/NoteManagementServiceTests.cs',
 'tests/CRM.ArchitectureTests/NoteManagementArchitectureTests.cs',
 'docs/roadmap/crm-sprint-20-s20-02-note-application-foundation-store.md',
 'codex/prompts/sprint-20-note-management-s20-03.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S20-02 artifact: $f"}}
$p=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($m in @('INoteManagementService, NoteManagementService','INoteFoundationStore, InMemoryNoteFoundationStore')){if(-not$p.Contains($m)){throw "Missing DI marker: $m"}}
if($p.Contains('MapGet("/api/crm/foundation/notes')){throw 'S20-02 must not expose Note API yet.'}
Write-Host 'CRM Sprint 20 S20-02 verification passed.'