$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Domain/ActivityManagement/ActivityManagementOperation.cs",
    "src/CRM.Domain/ActivityManagement/ActivityManagementCommand.cs",
    "src/CRM.Domain/ActivityManagement/ActivityManagementErrorCode.cs",
    "src/CRM.Domain/ActivityManagement/ActivityManagementRuleResult.cs",
    "src/CRM.Domain/ActivityManagement/ActivityManagementPolicy.cs",
    "tests/CRM.UnitTests/ActivityManagementPolicyTests.cs",
    "tests/CRM.ArchitectureTests/ActivityManagementArchitectureTests.cs",
    "docs/roadmap/crm-sprint-13-s13-01-activity-contracts-domain-rules.md",
    "codex/prompts/sprint-13-activity-follow-up-s13-02.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing S13-01 artifact: $file"
    }
}

$domain = (Get-ChildItem "src/CRM.Domain/ActivityManagement" -Filter "*.cs" | ForEach-Object { Get-Content $_.FullName -Raw }) -join "`n"
$activity = Get-Content "src/CRM.Domain/Entities/Activity.cs" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-13-s13-01-activity-contracts-domain-rules.md" -Raw
$tests = Get-Content "tests/CRM.UnitTests/ActivityManagementPolicyTests.cs" -Raw
$architectureTests = Get-Content "tests/CRM.ArchitectureTests/ActivityManagementArchitectureTests.cs" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "ActivityManagementPolicy",
    "ActivityManagementCommand",
    "ActivityManagementSnapshot",
    "ActivityManagementRuleResult",
    "ActivityManagementErrorCode",
    "ActivityManagementOperation",
    "MaxSubjectLength = 160",
    "ActivityTargetRequired",
    "MultipleActivityTargetsNotAllowed",
    "InvalidLeadId",
    "InvalidContactId",
    "CompletedActivityCannotBeModified",
    "CancelledActivityCannotBeModified",
    "CancelledActivityCannotBeCompleted",
    "CompletedActivityCannotBeCancelled")) {
    if (-not $domain.Contains($marker)) {
        throw "ActivityManagement domain marker missing: $marker"
    }
}

foreach ($marker in @("ScheduleForLead", "ScheduleForContact", "LeadId", "ContactId", "Cancel()")) {
    if (-not $activity.Contains($marker)) {
        throw "Activity entity compatibility marker missing: $marker"
    }
}

foreach ($marker in @(
    "ExactlyOneTargetRequired: true",
    "FollowUpModel: Activity with scheduled/due date",
    "SubjectRules: required, trimmed, max 160 characters",
    "PastScheduleDecision: AllowedForHistoricalRecording",
    "NoChangeBehavior: update with same normalized state returns Allowed=true and Changed=false",
    "CompletedActivityMutationRule:",
    "CancelledActivityMutationRule:",
    "DeleteBehaviorAdded: false",
    "ActivitySecurityReview: PASS",
    "S1301Decision: Implemented")) {
    if (-not $doc.Contains($marker)) {
        throw "S13-01 document marker missing: $marker"
    }
}

foreach ($marker in @(
    "Create_AllowsValidLeadTargetedActivity",
    "Create_AllowsValidContactTargetedActivity",
    "Create_RejectsMissingTarget",
    "Create_RejectsMultipleTargets",
    "Create_RejectsInvalidActivityType",
    "Create_AllowsPastScheduledAtForHistoricalRecording",
    "Update_ReturnsNoChangeForSameNormalizedState",
    "Complete_IsNoChangeForAlreadyCompletedActivity",
    "Complete_RejectsCancelledActivity",
    "Cancel_RejectsCompletedActivity")) {
    if (-not $tests.Contains($marker)) {
        throw "S13-01 unit test marker missing: $marker"
    }
}

foreach ($marker in @("ActivityManagementDomainRules_DoNotDependOnOuterLayers", "ProductiveActivityRoute_RemainsAbsent")) {
    if (-not $architectureTests.Contains($marker)) {
        throw "S13-01 architecture test marker missing: $marker"
    }
}

foreach ($forbidden in @('"/api/crm/activities"', 'MapGet("/api/crm/activities', 'MapPost("/api/crm/activities', 'MapPut("/api/crm/activities', 'MapDelete("/api/crm/activities', '"/api/crm/foundation/activities"', 'MapDelete("/api/crm/foundation/activities')) {
    if ($program.Contains($forbidden)) {
        throw "Forbidden Activity API/DELETE marker detected: $forbidden"
    }
}

if (-not ($nextTask.Contains("CRM Sprint 13 S13-02 - Activity Application Service and Foundation Store") -or $nextTask.Contains("CRM Sprint 13 S13-03 - Activity Foundation API"))) {
    throw "codex/next-task.md must point to S13-02 or a later approved Sprint 13 Activity task."
}

if (-not ($nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-02.md") -or $nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-s13-03.md"))) {
    throw "codex/next-task.md must reference S13-02 or a later approved Sprint 13 Activity prompt."
}

if (-not ($nextTask.Contains("S13-01 merge commit required") -or $nextTask.Contains("S13-02 merge commit required"))) {
    throw "codex/next-task.md must avoid invented future SHA."
}

foreach ($marker in @("S1301Decision: Implemented", "NextTaskPhase: CRM Sprint 13 S13-02 - Activity Application Service and Foundation Store")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md must record S13-01 marker: $marker"
    }
}

Write-Host "CRM Sprint 13 S13-01 verification passed."
