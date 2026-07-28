param()

$ErrorActionPreference = "Continue"
$failures = @()

function Require-Path($Path) {
    if (-not (Test-Path $Path)) {
        $script:failures += "Missing required path: $Path"
    }
}

@(
    "README.md",
    "CRM.sln",
    "src/CRM.Api/CRM.Api.csproj",
    "src/CRM.Application/CRM.Application.csproj",
    "src/CRM.Domain/CRM.Domain.csproj",
    "src/CRM.Infrastructure/CRM.Infrastructure.csproj",
    "tests/CRM.UnitTests/CRM.UnitTests.csproj",
    "tests/CRM.ArchitectureTests/CRM.ArchitectureTests.csproj",
    "docs/architecture/crm-architecture-overview.md",
    "docs/architecture/crm-clean-architecture.md",
    "docs/architecture/crm-portal-integration-principles.md",
    "docs/architecture/crm-security-guardrails.md",
    "docs/domain/crm-domain-model.md",
    "docs/domain/crm-business-rules.md",
    "docs/domain/crm-leads-foundation.md",
    "docs/domain/crm-accounts-foundation.md",
    "docs/domain/crm-contacts-foundation.md",
    "docs/data/crm-persistence-strategy.md",
    "docs/data/crm-read-models.md",
    "docs/data/crm-data-ownership.md",
    "docs/data/crm-migration-readiness-checklist.md",
    "docs/data/crm-sprint-2-p1-persistence-design-review.md",
    "docs/data/crm-logical-data-model.md",
    "docs/data/crm-persistence-activation-gates.md",
    "docs/data/crm-feature-flags-for-persistence.md",
    "docs/data/crm-nonproduction-data-policy.md",
    "docs/data/crm-migration-design-plan.md",
    "docs/data/crm-nonproduction-persistence-seam.md",
    "docs/data/crm-foundation-store-contracts.md",
    "docs/data/crm-persistence-feature-flags.md",
    "docs/data/crm-persistence-seam-risk-register.md",
    "docs/data/crm-foundation-crud-nonproduction-policy.md",
    "docs/data/crm-durable-persistence-readiness-checklist.md",
    "docs/data/crm-durable-persistence-no-go.md",
    "docs/data/crm-sprint-3-p1-durable-persistence-setup-design.md",
    "docs/data/crm-durable-persistence-target-architecture.md",
    "docs/data/crm-common-db-usage-strategy.md",
    "docs/data/crm-migration-and-rollback-strategy.md",
    "docs/data/crm-secrets-and-connection-management-strategy.md",
    "docs/data/crm-nonproduction-durable-persistence-gates.md",
    "docs/data/crm-sprint-3-p2-common-db-connection-secret-strategy.md",
    "docs/data/crm-sprint-3-p3-ef-dbcontext-prototype.md",
    "docs/data/crm-ef-prototype-disabled-flag-policy.md",
    "docs/data/crm-dbcontext-prototype-design.md",
    "docs/data/crm-ef-migrations-no-go-policy.md",
    "docs/data/crm-ef-runtime-activation-gates.md",
    "docs/data/crm-productive-api-persistence-gates.md",
    "docs/integration/crm-sprint-3-p4-portal-auth-runtime-contract-validation.md",
    "docs/integration/crm-portal-auth-runtime-contract.md",
    "docs/integration/crm-sprint-4-p3-portal-auth-runtime-probe.md",
    "docs/integration/crm-portal-auth-runtime-probe-disabled-flag-policy.md",
    "docs/integration/crm-portal-auth-runtime-probe-contract.md",
    "docs/integration/crm-portal-user-tenant-context-contract.md",
    "docs/security/crm-permission-capability-map.md",
    "docs/security/crm-auth-runtime-activation-gates.md",
    "docs/security/crm-portal-auth-runtime-probe-safety-gates.md",
    "docs/security/crm-auth-no-go-policy.md",
    "docs/data/crm-common-db-logical-naming.md",
    "docs/data/crm-db-secret-provider-contract.md",
    "docs/data/crm-connection-string-policy.md",
    "docs/data/crm-secret-rotation-and-access-policy.md",
    "docs/data/crm-db-runtime-readiness-gates.md",
    "docs/data/crm-sprint-4-p2-common-db-runtime-probe.md",
    "docs/data/crm-common-db-runtime-probe-disabled-flag-policy.md",
    "docs/data/crm-common-db-runtime-probe-contract.md",
    "docs/data/crm-common-db-runtime-probe-safety-gates.md",
    "docs/api/crm-api-contracts.md",
    "docs/api/crm-api-index.md",
    "docs/api/crm-sprint-3-p5-productive-api-route-draft.md",
    "docs/api/crm-sprint-4-p4-productive-routes-locked-stub-validation.md",
    "docs/api/crm-productive-api-disabled-route-policy.md",
    "docs/api/crm-productive-api-contract-draft.md",
    "docs/api/crm-productive-routes-locked-stub-policy.md",
    "docs/api/crm-productive-routes-locked-stub-contract.md",
    "docs/api/crm-foundation-preview-api.md",
    "docs/api/crm-foundation-crud-contracts.md",
    "docs/api/crm-read-model-preview-api.md",
    "docs/integration/crm-portal-boundary.md",
    "docs/integration/crm-portal-adapter-contracts.md",
    "docs/integration/crm-portal-capability-map.md",
    "docs/integration/crm-portal-readiness-checklist.md",
    "docs/integration/crm-portal-authorization-simulation.md",
    "docs/integration/crm-financial-boundary.md",
    "docs/integration/crm-financial-adapter-contracts.md",
    "docs/integration/crm-financial-capability-map.md",
    "docs/integration/crm-financial-readiness-checklist.md",
    "docs/integration/crm-financial-event-contracts.md",
    "docs/security/crm-portal-security-boundary.md",
    "docs/security/crm-portal-authorization-boundary.md",
    "docs/security/crm-foundation-permission-simulation.md",
    "docs/security/crm-foundation-crud-security-boundary.md",
    "docs/security/crm-financial-security-boundary.md",
    "docs/security/crm-reporting-security-boundary.md",
    "docs/reporting/crm-reporting-contracts.md",
    "docs/reporting/crm-kpi-catalog.md",
    "docs/reporting/crm-dashboard-catalog.md",
    "docs/reporting/crm-analytics-read-models.md",
    "docs/reporting/crm-powerbi-readiness-checklist.md",
    "docs/roadmap/crm-roadmap.md",
    "docs/roadmap/crm-sprint-plan.md",
    "docs/releases/crm-sprint-1-notes.md",
    "docs/releases/crm-sprint-1-foundation-closure.md",
    "docs/releases/crm-sprint-1-integrated-evidence.md",
    "docs/releases/crm-sprint-1-go-no-go.md",
    "docs/releases/crm-sprint-1-open-risks.md",
    "docs/releases/crm-sprint-1-next-sprint-options.md",
    "docs/releases/crm-sprint-2-p5-integration-readiness-review.md",
    "docs/releases/crm-sprint-2-p5-evidence.md",
    "docs/releases/crm-sprint-2-p5-go-no-go.md",
    "docs/releases/crm-sprint-2-p5-open-risks.md",
    "docs/releases/crm-sprint-2-p5-decision-record.md",
    "docs/releases/crm-sprint-2-closure.md",
    "docs/releases/crm-sprint-2-productization-gate-decision.md",
    "docs/releases/crm-sprint-2-integrated-evidence.md",
    "docs/releases/crm-sprint-2-open-risks.md",
    "docs/releases/crm-sprint-2-decision-record.md",
    "docs/releases/crm-sprint-3-closure.md",
    "docs/releases/crm-sprint-3-integrated-evidence.md",
    "docs/releases/crm-sprint-3-productization-review.md",
    "docs/releases/crm-sprint-3-go-no-go.md",
    "docs/releases/crm-sprint-3-open-risks.md",
    "docs/releases/crm-sprint-3-decision-record.md",
    "docs/architecture/crm-integrated-capability-matrix.md",
    "docs/architecture/crm-ownership-boundaries.md",
    "docs/architecture/crm-cross-module-dependency-map.md",
    "docs/architecture/crm-sprint-2-activation-gate-matrix.md",
    "docs/architecture/crm-db-auth-crud-readiness-map.md",
    "docs/architecture/crm-sprint-2-productization-decision-matrix.md",
    "docs/architecture/crm-sprint-3-option-map.md",
    "docs/architecture/crm-sprint-3-productization-review-matrix.md",
    "docs/architecture/crm-sprint-4-option-map.md",
    "docs/api/crm-foundation-endpoint-inventory.md",
    "docs/security/crm-foundation-guardrail-register.md",
    "docs/security/crm-sensitive-data-policy.md",
    "docs/security/crm-productive-security-readiness-checklist.md",
    "docs/security/crm-auth-productization-no-go.md",
    "docs/security/crm-productive-api-auth-gates.md",
    "docs/security/crm-productive-routes-locked-stub-safety-gates.md",
    "docs/security/crm-sprint-3-security-no-go-review.md",
    "docs/roadmap/crm-sprint-2-options.md",
    "docs/roadmap/crm-sprint-2-recommended-path.md",
    "docs/roadmap/crm-sprint-2-p3-portal-auth-simulation.md",
    "docs/roadmap/crm-sprint-2-p4-controlled-crud.md",
    "docs/roadmap/crm-productization-gates.md",
    "docs/roadmap/crm-sprint-3-options.md",
    "docs/roadmap/crm-sprint-3-recommended-path.md",
    "docs/roadmap/crm-sprint-3-gates.md",
    "docs/roadmap/crm-sprint-4-options.md",
    "docs/roadmap/crm-sprint-4-recommended-path.md",
    "docs/roadmap/crm-sprint-4-gates.md",
    "docs/data/crm-sprint-3-persistence-no-go-review.md",
    "docs/api/crm-sprint-3-api-no-go-review.md",
    "docs/operations/crm-sprint-4-p1-runtime-environment-readiness.md",
    "docs/operations/crm-local-development-runbook-windows.md",
    "docs/operations/crm-docker-compose-readiness.md",
    "docs/operations/crm-node-tooling-readiness.md",
    "docs/operations/crm-healthcheck-runbook.md",
    "docs/operations/crm-runtime-preflight-checklist.md",
    "docs/operations/crm-common-db-runtime-probe-runbook.md",
    "docs/operations/crm-portal-auth-runtime-probe-runbook.md",
    "docs/operations/crm-productive-routes-locked-stub-runbook.md",
    "docs/testing/crm-sprint-4-p5-nonproduction-e2e-pilot-readiness.md",
    "docs/testing/crm-nonproduction-e2e-scenario-matrix.md",
    "docs/testing/crm-foundation-only-e2e-test-plan.md",
    "docs/testing/crm-e2e-evidence-checklist.md",
    "docs/operations/crm-nonproduction-e2e-pilot-runbook.md",
    "docs/security/crm-e2e-pilot-safety-boundary.md",
    "docs/releases/crm-sprint-4-closure.md",
    "docs/releases/crm-sprint-4-integrated-evidence.md",
    "docs/releases/crm-sprint-4-gate-decision.md",
    "docs/releases/crm-sprint-4-go-no-go.md",
    "docs/releases/crm-sprint-4-open-risks.md",
    "docs/releases/crm-sprint-4-decision-record.md",
    "docs/architecture/crm-sprint-4-gate-matrix.md",
    "docs/security/crm-sprint-4-security-gate-review.md",
    "docs/data/crm-sprint-4-persistence-gate-review.md",
    "docs/api/crm-sprint-4-api-gate-review.md",
    "docs/testing/crm-sprint-4-e2e-gate-review.md",
    "docs/roadmap/crm-sprint-5-options.md",
    "docs/roadmap/crm-sprint-5-recommended-path.md",
    "docs/roadmap/crm-sprint-5-gates.md",
    "docs/operations/crm-sprint-5-p1-controlled-runtime-probe-activation-plan.md",
    "docs/operations/crm-runtime-probe-activation-approval-matrix.md",
    "docs/operations/crm-runtime-probe-activation-checklist.md",
    "docs/operations/crm-runtime-probe-rollback-plan.md",
    "docs/operations/crm-runtime-probe-observability-plan.md",
    "docs/security/crm-runtime-probe-synthetic-data-policy.md",
    "docs/security/crm-runtime-probe-secret-handling-policy.md",
    "docs/security/crm-sprint-5-p2-secret-provider-runtime-contract-validation.md",
    "docs/security/crm-secret-provider-runtime-contract.md",
    "docs/security/crm-secret-provider-no-secret-read-policy.md",
    "docs/security/crm-secret-provider-approval-gates.md",
    "docs/operations/crm-secret-provider-runtime-runbook.md",
    "docs/data/crm-sprint-5-p3-common-db-probe-optional-activation.md",
    "docs/data/crm-common-db-probe-optional-activation-policy.md",
    "docs/data/crm-common-db-probe-activation-gates.md",
    "docs/data/crm-common-db-probe-rollback-plan.md",
    "docs/operations/crm-common-db-probe-optional-activation-runbook.md",
    "docs/security/crm-common-db-probe-secret-dependency.md",
    "tools/preflight-crm-local.ps1",
    "tools/check-crm-guardrails.ps1",
    "tools/check-crm-health.ps1",
    "tools/check-crm-e2e-foundation.ps1",
    "frontend/crm-web/package.json",
    "src/CRM.Domain/Entities/Lead.cs",
    "src/CRM.Domain/Entities/Opportunity.cs",
    "src/CRM.Domain/Entities/Activity.cs",
    "src/CRM.Domain/ValueObjects/ContactValueObjects.cs",
    "src/CRM.Domain/ValueObjects/BusinessValueObjects.cs",
    "src/CRM.Domain/ValueObjects/FoundationValueObjects.cs",
    "src/CRM.Application/Contracts/CrmDomainCatalogService.cs",
    "src/CRM.Application/Foundation/CrmSprint1ClosureStatusService.cs",
    "src/CRM.Application/Foundation/LeadFoundationService.cs",
    "src/CRM.Application/Foundation/AccountFoundationService.cs",
    "src/CRM.Application/Foundation/ContactFoundationService.cs",
    "src/CRM.Application/Foundation/FoundationLeadCrudContracts.cs",
    "src/CRM.Application/Foundation/FoundationAccountCrudContracts.cs",
    "src/CRM.Application/Foundation/FoundationContactCrudContracts.cs",
    "src/CRM.Application/Foundation/FoundationCrudStatusContracts.cs",
    "src/CRM.Application/Foundation/FoundationLeadCrudService.cs",
    "src/CRM.Application/Foundation/FoundationAccountCrudService.cs",
    "src/CRM.Application/Foundation/FoundationContactCrudService.cs",
    "src/CRM.Application/Foundation/FoundationCrudStatusService.cs",
    "src/CRM.Application/Foundation/CrmSprint2IntegrationReadinessContracts.cs",
    "src/CRM.Application/Foundation/CrmSprint2IntegrationReadinessService.cs",
    "src/CRM.Application/Foundation/CrmSprint2ProductizationGateContracts.cs",
    "src/CRM.Application/Foundation/CrmSprint2ProductizationGateService.cs",
    "src/CRM.Application/Foundation/CrmDurablePersistenceSetupContracts.cs",
    "src/CRM.Application/Foundation/CrmDurablePersistenceSetupStatusService.cs",
    "src/CRM.Application/Foundation/CrmCommonDbConnectionStrategyContracts.cs",
    "src/CRM.Application/Foundation/CrmCommonDbConnectionStrategyStatusService.cs",
    "src/CRM.Application/Foundation/CrmEfPrototypeContracts.cs",
    "src/CRM.Application/Foundation/CrmEfPrototypeStatusService.cs",
    "src/CRM.Application/Foundation/CrmProductiveApiRouteDraftContracts.cs",
    "src/CRM.Application/Foundation/CrmProductiveApiRouteDraftStatusService.cs",
    "src/CRM.Application/Foundation/CrmSprint3ProductizationReviewContracts.cs",
    "src/CRM.Application/Foundation/CrmSprint3ProductizationReviewStatusService.cs",
    "src/CRM.Application/Foundation/CrmRuntimeEnvironmentReadinessContracts.cs",
    "src/CRM.Application/Foundation/CrmRuntimeEnvironmentReadinessStatusService.cs",
    "src/CRM.Application/Foundation/CrmCommonDbRuntimeProbeContracts.cs",
    "src/CRM.Application/Foundation/CrmCommonDbRuntimeProbeStatusService.cs",
    "src/CRM.Application/Foundation/CrmProductiveRoutesLockedStubContracts.cs",
    "src/CRM.Application/Foundation/CrmProductiveRoutesLockedStubStatusService.cs",
    "src/CRM.Application/Foundation/CrmNonProductionE2EPilotReadinessContracts.cs",
    "src/CRM.Application/Foundation/CrmNonProductionE2EPilotReadinessStatusService.cs",
    "src/CRM.Application/Foundation/CrmSprint4GateDecisionContracts.cs",
    "src/CRM.Application/Foundation/CrmSprint4GateDecisionStatusService.cs",
    "src/CRM.Application/Foundation/CrmControlledRuntimeProbeActivationPlanContracts.cs",
    "src/CRM.Application/Foundation/CrmControlledRuntimeProbeActivationPlanStatusService.cs",
    "src/CRM.Application/Foundation/CrmSecretProviderRuntimeContractContracts.cs",
    "src/CRM.Application/Foundation/CrmSecretProviderRuntimeContractStatusService.cs",
    "src/CRM.Application/Foundation/CrmCommonDbProbeOptionalActivationContracts.cs",
    "src/CRM.Application/Foundation/CrmCommonDbProbeOptionalActivationStatusService.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbProbeOptionalActivationOptions.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbProbeOptionalActivationPlaceholder.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeContractOptions.cs",
    "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeContractPlaceholder.cs",
    "src/CRM.Application/Portal/CrmPortalAuthRuntimeProbeContracts.cs",
    "src/CRM.Application/Portal/CrmPortalAuthRuntimeProbeStatusService.cs",
    "src/CRM.Infrastructure/Configuration/CrmSecretProviderPlaceholder.cs",
    "src/CRM.Infrastructure/Configuration/CrmDatabaseConfigurationPlaceholder.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbRuntimeProbeOptions.cs",
    "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbRuntimeProbePlaceholder.cs",
    "src/CRM.Infrastructure/Persistence/EfPrototype/CrmEfPrototypeOptions.cs",
    "src/CRM.Infrastructure/Persistence/EfPrototype/CrmEfPrototypeMarker.cs",
    "src/CRM.Infrastructure/Persistence/EfPrototype/CrmDbContextPrototype.cs",
    "src/CRM.Application/Persistence/CrmPersistencePorts.cs",
    "src/CRM.Application/Persistence/CrmPersistenceDesignContracts.cs",
    "src/CRM.Application/Persistence/CrmPersistenceReadinessService.cs",
    "src/CRM.Application/Persistence/CrmPersistenceSeamContracts.cs",
    "src/CRM.Application/Persistence/CrmPersistenceSeamStatusService.cs",
    "src/CRM.Application/Ports/Persistence/ILeadFoundationStore.cs",
    "src/CRM.Application/Ports/Persistence/IAccountFoundationStore.cs",
    "src/CRM.Application/Ports/Persistence/IContactFoundationStore.cs",
    "src/CRM.Application/Ports/Persistence/ICrmFoundationUnitOfWork.cs",
    "src/CRM.Application/Ports/Persistence/ICrmPersistenceFeatureFlagProvider.cs",
    "src/CRM.Infrastructure/Persistence/Foundation/InMemoryLeadFoundationStore.cs",
    "src/CRM.Infrastructure/Persistence/Foundation/InMemoryAccountFoundationStore.cs",
    "src/CRM.Infrastructure/Persistence/Foundation/InMemoryContactFoundationStore.cs",
    "src/CRM.Infrastructure/Persistence/Foundation/InMemoryCrmFoundationUnitOfWork.cs",
    "src/CRM.Infrastructure/Persistence/Foundation/StaticCrmPersistenceFeatureFlagProvider.cs",
    "src/CRM.Application/Portal/CrmPortalIntegrationStatusService.cs",
    "src/CRM.Application/Portal/PortalIntegrationContracts.cs",
    "src/CRM.Application/Portal/CrmPortalAuthorizationSimulationContracts.cs",
    "src/CRM.Application/Portal/CrmPortalAuthorizationSimulationService.cs",
    "src/CRM.Application/Portal/CrmPortalAuthRuntimeContracts.cs",
    "src/CRM.Application/Portal/CrmPortalAuthRuntimeContractStatusService.cs",
    "src/CRM.Application/Portal/CrmFoundationPermissionGuard.cs",
    "src/CRM.Application/Ports/Portal/IPortalAuthorizationScenarioProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalUserContextProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalPermissionProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalMenuRegistrationProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalAuditPublisher.cs",
    "src/CRM.Application/Ports/Portal/IPortalNotificationPublisher.cs",
    "src/CRM.Application/Ports/Portal/IPortalConfigurationProvider.cs",
    "src/CRM.Application/Ports/Portal/IPortalCorrelationContext.cs",
    "src/CRM.Infrastructure/Portal/PortalAdapterNotConfiguredException.cs",
    "src/CRM.Infrastructure/Portal/PortalIntegrationPlaceholder.cs",
    "src/CRM.Infrastructure/Portal/Simulation/SimulatedPortalUserContextProvider.cs",
    "src/CRM.Infrastructure/Portal/Simulation/SimulatedPortalPermissionProvider.cs",
    "src/CRM.Infrastructure/Portal/Simulation/SimulatedPortalAuthorizationScenarioProvider.cs",
    "src/CRM.Infrastructure/Portal/Runtime/PortalAuthRuntimeAdapterPlaceholder.cs",
    "src/CRM.Infrastructure/Portal/Runtime/PortalAuthContextMapperPlaceholder.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRuntimeProbeOptions.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRuntimeProbePlaceholder.cs",
    "src/CRM.Application/Financial/CrmFinancialIntegrationStatusService.cs",
    "src/CRM.Application/Financial/FinancialIntegrationContracts.cs",
    "src/CRM.Application/Financial/FinancialConceptualEvents.cs",
    "src/CRM.Application/Ports/Financial/IFinancialCustomerLookupPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialAccountStatusPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialInvoiceAwarenessPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialPaymentStatusPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialCollectionsSignalPort.cs",
    "src/CRM.Application/Ports/Financial/IFinancialEventPublisher.cs",
    "src/CRM.Infrastructure/Financial/FinancialAdapterNotConfiguredException.cs",
    "src/CRM.Infrastructure/Financial/FinancialIntegrationPlaceholder.cs",
    "src/CRM.Application/Reporting/CrmReportingIntegrationStatusService.cs",
    "src/CRM.Application/Reporting/ReportingContracts.cs",
    "src/CRM.Application/Ports/Reporting/ICrmKpiCatalogProvider.cs",
    "src/CRM.Application/Ports/Reporting/ICrmDashboardCatalogProvider.cs",
    "src/CRM.Application/Ports/Reporting/ICrmAnalyticsReadModelProvider.cs",
    "src/CRM.Application/Ports/Reporting/ICrmReportAuthorizationContext.cs",
    "src/CRM.Infrastructure/Reporting/ReportingAdapterNotConfiguredException.cs",
    "src/CRM.Infrastructure/Reporting/ReportingIntegrationPlaceholder.cs",
    "src/CRM.Application/ReadModels/ReadModelContracts.cs",
    "src/CRM.Application/ReadModels/ReadModelPreviewServices.cs"
) | ForEach-Object { Require-Path $_ }

