$ErrorActionPreference='Stop'
$required=@(
 'docs/roadmap/crm-sprint-20-note-management-functional-baseline.md',
 'codex/prompts/sprint-20-note-management-s20-01.md',
 'codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing Sprint 20 P1 artifact: $f"}}
$doc=Get-Content 'docs/roadmap/crm-sprint-20-note-management-functional-baseline.md' -Raw
$next=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('Sprint20P1Decision: Defined','RuntimeStatus: PlanningOnly','Text: required, trimmed, max 4000 characters.','Create starts `Active`','ProductiveNoteRouteEnabled: false','DeleteBehaviorAdded: false','PortalRuntimeEnabled: false','CommonDbRuntimeEnabled: false')){if(-not$doc.Contains($m)){throw "Missing Sprint 20 P1 marker: $m"}}
if(-not($next.Contains('CRM Sprint 20 S20-01 - Note Contracts and Domain Rules') -and $next.Contains('codex/prompts/sprint-20-note-management-s20-01.md'))){throw 'Invalid S20-01 handoff.'}
Write-Host 'CRM Sprint 20 P1 verification passed.'