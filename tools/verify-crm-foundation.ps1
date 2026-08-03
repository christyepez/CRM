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
foreach ($route in @('/health', '/health/live', '/health/ready', '/api/crm/readiness', '/api/crm/domain-catalog', '/api/crm/contracts', '/api/crm/integration-boundaries', '/api/crm/foundation/leads/preview', '/api/crm/foundation/accounts/preview', '/api/crm/foundation/contacts/preview', '/api/crm/foundation/crud/status', '/api/crm/foundation/leads', '/api/crm/foundation/leads/{id}', '/api/crm/foundation/accounts', '/api/crm/foundation/accounts/{id}', '/api/crm/foundation/contacts', '/api/crm/foundation/contacts/{id}', '/api/crm/foundation/leads/read-model-preview', '/api/crm/foundation/accounts/read-model-preview', '/api/crm/foundation/contacts/read-model-preview', '/api/crm/foundation/read-model-status', '/api/crm/foundation/portal-integration/status', '/api/crm/foundation/portal-integration/contracts', '/api/crm/foundation/portal-integration/required-capabilities', '/api/crm/foundation/portal-authorization/simulation-status', '/api/crm/foundation/portal-authorization/scenarios', '/api/crm/foundation/portal-authorization/permissions', '/api/crm/foundation/portal-authorization/sample-user-context', '/api/crm/foundation/portal-authorization/check-permission', '/api/crm/foundation/financial-integration/status', '/api/crm/foundation/financial-integration/contracts', '/api/crm/foundation/financial-integration/required-capabilities', '/api/crm/foundation/financial-integration/events', '/api/crm/foundation/reporting/status', '/api/crm/foundation/reporting/kpis', '/api/crm/foundation/reporting/dashboards', '/api/crm/foundation/reporting/analytics-read-models', '/api/crm/foundation/sprint-1/closure-status', '/api/crm/foundation/persistence/readiness', '/api/crm/foundation/persistence/seam-status', '/api/crm/foundation/persistence/feature-flags', '/api/crm/foundation/persistence/stores/status', '/api/crm/foundation/persistence/stores/clear-preview', '/api/crm/foundation/sprint-2/integration-readiness', '/api/crm/foundation/sprint-2/productization-gate', '/api/crm/foundation/sprint-3/durable-persistence-setup', '/api/crm/foundation/sprint-3/common-db-connection-strategy', '/api/crm/foundation/sprint-3/ef-prototype-status', '/api/crm/foundation/sprint-3/portal-auth-runtime-contract', '/api/crm/foundation/sprint-3/productive-api-route-draft', '/api/crm/foundation/sprint-3/productization-review', '/api/crm/foundation/sprint-4/runtime-readiness', '/api/crm/foundation/sprint-4/common-db-runtime-probe', '/api/crm/foundation/sprint-4/portal-auth-runtime-probe', '/api/crm/foundation/sprint-4/productive-routes-locked-stub', '/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness', '/api/crm/foundation/sprint-4/gate-decision', '/api/crm/foundation/sprint-5/runtime-probe-activation-plan', '/api/crm/foundation/sprint-5/secret-provider-runtime-contract', '/api/crm/foundation/sprint-5/common-db-probe-optional-activation', '/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run', '/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial')) {
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

$persistenceScanText = $sourceText.Replace("DbContextConfigured", "").Replace("dbContextConfigured", "").Replace("DbContext Configured", "").Replace("DbContextRuntimeActive", "").Replace("dbContextRuntimeActive", "").Replace("DbContext Runtime Active", "").Replace("AddDbContextRuntimeEnabled", "").Replace("addDbContextRuntimeEnabled", "").Replace("AddDbContext Runtime Enabled", "").Replace("CrmDbContextPrototypeContract", "").Replace("CrmDbContextPrototype", "").Replace("InheritsRealDbContext", "").Replace("UseSqlServerConfigured", "").Replace("useSqlServerConfigured", "").Replace("UseSqlServer Configured", "").Replace("UseSqlServerEnabled", "").Replace("useSqlServerEnabled", "").Replace("UseSqlServer Enabled", "").Replace("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", "").Replace("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", "").Replace("EfDbContextPrototypeDisabled", "").Replace("EF/DbContext prototype only; runtime disabled and no database configured", "")
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

$portalAuthSafeSourceText = $sourceText.
    Replace("PortalHttpClientCreated", "").
    Replace("portalHttpClientCreated", "").
    Replace("Portal HTTP Client Created", "").
    Replace("PortalAuthBaseUrlResolved", "").
    Replace("portalAuthBaseUrlResolved", "").
    Replace("Portal Auth Base URL Resolved", "").
    Replace("PortalAuthBaseUrlMaterialized", "").
    Replace("portalAuthBaseUrlMaterialized", "").
    Replace("Portal Auth Base URL Materialized", "").
    Replace("PortalAuthBaseUrlLogged", "").
    Replace("portalAuthBaseUrlLogged", "").
    Replace("Portal Auth Base URL Logged", "").
    Replace("PortalAuthBaseUrlReturnedToApi", "").
    Replace("portalAuthBaseUrlReturnedToApi", "").
    Replace("Portal Auth Base URL Returned To API", "").
    Replace("AuthorizationHeaderReadAttempted", "").
    Replace("AuthHeaderReadAttempted", "").
    Replace("authorizationHeaderReadAttempted", "").
    Replace("Authorization Header Read Attempted", "")

if ($portalAuthSafeSourceText -match "HttpClient|PortalCorporativoUrl|PortalBaseUrl|portalBaseUrl") {
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

foreach ($path in @("docs/integration/crm-sprint-6-p4-portal-auth-token-propagation-dry-run-contract.md", "docs/integration/crm-portal-auth-token-propagation-dry-run-policy.md", "docs/integration/crm-portal-auth-token-propagation-dry-run-contract.md", "docs/integration/crm-portal-auth-token-propagation-dry-run-observability.md", "docs/operations/crm-portal-auth-token-propagation-dry-run-runbook.md", "docs/security/crm-portal-auth-token-propagation-dry-run-boundary.md", "src/CRM.Application/Foundation/CrmPortalAuthTokenPropagationDryRunContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthTokenPropagationDryRunStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthTokenPropagationDryRun.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthTokenPropagationDryRunOptions.cs")) {
    Require-Path $path
}

if ($p3Program -notlike "*/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run*") {
    $failures += "Sprint 6 P4 Portal Auth token propagation dry-run endpoint missing."
}

if ($p3Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run") {
    $failures += "Sprint 6 P4 Portal Auth token propagation dry-run endpoint must remain GET-only."
}

foreach ($marker in @("Portal Auth token propagation dry-run contract only; no real tokens or headers are read", "PortalAuthTokenPropagationDryRunContract", "CrmPortalAuthTokenPropagationDryRunStatusService", "PortalAuthTokenPropagationDryRun", "PortalAuthTokenPropagationDryRunContractExists", "PortalAuthDryRunApprovalGranted", "PortalAuthDryRunEnabled", "PortalAuthRuntimeConnected", "TokenReadAttempted", "HeaderReadAttempted", "PortalHttpAttempted", "UsesSyntheticTokenMetadata", "mock://crm/portal-auth-token", "mock://crm/portal-user", "RealTokenUsed", "RealHeadersRead", "LoginImplementedByCrm", "IdentityImplementedByCrm", "PermissionsPersistedInCrm", "ProductiveAuthorizationEnabled", "Sprint6P5LockedStubRuntimeRegistrationTrial", "Sprint 6 P4 Portal Auth Token Propagation Dry-Run Contract: Exists", "Token Read Attempted: false", "Header Read Attempted: false", "Portal HTTP Attempted: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/integration/crm-sprint-6-p4-portal-auth-token-propagation-dry-run-contract.md") + "`n" + (Get-Content -Raw "docs/integration/crm-portal-auth-token-propagation-dry-run-contract.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 6 P4 Portal Auth token propagation dry-run marker: $marker"
    }
}

if ($portalAuthSafeSourceText -match "HttpContext\.Request\.Headers|Request\.Headers|Headers\[|AuthorizationHeader|authorizationHeader|Bearer|localStorage|sessionStorage|HttpClient|PortalBaseUrl|PortalCorporativoUrl") {
    $failures += "Sprint 6 P4 must not read tokens/headers, store tokens or call Portal."
}

foreach ($path in @("docs/api/crm-sprint-6-p5-locked-stub-runtime-registration-trial.md", "docs/api/crm-locked-stub-runtime-registration-trial-policy.md", "docs/api/crm-locked-stub-runtime-registration-trial-contract.md", "docs/security/crm-locked-stub-runtime-registration-trial-safety-boundary.md", "docs/operations/crm-locked-stub-runtime-registration-trial-runbook.md", "src/CRM.Application/Foundation/CrmLockedStubRuntimeRegistrationTrialContracts.cs", "src/CRM.Application/Foundation/CrmLockedStubRuntimeRegistrationTrialStatusService.cs")) {
    Require-Path $path
}

if ($p3Program -notlike "*/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial*") {
    $failures += "Sprint 6 P5 locked stub runtime registration trial endpoint missing."
}

if ($p3Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial") {
    $failures += "Sprint 6 P5 locked stub runtime registration trial endpoint must remain GET-only."
}

foreach ($marker in @("Locked stub runtime registration trial only; no productive routes are registered by default", "LockedStubRuntimeRegistrationTrial", "CrmLockedStubRuntimeRegistrationTrialStatusService", "LockedStubRuntimeRegistrationTrialExists", "LockedStubRuntimeRegistrationApprovalGranted", "LockedStubRuntimeRegistrationEnabled", "LockedStubsRegisteredAtRuntime", "ProductiveRoutesRegistered", "ProductiveCrudEnabled", "DeleteEndpointsEnabled", "DefaultNegativeRouteStatus", "FutureLockedResponseStatusIfExplicitlyEnabled", "RuntimeFlagDefaultEnabled", "UsesDomainServices", "UsesFoundationStores", "UsesDatabase", "UsesPortalAuth", "UsesTokenOrHeaderReads", "DocumentOnlyPreferredWithNoRuntimeRegistration", "Sprint6P6Sprint6GateDecision", "Sprint 6 P5 Locked Stub Runtime Registration Trial: Exists", "Default Negative Route Status: 404")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/api/crm-sprint-6-p5-locked-stub-runtime-registration-trial.md") + "`n" + (Get-Content -Raw "docs/api/crm-locked-stub-runtime-registration-trial-contract.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 6 P5 locked stub runtime registration marker: $marker"
    }
}

if (Test-Path "src/CRM.Api/ProductiveRoutes/LockedStubRuntimeRegistrationTrial.cs") {
    $failures += "P5 selected DocumentOnlyPreferredWithNoRuntimeRegistration; runtime registrar file must not exist."
}

foreach ($path in @("docs/releases/crm-sprint-6-closure.md", "docs/releases/crm-sprint-6-integrated-evidence.md", "docs/releases/crm-sprint-6-gate-decision.md", "docs/releases/crm-sprint-6-go-no-go.md", "docs/releases/crm-sprint-6-open-risks.md", "docs/releases/crm-sprint-6-decision-record.md", "docs/architecture/crm-sprint-6-gate-matrix.md", "docs/security/crm-sprint-6-security-gate-review.md", "docs/data/crm-sprint-6-persistence-gate-review.md", "docs/api/crm-sprint-6-api-gate-review.md", "docs/testing/crm-sprint-6-e2e-gate-review.md", "docs/roadmap/crm-sprint-7-options.md", "docs/roadmap/crm-sprint-7-recommended-path.md", "docs/roadmap/crm-sprint-7-gates.md", "src/CRM.Application/Foundation/CrmSprint6GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint6GateDecisionStatusService.cs")) {
    Require-Path $path
}

$sprint6P6Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint6P6Program -notlike "*/api/crm/foundation/sprint-6/gate-decision*") {
    $failures += "Sprint 6 P6 gate decision endpoint missing."
}

if ($sprint6P6Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-6/gate-decision") {
    $failures += "Sprint 6 P6 gate decision endpoint must remain GET-only."
}

foreach ($productiveRoute in @('"/api/crm/leads"', '"/api/crm/accounts"', '"/api/crm/contacts"', 'MapGet("/api/crm/leads', 'MapGet("/api/crm/accounts', 'MapGet("/api/crm/contacts', 'MapPost("/api/crm/leads', 'MapPost("/api/crm/accounts', 'MapPost("/api/crm/contacts', 'MapPut("/api/crm/leads', 'MapPut("/api/crm/accounts', 'MapPut("/api/crm/contacts')) {
    if ($sprint6P6Program -like "*$productiveRoute*") {
        $failures += "Productive CRM route is registered by default: $productiveRoute"
    }
}

foreach ($marker in @("Sprint 6 gate decision only; no real activation", "Sprint6GateDecision", "CrmSprint6GateDecisionStatusService", "GoForSprint7ControlledNonProductionActivationPlanning", "NoGo", "NotReady", "Sprint7P1SecretProviderRealNonProductionApproval", "Sprint 6: Closed", "Sprint 6 Gate Decision: Completed", "Sprint 7 Planning: Go")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-6-closure.md") + "`n" + (Get-Content -Raw "docs/releases/crm-sprint-6-gate-decision.md") + "`n" + (Get-Content -Raw "docs/roadmap/crm-sprint-7-recommended-path.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 6 P6 gate decision marker: $marker"
    }
}

foreach ($path in @("docs/security/crm-sprint-7-p1-secret-provider-real-nonproduction-approval.md", "docs/security/crm-secret-provider-real-nonproduction-approval-policy.md", "docs/security/crm-secret-provider-real-nonproduction-secret-boundary.md", "docs/security/crm-secret-provider-real-nonproduction-approved-secret-names.md", "docs/operations/crm-secret-provider-real-nonproduction-approval-runbook.md", "docs/operations/crm-secret-provider-real-nonproduction-rollback-plan.md", "docs/architecture/crm-secret-provider-real-nonproduction-architecture-review.md", "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionApprovalContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionApprovalStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionApprovalPlaceholder.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionApprovalOptions.cs")) {
    Require-Path $path
}

$sprint7P1Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint7P1Program -notlike "*/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval*") {
    $failures += "Sprint 7 P1 secret provider approval endpoint missing."
}

if ($sprint7P1Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval") {
    $failures += "Sprint 7 P1 secret provider approval endpoint must remain GET-only."
}

foreach ($marker in @("Secret Provider real NonProduction approval package only; no real secrets are read", "SecretProviderRealNonProductionApproval", "CrmSecretProviderRealNonProductionApprovalStatusService", "SecretProviderRealNonProductionApprovalPlaceholder", "SecretProviderRealNonProductionApprovalPackageExists", "SecretProviderRealNonProductionApprovalGranted", "SecretProviderRealRuntimeEnabled", "SecretProviderRealRuntimeConnected", "RealSecretReadAttempted", "KeyVaultRuntimeClientEnabled", "AzureSecretSdkRuntimeEnabled", "EnvFileRequired", "EnvSecretReadAllowed", "SecretsLogged", "SecretNamesApproved", "SecretValuesApproved", "Sprint7P2SecretProviderRealNonProductionRuntimeProbe", "Sprint 7 P1 Secret Provider Real NonProduction Approval: Exists", "Real Secret Read Attempted: false")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/security/crm-sprint-7-p1-secret-provider-real-nonproduction-approval.md") + "`n" + (Get-Content -Raw "docs/security/crm-secret-provider-real-nonproduction-approved-secret-names.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 7 P1 secret provider approval marker: $marker"
    }
}

foreach ($path in @("docs/security/crm-sprint-7-p2-secret-provider-real-nonproduction-runtime-probe.md", "docs/security/crm-secret-provider-real-nonproduction-runtime-probe-policy.md", "docs/security/crm-secret-provider-real-nonproduction-runtime-probe-contract.md", "docs/security/crm-secret-provider-real-nonproduction-runtime-probe-redaction.md", "docs/operations/crm-secret-provider-real-nonproduction-runtime-probe-runbook.md", "docs/operations/crm-secret-provider-real-nonproduction-runtime-probe-rollback.md", "docs/architecture/crm-secret-provider-real-nonproduction-runtime-probe-architecture.md", "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionRuntimeProbeContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderRealNonProductionRuntimeProbeStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionRuntimeProbe.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRealNonProductionRuntimeProbeOptions.cs")) {
    Require-Path $path
}

$sprint7P2Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint7P2Program -notlike "*/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe*") {
    $failures += "Sprint 7 P2 secret provider runtime probe endpoint missing."
}

