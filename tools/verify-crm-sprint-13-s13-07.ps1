$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-13-p1-activity-follow-up-functional-baseline.md",
    "docs/roadmap/crm-sprint-13-s13-01-activity-contracts-domain-rules.md",
    "docs/roadmap/crm-sprint-13-s13-02-activity-application-service-foundation-store.md",
    "docs/roadmap/crm-sprint-13-s13-03-activity-foundation-api.md",
    "docs/roadmap/crm-sprint-13-s13-04-activity-follow-up-frontend-foundation.md",
    "docs/roadmap/crm-sprint-13-s13-05-activity-test-guardrail-hardening.md",
    "docs/roadmap/crm-sprint-13-s13-06-activity-local-integration.md",
    "docs/roadmap/crm-sprint-13-activity-follow-up-closure.md",
    "codex/prompts/sprint-14-opportunity-pipeline-p1.md",
    "tools/verify-crm-sprint-13-s13-07.ps1",
    "src/CRM.Api/Program.cs",
    "src/CRM.Domain/ActivityManagement/ActivityManagementPolicy.cs",
    "src/CRM.Application/ActivityManagement/ActivityManagementService.cs",
    "src/CRM.Infrastructure/Persistence/Foundation/InMemoryActivityFoundationStore.cs",
    "frontend/crm-web/src/main.ts"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing S13-07 closure artifact or prerequisite: $file"
    }
}

$closure = Get-Content "docs/roadmap/crm-sprint-13-activity-follow-up-closure.md" -Raw
$nextPrompt = Get-Content "codex/prompts/sprint-14-opportunity-pipeline-p1.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw

foreach ($marker in @(
    "S1307Decision: ClosedSuccessfully",
    "Sprint13ActivityFollowUpClosed: true",
    "ActivityFollowUpFoundationSliceStatus: ClosedSuccessfully",
    "ActivityFollowUpFoundationOperationalState: ValidatedLocally",
    "ActivityFollowUpProductiveStatus: NotActivated",
    "DefinitionOfDone: PASS",
    "CriticalClosureBlockers: 0",
    "FinalGoNoGo: GoForFoundationClosureOnly",
    "ProductiveActivityGoNoGo: NoGo",
    "DeleteGoNoGo: NoGo",
    "PortalRuntimeGoNoGo: NoGo",
    "CommonDbRuntimeGoNoGo: NoGo",
    "ActivityManagementDomainClosure: PASS",
    "ActivityManagementApplicationClosure: PASS",
    "ActivityManagementApiClosure: PASS",
    "ActivityManagementFrontendClosure: PASS",
    "ActivityManagementIntegrationClosure: PASS",
    "ActivityManagementSecurityClosure: PASS",
    "ActivityFunctionalBaselineReviewed: true",
    "ActivityDomainRulesReviewed: true",
    "ActivityApplicationServiceReviewed: true",
    "ActivityFoundationStoreReviewed: true",
    "ActivityFoundationApiReviewed: true",
    "ActivityFrontendReviewed: true",
    "ActivityTestGuardrailsReviewed: true",
    "ActivityLocalIntegrationReviewed: true",
    "LeadTargetFoundationSeamReviewed: true",
    "ContactTargetFoundationSeamReviewed: true",
    "FoundationOnlyStatusConfirmed: true",
    "ProductiveActivityRouteEnabled: false",
    "ProductiveActivityRouteStatus: LockedOrUnavailable",
    "DeleteBehaviorAdded: false",
    "FoundationActivityDeleteRouteAvailable: false",
    "LeadConversionImplemented: false",
    "AccountManagementActivatedBySprint13: false",
    "OpportunityRuntimeActivatedBySprint13: false",
    "AssignmentRuntimeActivatedBySprint13: false",
    "PortalRuntimeEnabled: false",
    "PortalAuthClientAdded: false",
    "AuthorizationHeaderReadAdded: false",
    "TokenStorageAdded: false",
    "CRMOwnedIdentityAdded: false",
    "CommonDbRuntimeEnabled: false",
    "CommonDbReadAttempted: false",
    "CommonDbWriteAttempted: false",
    "DurablePersistenceEnabled: false",
    "CRMOwnedSqlServerDetected: false",
    "EfRuntimeEnabled: false",
    "MigrationsCreated: false",
    "SchemaChangesDetected: false",
    "SecretsAdded: false",
    "RealDataDetected: false",
    "MassAssignmentRisk: Controlled",
    "PiiLoggingDetected: false",
    "PiiPayloadLogged: false",
    "ScopedSecretScan: PASS",
    "XssReview: PASS",
    "ResidualRisksRecorded: true",
    "CriticalResidualRisks: 0",
    "RecommendedNextSliceId: S14-OPPORTUNITY-PIPELINE",
    "RecommendedNextSliceName: Opportunity Pipeline Foundation",
    "RecommendedNextSprint: Sprint14",
    "RecommendedNextCapabilityDecision: SelectedExactlyOneBusinessCapability",
    "RejectedNextCapabilityCategories: ProductiveActivation, PortalAuthRuntime, CommonDbRuntime, InfrastructureOnly",
    "NextTaskPhase: CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog",
    "NextTaskPromptFile: codex/prompts/sprint-14-opportunity-pipeline-p1.md")) {
    if (-not $closure.Contains($marker)) {
        throw "S13-07 closure document missing marker: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog",
    "S13-07 merge commit required",
    "crm-sprint-14-p1-opportunity-pipeline-functional-baseline",
    "codex/prompts/sprint-14-opportunity-pipeline-p1.md",
    "Do not activate productive Opportunity APIs",
    "Do not add DELETE",
    "Do not implement Lead conversion",
    "Do not activate Account Management as part of Sprint 14 P1",
    "Do not activate Portal Auth runtime",
    "Do not activate Common DB runtime")) {
    if (-not $nextPrompt.Contains($marker)) {
        throw "Sprint 14 P1 prompt missing marker: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog",
    "codex/prompts/sprint-14-opportunity-pipeline-p1.md",
    "S13-07 merge commit required")) {
    if (-not $nextTask.Contains($marker)) {
        throw "codex/next-task.md must hand off to Sprint 14 P1: $marker"
    }
}

