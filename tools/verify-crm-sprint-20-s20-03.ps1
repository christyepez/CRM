$ErrorActionPreference='Stop'
$required=@(
 'src/CRM.Api/Foundation/NoteManagementApiContracts.cs',
 'tests/CRM.UnitTests/NoteFoundationApiEndpointTests.cs',
 'docs/roadmap/crm-sprint-20-s20-03-note-foundation-api.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S20-03 artifact: $f"}}
$program=Get-Content 'src/CRM.Api/Program.cs' -Raw
foreach($m in @('MapGet("/api/crm/foundation/notes','MapPost("/api/crm/foundation/notes','MapPut("/api/crm/foundation/notes/{id}"','/archive','INoteManagementService service')){if(-not$program.Contains($m)){throw "Missing S20-03 marker: $m"}}
foreach($m in @('MapGet("/api/crm/notes','MapPost("/api/crm/notes','MapPut("/api/crm/notes','MapDelete("/api/crm/notes','MapDelete("/api/crm/foundation/notes')){if($program.Contains($m)){throw "Forbidden S20-03 marker: $m"}}
Write-Host 'CRM Sprint 20 S20-03 verification passed.'