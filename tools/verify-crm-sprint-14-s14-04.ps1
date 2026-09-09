$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
Set-Location $root

$required = @(
    "frontend/crm-web/src/main.ts",
    "frontend/crm-web/src/styles.css",
    "docs/roadmap/crm-sprint-14-s14-04-opportunity-pipeline-frontend-foundation.md",
    "codex/prompts/sprint-14-opportunity-pipeline-s14-05.md",
    "codex/next-task.md",
    "codex/TASKS.md"
)

foreach ($file in $required) {
    if (-not (Test-Path $file)) {
        throw "Missing S14-04 file $file"
    }
}

$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw
$styles = Get-Content "frontend/crm-web/src/styles.css" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-14-s14-04-opportunity-pipeline-frontend-foundation.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw

$opportunityStart = $frontend.IndexOf("type OpportunityStatus")
$activityStart = $frontend.IndexOf("type ActivityType")
if ($opportunityStart -lt 0 -or $activityStart -le $opportunityStart) {
    throw "Opportunity frontend source block was not found before Activity block."
}

$opportunitySource = $frontend.Substring($opportunityStart, $activityStart - $opportunityStart)

foreach ($marker in @(
    "OpportunityPipelinePageComponent",
    "OpportunityPipelineApiService",
    "foundationOpportunitiesRoute = '/api/crm/foundation/opportunities'",
    "getOpportunities()",
    "getOpportunity(id: string)",
    "createOpportunity(request: FoundationOpportunityCreateRequest)",
    "updateOpportunity(id: string, request: FoundationOpportunityUpdateRequest)",
    "progressOpportunity(id: string, request: FoundationOpportunityProgressRequest)",
    "winOpportunity(id: string)",
    "loseOpportunity(id: string)",
    "cancelOpportunity(id: string)",
    "Opportunity Pipeline",
    "AccountName",
    "ExpectedValue",
    "Currency",
    "Probability",
    "Pipeline",
    "Stage",
    "Status",
    "Loading foundation opportunities",
    "No opportunities available yet",
    "Validation issue",
    "Opportunity not found",
    "Opportunity workflow unavailable",
    "Terminal opportunities are read-only",
    "Progressed to",
    "Mark won",
    "Mark lost",
    "Cancel opportunity",
    "No changes were necessary",
    "this.loadOpportunities(false)",
    "portalRuntimeEnabled",
    "commonDbRuntimeEnabled"
)) {
    if (-not $opportunitySource.Contains($marker)) {
        throw "Missing S14-04 frontend marker: $marker"
    }
}

if (-not $frontend.Contains("{ path: 'foundation/opportunities', component: OpportunityPipelinePageComponent }")) {
    throw "Missing S14-04 frontend route marker."
}

foreach ($stage in @(
    "{ stageId: 'cccccccc-cccc-cccc-cccc-cccccccccccc', name: 'Qualification', order: 1 }",
    "{ stageId: 'dddddddd-dddd-dddd-dddd-dddddddddddd', name: 'Proposal', order: 2 }",
    "{ stageId: 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee', name: 'Negotiation', order: 3 }",
    "{ stageId: 'ffffffff-ffff-ffff-ffff-ffffffffffff', name: 'Commit', order: 4 }"
)) {
    if (-not $opportunitySource.Contains($stage)) {
        throw "Missing deterministic stage marker: $stage"
    }
}

if (-not $opportunitySource.Contains("if (this.opportunityForm.invalid || this.isSubmitting() || this.selectedOpportunityReadonly())")) {
    throw "Missing duplicate submission and read-only protection before Opportunity foundation API call."
}

if (-not $opportunitySource.Contains("opportunity.status !== 'Open' || this.isSubmitting()")) {
    throw "Missing Open-only lifecycle protection."
}

if (-not $opportunitySource.Contains("stage.order === current.order + 1")) {
    throw "Missing next-stage-only progression logic."
}

foreach ($forbidden in @(
    "/api/crm/opportunities",
    "deleteOpportunity",
    "Delete opportunity",
    "localStorage",
    "sessionStorage",
    "access_token",
    "refresh_token",
    "Bearer ",
    "Authorization",
    "ownerId",
    "assignee",
    "ConvertLead",
    "CreateAccount",
    "UpdateAccount"
)) {
    if ($opportunitySource.Contains($forbidden)) {
        throw "Forbidden S14-04 Opportunity frontend marker found: $forbidden"
    }
}

foreach ($marker in @(".foundation-nav", ".opportunity-grid", ".opportunity-list-item", ".opportunity-actions", ".form-row", "@media (max-width: 760px)")) {
    if (-not $styles.Contains($marker)) {
        throw "Missing S14-04 style marker: $marker"
    }
}

foreach ($marker in @(
    "S1404Decision: Implemented",
    "OpportunityPipelineFrontend: FoundationImplemented",
    "ProductiveOpportunityRouteEnabled: false",
    "DeleteBehaviorAdded: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "SimulatedProductionTouched: false",
    "CrmProdSimTouched: false"
)) {
    if (-not ($doc.Contains($marker) -and $tasks.Contains($marker))) {
        throw "Missing S14-04 documentation/task marker: $marker"
    }
}

foreach ($marker in @(
    "FrontendRoute: /foundation/opportunities",
    "FrontendApiRouteUsed: /api/crm/foundation/opportunities"
)) {
    $docHasMarker = $doc.Contains($marker) -or
        ($marker -eq "FrontendRoute: /foundation/opportunities" -and $doc.Contains("FrontendOpportunityRoute: /foundation/opportunities")) -or
        ($marker -eq "FrontendApiRouteUsed: /api/crm/foundation/opportunities" -and $doc.Contains("FrontendUsesFoundationOpportunityApiOnly: true"))
    if (-not ($docHasMarker -and $tasks.Contains($marker))) {
        throw "Missing S14-04 route/task marker: $marker"
    }
}

$approvedPostClosureHandoff = $nextTask.Contains("CRM Sprint 15 P1 - Campaign Management Functional Baseline and Backlog") -and $nextTask.Contains("codex/prompts/sprint-15-campaign-management-p1.md") -and $nextTask.Contains("S14-07 merge commit required")
if (-not (($nextTask -match "CRM Sprint 14 S14-0[5-7]") -or $approvedPostClosureHandoff)) {
    throw "next-task must point to legitimate Sprint 14 progression or approved Sprint 15 handoff."
}
if (-not (($nextTask -match "codex/prompts/sprint-14-opportunity-pipeline-s14-0[5-7]\.md") -or $approvedPostClosureHandoff)) {
    throw "next-task must reference legitimate Sprint 14 prompt or approved Sprint 15 handoff."
}
Write-Host "CRM Sprint 14 S14-04 verification passed."