if ($sprint7P2Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe") {
    $failures += "Sprint 7 P2 secret provider runtime probe endpoint must remain GET-only."
}

foreach ($marker in @("Secret Provider real NonProduction runtime probe is prepared but skipped because approval is not granted", "SecretProviderRealNonProductionRuntimeProbe", "CrmSecretProviderRealNonProductionRuntimeProbeStatusService", "SecretProviderRealNonProductionRuntimeProbeExists", "SecretProviderRealRuntimeProbeEnabled", "SecretProviderRealRuntimeProbeAttempted", "SecretProviderRealRuntimeConnected", "RealSecretValueMaterialized", "RealSecretValueLogged", "SecretValueReturnedToApi", "KeyVaultRuntimeClientCreated", "KeyVaultRuntimeCallAttempted", "EnvSecretReadAttempted", "LogicalSecretNamesValidated", "SecretValuesValidated", "ProbeSkippedBecauseApprovalNotGranted", "Sprint7P3CommonDbRealConnectivityNonProductionProbe", "Sprint 7 P2 Secret Provider Real NonProduction Runtime Probe: Exists")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/security/crm-sprint-7-p2-secret-provider-real-nonproduction-runtime-probe.md") + "`n" + (Get-Content -Raw "docs/security/crm-secret-provider-real-nonproduction-runtime-probe-contract.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 7 P2 secret provider runtime probe marker: $marker"
    }
}

foreach ($path in @("docs/data/crm-sprint-7-p3-common-db-real-connectivity-nonproduction-probe.md", "docs/data/crm-common-db-real-connectivity-nonproduction-probe-policy.md", "docs/data/crm-common-db-real-connectivity-nonproduction-probe-contract.md", "docs/data/crm-common-db-real-connectivity-nonproduction-probe-safety-boundary.md", "docs/operations/crm-common-db-real-connectivity-nonproduction-probe-runbook.md", "docs/operations/crm-common-db-real-connectivity-nonproduction-probe-rollback.md", "docs/architecture/crm-common-db-real-connectivity-nonproduction-probe-architecture.md", "src/CRM.Application/Foundation/CrmCommonDbRealConnectivityNonProductionProbeContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbRealConnectivityNonProductionProbeStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbRealConnectivityNonProductionProbe.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbRealConnectivityNonProductionProbeOptions.cs")) {
    Require-Path $path
}

$sprint7P3Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint7P3Program -notlike "*/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe*") {
    $failures += "Sprint 7 P3 Common DB real connectivity endpoint missing."
}

if ($sprint7P3Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe") {
    $failures += "Sprint 7 P3 Common DB endpoint must remain GET-only."
}

foreach ($marker in @("Common DB real connectivity NonProduction probe is prepared but skipped because Secret Provider approval is not granted", "CommonDbRealConnectivityNonProductionProbe", "CrmCommonDbRealConnectivityNonProductionProbeStatusService", "CommonDbRealConnectivityNonProductionProbeExists", "CommonDbRealConnectivityApprovalGranted", "ConnectionStringResolved", "ConnectionStringValueMaterialized", "ConnectionStringLogged", "ConnectionStringReturnedToApi", "CommonDbProbeEnabled", "CommonDbProbeAttempted", "CommonDbConnected", "SqlConnectionCreated", "DbConnectionCreated", "UseSqlServerEnabled", "EfRuntimeEnabled", "AddDbContextRuntimeEnabled", "MigrationsCreated", "DatabaseSchemaChanged", "ProductivePersistenceEnabled", "ApiRequiresDatabase", "UsesSecretProviderRuntime", "UsesSyntheticFallback", "mock://crm/common-db", "ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted", "Sprint7P4PortalAuthRealRuntimeProbe", "Sprint 7 P3 Common DB Real Connectivity NonProduction Probe: Exists")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/data/crm-sprint-7-p3-common-db-real-connectivity-nonproduction-probe.md") + "`n" + (Get-Content -Raw "docs/data/crm-common-db-real-connectivity-nonproduction-probe-contract.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 7 P3 Common DB probe marker: $marker"
    }
}