$composeText = if (Test-Path "docker-compose.yml") { Get-Content -Raw "docker-compose.yml" } else { "" }
$composeCrmText = if (Test-Path "docker-compose.crm.yml") { Get-Content -Raw "docker-compose.crm.yml" } else { "" }
if (($composeText + $composeCrmText) -match "mcr\.microsoft\.com/mssql|1433:1433|container_name:\s*.*sql") {
    $failures += "SQL Server container or default SQL port mapping found in compose."
}

if (Test-Path ".env") {
    $failures += ".env must not be committed or required."
}

$scanRoots = @("src", "tests", "frontend", "docker-compose.yml", "docker-compose.crm.yml")
$patterns = "BEGIN PRIVATE KEY|BEGIN CERTIFICATE|access_token=|id_token=|refresh_token=|localStorage|sessionStorage|Microsoft\.AspNetCore\.Identity|AddIdentity|HardcodedFinanciero|https://github\.com/christyepez/Financiero/.*/api|https?://.*Financiero|https?://.*PortalCorporativo"
foreach ($root in $scanRoots) {
    if (Test-Path $root) {
        $paths = if ((Get-Item $root).PSIsContainer) {
            Get-ChildItem -Path $root -Recurse -File -ErrorAction SilentlyContinue |
                Where-Object { $_.FullName -notmatch "\\(bin|obj|node_modules|dist|\.angular|tools)\\" } |
                Select-Object -ExpandProperty FullName
        } else {
            @($root)
        }
        $matches = if ($paths.Count -gt 0) { Select-String -Path $paths -Pattern $patterns -ErrorAction SilentlyContinue } else { @() }
        foreach ($match in $matches) {
            $failures += "Forbidden pattern in $($match.Path):$($match.LineNumber)"
        }
    }
}

