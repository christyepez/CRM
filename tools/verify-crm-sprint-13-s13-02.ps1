$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Application/ActivityManagement/IActivityManagementService.cs",
    "src/CRM.Application/ActivityManagement/ActivityManagementService.cs",
    "src/CRM.Application/ActivityManagement/ActivityManagementApplicationContracts.cs",
    "src/CRM.Application/Ports/Persistence/IActivityFoundationStore.cs",
    "src/CRM.Infrastructure/Persistence/Foundation/InMemoryActivityFoundationStore.cs",
    "tests/CRM.UnitTests/ActivityManagementServiceTests.cs",
    "tests/CRM.UnitTests/InMemoryActivityFoundationStoreTests.cs",
    "tests/CRM.ArchitectureTests/ActivityManagementArchitectureTests.cs",
    "docs/roadmap/crm-sprint-13-s13-02-activity-application-service-foundation-store.md",
    "codex/prompts/sprint-13-activity-follow-up-s13-03.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing S13-02 artifact: $file"
    }
}

$service = Get-Content "src/CRM.Application/ActivityManagement/ActivityManagementService.cs" -Raw
$contracts = Get-Content "src/CRM.Application/ActivityManagement/ActivityManagementApplicationContracts.cs" -Raw
$storeInterface = Get-Content "src/CRM.Application/Ports/Persistence/IActivityFoundationStore.cs" -Raw
$store = Get-Content "src/CRM.Infrastructure/Persistence/Foundation/InMemoryActivityFoundationStore.cs" -Raw
$serviceTests = Get-Content "tests/CRM.UnitTests/ActivityManagementServiceTests.cs" -Raw
$storeTests = Get-Content "tests/CRM.UnitTests/InMemoryActivityFoundationStoreTests.cs" -Raw
$architectureTests = Get-Content "tests/CRM.ArchitectureTests/ActivityManagementArchitectureTests.cs" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-13-s13-02-activity-application-service-foundation-store.md" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "IActivityManagementService",
    "ActivityManagementPolicy.Evaluate",
    "IActivityFoundationStore",
    "ILeadFoundationStore",
    "IContactFoundationStore",
    "ValidateTargetExistsAsync",
    "if (!evaluation.Changed)",
    "activities.SaveAsync",
    "DateTimeOffset.UtcNow",
    "ProductiveCrudEnabled: false")) {
    if (-not $service.Contains($marker)) {
        throw "ActivityManagementService marker missing: $marker"
    }
}

foreach ($marker in @("ActivityManagementCreateApplicationRequest", "ActivityManagementUpdateApplicationRequest", "ActivityManagementApplicationResult", "ActivityManagementApplicationActivity")) {
    if (-not $contracts.Contains($marker)) {
        throw "Activity application contract marker missing: $marker"
    }
}

foreach ($marker in @("GetAllAsync", "GetByIdAsync", "SaveAsync", "GetStatusAsync")) {
    if (-not $storeInterface.Contains($marker)) {
        throw "IActivityFoundationStore marker missing: $marker"
    }
}

foreach ($marker in @("InMemoryActivityFoundationStore", "ActivityFoundationStore", "NonProductionSeam", "Synthetic follow-up call", "Clone(Activity activity)", "cancellationToken.ThrowIfCancellationRequested")) {
    if (-not $store.Contains($marker)) {
        throw "InMemoryActivityFoundationStore marker missing: $marker"
    }
}

foreach ($marker in @(
    "CreateAsync_WithValidLeadTarget_WritesOnce",
    "CreateAsync_WithValidContactTarget_WritesOnce",
    "CreateAsync_InvalidDomainRequest_WritesZero",
    "CreateAsync_MissingLeadTarget_WritesZero",
    "UpdateAsync_ChangedActivity_WritesOnce",
    "UpdateAsync_NoChange_WritesZero",
    "UpdateAsync_MissingActivity_WritesZero",
    "CompleteAsync_ScheduledActivity_WritesOnceAndSetsCompletedAt",
    "CompleteAsync_AlreadyCompleted_WritesZero",
    "CompleteAsync_CancelledActivity_WritesZero",
    "CancelAsync_ScheduledActivity_WritesOnce",
    "CancelAsync_AlreadyCancelled_WritesZero",
    "CancelAsync_CompletedActivity_WritesZero")) {
    if (-not $serviceTests.Contains($marker)) {
        throw "S13-02 service test marker missing: $marker"
    }
}

foreach ($marker in @("SaveAsync_PersistsActivityAndGetByIdFindsCopy", "GetAllAsync_ReturnsSyntheticSeedAndSavedActivity", "SaveAsync_UsesDefensiveCopy", "Operations_HonorCancelledCancellationToken")) {
    if (-not $storeTests.Contains($marker)) {
        throw "S13-02 store test marker missing: $marker"
    }
}

foreach ($marker in @("ActivityApplicationService_DependsOnAbstractionsAndDomainPolicyOnly", "ActivityFoundationStore_IsInfrastructureOnlyAndAvoidsCommonDb")) {
    if (-not $architectureTests.Contains($marker)) {
        throw "S13-02 architecture test marker missing: $marker"
    }
}

foreach ($marker in @(
    "ActivityManagementImplementationStatus: ApplicationAndFoundationStoreImplemented",
    "ActivityManagementApplicationService: Implemented",
    "ActivityManagementFoundationStore: Implemented",
    "ActivityManagementApi: NotImplemented",
    "ActivityManagementFrontend: NotImplemented",
    "PersistenceClassification: FoundationOnly / NonProductionSeam",
    "SyntheticActivitySeedEnabled: true",
    "LeadExistenceValidation: ImplementedFoundationOnly",
    "ContactExistenceValidation: ImplementedFoundationOnly",
    "ValidCreateWriteCount: 1",
    "InvalidCreateWriteCount: 0",
    "ChangedUpdateWriteCount: 1",
    "NoChangeUpdateWriteCount: 0",
    "ValidCompleteWriteCount: 1",
    "RepeatCompleteWriteCount: 0",
    "CancelledCompleteWriteCount: 0",
    "ValidCancelWriteCount: 1",
    "RepeatCancelWriteCount: 0",
    "CompletedCancelWriteCount: 0",
    "NoChangePersistenceSuppressed: true",
    "CancellationTokenSupported: true",
    "ActivityApplicationSecurityReview: PASS",
    "S1302Decision: Implemented")) {
    if (-not $doc.Contains($marker)) {
        throw "S13-02 document marker missing: $marker"
    }
}

foreach ($forbidden in @('"/api/crm/activities"', 'MapGet("/api/crm/activities', 'MapPost("/api/crm/activities', 'MapPut("/api/crm/activities', 'MapDelete("/api/crm/activities', 'MapDelete("/api/crm/foundation/activities')) {
    if ($program.Contains($forbidden)) {
        throw "Forbidden Activity API/DELETE marker detected: $forbidden"
    }
}

foreach ($marker in @(
    "CRM Sprint 13 S13-04 - Activity / Follow-Up Frontend Foundation Page",
    "codex/prompts/sprint-13-activity-follow-up-s13-04.md",
    "S13-03 merge commit required")) {
    if (-not $nextTask.Contains($marker)) {
        throw "codex/next-task.md must point to S13-04 after S13-03: $marker"
    }
}

foreach ($marker in @("S1302Decision: Implemented")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md must record S13-02 marker: $marker"
    }
}

Write-Host "CRM Sprint 13 S13-02 verification passed."
