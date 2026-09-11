$ErrorActionPreference='Stop'
$required=@('docs/roadmap/crm-sprint-19-interaction-management-functional-baseline.md','codex/prompts/sprint-19-interaction-management-s19-01.md','codex/next-task.md')
foreach($f in $required){if(-not(Test-Path $f)){throw "Missing S19 P1 artifact: $f"}}
$d=Get-Content 'docs/roadmap/crm-sprint-19-interaction-management-functional-baseline.md' -Raw
$n=Get-Content 'codex/next-task.md' -Raw
foreach($m in @('S19P1Decision: Defined','InteractionDomainOwnership: CRM CREATE','ActivitySchedulingDuplicated: false','Foundation route family: `/api/crm/foundation/interactions`','ProductiveInteractionRouteEnabled: false','S19-01 Interaction contracts and domain policy','S19-07 sprint closure')){if(-not$d.Contains($m)){throw "Missing S19 P1 marker: $m"}}
if(-not($n.Contains('CRM Sprint 19 S19-01') -and $n.Contains('sprint-19-interaction-management-s19-01.md'))){throw 'Invalid S19-01 handoff'}
Write-Host 'CRM Sprint 19 P1 verification passed.'