$apiProgram = Get-Content -Raw "src/CRM.Api/Program.cs"
foreach ($route in @('/health', '/health/live', '/health/ready', '/api/crm/readiness', '/api/crm/domain-catalog', '/api/crm/contracts', '/api/crm/integration-boundaries', '/api/crm/foundation/leads/preview', '/api/crm/foundation/accounts/preview', '/api/crm/foundation/contacts/preview', '/api/crm/foundation/crud/status', '/api/crm/foundation/leads', '/api/crm/foundation/leads/{id}', '/api/crm/foundation/accounts', '/api/crm/foundation/accounts/{id}', '/api/crm/foundation/contacts', '/api/crm/foundation/contacts/{id}', '/api/crm/foundation/leads/read-model-preview', '/api/crm/foundation/accounts/read-model-preview', '/api/crm/foundation/contacts/read-model-preview', '/api/crm/foundation/read-model-status', '/api/crm/foundation/portal-integration/status', '/api/crm/foundation/portal-integration/contracts', '/api/crm/foundation/portal-integration/required-capabilities', '/api/crm/foundation/portal-authorization/simulation-status', '/api/crm/foundation/portal-authorization/scenarios', '/api/crm/foundation/portal-authorization/permissions', '/api/crm/foundation/portal-authorization/sample-user-context', '/api/crm/foundation/portal-authorization/check-permission', '/api/crm/foundation/financial-integration/status', '/api/crm/foundation/financial-integration/contracts', '/api/crm/foundation/financial-integration/required-capabilities', '/api/crm/foundation/financial-integration/events', '/api/crm/foundation/reporting/status', '/api/crm/foundation/reporting/kpis', '/api/crm/foundation/reporting/dashboards', '/api/crm/foundation/reporting/analytics-read-models', '/api/crm/foundation/sprint-1/closure-status', '/api/crm/foundation/persistence/readiness', '/api/crm/foundation/persistence/seam-status', '/api/crm/foundation/persistence/feature-flags', '/api/crm/foundation/persistence/stores/status', '/api/crm/foundation/persistence/stores/clear-preview', '/api/crm/foundation/sprint-2/integration-readiness', '/api/crm/foundation/sprint-2/productization-gate', '/api/crm/foundation/sprint-3/durable-persistence-setup', '/api/crm/foundation/sprint-3/common-db-connection-strategy', '/api/crm/foundation/sprint-3/ef-prototype-status', '/api/crm/foundation/sprint-3/portal-auth-runtime-contract', '/api/crm/foundation/sprint-3/productive-api-route-draft', '/api/crm/foundation/sprint-3/productization-review', '/api/crm/foundation/sprint-4/runtime-readiness', '/api/crm/foundation/sprint-4/common-db-runtime-probe', '/api/crm/foundation/sprint-4/portal-auth-runtime-probe', '/api/crm/foundation/sprint-4/productive-routes-locked-stub', '/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness', '/api/crm/foundation/sprint-4/gate-decision', '/api/crm/foundation/sprint-5/runtime-probe-activation-plan', '/api/crm/foundation/sprint-5/secret-provider-runtime-contract', '/api/crm/foundation/sprint-5/common-db-probe-optional-activation')) {
    if ($apiProgram -notlike "*$route*") {
        $failures += "Missing documented route $route"
    }
}

