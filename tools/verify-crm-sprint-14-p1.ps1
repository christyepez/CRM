$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-13-activity-follow-up-closure.md",
    "docs/roadmap/crm-sprint-14-opportunity-pipeline-functional-baseline.md",
    "codex/prompts/sprint-14-opportunity-pipeline-s14-01.md",
    "codex/next-task.md",
    "codex/TASKS.md",
    "README.md",
    "src/CRM.Domain/Entities/Opportunity.cs",
    "src/CRM.Domain/Entities/ConceptualEntities.cs",
    "src/CRM.Domain/ValueObjects/BusinessValueObjects.cs",
    "src/CRM.Application/Contracts/CrmDomainCatalogService.cs",
    "src/CRM.Api/Program.cs",
    "frontend/crm-web/src/main.ts"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing Sprint 14 P1 artifact or prerequisite: $file"
    }
}

$closure = Get-Content "docs/roadmap/crm-sprint-13-activity-follow-up-closure.md" -Raw
$baseline = Get-Content "docs/roadmap/crm-sprint-14-opportunity-pipeline-functional-baseline.md" -Raw
$prompt = Get-Content "codex/prompts/sprint-14-opportunity-pipeline-s14-01.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$readme = Get-Content "README.md" -Raw
$opportunity = Get-Content "src/CRM.Domain/Entities/Opportunity.cs" -Raw
$conceptual = Get-Content "src/CRM.Domain/Entities/ConceptualEntities.cs" -Raw
$valueObjects = Get-Content "src/CRM.Domain/ValueObjects/BusinessValueObjects.cs" -Raw
$catalog = Get-Content "src/CRM.Application/Contracts/CrmDomainCatalogService.cs" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw

foreach ($marker in @(
    "S1307Decision: ClosedSuccessfully",
    "Sprint13ActivityFollowUpClosed: true",
    "RecommendedNextSliceId: S14-OPPORTUNITY-PIPELINE",
    "RecommendedNextSliceName: Opportunity Pipeline Foundation",
    "NextTaskPhase: CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog")) {
    if (-not $closure.Contains($marker)) {
        throw "Sprint 13 closure prerequisite missing marker: $marker"
    }
}

foreach ($marker in @(
    "Sprint14P1BaseMainCommit: b1a92f84016136ce2462a74ea2377245aec367d0",
    "SelectedSliceId: S14-OPPORTUNITY-PIPELINE",
    "SelectedSliceName: Opportunity Pipeline Foundation",
    "Sprint14P1Decision: ReadyForS1401OpportunityPipelineContractsAndDomainRules",
    "FirstImplementationStoryId: S14-01",
    "FirstImplementationStoryName: Opportunity Pipeline Contracts and Domain Rules",
    "ProductiveOpportunityRouteEnabled: false",
    "DeleteBehaviorAdded: false",
    "LeadConversionImplemented: false",
    "AccountManagementActivatedBySprint14P1: false",
    "AssignmentRuntimeActivatedBySprint14P1: false",
    "PortalRuntimeEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "EfRuntimeEnabled: false",
    "MigrationsCreated: false",
    "SchemaChangesDetected: false",
    "RealDataDetected: false",
    "SimulatedProductionTouched: false",
    "CrmProdSimTouched: false",
    "FoundationOpportunityRouteEnabledByP1: false",
    "OpportunityDomainStatus: ThinExistingEntity",
    "PipelineDomainStatus: CatalogOnly",
    "PipelineStageDomainStatus: CatalogOnly",
    "OpportunityManagementServiceExists: false",
    "OpportunityFoundationStoreExists: false",
    "FoundationOpportunityApiRouteExists: false",
    "ProductiveOpportunityApiRouteExists: false",
    "AngularOpportunityFoundationRouteExists: false",
    "OpportunityFrontendServiceExists: false",
    "OpportunityApplicationStatus: NotStarted",
    "OpportunityApiStatus: NotStarted",
    "OpportunityFrontendStatus: NotStarted",
    "FirstImplementationPrompt: codex/prompts/sprint-14-opportunity-pipeline-s14-01.md",
    "NextGate: CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules")) {
    if (-not $baseline.Contains($marker)) {
        throw "Sprint 14 P1 baseline missing marker: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules",
    "Sprint 14 P1 merge commit required",
    "crm-sprint-14-s14-01-opportunity-pipeline-contracts-domain-rules",
    'Do not add `IOpportunityManagementService`',
    "Do not add foundation Opportunity API routes in S14-01",
    'Do not add productive `/api/crm/opportunities`',
    "Do not add DELETE",
    "Do not implement Lead conversion",
    "Do not activate Account Management",
    "Do not activate Portal Auth runtime",
    "Do not activate Common DB runtime",
    "tools/verify-crm-sprint-14-p1.ps1")) {
    if (-not $prompt.Contains($marker)) {
        throw "Sprint 14 S14-01 prompt missing marker: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules",
    "codex/prompts/sprint-14-opportunity-pipeline-s14-01.md",
    "Sprint 14 P1 merge commit required")) {
    if (-not $nextTask.Contains($marker)) {
        throw "codex/next-task.md must hand off to S14-01: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog",
    "Sprint14P1Decision: ReadyForS1401OpportunityPipelineContractsAndDomainRules",
    "FirstImplementationStoryId: S14-01",
    "NextTaskPhase: CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md missing Sprint 14 P1 marker: $marker"
    }
}