foreach ($path in @("docs/integration/crm-sprint-7-p4-portal-auth-real-runtime-probe.md", "docs/integration/crm-portal-auth-real-runtime-probe-policy.md", "docs/integration/crm-portal-auth-real-runtime-probe-contract.md", "docs/integration/crm-portal-auth-real-runtime-probe-safety-boundary.md", "docs/operations/crm-portal-auth-real-runtime-probe-runbook.md", "docs/operations/crm-portal-auth-real-runtime-probe-rollback.md", "docs/architecture/crm-portal-auth-real-runtime-probe-architecture.md", "docs/security/crm-portal-auth-real-runtime-probe-token-boundary.md", "src/CRM.Application/Foundation/CrmPortalAuthRealRuntimeProbeContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthRealRuntimeProbeStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRealRuntimeProbe.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRealRuntimeProbeOptions.cs")) {
    Require-Path $path
}

$sprint7P4Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint7P4Program -notlike "*/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe*") {
    $failures += "Sprint 7 P4 Portal Auth real runtime endpoint missing."
}

if ($sprint7P4Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe") {
    $failures += "Sprint 7 P4 Portal Auth endpoint must remain GET-only."
}

foreach ($marker in @("Portal Auth real runtime probe is prepared but skipped because Portal Auth approval is not granted", "PortalAuthRealRuntimeProbe", "CrmPortalAuthRealRuntimeProbeStatusService", "PortalAuthRealRuntimeProbeExists", "PortalAuthRealRuntimeApprovalGranted", "SecretProviderRealNonProductionApprovalGranted", "PortalAuthRealRuntimeProbeEnabled", "PortalAuthRealRuntimeProbeAttempted", "PortalAuthRuntimeConnected", "PortalAuthBaseUrlResolved", "PortalAuthBaseUrlMaterialized", "PortalAuthBaseUrlLogged", "PortalAuthBaseUrlReturnedToApi", "PortalHttpClientCreated", "PortalHttpCallAttempted", "PortalAuthTokenValidationAttempted", "TokenReadAttempted", "HeaderReadAttempted", "AuthHeaderReadAttempted", "RealTokenMaterialized", "RealTokenLogged", "TokenReturnedToApi", "LoginImplementedByCrm", "LogoutImplementedByCrm", "IdentityImplementedByCrm", "RolesPersistedInCrm", "PermissionsPersistedInCrm", "ProductiveAuthorizationEnabled", "ApiRequiresPortalAuth", "UsesSyntheticFallback", "mock://crm/portal-auth", "mock://crm/portal-user", "ProbeSkippedBecausePortalAuthApprovalNotGranted", "Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423", "Sprint 7 P4 Portal Auth Real Runtime Probe: Exists")) {
    if (($sourceText + "`n" + (Get-Content -Raw "README.md") + "`n" + (Get-Content -Raw "codex/TASKS.md") + "`n" + (Get-Content -Raw "docs/integration/crm-sprint-7-p4-portal-auth-real-runtime-probe.md") + "`n" + (Get-Content -Raw "docs/integration/crm-portal-auth-real-runtime-probe-contract.md") + "`n" + (Get-Content -Raw "docs/security/crm-portal-auth-real-runtime-probe-token-boundary.md") + "`n" + (Get-Content -Raw "frontend/crm-web/src/main.ts")) -notlike "*$marker*") {
        $failures += "Missing Sprint 7 P4 Portal Auth probe marker: $marker"
    }
}

if ($sourceText -match "AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|MapDelete") {
    $failures += "Productive Auth middleware, JWT/cookie auth, authorization attribute or DELETE endpoint found."
}

$connectionScanText = $sourceText.Replace("ConnectionStringsConfigured", "").Replace("connectionStringsConfigured", "").Replace("Connection Strings Configured", "").Replace("CrmConnectionStringPolicyContract", "").Replace("ConnectionStringPolicy", "").Replace("connectionStringPolicy", "").Replace("RealConnectionStringUsed", "").Replace("realConnectionStringUsed", "").Replace("Real Connection String Used", "").Replace("ConnectionStringResolved", "").Replace("connectionStringResolved", "").Replace("Connection String Resolved", "").Replace("ConnectionStringValueMaterialized", "").Replace("connectionStringValueMaterialized", "").Replace("Connection String Value Materialized", "").Replace("ConnectionStringMaterializedInPublicContract", "").Replace("connectionStringMaterializedInPublicContract", "").Replace("Connection String Materialized In Public Contract", "").Replace("ConnectionStringLogged", "").Replace("connectionStringLogged", "").Replace("Connection String Logged", "").Replace("ConnectionStringReturnedToApi", "").Replace("connectionStringReturnedToApi", "").Replace("Connection String Returned To API", "").Replace("ConnectionStringReturned", "").Replace("connectionStringReturned", "").Replace("Connection String Returned", "").Replace("ConnectionStringPersisted", "").Replace("connectionStringPersisted", "").Replace("Connection String Persisted", "").Replace("ConnectionStringCached", "").Replace("connectionStringCached", "").Replace("Connection String Cached", "").Replace("UseSqlServerConfigured", "").Replace("useSqlServerConfigured", "").Replace("UseSqlServer Configured", "").Replace("UseSqlServerEnabled", "").Replace("useSqlServerEnabled", "").Replace("UseSqlServer Enabled", "")
if ($connectionScanText -match "FinancieroDb|UseSqlServer|ConnectionString|FinancieroUrl|financialBaseUrl") {
    $failures += "Runtime Financial adapter, connection string, shared DB or URL found before integration approval."
}

if ($connectionScanText -cmatch "Microsoft\.PowerBI|embedToken|workspaceId|reportId|datasetId|embedUrl|powerbi\.com|ConnectionString") {
    $failures += "Runtime BI adapter, token, ID, URL or connection string found before analytics approval."
}

if (Test-Path "database") {
    $failures += "Database directory or migration baseline must not exist in foundation sprints."
}