if ($apiProgram -match "MapPut|MapPatch|MapDelete|CreateLead|CreateCustomer|CreateOpportunity") {
    $allowedMutationsProgram = $apiProgram.
        Replace('MapPut("/api/crm/foundation/leads/{id}"', '').
        Replace('MapPut("/api/crm/foundation/accounts/{id}"', '').
        Replace('MapPut("/api/crm/foundation/contacts/{id}"', '')
    if ($allowedMutationsProgram -notmatch "MapPut|MapPatch|MapDelete|CreateLead|CreateCustomer|CreateOpportunity") {
        $null = $true
    }
    else {
    $failures += "Premature CRM mutating endpoint found."
    }
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/portal-integration") {
    $failures += "Portal integration endpoints must remain GET-only foundation endpoints."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/financial-integration") {
    $failures += "Financial integration endpoints must remain GET-only foundation endpoints."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/reporting") {
    $failures += "Reporting endpoints must remain GET-only foundation endpoints."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-1") {
    $failures += "Closure endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/persistence/readiness|Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/persistence/seam-status|Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/persistence/feature-flags|Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/persistence/stores/status|Map(Put|Patch|Delete)\(`"/api/crm/foundation/persistence") {
    $failures += "Persistence seam endpoints must remain foundation-only; only clear-preview POST is allowed."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-2/integration-readiness") {
    $failures += "Integration readiness endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-2/productization-gate") {
    $failures += "Productization gate endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-3/durable-persistence-setup") {
    $failures += "Durable persistence setup endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-3/common-db-connection-strategy") {
    $failures += "Common DB connection strategy endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-3/ef-prototype-status") {
    $failures += "EF prototype endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-3/portal-auth-runtime-contract") {
    $failures += "Portal Auth runtime contract endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-3/productive-api-route-draft") {
    $failures += "Productive API route draft endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-3/productization-review") {
    $failures += "Sprint 3 productization review endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/runtime-readiness") {
    $failures += "Sprint 4 runtime readiness endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/common-db-runtime-probe") {
    $failures += "Sprint 4 common DB runtime probe endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/portal-auth-runtime-probe") {
    $failures += "Sprint 4 Portal Auth runtime probe endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/productive-routes-locked-stub") {
    $failures += "Sprint 4 productive routes locked stub endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness") {
    $failures += "Sprint 4 non-production E2E pilot readiness endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-4/gate-decision") {
    $failures += "Sprint 4 gate decision endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/runtime-probe-activation-plan") {
    $failures += "Sprint 5 controlled runtime probe activation plan endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/secret-provider-runtime-contract") {
    $failures += "Sprint 5 secret provider runtime contract endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/common-db-probe-optional-activation") {
    $failures += "Sprint 5 common DB probe optional activation endpoint must remain GET-only foundation endpoint."
}

if ($apiProgram -match "/login|/logout") {
    $failures += "CRM login/logout endpoint found."
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($apiProgram -like "*$productiveRoute*") {
        $failures += "Productive CRM endpoint found: $productiveRoute"
    }
}

foreach ($productiveRoute in @('MapGet("/api/crm/leads', 'MapGet("/api/crm/accounts', 'MapGet("/api/crm/contacts', 'MapPost("/api/crm/leads', 'MapPost("/api/crm/accounts', 'MapPost("/api/crm/contacts', 'MapPut("/api/crm/leads', 'MapPut("/api/crm/accounts', 'MapPut("/api/crm/contacts')) {
    if ($apiProgram -like "*$productiveRoute*") {
        $failures += "Productive CRM route registration found: $productiveRoute"
    }
}

$foundationText = ""
Get-ChildItem -Path "src/CRM.Application/Foundation" -Filter "*.cs" -File | ForEach-Object { $foundationText += "`n" + (Get-Content -Raw $_.FullName) }
if ($apiProgram -notlike "*Preview only, not persisted*" -and $foundationText -notlike "*Preview only, not persisted*") {
    $failures += "Foundation preview warning is missing."
}

$readModelText = Get-Content -Raw "src/CRM.Application/ReadModels/ReadModelPreviewServices.cs"
if ($apiProgram -notlike "*Read model preview only, not persisted*" -and $readModelText -notlike "*Read model preview only, not persisted*") {
    $failures += "Read model preview warning is missing."
}

$portText = Get-Content -Raw "src/CRM.Application/Persistence/CrmPersistencePorts.cs"
foreach ($port in @("ILeadReadModelStore", "IAccountReadModelStore", "IContactReadModelStore", "ICrmUnitOfWork", "ICrmClock", "FuturePersistencePort")) {
    if ($portText -notlike "*$port*") {
        $failures += "Missing persistence port marker: $port"
    }
}

$sourceText = ""
foreach ($root in @("src")) {
    if (Test-Path $root) {
        Get-ChildItem -Path $root -Recurse -File -ErrorAction SilentlyContinue |
            Where-Object { $_.FullName -notmatch "\\(bin|obj)\\" } |
            ForEach-Object { $sourceText += "`n" + (Get-Content -Raw $_.FullName) }
    }
}