foreach ($marker in @(
    "CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog",
    'Next gate: `CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules`')) {
    if (-not $readme.Contains($marker)) {
        throw "README.md missing Sprint 14 P1 marker: $marker"
    }
}

foreach ($marker in @(
    "public sealed class Opportunity",
    "public CrmId Id",
    "public CompanyName AccountName",
    "public MoneyAmount ExpectedValue",
    "public Probability Probability",
    "public OpportunityStatus Status",
    "public IReadOnlyCollection<DomainEvent> DomainEvents",
    "public static Opportunity Create",
    "public void MarkWon",
    "OpportunityWonDomainEvent")) {
    if (-not $opportunity.Contains($marker)) {
        throw "Opportunity evidence missing marker: $marker"
    }
}

foreach ($marker in @(
    "public sealed record Pipeline",
    "IReadOnlyCollection<PipelineStage> Stages",
    "public sealed record PipelineStage",
    "int Order")) {
    if (-not $conceptual.Contains($marker)) {
        throw "Pipeline conceptual evidence missing marker: $marker"
    }
}

foreach ($marker in @(
    "Money amount cannot be negative.",
    "Currency must be ISO-like 3 letters.",
    "Probability must be between 0 and 100.")) {
    if (-not $valueObjects.Contains($marker)) {
        throw "Value object evidence missing marker: $marker"
    }
}

foreach ($marker in @(
    'new("Opportunity"',
    'new("Pipeline"',
    'new("PipelineStage"',
    'new("Lead", "Opportunity"',
    'new("Pipeline", "PipelineStage"',
    'new("Opportunity", "Activity"',
    'new("OpportunityWonDomainEvent"')) {
    if (-not $catalog.Contains($marker)) {
        throw "Catalog evidence missing marker: $marker"
    }
}

$runtimeSource = @($program, $frontend) -join "`n"
foreach ($forbidden in @(
    "IOpportunityManagementService",
    "OpportunityManagementService",
    "IOpportunityFoundationStore",
    "InMemoryOpportunityFoundationStore",
    "/api/crm/foundation/opportunities",
    "/api/crm/opportunities",
    "OpportunityApiService",
    "path: 'opportunities'",
    'path: "opportunities"',
    "foundation/opportunities")) {
    if ($runtimeSource.Contains($forbidden)) {
        throw "Forbidden Sprint 14 P1 runtime/API/UI marker detected: $forbidden"
    }
}

foreach ($forbidden in @(
    "MapDelete(",
    ".delete<",
    "deleteOpportunity",
    "LeadConversion",
    "ConvertLead",
    "crm-prod-sim")) {
    if ($program.Contains($forbidden) -or $frontend.Contains($forbidden)) {
        throw "Forbidden Sprint 14 P1 behavior marker detected in runtime source: $forbidden"
    }
}

Write-Host "CRM Sprint 14 P1 verification passed."
