$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-12-contact-management-closure.md",
    "docs/roadmap/crm-sprint-13-activity-follow-up-roadmap.md",
    "docs/roadmap/crm-sprint-13-p1-activity-follow-up-functional-baseline.md",
    "codex/prompts/sprint-13-activity-follow-up-s13-01.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing Sprint 13 P1 artifact or prerequisite: $file"
    }
}

$closure = Get-Content "docs/roadmap/crm-sprint-12-contact-management-closure.md" -Raw
$baseline = Get-Content "docs/roadmap/crm-sprint-13-p1-activity-follow-up-functional-baseline.md" -Raw
$roadmap = Get-Content "docs/roadmap/crm-sprint-13-activity-follow-up-roadmap.md" -Raw
$prompt = Get-Content "codex/prompts/sprint-13-activity-follow-up-s13-01.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw

foreach ($marker in @(
    "Sprint12ContactManagementClosed: true",
    "ProductiveContactRouteEnabled: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "SimulatedProductionTouchedBySprint12: false",
    "RealProductionStatus: Deferred")) {
    if (-not $closure.Contains($marker)) {
        throw "Sprint 12 closure prerequisite missing marker: $marker"
    }
}

foreach ($marker in @(
    "CanonicalActivityTerm: Activity",
    "CanonicalFollowUpTerm: Follow-Up",
    "ActivityDomainStatus: FoundationOnly",
    "ActivityApplicationStatus: NotStarted",
    "ActivityPersistenceArchitecture: NotStarted; target FoundationOnly / NonProductionSeam",
    "ActivityApiStatus: NotStarted",
    "ActivityFrontendStatus: NotStarted",
    "ActivityLeadRelationshipExists: false",
    "ActivityLeadRelationshipDecision: RequiredForFoundation",
    "ActivityContactRelationshipExists: false",
    "ActivityContactRelationshipRequiredForFoundation: true",
    "AccountRelationshipRequiredForFoundation: false",
    "OpportunityRelationshipDecision: Deferred",
    "FollowUpModelDecision: Follow-Up is an Activity",
    "ProductiveActivityRouteEnabled: false",
    "Sprint13FrontendIncluded: true",
    "DeleteBehaviorAllowed: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "SimulatedProductionTouched: false",
    "Sprint13P1Decision: ReadyForS1301ActivityContractsAndDomainRules",
    "FirstImplementationStoryId: S13-01",
    "FirstImplementationStoryName: Activity Contracts and Domain Rules")) {
    if (-not $baseline.Contains($marker)) {
        throw "Sprint 13 P1 baseline missing marker: $marker"
    }
}

foreach ($marker in @(
    "Sprint13SelectedSliceId: S13-ACTIVITY",
    "ActivityArchitectureTarget: FoundationOnly",
    "S13-01 - Activity Contracts and Domain Rules",
    "S13-02 - Activity Application Service",
    "S13-03 - Activity Foundation API",
    "S13-04 - Activity / Follow-Up Frontend Foundation Page",
    "S13-05 - Activity Test and Guardrail Hardening",
    "S13-06 - Activity Local Integration Validation",
    "S13-07 - Activity / Follow-Up Sprint Closure")) {
    if (-not $roadmap.Contains($marker)) {
        throw "Sprint 13 roadmap missing marker: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 13 S13-01 - Activity Contracts and Domain Rules",
    "Base Main Commit: Sprint 13 P1 merge commit required",
    'Do not add productive `/api/crm/activities`',
    "Do not add DELETE",
    "Do not activate Portal Auth runtime",
    "Do not activate Common DB")) {
    if (-not $prompt.Contains($marker)) {
        throw "Sprint 13 S13-01 prompt missing marker: $marker"
    }
}

if (-not ($nextTask.Contains("CRM Sprint 13 S13-01 - Activity Contracts and Domain Rules") -or $nextTask.Contains("CRM Sprint 13 S13-02 - Activity Application Service and Foundation Store") -or $nextTask.Contains("CRM Sprint 13 S13-03 - Activity Foundation API") -or $nextTask.Contains("CRM Sprint 13 S13-04 - Activity / Follow-Up Frontend Foundation Page"))) {
    throw "codex/next-task.md must point to Sprint 13 S13-01 or a later approved Sprint 13 Activity task."
}

if (-not ($nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-01.md") -or $nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-02.md") -or $nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-03.md") -or $nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-04.md"))) {
    throw "codex/next-task.md must reference S13-01 or a later approved Sprint 13 Activity prompt."
}

if (-not ($nextTask.Contains("Sprint 13 P1 merge commit required") -or $nextTask.Contains("S13-01 merge commit required") -or $nextTask.Contains("S13-02 merge commit required") -or $nextTask.Contains("S13-03 merge commit required"))) {
    throw "codex/next-task.md must avoid invented future SHA."
}

foreach ($marker in @(
    "CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline and Backlog",
    "Sprint13P1Decision: ReadyForS1301ActivityContractsAndDomainRules",
    "FirstImplementationStoryId: S13-01")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md must record Sprint 13 P1 marker: $marker"
    }
}

$programWithoutFoundationSprints = $program.Replace('"/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-scaffold"', '""')
foreach ($forbidden in @('"/api/crm/activities"', 'MapGet("/api/crm/activities', 'MapPost("/api/crm/activities', 'MapPut("/api/crm/activities', 'MapDelete("/api/crm/activities', 'MapDelete(')) {
    if ($programWithoutFoundationSprints.Contains($forbidden)) {
        throw "Forbidden Activity runtime/DELETE marker detected: $forbidden"
    }
}

Write-Host "CRM Sprint 13 P1 verification passed."
