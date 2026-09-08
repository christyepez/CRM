$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "frontend/crm-web/src/main.ts",
    "docs/roadmap/crm-sprint-13-s13-04-activity-follow-up-frontend-foundation.md",
    "codex/prompts/sprint-13-activity-follow-up-s13-05.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S13-04 file: $file"
    }
}

$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw
$activityStart = $frontend.IndexOf("type ActivityType")
$activityEnd = $frontend.IndexOf("selector: 'crm-home'")
if ($activityStart -lt 0 -or $activityEnd -le $activityStart) {
    throw "Could not isolate S13-04 Activity frontend source."
}
$activitySource = $frontend.Substring($activityStart, $activityEnd - $activityStart)
$doc = Get-Content "docs/roadmap/crm-sprint-13-s13-04-activity-follow-up-frontend-foundation.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "ActivityManagementPageComponent",
    "ActivityManagementApiService",
    "foundationActivitiesRoute = '/api/crm/foundation/activities'",
    "getActivities()",
    "getActivity(id: string)",
    "createActivity(request",
    "updateActivity(id: string",
    "completeActivity(id: string)",
    "cancelActivity(id: string)",
    "Activities & Follow-Up",
    "Create activity",
    "Save activity",
    "Complete",
    "Cancel activity",
    "No activities scheduled yet.",
    "DuplicateSubmissionProtected: true")) {
    if (-not $activitySource.Contains($marker) -and -not $doc.Contains($marker)) {
        throw "S13-04 marker missing: $marker"
    }
}

if (-not $frontend.Contains("path: 'foundation/activities'")) {
    throw "S13-04 route marker missing: path: 'foundation/activities'"
}

foreach ($forbidden in @(
    "/api/crm/activities",
    "path: 'activities'",
    "deleteActivity",
    ".delete<",
    "Delete activity",
    "localStorage",
    "sessionStorage",
    "Authorization",
    "Bearer",
    "bypassSecurityTrustHtml",
    "innerHTML",
    "console.log",
    "SqlConnection",
    "UseSqlServer")) {
    if ($activitySource.Contains($forbidden)) {
        throw "Forbidden S13-04 frontend marker detected: $forbidden"
    }
}

foreach ($marker in @(
    "S1304Decision: Implemented",
    "ActivityManagementImplementationStatus: FrontendFoundationImplemented",
    "ActivityManagementFrontend: FoundationImplemented",
    "FrontendActivityRoute: /foundation/activities",
    "FrontendUsesFoundationActivityApiOnly: true",
    "FrontendUsesProductiveActivityRoute: false",
    "DeleteBehaviorAdded: false",
    "PortalRuntimeEnabled: false",
    "TokenStorageAdded: false",
    "CommonDbDependency: false")) {
    if (-not $doc.Contains($marker)) {
        throw "S13-04 roadmap marker missing: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation",
    "codex/prompts/sprint-13-activity-follow-up-s13-06.md",
    "S13-05 merge commit required")) {
    if (-not $nextTask.Contains($marker)) {
        throw "codex/next-task.md must point to S13-06 after S13-05: $marker"
    }
}

foreach ($marker in @(
    "S1304Decision: Implemented")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md missing S13-04 marker: $marker"
    }
}

Write-Host "CRM Sprint 13 S13-04 verification passed."
