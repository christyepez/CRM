$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "frontend/crm-web/src/main.ts",
    "frontend/crm-web/src/styles.css",
    "frontend/crm-web/tools/verify-crm-foundation.mjs",
    "docs/roadmap/crm-sprint-12-s12-04-contact-management-frontend-foundation.md",
    "codex/prompts/sprint-12-contact-management-s12-05.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing Sprint 12 S12-04 artifact: $file"
    }
}

$main = Get-Content "frontend/crm-web/src/main.ts" -Raw
$styles = Get-Content "frontend/crm-web/src/styles.css" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-12-s12-04-contact-management-frontend-foundation.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "ContactManagementPageComponent",
    "ContactManagementApiService",
    "foundation/contacts",
    "/api/crm/foundation/contacts",
    "getContacts()",
    "getContact(id: string)",
    "createContact(request: FoundationContactCreateRequest)",
    "updateContact(id: string, request: FoundationContactUpdateRequest)",
    "Preferred contact method",
    "No changes were necessary",
    "if (this.contactForm.invalid || this.isSubmitting())")) {
    if (-not $main.Contains($marker)) {
        throw "S12-04 frontend missing marker: $marker"
    }
}

foreach ($marker in @(".contact-grid", ".contact-list-item", ".empty-state", "@media (max-width: 760px)")) {
    if (-not $styles.Contains($marker)) {
        throw "S12-04 styles missing marker: $marker"
    }
}

foreach ($forbidden in @("/api/crm/contacts", "innerHTML", "bypassSecurityTrustHtml", "localStorage", "sessionStorage", "Bearer ", "access_token", "refresh_token", "ConvertLead", "DeleteContact")) {
    if ($main.Contains($forbidden)) {
        throw "S12-04 frontend contains forbidden marker: $forbidden"
    }
}

foreach ($marker in @(
    "ContactManagementImplementationStatus: FrontendFoundationImplemented",
    "ContactManagementFrontend: FoundationImplemented",
    "FrontendUsesProductiveContactRoute: false",
    "DeleteBehaviorAdded: false",
    "LeadContactRuntimeImplemented: false",
    "PortalRuntimeEnabled: false",
    "TokenStorageAdded: false",
    "CommonDbDependency: none",
    "DuplicateSubmissionProtected: true",
    "S1204Decision: Implemented")) {
    if (-not $doc.Contains($marker)) {
        throw "S12-04 roadmap document missing marker: $marker"
    }
}

if (-not ($nextTask.Contains("CRM Sprint 12 S12-05 - Contact Management Test and Guardrail Hardening") -or $nextTask.Contains("CRM Sprint 12 S12-06 - Contact Management Local Integration Validation") -or $nextTask.Contains("CRM Sprint 12 S12-07 - Contact Management Sprint Closure") -or $nextTask.Contains("CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline and Backlog"))) {
    throw "codex/next-task.md must point to S12-05 or a later approved Contact Management handoff."
}

if (-not ($nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-05.md") -or $nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-06.md") -or $nextTask.Contains("codex/prompts/sprint-12-contact-management-s12-07.md") -or $nextTask.Contains("codex/prompts/sprint-13-activity-follow-up-p1.md"))) {
    throw "codex/next-task.md must reference the S12-05 prompt or a later approved Contact Management handoff prompt."
}

if (-not ($nextTask.Contains("S12-04 merge commit required") -or $nextTask.Contains("S12-05 merge commit required") -or $nextTask.Contains("S12-06 merge commit required") -or $nextTask.Contains("S12-07 merge commit required"))) {
    throw "codex/next-task.md must avoid invented future SHA."
}

if (-not $tasks.Contains("S1204Decision: Implemented")) {
    throw "codex/TASKS.md must record S12-04 implementation."
}

Write-Host "CRM Sprint 12 S12-04 verification passed."