$persistenceScanText = $sourceText.Replace("DbContextConfigured", "").Replace("dbContextConfigured", "").Replace("DbContext Configured", "").Replace("DbContextRuntimeActive", "").Replace("dbContextRuntimeActive", "").Replace("DbContext Runtime Active", "").Replace("CrmDbContextPrototypeContract", "").Replace("CrmDbContextPrototype", "").Replace("InheritsRealDbContext", "").Replace("UseSqlServerConfigured", "").Replace("useSqlServerConfigured", "").Replace("UseSqlServer Configured", "").Replace("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", "").Replace("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", "").Replace("EfDbContextPrototypeDisabled", "").Replace("EF/DbContext prototype only; runtime disabled and no database configured", "")
if ($persistenceScanText -match "DbContext|DbSet<|MigrationBuilder|UseSqlServer|UseNpgsql|AddDbContext") {
    $failures += "Productive persistence, migration or DbContext reference found."
}

foreach ($marker in @("Portal integration contracts only; no runtime calls configured", "FuturePortalAdapter", "NonProductionPlaceholder")) {
    if ($sourceText -notlike "*$marker*") {
        $failures += "Missing Portal integration guardrail marker: $marker"
    }
}

foreach ($marker in @("Financial integration contracts only; no runtime calls configured", "FutureFinancialAdapter", "NoSharedDatabase", "CustomerConvertedForFinancialIntegration", "CollectionsRiskRaisedFinancialSignal")) {
    if ($sourceText -notlike "*$marker*") {
        $failures += "Missing Financial integration guardrail marker: $marker"
    }
}

foreach ($marker in @("Reporting contracts only; no analytics runtime configured", "FutureReportingAdapter", "LeadConversionRate", "CRM Executive Overview", "FoundationMock")) {
    if ($sourceText -notlike "*$marker*") {
        $failures += "Missing Reporting guardrail marker: $marker"
    }
}

foreach ($marker in @("Foundation closure only; no productive activation", "FoundationClosed", "NotReady", "Sprint2Planning")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md")) -notlike "*$marker*") {
        $failures += "Missing closure guardrail marker: $marker"
    }
}

foreach ($marker in @("Persistence design review only; no database configured", "PersistenceDesignReview", "DesignOnly", "Sprint2P2PersistenceSeam", "CRM_PERSISTENCE_ENABLED=false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-feature-flags-for-persistence.md")) -notlike "*$marker*") {
        $failures += "Missing persistence design marker: $marker"
    }
}

foreach ($marker in @("Non-production persistence seam only; no database configured", "PersistenceSeamActive", "NonProductionSeam", "Sprint2P3PortalAuthorizationAdapterSimulation", "CRM_PERSISTENCE_SEAM_ENABLED=true", "CRM_PRODUCTIVE_CRUD_ENABLED=false", "CRM_DURABLE_PERSISTENCE_ENABLED=false", "ILeadFoundationStore", "IAccountFoundationStore", "IContactFoundationStore", "InMemoryLeadFoundationStore", "InMemoryAccountFoundationStore", "InMemoryContactFoundationStore")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-persistence-feature-flags.md")) -notlike "*$marker*") {
        $failures += "Missing persistence seam marker: $marker"
    }
}

if ($sourceText -match "HttpClient|PortalCorporativoUrl|PortalBaseUrl|portalBaseUrl") {
    $failures += "Runtime Portal adapter, URL or HTTP client found before integration approval."
}

foreach ($marker in @("Portal authorization simulation only; no real Portal runtime configured", "PortalAuthorizationSimulationActive", "FoundationSimulation", "Sprint2P4ControlledCrudBehindFoundationFlag", "CrmPortalAuthorizationSimulationService", "CrmFoundationPermissionGuard", "SimulatedPortalUserContextProvider", "SimulatedPortalPermissionProvider", "SimulatedPortalAuthorizationScenarioProvider", "crm.foundation.preview.clear")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/integration/crm-portal-authorization-simulation.md") + "`n" + (Get-Content -Raw "docs/security/crm-foundation-permission-simulation.md")) -notlike "*$marker*") {
        $failures += "Missing Portal authorization simulation marker: $marker"
    }
}

foreach ($marker in @("Foundation CRUD only; no productive endpoint or database configured", "FoundationCrudEnabled", "Sprint2P5IntegrationReadinessReview", "FoundationLeadCrudService", "FoundationAccountCrudService", "FoundationContactCrudService", "FoundationCrudStatusService")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/api/crm-foundation-crud-contracts.md") + "`n" + (Get-Content -Raw "docs/data/crm-foundation-crud-nonproduction-policy.md")) -notlike "*$marker*") {
        $failures += "Missing foundation CRUD marker: $marker"
    }
}

foreach ($marker in @("Integration readiness review only; no productive activation", "IntegrationReadinessReview", "Sprint2P6ProductizationGateDecision", "ContinueReview", "CrmSprint2IntegrationReadinessService", "Database Ready: false", "Auth Ready: false", "Productive CRUD Ready: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-2-p5-integration-readiness-review.md") + "`n" + (Get-Content -Raw "docs/architecture/crm-sprint-2-activation-gate-matrix.md")) -notlike "*$marker*") {
        $failures += "Missing integration readiness marker: $marker"
    }
}

foreach ($marker in @("Productization gate decision only; no productive activation", "Sprint2Closed", "NoGoForProductiveActivation", "GoFoundationOnly", "Sprint3P1DurablePersistenceSetupDesign", "CrmSprint2ProductizationGateService", "Sprint 2: Closed", "Overall Decision: NoGoForProductiveActivation", "Sprint 3 Planning: Go")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-2-productization-gate-decision.md") + "`n" + (Get-Content -Raw "docs/architecture/crm-sprint-2-productization-decision-matrix.md")) -notlike "*$marker*") {
        $failures += "Missing productization gate marker: $marker"
    }
}

foreach ($marker in @("Durable persistence setup design only; no database, EF runtime, migrations, or connection strings configured", "DurablePersistenceSetupDesign", "DesignOnly", "Sprint3P2CommonDbConnectionContractAndSecretStrategy", "CrmDurablePersistenceSetupStatusService", "Sprint 3 P1 Durable Persistence Setup: DesignOnly", "Real Database Configured: false", "EF Runtime Enabled: false", "Connection Strings Configured: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-sprint-3-p1-durable-persistence-setup-design.md") + "`n" + (Get-Content -Raw "docs/data/crm-nonproduction-durable-persistence-gates.md")) -notlike "*$marker*") {
        $failures += "Missing durable persistence setup marker: $marker"
    }
}

foreach ($marker in @("Common DB connection contract only; no real database or secrets configured", "CommonDbConnectionStrategy", "CrmCommonDbConnectionStrategyStatusService", "CrmSecretProviderPlaceholder", "CrmDatabaseConfigurationPlaceholder", "NoRealValuesInRepository", "Sprint3P3EfDbContextPrototypeBehindDisabledFlag", "Sprint 3 P2 Common DB Strategy: ContractOnly", "Logical Database Name: CrmDb", "Secret Provider Configured: false", "Secret Provider Runtime Connected: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-sprint-3-p2-common-db-connection-secret-strategy.md") + "`n" + (Get-Content -Raw "docs/data/crm-db-runtime-readiness-gates.md")) -notlike "*$marker*") {
        $failures += "Missing common DB connection strategy marker: $marker"
    }
}

foreach ($marker in @("EF/DbContext prototype only; runtime disabled and no database configured", "EfDbContextPrototypeDisabled", "CrmEfPrototypeStatusService", "CrmDbContextPrototype", "CrmEfPrototypeMarker", "CRM_EF_RUNTIME_ENABLED=false", "CRM_DBCONTEXT_RUNTIME_ACTIVE=false", "Sprint3P4PortalAuthRuntimeContractValidation", "Sprint 3 P3 EF Prototype: Exists", "DbContext Runtime Active: false", "Provider Configured: false", "UseSqlServer Configured: false", "Foundation Stores Remain Active: true", "Productive CRUD Enabled: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-sprint-3-p3-ef-dbcontext-prototype.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing EF prototype disabled marker: $marker"
    }
}

