$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-12-p1-contact-management-functional-baseline.md",
    "docs/roadmap/crm-sprint-12-s12-01-contact-contracts-domain-rules.md",
    "docs/roadmap/crm-sprint-12-s12-02-contact-application-service.md",
    "docs/roadmap/crm-sprint-12-s12-03-contact-foundation-api-integration.md",
    "docs/roadmap/crm-sprint-12-s12-04-contact-management-frontend-foundation.md",
    "docs/roadmap/crm-sprint-12-s12-05-contact-management-test-guardrail-hardening.md",
    "docs/roadmap/crm-sprint-12-s12-06-contact-management-local-integration.md",
    "docs/roadmap/crm-sprint-12-contact-management-closure.md",
    "codex/prompts/sprint-13-activity-follow-up-p1.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing S12-07 closure artifact or prerequisite: $file"
    }
}

$closure = Get-Content "docs/roadmap/crm-sprint-12-contact-management-closure.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw

foreach ($marker in @(
    "S1207Decision: ClosedSuccessfully",
    "Sprint12ContactManagementClosed: true",
    "ContactManagementDomainClosure: PASS",
    "ContactManagementApplicationClosure: PASS",
    "ContactManagementApiClosure: PASS",
    "ContactManagementFrontendClosure: PASS",
    "ContactManagementIntegrationClosure: PASS",
    "ContactManagementSecurityClosure: PASS",
    "ContactManagementUxClosure: PASS",
    "S1206DefectClosure: PASS",
    "ProductiveContactRouteEnabled: false",
    "ProductiveContactRouteStatus: LockedOrUnavailable",
    "DeleteBehaviorAdded: false",
    "LeadContactRuntimeImplemented: false",
    "AccountManagementDependency: false",
    "PortalRuntimeEnabled: false",
    "PortalAuthClientAdded: false",
    "AuthorizationHeaderReadAdded: false",
    "TokenStorageAdded: false",
    "CRMOwnedIdentityAdded: false",
    "CommonDbRuntimeEnabled: false",
    "CommonDbReadAttempted: false",
    "CommonDbWriteAttempted: false",
    "CRMOwnedSqlServerDetected: false",
    "SchemaChangesDetected: false",
    "MassAssignmentRisk: Controlled",
    "PiiLoggingDetected: false",
    "PiiPayloadLogged: false",
    "ScopedSecretScan: PASS",
    "RealDataDetected: false",
    "XssReview: PASS",
    "NotFoundDetailContract: 200NullData",
    "NotFoundUpdateContract: 404",
    "NotFoundContractDecision: FutureConsistencyImprovement",
    "DefinitionOfDone: PASS",
    "CriticalClosureBlockers: 0",
    "ContactManagementFoundationSliceStatus: ClosedSuccessfully",
    "ContactManagementFoundationOperationalState: ValidatedLocally",
    "ContactManagementProductiveStatus: NotActivated",
    "SimulatedProductionTouchedBySprint12: false",
    "RealProductionStatus: Deferred",
    "RecommendedNextSliceId: S13-ACTIVITY",
    "RecommendedNextSliceName: Activity / Follow-Up Foundation",
    "RecommendedNextSprint: Sprint13")) {
    if (-not $closure.Contains($marker)) {
        throw "S12-07 closure document missing marker: $marker"
    }
}

foreach ($marker in @("CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline and Backlog", "codex/prompts/sprint-13-activity-follow-up-p1.md", "S12-07 merge commit required")) {
    if (-not $nextTask.Contains($marker)) {
        throw "codex/next-task.md must hand off to Sprint 13 Activity / Follow-Up: $marker"
    }
}

if ($nextTask.Contains("CRM Sprint 12 S12-07 - Contact Management Sprint Closure")) {
    throw "codex/next-task.md must no longer point to S12-07 after closure."
}

foreach ($marker in @("S1207Decision: ClosedSuccessfully", "Sprint12ContactManagementClosed: true", "RecommendedNextSliceId: S13-ACTIVITY")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md must record S12-07 closure marker: $marker"
    }
}

$programWithoutFoundation = $program.Replace('"/api/crm/foundation/contacts"', '""').Replace('"/api/crm/foundation/contacts/{id}"', '""').Replace('"/api/crm/foundation/contacts/preview"', '""').Replace('"/api/crm/foundation/contacts/read-model-preview"', '""')
foreach ($forbidden in @('"/api/crm/contacts"', 'MapDelete("/api/crm/contacts', "ConvertLeadToContact", "CreateContactFromLead")) {
    if ($programWithoutFoundation.Contains($forbidden)) {
        throw "Forbidden productive Contact/Lead runtime marker detected: $forbidden"
    }
}

$contactSourceStart = $frontend.IndexOf("type PreferredContactMethod")
$contactSourceEnd = $frontend.IndexOf("selector: 'crm-home'")
$contactSource = if ($contactSourceStart -ge 0 -and $contactSourceEnd -gt $contactSourceStart) { $frontend.Substring($contactSourceStart, $contactSourceEnd - $contactSourceStart) } else { "" }
foreach ($forbidden in @("/api/crm/contacts", "innerHTML", "bypassSecurityTrustHtml", "localStorage", "sessionStorage", "Bearer ", "Authorization", "DeleteContact", "deleteContact", "ConvertLeadToContact", "CreateContactFromLead")) {
    if ($contactSource.Contains($forbidden)) {
        throw "Forbidden Contact frontend marker detected: $forbidden"
    }
}

Write-Host "CRM Sprint 12 S12-07 verification passed."