# Sprint 7 P5 Locked Productive Route Runtime Registration checks
foreach ($path in @("docs/api/crm-sprint-7-p5-locked-productive-route-runtime-registration-with-423.md", "docs/api/crm-locked-productive-route-runtime-registration-policy.md", "docs/api/crm-locked-productive-route-runtime-registration-contract.md", "docs/security/crm-locked-productive-route-runtime-registration-safety-boundary.md", "docs/operations/crm-locked-productive-route-runtime-registration-runbook.md", "docs/operations/crm-locked-productive-route-runtime-registration-rollback.md", "docs/architecture/crm-locked-productive-route-runtime-registration-architecture.md", "src/CRM.Application/Foundation/CrmLockedProductiveRouteRuntimeRegistrationContracts.cs", "src/CRM.Application/Foundation/CrmLockedProductiveRouteRuntimeRegistrationStatusService.cs", "src/CRM.Api/ProductiveRoutes/LockedProductiveRouteRuntimeRegistration.cs", "src/CRM.Api/ProductiveRoutes/LockedProductiveRouteRuntimeRegistrationOptions.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 7 P5 required file: $path"
    }
}
$sprint7P5Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint7P5Program -notlike "*/api/crm/foundation/sprint-7/locked-productive-route-runtime-registration*") {
    $failures += "Sprint 7 P5 locked productive route endpoint missing."
}
if ($sprint7P5Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/locked-productive-route-runtime-registration") {
    $failures += "Sprint 7 P5 foundation endpoint must remain GET-only."
}
$sprint7P5Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmLockedProductiveRouteRuntimeRegistrationContracts.cs", "src/CRM.Application/Foundation/CrmLockedProductiveRouteRuntimeRegistrationStatusService.cs", "src/CRM.Api/ProductiveRoutes/LockedProductiveRouteRuntimeRegistration.cs", "README.md", "codex/TASKS.md", "docs/api/crm-sprint-7-p5-locked-productive-route-runtime-registration-with-423.md", "docs/api/crm-locked-productive-route-runtime-registration-contract.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint7P5Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("Locked productive routes are not registered by default; explicit NonProduction flag returns 423 without side effects", "LockedProductiveRouteRuntimeRegistrationWith423", "CrmLockedProductiveRouteRuntimeRegistrationStatusService", "Crm:ProductiveRoutes:LockedRegistrationEnabled", "LockedProductiveRouteRuntimeRegistrationExists", "LockedProductiveRouteRuntimeRegistrationApprovalGranted", "LockedProductiveRouteRuntimeRegistrationEnabled", "ProductiveRoutesRegisteredByDefault", "ProductiveRoutesRegisteredWhenExplicitlyEnabled", "DefaultNegativeRouteStatus", "ExplicitlyEnabledLockedRouteStatus", "ProductiveCrudEnabled", "ProductiveDomainExecutionEnabled", "ProductivePersistenceEnabled", "DeleteEndpointsEnabled", "PortalAuthRuntimeRequired", "PortalAuthRuntimeEnabled", "TokenReadAttempted", "HeaderReadAttempted", "DbRuntimeEnabled", "EfRuntimeEnabled", "MigrationsCreated", "SideEffectsAllowed", "Sprint7P6Sprint7GateDecision", "Sprint 7 P5 Locked Productive Route Runtime Registration With 423: Exists")) {
    if ($sprint7P5Text -notlike "*$marker*") {
        $failures += "Missing Sprint 7 P5 locked route marker: $marker"
    }
}
if ($sprint7P5Text -match "MapDelete|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") {
    $failures += "Sprint 7 P5 must not enable DELETE, DB, Portal/Auth runtime, token/header reads or token storage."
}

# Sprint 7 P6 Gate Decision checks
foreach ($path in @("docs/releases/crm-sprint-7-closure.md", "docs/releases/crm-sprint-7-integrated-evidence.md", "docs/releases/crm-sprint-7-gate-decision.md", "docs/releases/crm-sprint-7-go-no-go.md", "docs/releases/crm-sprint-7-open-risks.md", "docs/releases/crm-sprint-7-decision-record.md", "docs/architecture/crm-sprint-7-gate-matrix.md", "docs/security/crm-sprint-7-security-gate-review.md", "docs/data/crm-sprint-7-persistence-gate-review.md", "docs/api/crm-sprint-7-api-gate-review.md", "docs/testing/crm-sprint-7-e2e-gate-review.md", "docs/roadmap/crm-sprint-8-options.md", "docs/roadmap/crm-sprint-8-recommended-path.md", "docs/roadmap/crm-sprint-8-gates.md", "src/CRM.Application/Foundation/CrmSprint7GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint7GateDecisionStatusService.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 7 P6 required file: $path"
    }
}
$sprint7P6Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint7P6Program -notlike "*/api/crm/foundation/sprint-7/gate-decision*") {
    $failures += "Sprint 7 P6 gate decision endpoint missing."
}
if ($sprint7P6Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-7/gate-decision") {
    $failures += "Sprint 7 P6 foundation endpoint must remain GET-only."
}
$sprint7P6Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmSprint7GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint7GateDecisionStatusService.cs", "README.md", "codex/TASKS.md", "docs/releases/crm-sprint-7-closure.md", "docs/releases/crm-sprint-7-gate-decision.md", "docs/roadmap/crm-sprint-8-recommended-path.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint7P6Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("Sprint 7 gate decision only; no real activation", "Sprint7GateDecision", "CrmSprint7GateDecisionStatusService", "GoForSprint8ControlledRuntimeApprovalAndPilotPlanning", "RealActivationDecision", "SecretProviderRealRuntimeDecision", "CommonDbRealConnectionDecision", "PortalAuthRealRuntimeDecision", "GoOnlyAsExplicitNonProductionLocked423", "ProductiveRoutesDefaultDecision", "ProductiveCrudDecision", "DeleteDecision", "ProductiveUiDecision", "ProductizationStatus", "Sprint8PlanningDecision", "Sprint8P1SecretProviderApprovalDecision", "Sprint 7: Closed", "Sprint 7 Gate Decision: Completed", "Sprint 8 Planning: Go")) {
    if ($sprint7P6Text -notlike "*$marker*") {
        $failures += "Missing Sprint 7 P6 gate marker: $marker"
    }
}
if ($sprint7P6Text -match "SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") {
    $failures += "Sprint 7 P6 must not activate DB, Portal/Auth runtime, token/header reads or token storage."
}

# Sprint 8 P1 Secret Provider Approval Decision checks
foreach ($path in @("docs/security/crm-sprint-8-p1-secret-provider-approval-decision.md", "docs/security/crm-secret-provider-approval-decision-policy.md", "docs/security/crm-secret-provider-controlled-read-approval-criteria.md", "docs/security/crm-secret-provider-approved-logical-secret-names.md", "docs/security/crm-secret-provider-redaction-approval.md", "docs/operations/crm-secret-provider-controlled-read-runbook.md", "docs/operations/crm-secret-provider-controlled-read-rollback.md", "docs/architecture/crm-secret-provider-controlled-read-architecture-decision.md", "src/CRM.Application/Foundation/CrmSecretProviderApprovalDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderApprovalDecisionStatusService.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 8 P1 required file: $path"
    }
}
$sprint8P1Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint8P1Program -notlike "*/api/crm/foundation/sprint-8/secret-provider-approval-decision*") {
    $failures += "Sprint 8 P1 secret provider approval endpoint missing."
}
if ($sprint8P1Program -match "Map(Post|Put|Patch|Delete)\(`"/api/crm/foundation/sprint-8/secret-provider-approval-decision") {
    $failures += "Sprint 8 P1 foundation endpoint must remain GET-only."
}
$sprint8P1Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmSecretProviderApprovalDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderApprovalDecisionStatusService.cs", "README.md", "codex/TASKS.md", "docs/security/crm-sprint-8-p1-secret-provider-approval-decision.md", "docs/security/crm-secret-provider-approved-logical-secret-names.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint8P1Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("Secret Provider approval decision only; no real secret read in Sprint 8 P1", "SecretProviderApprovalDecision", "CrmSecretProviderApprovalDecisionStatusService", "ApprovedForControlledNonProductionReadPlanning", "SecretProviderRealReadApprovedForNextSprint", "SecretProviderRealReadEnabledNow", "RealSecretReadAttempted", "RealSecretValueMaterialized", "RealSecretValueLogged", "SecretValueReturnedToApi", "KeyVaultRuntimeClientCreated", "KeyVaultRuntimeCallAttempted", "AzureSecretSdkRuntimeEnabled", "EnvFileRequired", "EnvSecretReadAllowed", "ApprovedSecretNamesOnly", "ApprovedSecretValues", "ApprovedForNonProductionOnly", "SecurityApprovalRecorded", "ArchitectureApprovalRecorded", "DevOpsApprovalRecorded", "RollbackPlanApproved", "ObservabilityPlanApproved", "RedactionPlanApproved", "Sprint8P2SecretProviderControlledRealNonProductionRead", "Sprint 8 P1 Secret Provider Approval Decision: Exists")) {
    if ($sprint8P1Text -notlike "*$marker*") {
        $failures += "Missing Sprint 8 P1 marker: $marker"
    }
}
if ($sprint8P1Text -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") {
    $failures += "Sprint 8 P1 must not read secrets/env/files or activate DB, Portal/Auth runtime, token/header reads or token storage."
}

# Sprint 8 P2 Secret Provider Controlled Real NonProduction Read checks
foreach ($path in @("docs/security/crm-sprint-8-p2-secret-provider-controlled-real-nonproduction-read.md", "docs/security/crm-secret-provider-controlled-real-read-policy.md", "docs/security/crm-secret-provider-controlled-real-read-contract.md", "docs/security/crm-secret-provider-controlled-real-read-redaction.md", "docs/operations/crm-secret-provider-controlled-real-read-runbook.md", "docs/operations/crm-secret-provider-controlled-real-read-rollback.md", "docs/architecture/crm-secret-provider-controlled-real-read-architecture.md", "src/CRM.Application/Foundation/CrmSecretProviderControlledRealReadContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderControlledRealReadStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/ISecretProviderRuntime.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeOptions.cs", "src/CRM.Infrastructure/Security/Secrets/DisabledSecretProviderRuntime.cs", "src/CRM.Infrastructure/Security/Secrets/ControlledNonProductionSecretProviderRuntime.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 8 P2 required file: $path"
    }
}
$sprint8P2Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint8P2Program -notlike "*/api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read*") {
    $failures += "Sprint 8 P2 secret provider controlled read endpoint missing."
}
$sprint8P2Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmSecretProviderControlledRealReadContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderControlledRealReadStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/ISecretProviderRuntime.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeOptions.cs", "src/CRM.Infrastructure/Security/Secrets/DisabledSecretProviderRuntime.cs", "src/CRM.Infrastructure/Security/Secrets/ControlledNonProductionSecretProviderRuntime.cs", "docs/security/crm-sprint-8-p2-secret-provider-controlled-real-nonproduction-read.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint8P2Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("SecretProviderControlledRealNonProductionRead", "CrmSecretProviderControlledRealReadStatusService", "SecretProviderControlledRealNonProductionReadEnabled: false", "SecretProviderControlledRealNonProductionReadAttempted: false", "RealSecretReadAttempted: false", "RealSecretValueMaterialized: false", "RealSecretValueLogged: false", "SecretValueReturnedToApi: false", "SecretValuePersisted: false", "SecretValueCached: false", "KeyVaultRuntimeClientCreated: false", "KeyVaultRuntimeCallAttempted: false", "AzureSecretSdkRuntimeEnabled: false", "UsesApprovedSecretNamesOnly: true", "NonProductionOnly: true", "FailClosedByDefault: true", "Sprint8P3CommonDbControlledRealConnectivity", "Controlled real secret read is disabled by default and never returns secret values", "ISecretProviderRuntime", "DisabledSecretProviderRuntime", "ControlledNonProductionSecretProviderRuntime", "Sprint 8 P2 Secret Provider Controlled Real NonProduction Read: Exists")) {
    if ($sprint8P2Text -notlike "*$marker*") {
        $failures += "Missing Sprint 8 P2 marker: $marker"
    }
}
if ($sprint8P2Text -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") {
    $failures += "Sprint 8 P2 must not activate secret SDK, DB, Portal/Auth runtime, token/header reads or token storage."
}

