$ErrorActionPreference = 'Stop'

function Assert-FileContains {
    param(
        [string] $Path,
        [string] $Pattern,
        [string] $Message
    )

    if (-not (Test-Path $Path)) {
        throw "Missing required file: $Path"
    }

    $content = Get-Content $Path -Raw
    if ($content -notmatch [regex]::Escape($Pattern)) {
        throw $Message
    }
}

Assert-FileContains 'docs/roadmap/crm-sprint-10-p50-local-simulated-production-pilot-closure.md' 'Sprint10SimulatedProductionPilotClosed: true' 'Sprint 10 pilot closure is missing.'
Assert-FileContains 'docs/roadmap/crm-sprint-10-p50-local-simulated-production-pilot-closure.md' 'P50Decision: ClosedSuccessfully' 'P50 closure decision mismatch.'
Assert-FileContains 'docs/roadmap/crm-sprint-11-functional-slice-assessment.md' 'SelectedSprint11SliceId: S11-LEAD-QUAL' 'Sprint 11 selected slice mismatch.'
Assert-FileContains 'docs/roadmap/crm-sprint-11-functional-slice-assessment.md' 'Sprint11FrontendIncluded: true' 'Sprint 11 frontend inclusion decision mismatch.'
Assert-FileContains 'docs/roadmap/crm-sprint-11-backlog.md' 'Sprint11PlanningStatus: ReadyForImplementation' 'Sprint 11 planning is not ready.'
Assert-FileContains 'docs/roadmap/crm-sprint-11-roadmap.md' 'SelectedSlice: S11-LEAD-QUAL - Lead Intake and Qualification Foundation' 'Sprint 11 roadmap selected slice mismatch.'
Assert-FileContains 'docs/architecture/crm-sprint-11-selected-slice-architecture.md' 'Productive `/api/crm/leads` remains out of scope and locked.' 'Productive lead route guardrail missing.'
Assert-FileContains 'codex/next-task.md' 'CRM Sprint 11 S11-01 - Lead Qualification Contracts and Domain Rules' 'Next task was not refreshed to Sprint 11 S11-01.'
Assert-FileContains 'codex/prompts/sprint-11-lead-qualification-s11-01.md' 'Do not redeploy, restart or alter `crm-prod-sim`.' 'S11-01 prompt does not preserve simulated Production baseline.'

$nextTask = Get-Content 'codex/next-task.md' -Raw
if ($nextTask -match 'CRM Sprint 10 - P1 Productization Readiness Decision') {
    throw 'Stale Sprint 10 P1 next task reference remains.'
}

Write-Output 'PASS CRM Sprint 11 P1 planning verifier passed.'