foreach ($marker in @(
    "S1307Decision: ClosedSuccessfully",
    "Sprint13ActivityFollowUpClosed: true",
    "RecommendedNextSliceId: S14-OPPORTUNITY-PIPELINE",
    "NextTaskPhase: CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md missing S13-07 marker: $marker"
    }
}

$programWithoutFoundationActivity = $program.
    Replace('"/api/crm/foundation/activities"', '""').
    Replace('"/api/crm/foundation/activities/{id}"', '""').
    Replace('"/api/crm/foundation/activities/{id}/complete"', '""').
    Replace('"/api/crm/foundation/activities/{id}/cancel"', '""')

foreach ($forbidden in @(
    '"/api/crm/activities"',
    'MapGet("/api/crm/activities',
    'MapPost("/api/crm/activities',
    'MapPut("/api/crm/activities',
    'MapPatch("/api/crm/activities',
    'MapDelete("/api/crm/activities',
    'MapDelete("/api/crm/foundation/activities',
    '"/api/crm/opportunities"',
    'MapGet("/api/crm/opportunities',
    'MapPost("/api/crm/opportunities',
    'MapPut("/api/crm/opportunities',
    'MapPatch("/api/crm/opportunities',
    'MapDelete("/api/crm/opportunities',
    "UseAuthentication",
    "UseAuthorization",
    "UseSqlServer",
    "SqlConnection")) {
    if ($programWithoutFoundationActivity.Contains($forbidden)) {
        throw "Forbidden productive Activity/Opportunity/runtime marker detected: $forbidden"
    }
}

$activityStart = $frontend.IndexOf("type ActivityType")
$activityEnd = $frontend.IndexOf("selector: 'crm-home'")
$activitySource = if ($activityStart -ge 0 -and $activityEnd -gt $activityStart) { $frontend.Substring($activityStart, $activityEnd - $activityStart) } else { "" }

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
    "AccountId",
    "OpportunityId",
    "AssignedUserId",
    "OwnerId",
    "AgentId")) {
    if ($activitySource.Contains($forbidden)) {
        throw "Forbidden Activity frontend marker detected: $forbidden"
    }
}

Write-Host "CRM Sprint 13 S13-07 verification passed."