# Sprint 8 P3 Common DB Controlled Real Connectivity checks
foreach ($path in @("docs/data/crm-sprint-8-p3-common-db-controlled-real-connectivity.md", "docs/data/crm-common-db-controlled-real-connectivity-policy.md", "docs/data/crm-common-db-controlled-real-connectivity-contract.md", "docs/data/crm-common-db-controlled-real-connectivity-safety-boundary.md", "docs/operations/crm-common-db-controlled-real-connectivity-runbook.md", "docs/operations/crm-common-db-controlled-real-connectivity-rollback.md", "docs/architecture/crm-common-db-controlled-real-connectivity-architecture.md", "src/CRM.Application/Foundation/CrmCommonDbControlledRealConnectivityContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbControlledRealConnectivityStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/ICommonDbConnectivityProbe.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityProbeOptions.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/DisabledCommonDbConnectivityProbe.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/ControlledNonProductionCommonDbConnectivityProbe.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 8 P3 required file: $path"
    }
}
$sprint8P3Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint8P3Program -notlike "*/api/crm/foundation/sprint-8/common-db-controlled-real-connectivity*") {
    $failures += "Sprint 8 P3 common DB controlled connectivity endpoint missing."
}
$sprint8P3Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmCommonDbControlledRealConnectivityContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbControlledRealConnectivityStatusService.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/ICommonDbConnectivityProbe.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/CommonDbConnectivityProbeOptions.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/DisabledCommonDbConnectivityProbe.cs", "src/CRM.Infrastructure/Persistence/RuntimeProbe/ControlledNonProductionCommonDbConnectivityProbe.cs", "docs/data/crm-sprint-8-p3-common-db-controlled-real-connectivity.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint8P3Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("CommonDbControlledRealConnectivity", "CrmCommonDbControlledRealConnectivityStatusService", "CommonDbControlledRealConnectivityEnabled: false", "CommonDbConnectivityAttempted: false", "CommonDbConnected: false", "SecretProviderAvailabilityMetadataUsed: true", "ConnectionStringResolved: false", "ConnectionStringReturnedToApi: false", "ConnectionStringLogged: false", "MigrationsCreated: false", "DatabaseSchemaChanged: false", "ProductivePersistenceEnabled: false", "ProductiveCrudEnabled: false", "ApiRequiresDatabase: false", "NonProductionOnly: true", "FailClosedByDefault: true", "Sprint8P4PortalAuthControlledRealRuntimeValidation", "Common DB controlled real connectivity is disabled by default and never exposes connection strings", "ICommonDbConnectivityProbe", "DisabledCommonDbConnectivityProbe", "ControlledNonProductionCommonDbConnectivityProbe", "Sprint 8 P3 Common DB Controlled Real Connectivity: Exists")) {
    if ($sprint8P3Text -notlike "*$marker*") {
        $failures += "Missing Sprint 8 P3 marker: $marker"
    }
}
if ($sprint8P3Text -match "System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage") {
    $failures += "Sprint 8 P3 must not activate DB, EF, migrations, Portal/Auth runtime, token/header reads or token storage."
}

# Sprint 8 P4 Portal Auth Controlled Real Runtime Validation checks
foreach ($path in @("docs/integration/crm-sprint-8-p4-portal-auth-controlled-real-runtime-validation.md", "docs/integration/crm-portal-auth-controlled-real-runtime-validation-policy.md", "docs/integration/crm-portal-auth-controlled-real-runtime-validation-contract.md", "docs/security/crm-portal-auth-controlled-runtime-token-boundary.md", "docs/security/crm-portal-auth-controlled-runtime-redaction.md", "docs/operations/crm-portal-auth-controlled-runtime-validation-runbook.md", "docs/operations/crm-portal-auth-controlled-runtime-validation-rollback.md", "docs/architecture/crm-portal-auth-controlled-runtime-validation-architecture.md", "src/CRM.Application/Foundation/CrmPortalAuthControlledRealRuntimeValidationContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthControlledRealRuntimeValidationStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/IPortalAuthRuntimeValidationProbe.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRuntimeValidationProbeOptions.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/DisabledPortalAuthRuntimeValidationProbe.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/ControlledNonProductionPortalAuthRuntimeValidationProbe.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 8 P4 required file: $path"
    }
}
$sprint8P4Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint8P4Program -notlike "*/api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation*") {
    $failures += "Sprint 8 P4 Portal Auth controlled validation endpoint missing."
}
$sprint8P4Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmPortalAuthControlledRealRuntimeValidationContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthControlledRealRuntimeValidationStatusService.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/IPortalAuthRuntimeValidationProbe.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/PortalAuthRuntimeValidationProbeOptions.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/DisabledPortalAuthRuntimeValidationProbe.cs", "src/CRM.Infrastructure/Portal/RuntimeProbe/ControlledNonProductionPortalAuthRuntimeValidationProbe.cs", "docs/integration/crm-sprint-8-p4-portal-auth-controlled-real-runtime-validation.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint8P4Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("PortalAuthControlledRealRuntimeValidation", "CrmPortalAuthControlledRealRuntimeValidationStatusService", "PortalAuthControlledRealRuntimeValidationEnabled: false", "PortalAuthRuntimeValidationAttempted: false", "PortalAuthRuntimeConnected: false", "SecretProviderAvailabilityMetadataUsed: true", "PortalAuthBaseUrlReturnedToApi: false", "TokenReturnedToApi: false", "HeaderReadAttempted: false", "ProductiveAuthorizationEnabled: false", "ApiRequiresPortalAuth: false", "NonProductionOnly: true", "FailClosedByDefault: true", "Sprint8P5LockedRouteAuthorizationPolicyIntegration", "Portal Auth controlled real runtime validation is disabled by default and never reads request tokens", "IPortalAuthRuntimeValidationProbe", "DisabledPortalAuthRuntimeValidationProbe", "ControlledNonProductionPortalAuthRuntimeValidationProbe", "Sprint 8 P4 Portal Auth Controlled Real Runtime Validation: Exists")) {
    if ($sprint8P4Text -notlike "*$marker*") {
        $failures += "Missing Sprint 8 P4 marker: $marker"
    }
}
if ($sprint8P4Text -match "HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder") {
    $failures += "Sprint 8 P4 must not activate Portal/Auth runtime, token/header reads, token storage, DB, EF or migrations."
}

# Sprint 8 P5 Locked Route Authorization Policy Integration checks
foreach ($path in @("docs/api/crm-sprint-8-p5-locked-route-authorization-policy-integration.md", "docs/api/crm-locked-route-authorization-policy-contract.md", "docs/api/crm-locked-route-authorization-policy-boundary.md", "docs/security/crm-locked-route-authorization-policy-security-review.md", "docs/security/crm-locked-route-authorization-policy-token-boundary.md", "docs/operations/crm-locked-route-authorization-policy-runbook.md", "docs/operations/crm-locked-route-authorization-policy-rollback.md", "docs/architecture/crm-locked-route-authorization-policy-architecture.md", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyIntegrationContracts.cs", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyIntegrationStatusService.cs", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyEvaluator.cs", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyEvaluationRequest.cs", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyEvaluationResult.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 8 P5 required file: $path"
    }
}
$sprint8P5Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint8P5Program -notlike "*/api/crm/foundation/sprint-8/locked-route-authorization-policy-integration*") {
    $failures += "Sprint 8 P5 locked route authorization policy endpoint missing."
}
$sprint8P5Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyIntegrationContracts.cs", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyIntegrationStatusService.cs", "src/CRM.Application/Foundation/CrmLockedRouteAuthorizationPolicyEvaluator.cs", "src/CRM.Api/ProductiveRoutes/LockedProductiveRouteRuntimeRegistration.cs", "docs/api/crm-sprint-8-p5-locked-route-authorization-policy-integration.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint8P5Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("LockedRouteAuthorizationPolicyIntegration", "CrmLockedRouteAuthorizationPolicyIntegrationStatusService", "CrmLockedRouteAuthorizationPolicyEvaluator", "LockedRouteAuthorizationPolicyIntegrationEnabled: false", "AuthorizationPolicyEvaluated: false", "NotEvaluatedBecauseDisabled", "BlockedBecauseRouteLocked", "PortalAuthMetadataUsed: true", "PortalAuthRuntimeRequired: false", "PortalAuthRuntimeConnected: false", "TokenReadAttempted: false", "HeaderReadAttempted: false", "AuthHeaderReadAttempted: false", "PortalHttpCallAttempted: false", "ProductiveRoutesRegisteredByDefault: false", "DefaultNegativeRouteStatus: 404", "LockedRoutesEnabledOnlyWithExplicitNonProductionFlag: true", "LockedRouteStatus: 423", "LockedRouteAuthorizationDecisionReturned: false", "ProductiveCrudEnabled: false", "ProductiveDomainExecutionEnabled: false", "ProductivePersistenceEnabled: false", "DeleteEndpointsEnabled: false", "SideEffectsAllowed: false", "DbRuntimeEnabled: false", "EfRuntimeEnabled: false", "Sprint8P6Sprint8GateDecision", "Locked route authorization policy is disabled by default and never activates productive CRM routes", "Sprint 8 P5 Locked Route Authorization Policy Integration: Exists")) {
    if ($sprint8P5Text -notlike "*$marker*") {
        $failures += "Missing Sprint 8 P5 marker: $marker"
    }
}
if ($sprint8P5Text -match "HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder|MapDelete") {
    $failures += "Sprint 8 P5 must not activate Portal/Auth runtime, token/header reads, token storage, DB, EF, migrations or DELETE."
}