foreach ($marker in @("Portal Auth runtime contract validation only; no real Auth runtime configured", "PortalAuthRuntimeContractValidation", "CrmPortalAuthRuntimeContractStatusService", "PortalAuthRuntimeAdapterPlaceholder", "PortalAuthContextMapperPlaceholder", "PortalCorporativo", "Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag", "Sprint 3 P4 Portal Auth Runtime Contract: ContractOnly", "Portal Runtime Connected: false", "Auth Runtime Enabled: false", "Token Storage Enabled: false", "Login Implemented By CRM: false", "Identity Implemented By CRM: false", "Permissions Persisted In CRM: false", "Foundation Simulation Active: true", "Productive Authorization Enabled: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/integration/crm-sprint-3-p4-portal-auth-runtime-contract-validation.md") + "`n" + (Get-Content -Raw "docs/integration/crm-portal-auth-runtime-contract.md") + "`n" + (Get-Content -Raw "docs/security/crm-permission-capability-map.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Portal Auth runtime contract marker: $marker"
    }
}

foreach ($marker in @("Productive API route draft only; routes are not active", "ProductiveApiRouteDraft", "CrmProductiveApiRouteDraftStatusService", "Sprint3P6Sprint3ProductizationReview", "Sprint 3 P5 Productive API Draft: Exists", "Productive Routes Registered: false", "DELETE Endpoints Enabled: false", "Foundation CRUD Still Separate: true")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/api/crm-sprint-3-p5-productive-api-route-draft.md") + "`n" + (Get-Content -Raw "docs/api/crm-productive-api-disabled-route-policy.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Productive API route draft marker: $marker"
    }
}

foreach ($marker in @("Sprint 3 productization review only; no real activation", "Sprint3ProductizationReview", "CrmSprint3ProductizationReviewStatusService", "NoGoForRealActivation", "Productization Review: Completed", "Sprint 3: Closed", "Productive CRM UI: NoGo", "Foundation Capabilities: GoFoundationOnly", "Sprint 4 Planning: Go", "Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-3-productization-review.md") + "`n" + (Get-Content -Raw "docs/architecture/crm-sprint-3-productization-review-matrix.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 3 productization review marker: $marker"
    }
}

if ($sourceText -match "ProductiveLeadComponent|ProductiveAccountComponent|ProductiveContactComponent|CrmProductiveDashboard") {
    $failures += "Productive CRM UI marker found before productization approval."
}

foreach ($marker in @("Runtime readiness only; no real activation", "RuntimeEnvironmentReadiness", "CrmRuntimeEnvironmentReadinessStatusService", "Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag", "Sprint 4 P1 Runtime Readiness: Active", "Docker Compose Expected: true", "CRM API Port: 8093", "Node PATH Required For Frontend Verifier: false", "Productive Routes Active: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/operations/crm-sprint-4-p1-runtime-environment-readiness.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 4 runtime readiness marker: $marker"
    }
}

foreach ($marker in @("Common DB runtime probe exists but is disabled; no database connection is attempted", "CommonDbRuntimeProbe", "CrmCommonDbRuntimeProbeStatusService", "CommonDbRuntimeProbePlaceholder", "Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag", "Sprint 4 P2 Common DB Runtime Probe: Exists", "Common DB Runtime Probe Enabled: false", "DB Connection Attempted By Runtime: false", "SQL Server Owned By CRM: false", "API Requires Database: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-sprint-4-p2-common-db-runtime-probe.md") + "`n" + (Get-Content -Raw "docs/operations/crm-common-db-runtime-probe-runbook.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 4 P2 common DB runtime probe marker: $marker"
    }
}

foreach ($marker in @("Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted", "PortalAuthRuntimeProbe", "CrmPortalAuthRuntimeProbeStatusService", "PortalAuthRuntimeProbePlaceholder", "Sprint4P4ProductiveRoutesLockedStubValidation", "Sprint 4 P3 Portal Auth Runtime Probe: Exists", "Portal Auth Runtime Probe Enabled: false", "Token Read Attempted By Runtime: false", "Portal HTTP Attempted By Runtime: false", "Login Implemented By CRM: false", "Identity Implemented By CRM: false", "Permissions Persisted In CRM: false", "Foundation Simulation Active: true")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/integration/crm-sprint-4-p3-portal-auth-runtime-probe.md") + "`n" + (Get-Content -Raw "docs/operations/crm-portal-auth-runtime-probe-runbook.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 4 P3 Portal Auth runtime probe marker: $marker"
    }
}

foreach ($marker in @("Productive routes locked stub validation only; no productive routes are active", "ProductiveRoutesLockedStubValidation", "CrmProductiveRoutesLockedStubStatusService", "DocumentOnlyPreferred", "Sprint4P5NonProductionE2EPilotReadiness", "Sprint 4 P4 Productive Routes Locked Stub Validation: Active", "Locked Stubs Strategy: DocumentOnlyPreferred", "Productive Routes Registered: false", "Locked Stubs Registered: false", "DELETE Endpoints Enabled: false", "DB Required: false", "Auth Runtime Required: false", "Foundation CRUD Still Separate: true")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/api/crm-sprint-4-p4-productive-routes-locked-stub-validation.md") + "`n" + (Get-Content -Raw "docs/operations/crm-productive-routes-locked-stub-runbook.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 4 P4 productive route locked stub marker: $marker"
    }
}

foreach ($marker in @("Non-production E2E pilot readiness only; no real activation", "NonProductionE2EPilotReadiness", "CrmNonProductionE2EPilotReadinessStatusService", "Sprint4P6Sprint4GateDecision", "Sprint 4 P5 Non-Production E2E Pilot Readiness: Prepared", "E2E Pilot Can Run: true", "E2E Pilot Scope: FoundationOnly", "Productive Routes Used: false", "Real Database Used: false", "Portal Auth Runtime Used: false", "Durable Persistence Used: false", "Synthetic Data Only: true", "Foundation Endpoints Only: true", "Negative Route Validation Required: true")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/testing/crm-sprint-4-p5-nonproduction-e2e-pilot-readiness.md") + "`n" + (Get-Content -Raw "docs/testing/crm-nonproduction-e2e-scenario-matrix.md") + "`n" + (Get-Content -Raw "docs/testing/crm-foundation-only-e2e-test-plan.md") + "`n" + (Get-Content -Raw "docs/testing/crm-e2e-evidence-checklist.md") + "`n" + (Get-Content -Raw "docs/operations/crm-nonproduction-e2e-pilot-runbook.md") + "`n" + (Get-Content -Raw "docs/security/crm-e2e-pilot-safety-boundary.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 4 P5 non-production E2E pilot marker: $marker"
    }
}

foreach ($marker in @("Sprint 4 gate decision only; no real activation", "Sprint4GateDecision", "CrmSprint4GateDecisionStatusService", "GoForNonProductionFoundationPilot", "Sprint5P1ControlledRuntimeProbeActivationPlan", "Sprint 4: Closed", "Sprint 4 Gate Decision: Completed", "Overall Decision: GoForNonProductionFoundationPilot", "Real Activation Decision: NoGo", "Common DB Runtime: NoGoForRuntimeActivation", "Portal Auth Runtime: NoGoForRuntimeActivation", "Productive Routes: NoGo", "DELETE: NoGo", "Non-Production E2E Pilot: GoFoundationOnly", "Sprint 5 Planning: Go")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-4-closure.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-4-gate-decision.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-4-go-no-go.md") + "`n" + (Get-Content -Raw "docs/architecture/crm-sprint-4-gate-matrix.md") + "`n" + (Get-Content -Raw "docs/roadmap/crm-sprint-5-recommended-path.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 4 P6 gate decision marker: $marker"
    }
}

foreach ($marker in @("Runtime probe activation plan only; no runtime activation approved", "ControlledRuntimeProbeActivationPlan", "CrmControlledRuntimeProbeActivationPlanStatusService", "Sprint5P2SecretProviderRuntimeContractValidation", "Sprint 5 P1 Controlled Runtime Probe Activation Plan: Exists", "Runtime Probe Activation Approved: false", "Common DB Probe Activation Approved: false", "Portal Auth Probe Activation Approved: false", "Productive Routes Activation Approved: false", "Real Activation Approved: false", "Non-Production Only: true", "Synthetic Data Required: true", "Rollback Plan Required: true", "Observability Required: true", "Secret Provider Required: true", "DELETE Still NoGo: true")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/operations/crm-sprint-5-p1-controlled-runtime-probe-activation-plan.md") + "`n" + (Get-Content -Raw "docs/operations/crm-runtime-probe-activation-approval-matrix.md") + "`n" + (Get-Content -Raw "docs/security/crm-runtime-probe-secret-handling-policy.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 5 P1 controlled runtime probe activation marker: $marker"
    }
}

foreach ($marker in @("Secret Provider contract validation only; no secrets are read", "SecretProviderRuntimeContractValidation", "CrmSecretProviderRuntimeContractStatusService", "SecretProviderRuntimeContractPlaceholder", "Sprint5P3CommonDbProbeOptionalActivationInNonProduction", "Sprint 5 P2 Secret Provider Runtime Contract: Exists", "Secret Provider Contract Exists: true", "Secret Provider Runtime Connected: false", "Secret Provider Reads Enabled: false", "Secret Read Attempted By Runtime: false", "Real Secrets Configured: false", "Env File Required: false", "Connection Strings Configured: false", "Key Vault Client Configured: false", "Secret Values Exposed: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/security/crm-sprint-5-p2-secret-provider-runtime-contract-validation.md") + "`n" + (Get-Content -Raw "docs/security/crm-secret-provider-runtime-contract.md") + "`n" + (Get-Content -Raw "docs/security/crm-secret-provider-no-secret-read-policy.md") + "`n" + (Get-Content -Raw "docs/operations/crm-secret-provider-runtime-runbook.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 5 P2 secret provider runtime contract marker: $marker"
    }
}