# Sprint 8 P6 Gate Decision checks
foreach ($path in @("docs/releases/crm-sprint-8-closure.md", "docs/releases/crm-sprint-8-integrated-evidence.md", "docs/releases/crm-sprint-8-gate-decision.md", "docs/releases/crm-sprint-8-go-no-go.md", "docs/releases/crm-sprint-8-open-risks.md", "docs/releases/crm-sprint-8-decision-record.md", "docs/architecture/crm-sprint-8-gate-matrix.md", "docs/security/crm-sprint-8-security-gate-review.md", "docs/data/crm-sprint-8-persistence-gate-review.md", "docs/api/crm-sprint-8-api-gate-review.md", "docs/testing/crm-sprint-8-e2e-gate-review.md", "docs/roadmap/crm-sprint-9-options.md", "docs/roadmap/crm-sprint-9-recommended-path.md", "docs/roadmap/crm-sprint-9-gates.md", "src/CRM.Application/Foundation/CrmSprint8GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint8GateDecisionStatusService.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 8 P6 required file: $path"
    }
}
$sprint8P6Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint8P6Program -notlike "*/api/crm/foundation/sprint-8/gate-decision*") {
    $failures += "Sprint 8 P6 gate decision endpoint missing."
}
$sprint8P6Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmSprint8GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint8GateDecisionStatusService.cs", "docs/releases/crm-sprint-8-gate-decision.md", "docs/roadmap/crm-sprint-9-recommended-path.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint8P6Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("Sprint8GateDecision", "CrmSprint8GateDecisionStatusService", "GoForSprint9ControlledRuntimeActivationPlanning", "RealProductionActivationDecision", "NoGo", "SecretProviderControlledReadDecision", "GoOnlyAsExplicitNonProductionFlag", "CommonDbControlledConnectivityDecision", "PortalAuthControlledValidationDecision", "LockedRouteAuthorizationPolicyDecision", "GoOnlyAsExplicitNonProductionLocked423", "ProductiveRoutesDefaultDecision", "ProductiveCrudDecision", "DeleteDecision", "ProductiveUiDecision", "ProductizationStatus", "NotReady", "Sprint9PlanningDecision", "Sprint9P1ControlledRuntimeActivationDecision", "Sprint 8 gate decision only; no production activation", "Sprint 8: Closed")) {
    if ($sprint8P6Text -notlike "*$marker*") {
        $failures += "Missing Sprint 8 P6 marker: $marker"
    }
}
if ($sprint8P6Text -match "HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder|MapDelete") {
    $failures += "Sprint 8 P6 must not activate Portal/Auth runtime, token/header reads, token storage, DB, EF, migrations or DELETE."
}

# Sprint 9 P1 Controlled Runtime Activation Decision checks
foreach ($path in @("docs/releases/crm-sprint-9-p1-controlled-runtime-activation-decision.md", "docs/architecture/crm-controlled-runtime-activation-decision.md", "docs/security/crm-controlled-runtime-activation-approval-policy.md", "docs/operations/crm-controlled-runtime-activation-runbook.md", "docs/operations/crm-controlled-runtime-activation-rollback.md", "docs/testing/crm-controlled-runtime-activation-test-strategy.md", "docs/architecture/crm-sprint-9-gate-matrix.md", "docs/security/crm-sprint-9-security-gate-review.md", "docs/data/crm-sprint-9-runtime-data-gate-review.md", "docs/api/crm-sprint-9-runtime-api-gate-review.md", "docs/testing/crm-sprint-9-runtime-e2e-gate-review.md", "src/CRM.Application/Foundation/CrmControlledRuntimeActivationDecisionContracts.cs", "src/CRM.Application/Foundation/CrmControlledRuntimeActivationDecisionStatusService.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 9 P1 required file: $path"
    }
}
$sprint9P1Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint9P1Program -notlike "*/api/crm/foundation/sprint-9/controlled-runtime-activation-decision*") {
    $failures += "Sprint 9 P1 controlled runtime activation decision endpoint missing."
}
$sprint9P1Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmControlledRuntimeActivationDecisionContracts.cs", "src/CRM.Application/Foundation/CrmControlledRuntimeActivationDecisionStatusService.cs", "docs/releases/crm-sprint-9-p1-controlled-runtime-activation-decision.md", "docs/architecture/crm-sprint-9-gate-matrix.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint9P1Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("ControlledRuntimeActivationDecision", "CrmControlledRuntimeActivationDecisionStatusService", "ApprovedForNonProductionTrialsOnly", "ProductionActivationDecision", "NoGo", "SecretProviderRuntimeEnablementTrialApproved", "CommonDbRuntimeConnectivityTrialApproved", "PortalAuthRuntimeValidationTrialApproved", "ProductiveRouteDryRunTrialApproved", "RuntimeTrialsEnabledNow: false", "ProductionRuntimeEnabledNow: false", "SecretProviderRuntimeEnabledNow: false", "CommonDbRuntimeEnabledNow: false", "PortalAuthRuntimeEnabledNow: false", "ProductiveRoutesEnabledNow: false", "ProductiveCrudEnabledNow: false", "DeleteEnabledNow: false", "ProductiveUiEnabledNow: false", "DefaultFailClosedRequired: true", "ExplicitNonProductionFlagsRequired: true", "Sprint9P2SecretProviderRuntimeEnablementTrial", "Sprint 9 P1 is an approval decision only; no runtime trial is enabled now", "Sprint 9 P1 Controlled Runtime Activation Decision: Exists")) {
    if ($sprint9P1Text -notlike "*$marker*") {
        $failures += "Missing Sprint 9 P1 marker: $marker"
    }
}
if ($sprint9P1Text -match "HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|System\.Data\.SqlClient|Microsoft\.Data\.SqlClient|UseSqlServer\(|AddDbContext\(|MigrationBuilder|MapDelete") {
    $failures += "Sprint 9 P1 must not activate Portal/Auth runtime, token/header reads, token storage, DB, EF, migrations or DELETE."
}

# Sprint 9 P2 Secret Provider Runtime Enablement Trial checks
foreach ($path in @("docs/security/crm-sprint-9-p2-secret-provider-runtime-enablement-trial.md", "docs/security/crm-secret-provider-runtime-enablement-trial-policy.md", "docs/security/crm-secret-provider-runtime-enablement-trial-contract.md", "docs/security/crm-secret-provider-runtime-enablement-trial-redaction.md", "docs/operations/crm-secret-provider-runtime-enablement-trial-runbook.md", "docs/operations/crm-secret-provider-runtime-enablement-trial-rollback.md", "docs/architecture/crm-secret-provider-runtime-enablement-trial-architecture.md", "src/CRM.Application/Foundation/CrmSecretProviderRuntimeEnablementTrialContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderRuntimeEnablementTrialStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeTrialOptions.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeTrialService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeTrialResult.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 9 P2 required file: $path"
    }
}
$sprint9P2Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint9P2Program -notlike "*/api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial*") {
    $failures += "Sprint 9 P2 Secret Provider trial endpoint missing."
}
$sprint9P2Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmSecretProviderRuntimeEnablementTrialContracts.cs", "src/CRM.Application/Foundation/CrmSecretProviderRuntimeEnablementTrialStatusService.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeTrialOptions.cs", "src/CRM.Infrastructure/Security/Secrets/SecretProviderRuntimeTrialService.cs", "docs/security/crm-sprint-9-p2-secret-provider-runtime-enablement-trial.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint9P2Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("SecretProviderRuntimeEnablementTrial", "CrmSecretProviderRuntimeEnablementTrialStatusService", "SecretProviderRuntimeEnablementTrialEnabled: false", "SecretProviderRuntimeTrialAttempted: false", "SecretProviderRuntimeConnected: false", "RealSecretReadAttempted: false", "SecretValueReturnedToApi: false", "AllowedLogicalSecretNamesEnforced: true", "NonProductionOnly: true", "ProductionBlocked: true", "FailClosedByDefault: true", "ObservabilityMetadataOnly: true", "Sprint9P3CommonDbRuntimeConnectivityTrial", "Secret Provider runtime trial is disabled by default and never returns secret values", "Crm:RuntimeTrials:SecretProviderEnabled", "Secret Provider Runtime Enablement Trial: Exists")) {
    if ($sprint9P2Text -notlike "*$marker*") {
        $failures += "Missing Sprint 9 P2 marker: $marker"
    }
}
if ($sprint9P2Text -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|MapDelete") {
    $failures += "Sprint 9 P2 must not activate secret SDK, env/file reads, DB, Portal/Auth runtime, token/header reads, token storage or DELETE."
}

# Sprint 9 P3 Common DB Runtime Connectivity Trial checks
foreach ($path in @("docs/data/crm-sprint-9-p3-common-db-runtime-connectivity-trial.md", "docs/data/crm-common-db-runtime-connectivity-trial-policy.md", "docs/data/crm-common-db-runtime-connectivity-trial-contract.md", "docs/data/crm-common-db-runtime-connectivity-trial-redaction.md", "docs/operations/crm-common-db-runtime-connectivity-trial-runbook.md", "docs/operations/crm-common-db-runtime-connectivity-trial-rollback.md", "docs/architecture/crm-common-db-runtime-connectivity-trial-architecture.md", "src/CRM.Application/Foundation/CrmCommonDbRuntimeConnectivityTrialContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbRuntimeConnectivityTrialStatusService.cs", "src/CRM.Infrastructure/Data/CommonDb/CommonDbRuntimeConnectivityTrialOptions.cs", "src/CRM.Infrastructure/Data/CommonDb/CommonDbRuntimeConnectivityTrialService.cs", "src/CRM.Infrastructure/Data/CommonDb/CommonDbRuntimeConnectivityTrialResult.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 9 P3 required file: $path"
    }
}
$sprint9P3Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint9P3Program -notlike "*/api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial*") {
    $failures += "Sprint 9 P3 Common DB trial endpoint missing."
}
$sprint9P3Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmCommonDbRuntimeConnectivityTrialContracts.cs", "src/CRM.Application/Foundation/CrmCommonDbRuntimeConnectivityTrialStatusService.cs", "src/CRM.Infrastructure/Data/CommonDb/CommonDbRuntimeConnectivityTrialOptions.cs", "src/CRM.Infrastructure/Data/CommonDb/CommonDbRuntimeConnectivityTrialService.cs", "docs/data/crm-sprint-9-p3-common-db-runtime-connectivity-trial.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint9P3Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("CommonDbRuntimeConnectivityTrial", "CrmCommonDbRuntimeConnectivityTrialStatusService", "CommonDbRuntimeConnectivityTrialEnabled: false", "CommonDbConnectionAttempted: false", "CommonDbConnected: false", "CommonDbConnectionStringResolved: false", "CommonDbConnectionStringReturnedToApi: false", "CommonDbConnectionStringLogged: false", "CommonDbConnectionStringPersisted: false", "CommonDbConnectionStringCached: false", "SecretProviderMetadataDependencyValidated: true", "SchemaCreated: false", "MigrationExecuted: false", "EfRuntimeEnabled: false", "ProductivePersistenceEnabled: false", "NonProductionOnly: true", "ProductionBlocked: true", "FailClosedByDefault: true", "ObservabilityMetadataOnly: true", "Sprint9P4PortalAuthRuntimeValidationTrial", "Common DB runtime connectivity trial is disabled by default and never exposes connection strings", "Crm:RuntimeTrials:CommonDbConnectivityEnabled", "Common DB Runtime Connectivity Trial: Exists")) {
    if ($sprint9P3Text -notlike "*$marker*") {
        $failures += "Missing Sprint 9 P3 marker: $marker"
    }
}
if ($sprint9P3Text -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|MigrationBuilder|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|MapDelete") {
    $failures += "Sprint 9 P3 must not activate secret SDK, env/file reads, DB, EF, migrations, Portal/Auth runtime, token/header reads, token storage or DELETE."
}

# Sprint 9 P4 Portal Auth Runtime Validation Trial checks
foreach ($path in @("docs/security/crm-sprint-9-p4-portal-auth-runtime-validation-trial.md", "docs/security/crm-portal-auth-runtime-validation-trial-policy.md", "docs/security/crm-portal-auth-runtime-validation-trial-contract.md", "docs/security/crm-portal-auth-runtime-validation-trial-redaction.md", "docs/operations/crm-portal-auth-runtime-validation-trial-runbook.md", "docs/operations/crm-portal-auth-runtime-validation-trial-rollback.md", "docs/architecture/crm-portal-auth-runtime-validation-trial-architecture.md", "src/CRM.Application/Foundation/CrmPortalAuthRuntimeValidationTrialContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthRuntimeValidationTrialStatusService.cs", "src/CRM.Infrastructure/Portal/Auth/PortalAuthRuntimeValidationTrialOptions.cs", "src/CRM.Infrastructure/Portal/Auth/PortalAuthRuntimeValidationTrialService.cs", "src/CRM.Infrastructure/Portal/Auth/PortalAuthRuntimeValidationTrialResult.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 9 P4 required file: $path"
    }
}
$sprint9P4Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint9P4Program -notlike "*/api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial*") {
    $failures += "Sprint 9 P4 Portal Auth trial endpoint missing."
}
$sprint9P4Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmPortalAuthRuntimeValidationTrialContracts.cs", "src/CRM.Application/Foundation/CrmPortalAuthRuntimeValidationTrialStatusService.cs", "src/CRM.Infrastructure/Portal/Auth/PortalAuthRuntimeValidationTrialOptions.cs", "src/CRM.Infrastructure/Portal/Auth/PortalAuthRuntimeValidationTrialService.cs", "docs/security/crm-sprint-9-p4-portal-auth-runtime-validation-trial.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint9P4Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("PortalAuthRuntimeValidationTrial", "CrmPortalAuthRuntimeValidationTrialStatusService", "PortalAuthRuntimeValidationTrialEnabled: false", "PortalAuthValidationAttempted: false", "PortalAuthValidated: false", "PortalHttpAttempted: false", "PortalHttpConfigured: false", "PortalAuthUrlResolved: false", "PortalAuthUrlReturnedToApi: false", "PortalClientSecretResolved: false", "PortalClientSecretReturnedToApi: false", "AuthHeaderRead: false", "TokenRead: false", "TokenStored: false", "ClaimsMapped: false", "ProductiveAuthEnabled: false", "LoginEndpointCreated: false", "LogoutEndpointCreated: false", "IdentityRuntimeEnabled: false", "AuthAttributeEnabled: false", "SecretProviderMetadataDependencyValidated: true", "CommonDbMetadataDependencyValidated: true", "NonProductionOnly: true", "ProductionBlocked: true", "FailClosedByDefault: true", "ObservabilityMetadataOnly: true", "Sprint9P5ProductiveRouteDryRunTrial", "Portal Auth runtime validation trial is disabled by default and never reads authorization headers or tokens", "Crm:RuntimeTrials:PortalAuthValidationEnabled", "Portal Auth Runtime Validation Trial: Exists")) {
    if ($sprint9P4Text -notlike "*$marker*") {
        $failures += "Missing Sprint 9 P4 marker: $marker"
    }
}
$sprint9P4ScanText = $sprint9P4Text.Replace("AuthAttributeEnabled", "").Replace("AuthHeaderRead", "")
if ($sprint9P4ScanText -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|MigrationBuilder|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|MapDelete") {
    $failures += "Sprint 9 P4 must not activate secret SDK, env/file reads, DB, EF, migrations, Portal/Auth runtime, token/header reads, token storage or DELETE."
}

# Sprint 9 P5 Productive Route Dry Run Trial checks
foreach ($path in @("docs/api/crm-sprint-9-p5-productive-route-dry-run-trial.md", "docs/api/crm-productive-route-dry-run-trial-policy.md", "docs/api/crm-productive-route-dry-run-trial-contract.md", "docs/api/crm-productive-route-dry-run-trial-redaction.md", "docs/operations/crm-productive-route-dry-run-trial-runbook.md", "docs/operations/crm-productive-route-dry-run-trial-rollback.md", "docs/architecture/crm-productive-route-dry-run-trial-architecture.md", "src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialContracts.cs", "src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialStatusService.cs", "src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialEvaluator.cs", "src/CRM.Api/ProductiveRoutes/ProductiveRouteDryRunTrialOptions.cs", "src/CRM.Api/ProductiveRoutes/ProductiveRouteDryRunTrialService.cs", "src/CRM.Api/ProductiveRoutes/ProductiveRouteDryRunTrialResult.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 9 P5 required file: $path"
    }
}
$sprint9P5Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint9P5Program -notlike "*/api/crm/foundation/sprint-9/productive-route-dry-run-trial*") {
    $failures += "Sprint 9 P5 Productive Route dry-run endpoint missing."
}
$sprint9P5Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialContracts.cs", "src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialStatusService.cs", "src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialEvaluator.cs", "src/CRM.Api/ProductiveRoutes/ProductiveRouteDryRunTrialOptions.cs", "src/CRM.Api/ProductiveRoutes/ProductiveRouteDryRunTrialService.cs", "docs/api/crm-sprint-9-p5-productive-route-dry-run-trial.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint9P5Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("ProductiveRouteDryRunTrial", "CrmProductiveRouteDryRunTrialStatusService", "ProductiveRouteDryRunTrialEnabled: false", "ProductiveRoutesRegisteredByDefault: false", "ProductiveRoutesDryRunRegistered: false", "ProductiveRouteDryRunAttempted: false", "ProductiveRouteDryRunAllowed: false", "ProductiveRouteDryRunDecisionReturned: false", "ProductiveRouteDryRunStatusCode: 423", "ProductiveCrudEnabled: false", "ProductiveDomainExecutionEnabled: false", "ProductivePersistenceEnabled: false", "DatabaseWriteAttempted: false", "SideEffectsAllowed: false", "DeleteEndpointsEnabled: false", "DbRuntimeEnabled: false", "EfRuntimeEnabled: false", "MigrationsEnabled: false", "SchemaChangeAllowed: false", "PortalAuthMetadataDependencyValidated: true", "CommonDbMetadataDependencyValidated: true", "SecretProviderMetadataDependencyValidated: true", "AuthHeaderRead: false", "TokenRead: false", "TokenStored: false", "AuthAttributeEnabled: false", "NonProductionOnly: true", "ProductionBlocked: true", "FailClosedByDefault: true", "ObservabilityMetadataOnly: true", "Sprint9P6Sprint9GateDecision", "Productive route dry-run trial is disabled by default and never registers productive CRM routes", "Crm:RuntimeTrials:ProductiveRouteDryRunEnabled", "Productive Route Dry Run Trial: Exists")) {
    if ($sprint9P5Text -notlike "*$marker*") {
        $failures += "Missing Sprint 9 P5 marker: $marker"
    }
}
$sprint9P5ScanText = $sprint9P5Text.Replace("AuthAttributeEnabled", "").Replace("AuthHeaderRead", "")
if ($sprint9P5ScanText -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|MigrationBuilder|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|MapDelete") {
    $failures += "Sprint 9 P5 must not activate secret SDK, env/file reads, DB, EF, migrations, Portal/Auth runtime, token/header reads, token storage or DELETE."
}