foreach ($marker in @("Common DB probe optional activation only; no database connection is attempted", "CommonDbProbeOptionalActivation", "CrmCommonDbProbeOptionalActivationStatusService", "CommonDbProbeOptionalActivationPlaceholder", "Sprint5P4PortalAuthProbeOptionalActivationInNonProduction", "Sprint 5 P3 Common DB Probe Optional Activation: Exists", "Common DB Probe Optional Activation Exists: true", "Common DB Probe Activation Approved: false", "Common DB Probe Enabled: false", "Common DB Connection Attempted: false", "Secret Reads Required Before Activation: true", "Secret Reads Enabled: false", "Real Database Configured: false", "Connection Strings Configured: false", "EF Runtime Enabled: false", "Migrations Created: false", "API Requires Database: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-sprint-5-p3-common-db-probe-optional-activation.md") + "`n" + (Get-Content -Raw "docs/data/crm-common-db-probe-optional-activation-policy.md") + "`n" + (Get-Content -Raw "docs/operations/crm-common-db-probe-optional-activation-runbook.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 5 P3 common DB probe optional activation marker: $marker"
    }
}

foreach ($path in @("docs/api/crm-sprint-5-p5-locked-productive-route-stub-trial.md", "docs/api/crm-locked-productive-route-stub-trial-policy.md", "docs/api/crm-locked-productive-route-stub-trial-contract.md", "docs/security/crm-locked-productive-route-stub-trial-safety-gates.md", "docs/operations/crm-locked-productive-route-stub-trial-runbook.md", "src/CRM.Application/Foundation/CrmLockedProductiveRouteStubTrialContracts.cs", "src/CRM.Application/Foundation/CrmLockedProductiveRouteStubTrialStatusService.cs")) {
    Require-Path $path
}

$p5Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($p5Program -notlike "*/api/crm/foundation/sprint-5/locked-productive-route-stub-trial*") {
    $failures += "Sprint 5 P5 locked productive route stub trial endpoint missing."
}

if ($p5Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial") {
    $failures += "Sprint 5 P5 locked productive route stub trial endpoint must remain GET-only."
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($p5Program -like "*$productiveRoute*") {
        $failures += "Productive CRM route is registered by default: $productiveRoute"
    }
}

foreach ($marker in @("Locked productive route stub trial only; no productive routes are registered by default", "LockedProductiveRouteStubTrial", "CrmLockedProductiveRouteStubTrialStatusService", "DocumentOnlyPreferredWithNoRuntimeRegistration", "Sprint5P6Sprint5GateDecision", "Sprint 5 P5 Locked Productive Route Stub Trial: Exists", "Locked Productive Route Stub Registration Approved: false", "Locked Productive Route Stubs Registered: false", "Productive Routes Registered: false", "DELETE Endpoints Enabled: false", "Runtime Flag Default Enabled: false", "Locked Response If Enabled: 423", "Default Negative Route Status: 404", "Foundation CRUD Still Separate: true")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/api/crm-sprint-5-p5-locked-productive-route-stub-trial.md") + "`n" + (Get-Content -Raw "docs/api/crm-locked-productive-route-stub-trial-policy.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 5 P5 locked productive route stub trial marker: $marker"
    }
}

foreach ($path in @("docs/releases/crm-sprint-5-closure.md", "docs/releases/crm-sprint-5-integrated-evidence.md", "docs/releases/crm-sprint-5-gate-decision.md", "docs/releases/crm-sprint-5-go-no-go.md", "docs/releases/crm-sprint-5-open-risks.md", "docs/releases/crm-sprint-5-decision-record.md", "docs/architecture/crm-sprint-5-gate-matrix.md", "docs/security/crm-sprint-5-security-gate-review.md", "docs/data/crm-sprint-5-persistence-gate-review.md", "docs/api/crm-sprint-5-api-gate-review.md", "docs/testing/crm-sprint-5-e2e-gate-review.md", "docs/roadmap/crm-sprint-6-options.md", "docs/roadmap/crm-sprint-6-recommended-path.md", "docs/roadmap/crm-sprint-6-gates.md", "src/CRM.Application/Foundation/CrmSprint5GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint5GateDecisionStatusService.cs")) {
    Require-Path $path
}

$p6Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($p6Program -notlike "*/api/crm/foundation/sprint-5/gate-decision*") {
    $failures += "Sprint 5 P6 gate decision endpoint missing."
}

if ($p6Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/gate-decision") {
    $failures += "Sprint 5 P6 gate decision endpoint must remain GET-only."
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($p6Program -like "*$productiveRoute*") {
        $failures += "Productive CRM route is registered by default: $productiveRoute"
    }
}

foreach ($marker in @("Sprint 5 gate decision only; no real activation", "Sprint5GateDecision", "CrmSprint5GateDecisionStatusService", "GoForControlledNonProductionPreparation", "NoGoForRuntimeRead", "NoGoForConnectionAttempt", "NoGoForPortalHttpOrTokenRead", "NoGoForRuntimeRegistration", "Sprint6P1NonProductionRuntimeApprovalPackage", "Sprint 5: Closed", "Sprint 5 Gate Decision: Completed", "Sprint 6 Planning: Go")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-5-closure.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-5-gate-decision.md") + "`n" + (Get-Content -Raw "docs/roadmap/crm-sprint-6-recommended-path.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 5 P6 gate decision marker: $marker"
    }
}

foreach ($path in @("docs/operations/crm-sprint-6-p1-nonproduction-runtime-approval-package.md", "docs/operations/crm-nonproduction-runtime-approval-matrix.md", "docs/operations/crm-nonproduction-runtime-entry-exit-criteria.md", "docs/operations/crm-nonproduction-runtime-rollback-approval.md", "docs/security/crm-nonproduction-runtime-security-approval.md", "docs/architecture/crm-nonproduction-runtime-architecture-approval.md", "src/CRM.Application/Foundation/CrmNonProductionRuntimeApprovalPackageContracts.cs", "src/CRM.Application/Foundation/CrmNonProductionRuntimeApprovalPackageStatusService.cs")) {
    Require-Path $path
}

$p1Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($p1Program -notlike "*/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package*") {
    $failures += "Sprint 6 P1 non-production runtime approval package endpoint missing."
}

if ($p1Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package") {
    $failures += "Sprint 6 P1 non-production runtime approval package endpoint must remain GET-only."
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($p1Program -like "*$productiveRoute*") {
        $failures += "Productive CRM route is registered by default: $productiveRoute"
    }
}

foreach ($marker in @("NonProduction runtime approval package only; no runtime approval is granted", "NonProductionRuntimeApprovalPackage", "CrmNonProductionRuntimeApprovalPackageStatusService", "NonProductionRuntimeApprovalPackageExists", "NonProductionRuntimeApprovalGranted", "SecretProviderMockApprovalGranted", "CommonDbDryRunApprovalGranted", "PortalAuthDryRunApprovalGranted", "LockedStubRuntimeTrialApprovalGranted", "RealActivationApprovalGranted", "ProductiveRoutesApprovalGranted", "DeleteApprovalGranted", "Sprint6P2SecretProviderSafeMockActivation", "Sprint 6 P1 NonProduction Runtime Approval Package: Exists", "NonProduction Runtime Approval Granted: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/operations/crm-sprint-6-p1-nonproduction-runtime-approval-package.md") + "`n" + (Get-Content -Raw "docs/operations/crm-nonproduction-runtime-approval-matrix.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 6 P1 approval package marker: $marker"
    }
}

foreach ($path in @("docs/security/crm-sprint-6-p2-secret-provider-safe-mock-activation.md", "docs/security/crm-secret-provider-safe-mock-policy.md", "docs/security/crm-secret-provider-safe-mock-contract.md", "docs/security/crm-secret-provider-safe-mock-synthetic-values.md", "docs/operations/crm-secret-provider-safe-mock-runbook.md", "src/CRM.Application/Foundation/CrmSecretProviderSafeMockActivationContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderSafeMockActivationStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderSafeMock.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderSafeMockOptions.cs")) {
    Require-Path $path
}

$p2Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($p2Program -notlike "*/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation*") {
    $failures += "Sprint 6 P2 secret provider safe mock endpoint missing."
}

if ($p2Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation") {
    $failures += "Sprint 6 P2 secret provider safe mock endpoint must remain GET-only."
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($p2Program -like "*$productiveRoute*") {
        $failures += "Productive CRM route is registered by default: $productiveRoute"
    }
}

foreach ($marker in @("Secret Provider safe mock only; no real secrets are read", "SecretProviderSafeMockActivation", "CrmSecretProviderSafeMockActivationStatusService", "SecretProviderSafeMock", "SecretProviderSafeMockExists", "SecretProviderSafeMockEnabled", "SecretProviderRuntimeConnected", "SecretProviderReadsRealSecrets", "SecretProviderReadsSyntheticValues", "SecretProviderReadsEnabledForMockOnly", "RealSecretsConfigured", "EnvFileRequired", "KeyVaultClientConfigured", "AzureSdkForSecretsConfigured", "SecretValuesExposedInLogs", "Sprint6P3CommonDbConnectivityDryRunContract", "Sprint 6 P2 Secret Provider Safe Mock Activation: Enabled", "Reads Real Secrets: false", "Reads Synthetic Values: true", "mock://crm/common-db", "mock://crm/portal-auth-base-url", "mock-client-id", "mock-client-secret-not-real", "mock://crm/observability")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/security/crm-sprint-6-p2-secret-provider-safe-mock-activation.md") + "`n" + (Get-Content -Raw "docs/security/crm-secret-provider-safe-mock-contract.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 6 P2 safe mock marker: $marker"
    }
}

foreach ($path in @("docs/data/crm-sprint-6-p3-common-db-connectivity-dry-run-contract.md", "docs/data/crm-common-db-connectivity-dry-run-policy.md", "docs/data/crm-common-db-connectivity-dry-run-contract.md", "docs/data/crm-common-db-connectivity-dry-run-observability.md", "docs/operations/crm-common-db-connectivity-dry-run-runbook.md", "docs/security/crm-common-db-connectivity-dry-run-secret-boundary.md", "src/CRM.Application/Foundation/CrmCommonDbConnectivityDryRunContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbConnectivityDryRunStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityDryRun.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityDryRunOptions.cs")) {
    Require-Path $path
}

$p3Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($p3Program -notlike "*/api/crm/foundation/sprint-6/common-db-connectivity-dry-run*") {
    $failures += "Sprint 6 P3 common DB connectivity dry-run endpoint missing."
}

if ($p3Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/common-db-connectivity-dry-run") {
    $failures += "Sprint 6 P3 common DB connectivity dry-run endpoint must remain GET-only."
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"')) {
    if ($p3Program -like "*$productiveRoute*") {
        $failures += "Productive CRM route is registered by default: $productiveRoute"
    }
}

foreach ($marker in @("Common DB connectivity dry-run contract only; no database connection is attempted", "CommonDbConnectivityDryRunContract", "CrmCommonDbConnectivityDryRunStatusService", "CommonDbConnectivityDryRun", "CommonDbConnectivityDryRunContractExists", "CommonDbDryRunApprovalGranted", "CommonDbDryRunEnabled", "CommonDbConnectionAttempted", "UsesSecretProviderSafeMockMetadata", "UsesSyntheticConnectionReference", "mock://crm/common-db", "RealConnectionStringUsed", "ConnectionStringResolved", "SqlConnectionCreated", "DbConnectionCreated", "EfRuntimeEnabled", "MigrationsCreated", "ApiRequiresDatabase", "Sprint6P4PortalAuthTokenPropagationDryRunContract", "Sprint 6 P3 Common DB Connectivity Dry-Run Contract: Exists", "Common DB Connection Attempted: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-sprint-6-p3-common-db-connectivity-dry-run-contract.md") + "`n" + (Get-Content -Raw "docs/data/crm-common-db-connectivity-dry-run-contract.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 6 P3 common DB dry-run marker: $marker"
    }
}

if ($sourceText -match "AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|MapDelete") {
    $failures += "Productive Auth middleware, JWT/cookie auth, authorization attribute or DELETE endpoint found."
}

$connectionScanText = $sourceText.Replace("ConnectionStringsConfigured", "").Replace("connectionStringsConfigured", "").Replace("Connection Strings Configured", "").Replace("CrmConnectionStringPolicyContract", "").Replace("ConnectionStringPolicy", "").Replace("connectionStringPolicy", "").Replace("RealConnectionStringUsed", "").Replace("realConnectionStringUsed", "").Replace("Real Connection String Used", "").Replace("ConnectionStringResolved", "").Replace("connectionStringResolved", "").Replace("Connection String Resolved", "").Replace("UseSqlServerConfigured", "").Replace("useSqlServerConfigured", "").Replace("UseSqlServer Configured", "")
if ($connectionScanText -match "FinancieroDb|UseSqlServer|ConnectionString|FinancieroUrl|financialBaseUrl") {
    $failures += "Runtime Financial adapter, connection string, shared DB or URL found before integration approval."
}

if ($connectionScanText -cmatch "Microsoft\.PowerBI|embedToken|workspaceId|reportId|datasetId|embedUrl|powerbi\.com|ConnectionString") {
    $failures += "Runtime BI adapter, token, ID, URL or connection string found before analytics approval."
}

if (Test-Path "database") {
    $failures += "Database directory or migration baseline must not exist in foundation sprints."
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

Write-Output "CRM foundation verification passed."
exit 0

# Sprint 5 P4 Portal Auth Probe Optional Activation checks
$P4RequiredFiles = @(
    "docs/integration/crm-sprint-5-p4-portal-auth-probe-optional-activation.md",
    "docs/integration/crm-portal-auth-probe-optional-activation-policy.md",
    "docs/integration/crm-portal-auth-probe-activation-gates.md",
    "docs/integration/crm-portal-auth-probe-rollback-plan.md",
    "docs/operations/crm-portal-auth-probe-optional-activation-runbook.md",
    "docs/security/crm-portal-auth-probe-token-boundary.md",
    "src/CRM.Application/Foundation/CrmPortalAuthProbeOptionalActivationContracts.cs",
    "src/CRM.Application/Foundation/CrmPortalAuthProbeOptionalActivationStatusService.cs",
    "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthProbeOptionalActivationPlaceholder.cs"
)
foreach ($P4RequiredFile in $P4RequiredFiles) {
    if (-not (Test-Path $P4RequiredFile)) { Fail "Missing Sprint 5 P4 required file: $P4RequiredFile" } else { Pass "Required P4 file exists: $P4RequiredFile" }
}
$P4Program = Get-Content "src/CRM.Api/Program.cs" -Raw
if ($P4Program -notmatch "portal-auth-probe-optional-activation") { Fail "Missing Sprint 5 P4 foundation endpoint" } else { Pass "Sprint 5 P4 Portal Auth optional activation endpoint registered." }
if ($P4Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation") { Fail "Sprint 5 P4 endpoint must remain GET-only." }
$P4Text = ""
foreach ($P4File in @("src/CRM.Application/Foundation/CrmPortalAuthProbeOptionalActivationContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthProbeOptionalActivationStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthProbeOptionalActivationPlaceholder.cs")) {
    if (Test-Path $P4File) { $P4Text += "`n" + (Get-Content -Raw $P4File) }
}
foreach ($P4Marker in @("PortalAuthProbeOptionalActivation", "PortalAuthProbeEnabled", "PortalHttpAttempted", "TokenReadAttempted", "HeaderReadAttempted", "SecretProviderRuntimeRequired", "SecretReadsEnabled", "LoginImplementedByCrm", "IdentityImplementedByCrm", "PermissionsPersistedInCrm", "ProductiveAuthorizationEnabled", "Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted")) {
    if ($P4Text -notmatch [regex]::Escape($P4Marker)) { Fail "Missing Sprint 5 P4 marker: $P4Marker" }
}