# Sprint 9 P6 Gate Decision checks
foreach ($path in @("docs/roadmap/crm-sprint-9-gate-decision.md", "docs/roadmap/crm-sprint-9-go-no-go.md", "docs/roadmap/crm-sprint-9-evidence-summary.md", "docs/roadmap/crm-sprint-9-risk-register.md", "docs/roadmap/crm-sprint-9-release-notes.md", "docs/operations/crm-sprint-9-gate-decision-runbook.md", "docs/architecture/crm-sprint-9-gate-decision-architecture.md", "src/CRM.Application/Foundation/CrmSprint9GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint9GateDecisionStatusService.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 9 P6 required file: $path"
    }
}
$sprint9P6Program = Get-Content -Raw "src/CRM.Api/Program.cs"
if ($sprint9P6Program -notlike "*/api/crm/foundation/sprint-9/gate-decision*") {
    $failures += "Sprint 9 P6 gate decision endpoint missing."
}
if ($sprint9P6Program -like "*/api/crm/foundation/sprint-9/gate-decision/probe*") {
    $failures += "Sprint 9 P6 must remain GET-only and must not add a probe route."
}
$sprint9P6Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmSprint9GateDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint9GateDecisionStatusService.cs", "docs/roadmap/crm-sprint-9-gate-decision.md", "docs/roadmap/crm-sprint-9-go-no-go.md", "docs/roadmap/crm-sprint-9-evidence-summary.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint9P6Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("Sprint9GateDecision", "CrmSprint9GateDecisionStatusService", "Sprint9GateDecisionExists: true", "Sprint9Closed: true", "Sprint9EvidenceComplete: true", "GoForSprint10ControlledProductizationReadinessPlanning", "ProductionActivationDecision", "NoGo", "SecretProviderRuntimeTrialDecision", "GoOnlyAsExplicitNonProductionTrial", "CommonDbRuntimeConnectivityTrialDecision", "PortalAuthRuntimeValidationTrialDecision", "ProductiveRouteDryRunTrialDecision", "GoOnlyAsExplicitNonProductionDryRun", "ProductiveRouteRegistrationDecision", "NoGoByDefault", "ProductiveCrudDecision", "DeleteDecision", "DbRuntimeDecision", "NoGoForProduction", "PortalAuthEnforcementDecision", "ProductionActivationApproved: false", "RuntimeActivationApprovedForProduction: false", "ProductiveRoutesApprovedByDefault: false", "ProductiveCrudApproved: false", "DeleteApproved: false", "DatabaseWritesApproved: false", "EfRuntimeApproved: false", "MigrationsApproved: false", "SchemaChangesApproved: false", "PortalAuthEnforcementApproved: false", "TokenHeaderReadsApproved: false", "LoginLogoutApproved: false", "IdentityRuntimeApproved: false", "ProductiveUiApproved: false", "NonProductionTrialsRemainAllowedOnlyWithExplicitFlags: true", "AllTrialsFailClosedByDefault: true", "AllObservabilityMetadataOnly: true", "ProductizationStatus", "NotReady", "Sprint10P1ProductizationReadinessDecision", "Sprint 9 gate decision only; production activation remains NoGo", "Sprint 9 Gate Decision: Completed")) {
    if ($sprint9P6Text -notlike "*$marker*") {
        $failures += "Missing Sprint 9 P6 marker: $marker"
    }
}
if ($sprint9P6Text -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|MigrationBuilder|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|MapDelete") {
    $failures += "Sprint 9 P6 must not activate secret SDK, env/file reads, DB, EF, migrations, Portal/Auth runtime, token/header reads, token storage or DELETE."
}

# Sprint 10 P1 Productization Readiness Decision checks
foreach ($path in @("docs/roadmap/crm-sprint-10-productization-readiness-decision.md", "docs/roadmap/crm-sprint-10-go-no-go.md", "docs/roadmap/crm-sprint-10-risk-register.md", "docs/roadmap/crm-sprint-10-recommended-path.md", "docs/operations/crm-sprint-10-productization-readiness-runbook.md", "docs/architecture/crm-sprint-10-productization-readiness-architecture.md", "src/CRM.Application/Foundation/CrmSprint10ProductizationReadinessDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint10ProductizationReadinessDecisionStatusService.cs")) {
    if (-not (Test-Path $path)) {
        $failures += "Missing Sprint 10 P1 required file: $path"
    }
}
if ($apiProgram -notlike "*/api/crm/foundation/sprint-10/productization-readiness-decision*") {
    $failures += "Missing Sprint 10 P1 productization readiness endpoint."
}
if ($apiProgram -like "*/api/crm/foundation/sprint-10/productization-readiness-decision/probe*") {
    $failures += "Sprint 10 P1 must remain GET-only and must not add a probe route."
}
$sprint10P1Text = ""
foreach ($file in @("src/CRM.Application/Foundation/CrmSprint10ProductizationReadinessDecisionContracts.cs", "src/CRM.Application/Foundation/CrmSprint10ProductizationReadinessDecisionStatusService.cs", "docs/roadmap/crm-sprint-10-productization-readiness-decision.md", "docs/roadmap/crm-sprint-10-go-no-go.md", "docs/roadmap/crm-sprint-10-risk-register.md", "docs/roadmap/crm-sprint-10-recommended-path.md", "docs/operations/crm-sprint-10-productization-readiness-runbook.md", "docs/architecture/crm-sprint-10-productization-readiness-architecture.md", "frontend/crm-web/src/main.ts")) {
    if (Test-Path $file) { $sprint10P1Text += "`n" + (Get-Content -Raw $file) }
}
foreach ($marker in @("Sprint10P1ProductizationReadinessDecision", "CrmSprint10ProductizationReadinessDecisionStatusService", "Sprint10P1ProductizationReadinessDecisionExists: true", "Sprint10P1Approved: true", "Sprint9GateReviewed: true", "Sprint9ProductionNoGoPreserved: true", "GoForControlledNonProductionProductizationPreparation", "ProductionActivationDecision", "NoGo", "ProductiveRuntimeActivationDecision", "NoGoForProduction", "CommonDbControlledActivationDecision", "GoOnlyAsExplicitNonProductionPreparation", "PortalAuthControlledActivationDecision", "ProductiveRouteControlledActivationDecision", "ProductiveCrudPilotDecision", "NoGoUntilP5", "ProductiveUiDecision", "ProductionActivationApproved: false", "ProductiveRuntimeActivationApprovedForProduction: false", "CommonDbControlledPreparationApproved: true", "PortalAuthControlledPreparationApproved: true", "ProductiveRouteControlledPreparationApproved: true", "ProductiveCrudPilotApproved: false", "ProductiveUiApproved: false", "NonProductionOnly: true", "ExplicitFlagsRequired: true", "FailClosedByDefault: true", "ObservabilityMetadataOnly: true", "ProductizationStatus", "PreparationOnly", "Sprint10P2CommonDbControlledActivationPlan", "Sprint 10 P1 Productization Readiness Decision: Exists")) {
    if ($sprint10P1Text -notlike "*$marker*") {
        $failures += "Missing Sprint 10 P1 marker: $marker"
    }
}
if ($sprint10P1Text -match "SecretClient|DefaultAzureCredential|ManagedIdentityCredential|EnvironmentCredential|Environment\.GetEnvironmentVariable|File\.ReadAllText|SqlConnection\(|DbConnection\(|UseSqlServer\(|AddDbContext\(|MigrationBuilder|HttpClient\(|new HttpClient|Request\.Headers|Headers\[|AddAuthentication|UseAuthentication|UseAuthorization|AuthorizeAttribute|JwtBearer|CookieAuthentication|localStorage|sessionStorage|MapDelete") {
    $failures += "Sprint 10 P1 must not activate secret SDK, env/file reads, DB, EF, migrations, Portal/Auth runtime, token/header reads, token storage or DELETE."
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
