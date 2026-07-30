using System.Reflection;
using Xunit;

namespace CRM.ArchitectureTests;

public sealed class ArchitectureDependencyTests
{
    [Fact]
    public void Domain_DoesNotDependOnApplicationInfrastructureOrApi()
    {
        var references = ReferencedAssemblyNames(typeof(Domain.AssemblyMarker).Assembly);

        Assert.DoesNotContain("CRM.Application", references);
        Assert.DoesNotContain("CRM.Infrastructure", references);
        Assert.DoesNotContain("CRM.Api", references);
    }

    [Fact]
    public void Application_DoesNotDependOnInfrastructureOrApi()
    {
        var references = ReferencedAssemblyNames(typeof(Application.Foundation.CrmReadinessService).Assembly);

        Assert.DoesNotContain("CRM.Infrastructure", references);
        Assert.DoesNotContain("CRM.Api", references);
    }

    [Fact]
    public void Domain_IsNotCoupledToIdentity()
    {
        var references = ReferencedAssemblyNames(typeof(Domain.AssemblyMarker).Assembly);

        Assert.DoesNotContain(references, name => name.Contains("Identity", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Infrastructure_DoesNotContainProductivePersistenceYet()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Infrastructure"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);

        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseNpgsql", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Api_OnlyExposesAllowedContractAndFoundationPreviewEndpoints()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("/api/crm/domain-catalog", program);
        Assert.Contains("/api/crm/contracts", program);
        Assert.Contains("/api/crm/integration-boundaries", program);
        Assert.Contains("/api/crm/foundation/leads/preview", program);
        Assert.Contains("/api/crm/foundation/accounts/preview", program);
        Assert.Contains("/api/crm/foundation/contacts/preview", program);
        Assert.Contains("/api/crm/foundation/leads/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/accounts/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/contacts/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/read-model-status", program);
        Assert.Contains("/api/crm/foundation/portal-integration/status", program);
        Assert.Contains("/api/crm/foundation/portal-integration/contracts", program);
        Assert.Contains("/api/crm/foundation/portal-integration/required-capabilities", program);
        Assert.Contains("/api/crm/foundation/financial-integration/status", program);
        Assert.Contains("/api/crm/foundation/financial-integration/contracts", program);
        Assert.Contains("/api/crm/foundation/financial-integration/required-capabilities", program);
        Assert.Contains("/api/crm/foundation/financial-integration/events", program);
        Assert.Contains("/api/crm/foundation/reporting/status", program);
        Assert.Contains("/api/crm/foundation/reporting/kpis", program);
        Assert.Contains("/api/crm/foundation/reporting/dashboards", program);
        Assert.Contains("/api/crm/foundation/reporting/analytics-read-models", program);
        Assert.Contains("/api/crm/foundation/sprint-1/closure-status", program);
        Assert.Contains("/api/crm/foundation/persistence/readiness", program);
        Assert.Contains("/api/crm/foundation/persistence/seam-status", program);
        Assert.Contains("/api/crm/foundation/persistence/feature-flags", program);
        Assert.Contains("/api/crm/foundation/persistence/stores/status", program);
        Assert.Contains("/api/crm/foundation/persistence/stores/clear-preview", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/simulation-status", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/scenarios", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/permissions", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/sample-user-context", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/check-permission", program);
        Assert.Contains("/api/crm/foundation/crud/status", program);
        Assert.Contains("/api/crm/foundation/sprint-2/integration-readiness", program);
        Assert.Contains("/api/crm/foundation/sprint-2/productization-gate", program);
        Assert.Contains("/api/crm/foundation/sprint-3/durable-persistence-setup", program);
        Assert.Contains("/api/crm/foundation/sprint-3/common-db-connection-strategy", program);
        Assert.Contains("/api/crm/foundation/sprint-3/ef-prototype-status", program);
        Assert.Contains("/api/crm/foundation/sprint-3/portal-auth-runtime-contract", program);
        Assert.Contains("/api/crm/foundation/sprint-3/productive-api-route-draft", program);
        Assert.Contains("/api/crm/foundation/sprint-3/productization-review", program);
        Assert.Contains("/api/crm/foundation/sprint-4/runtime-readiness", program);
        Assert.Contains("/api/crm/foundation/sprint-4/common-db-runtime-probe", program);
        Assert.Contains("/api/crm/foundation/sprint-4/portal-auth-runtime-probe", program);
        Assert.Contains("/api/crm/foundation/sprint-4/productive-routes-locked-stub", program);
        Assert.Contains("/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness", program);
        Assert.Contains("/api/crm/foundation/sprint-4/gate-decision", program);
        Assert.Contains("/api/crm/foundation/sprint-5/runtime-probe-activation-plan", program);
        Assert.Contains("/api/crm/foundation/sprint-5/secret-provider-runtime-contract", program);
        Assert.Contains("/api/crm/foundation/sprint-5/common-db-probe-optional-activation", program);
        Assert.Contains("/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation", program);
        Assert.Contains("/api/crm/foundation/sprint-5/locked-productive-route-stub-trial", program);
        Assert.Contains("/api/crm/foundation/sprint-5/gate-decision", program);
        Assert.Contains("/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package", program);
        Assert.Contains("/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation", program);
        Assert.Contains("/api/crm/foundation/sprint-6/common-db-connectivity-dry-run", program);
        Assert.Contains("/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run", program);
        Assert.Contains("/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial", program);
        Assert.Contains("/api/crm/foundation/sprint-6/gate-decision", program);
        Assert.Contains("/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval", program);
        Assert.Contains("/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe", program);
        Assert.Contains("/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe", program);
        Assert.Contains("/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe", program);
        Assert.Contains("/api/crm/foundation/leads", program);
        Assert.Contains("/api/crm/foundation/accounts", program);
        Assert.Contains("/api/crm/foundation/contacts", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/", program.Replace("MapPut(\"/api/crm/foundation/leads/{id}\"", string.Empty, StringComparison.Ordinal)
            .Replace("MapPut(\"/api/crm/foundation/accounts/{id}\"", string.Empty, StringComparison.Ordinal)
            .Replace("MapPut(\"/api/crm/foundation/contacts/{id}\"", string.Empty, StringComparison.Ordinal));
        Assert.DoesNotContain("MapPatch", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("Create" + "Lead", program);
    }

    [Fact]
    public void EfPrototype_IsDisabledAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmEfPrototypeStatusService", source);
        Assert.Contains("CrmDbContextPrototype", source);
        Assert.Contains("CrmEfPrototypeMarker", source);
        Assert.Contains("EF/DbContext prototype only; runtime disabled and no database configured", source);
        Assert.Contains("CRM_EF_RUNTIME_ENABLED=false", source);
        Assert.Contains("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", source);
        Assert.Contains("Sprint3P4PortalAuthRuntimeContractValidation", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/ef-prototype-status\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/ef-prototype-status", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/ef-prototype-status", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseNpgsql", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalAuthRuntimeContract_IsContractOnlyAndDoesNotActivateAuth()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmPortalAuthRuntimeContractStatusService", source);
        Assert.Contains("PortalAuthRuntimeAdapterPlaceholder", source);
        Assert.Contains("PortalAuthContextMapperPlaceholder", source);
        Assert.Contains("Portal Auth runtime contract validation only; no real Auth runtime configured", source);
        Assert.Contains("PortalAuthRuntimeContractValidation", source);
        Assert.Contains("Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/portal-auth-runtime-contract\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/portal-auth-runtime-contract", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/portal-auth-runtime-contract", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("/login", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("/logout", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalAuthRuntimeProbe_IsDisabledAndDoesNotReadTokensOrCallPortal()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmPortalAuthRuntimeProbeStatusService", source);
        Assert.Contains("PortalAuthRuntimeProbePlaceholder", source);
        Assert.Contains("Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted", source);
        Assert.Contains("PortalAuthRuntimeProbe", source);
        Assert.Contains("Sprint4P4ProductiveRoutesLockedStubValidation", source);
        Assert.Contains("tokenReadAttemptedByRuntime: false", source);
        Assert.Contains("portalHttpAttemptedByRuntime: false", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/portal-auth-runtime-probe\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/portal-auth-runtime-probe", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/portal-auth-runtime-probe", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("/login", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("/logout", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductiveRoutesLockedStubValidation_DoesNotRegisterProductiveRoutes()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmProductiveRoutesLockedStubStatusService", source);
        Assert.Contains("ProductiveRoutesLockedStubValidation", source);
        Assert.Contains("DocumentOnlyPreferred", source);
        Assert.Contains("Productive routes locked stub validation only; no productive routes are active", source);
        Assert.Contains("Sprint4P5NonProductionE2EPilotReadiness", source);
        Assert.Contains("lockedStubsRegistered: false", source);
        Assert.Contains("p4FoundationCrudStillSeparate: true", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/productive-routes-locked-stub\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/productive-routes-locked-stub", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/productive-routes-locked-stub", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void NonProductionE2EPilotReadiness_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmNonProductionE2EPilotReadinessStatusService", source);
        Assert.Contains("NonProductionE2EPilotReadiness", source);
        Assert.Contains("Non-production E2E pilot readiness only; no real activation", source);
        Assert.Contains("Sprint4P6Sprint4GateDecision", source);
        Assert.Contains("e2ePilotCanRun: true", source);
        Assert.Contains("e2ePilotScope: 'FoundationOnly'", source);
        Assert.Contains("productiveRoutesUsed: false", source);
        Assert.Contains("realDatabaseUsed: false", source);
        Assert.Contains("portalAuthRuntimeUsed: false", source);
        Assert.Contains("durablePersistenceUsed: false", source);
        Assert.Contains("deleteOperationsUsed: false", source);
        Assert.Contains("syntheticDataOnly: true", source);
        Assert.Contains("foundationEndpointsOnly: true", source);
        Assert.Contains("negativeRouteValidationRequired: true", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Sprint4GateDecision_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmSprint4GateDecisionStatusService", source);
        Assert.Contains("Sprint4GateDecision", source);
        Assert.Contains("Sprint 4 gate decision only; no real activation", source);
        Assert.Contains("GoForNonProductionFoundationPilot", source);
        Assert.Contains("NoGoForRuntimeActivation", source);
        Assert.Contains("Sprint5P1ControlledRuntimeProbeActivationPlan", source);
        Assert.Contains("sprint4: 'Closed'", source);
        Assert.Contains("sprint4GateDecision: 'Completed'", source);
        Assert.Contains("sprint4OverallDecision: 'GoForNonProductionFoundationPilot'", source);
        Assert.Contains("realActivationDecision: 'NoGo'", source);
        Assert.Contains("commonDbRuntimeDecision: 'NoGoForRuntimeActivation'", source);
        Assert.Contains("sprint4PortalAuthRuntimeDecision: 'NoGoForRuntimeActivation'", source);
        Assert.Contains("productiveRoutesDecision: 'NoGo'", source);
        Assert.Contains("deleteDecision: 'NoGo'", source);
        Assert.Contains("nonProductionE2EPilotDecision: 'GoFoundationOnly'", source);
        Assert.Contains("sprint5PlanningDecision: 'Go'", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/gate-decision\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/gate-decision", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/gate-decision", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductiveApiRouteDraft_IsDocumentedOnlyAndDoesNotRegisterProductiveRoutes()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmProductiveApiRouteDraftStatusService", source);
        Assert.Contains("ProductiveApiRouteDraft", source);
        Assert.Contains("Productive API route draft only; routes are not active", source);
        Assert.Contains("Sprint3P6Sprint3ProductizationReview", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/productive-api-route-draft\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/productive-api-route-draft", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/productive-api-route-draft", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Sprint3ProductizationReview_IsNoGoAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmSprint3ProductizationReviewStatusService", source);
        Assert.Contains("Sprint3ProductizationReview", source);
        Assert.Contains("NoGoForRealActivation", source);
        Assert.Contains("Sprint 3 productization review only; no real activation", source);
        Assert.Contains("Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/productization-review\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/productization-review", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/productization-review", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void RuntimeEnvironmentReadiness_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmRuntimeEnvironmentReadinessStatusService", source);
        Assert.Contains("RuntimeEnvironmentReadiness", source);
        Assert.Contains("Runtime readiness only; no real activation", source);
        Assert.Contains("Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/runtime-readiness\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/runtime-readiness", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/runtime-readiness", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommonDbRuntimeProbe_IsDisabledAndDoesNotConnectDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmCommonDbRuntimeProbeStatusService", source);
        Assert.Contains("CommonDbRuntimeProbePlaceholder", source);
        Assert.Contains("CommonDbRuntimeProbe", source);
        Assert.Contains("Common DB runtime probe exists but is disabled; no database connection is attempted", source);
        Assert.Contains("Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/common-db-runtime-probe\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/common-db-runtime-probe", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/common-db-runtime-probe", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Source_DoesNotContainIdentityTokenStorageOrOwnSqlServer()
    {
        var source = ReadSourceFiles("src", "tests", "frontend", "docker-compose.yml", "docker-compose.crm.yml");

        Assert.DoesNotContain("Microsoft.AspNetCore." + "Identity", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Identity", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("mcr.microsoft.com/" + "mssql", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Portal");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FuturePortalAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("BaseUrl", source, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferencePortalIntegrationPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Portal", source, StringComparison.Ordinal);
        Assert.DoesNotContain("IPortal", source, StringComparison.Ordinal);
    }

    [Fact]
    public void PortalPlaceholder_DoesNotMakeRuntimePortalCalls()
    {
        var source = ReadSourceFiles("src", "CRM.Infrastructure");

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void PortalEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/contracts\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/required-capabilities\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/portal-integration", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/portal-integration", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/portal-integration", program);
    }

    [Fact]
    public void FinancialPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Financial");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FutureFinancialAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("ConnectionString", source.Replace("ConnectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("connectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("Connection Strings Configured", string.Empty, StringComparison.Ordinal).Replace("CrmConnectionStringPolicyContract", string.Empty, StringComparison.Ordinal).Replace("ConnectionStringPolicy", string.Empty, StringComparison.Ordinal).Replace("connectionStringPolicy", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferenceFinancialIntegrationPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Financial", source, StringComparison.Ordinal);
        Assert.DoesNotContain("IFinancial", source, StringComparison.Ordinal);
    }

    [Fact]
    public void FinancialPlaceholder_DoesNotMakeRuntimeFinancialCallsOrDbAccess()
    {
        var source = ReadSourceFiles("src", "CRM.Infrastructure");
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("FinancieroUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void FinancialEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/contracts\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/required-capabilities\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/events\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/financial-integration", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/financial-integration", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/financial-integration", program);
    }

    [Fact]
    public void Source_DoesNotReferenceFinancieroRuntimeSriOrSharedDatabase()
    {
        var source = ReadSourceFiles("src", "docker-compose.yml", "docker-compose.crm.yml");

        Assert.DoesNotContain("ProjectReference Include=\"..\\..\\Financiero", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("FinancieroDb", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SharedDatabase", source.Replace("NoSharedDatabase", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SriClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("RIDE", source, StringComparison.Ordinal);
        Assert.DoesNotContain("XAdES", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ATS", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Reporting");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FutureReportingAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("PowerBI", source, StringComparison.Ordinal);
            Assert.DoesNotContain("EmbedToken", source, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferenceReportingPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Reporting", source, StringComparison.Ordinal);
        Assert.DoesNotContain("ICrmKpi", source, StringComparison.Ordinal);
        Assert.DoesNotContain("ICrmDashboard", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingPlaceholder_DoesNotUsePowerBiRuntimeOrIds()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Infrastructure"))
            .Replace("FuturePowerBiEmbedding", string.Empty, StringComparison.Ordinal)
            .Replace("\"EmbedToken\"", string.Empty, StringComparison.Ordinal);

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Microsoft.PowerBI", source, StringComparison.Ordinal);
        Assert.DoesNotContain("EmbedToken", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("WorkspaceId", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ReportId", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DatasetId", source, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/kpis\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/dashboards\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/analytics-read-models\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/reporting", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/reporting", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/reporting", program);
    }

    [Fact]
    public void ClosureEndpoint_IsFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-1/closure-status\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-1", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-1", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/sprint-1", program);
        Assert.Contains("Foundation closure only; no productive activation", ReadSourceFiles("src", "CRM.Application"));
    }

    [Fact]
    public void PersistenceReadinessEndpoint_IsFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/readiness\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/seam-status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/feature-flags\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/stores/status\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/persistence/stores/clear-preview\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence/readiness", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence/seam-status", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence/feature-flags", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence/stores/status", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/persistence", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/persistence", program);
        Assert.Contains("Persistence design review only; no database configured", ReadSourceFiles("src", "CRM.Application"));
        Assert.Contains("Non-production persistence seam only; no database configured", ReadSourceFiles("src", "CRM.Application"));
    }

    [Fact]
    public void PortalAuthorizationSimulation_DoesNotAddProductiveAuthentication()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("CrmPortalAuthorizationSimulationService", source);
        Assert.Contains("SimulatedPortalUserContextProvider", source);
        Assert.Contains("SimulatedPortalPermissionProvider", source);
        Assert.Contains("SimulatedPortalAuthorizationScenarioProvider", source);
        Assert.Contains("CrmFoundationPermissionGuard", source);
        Assert.Contains("FoundationSimulation", source);
        Assert.Contains("Portal authorization simulation only; no real Portal runtime configured", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-authorization/simulation-status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-authorization/scenarios\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-authorization/permissions\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-authorization/sample-user-context\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/portal-authorization/check-permission\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete", program, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void FoundationCrud_IsOnlyFoundationAndNonProductive()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("FoundationLeadCrudService", source);
        Assert.Contains("FoundationAccountCrudService", source);
        Assert.Contains("FoundationContactCrudService", source);
        Assert.Contains("FoundationCrudStatusService", source);
        Assert.Contains("Foundation CRUD only; no productive endpoint or database configured", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/crud/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/leads\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/leads\"", program);
        Assert.Contains("MapPut(\"/api/crm/foundation/leads/{id}\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/accounts\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/accounts\"", program);
        Assert.Contains("MapPut(\"/api/crm/foundation/accounts/{id}\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/contacts\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/contacts\"", program);
        Assert.Contains("MapPut(\"/api/crm/foundation/contacts/{id}\"", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void IntegrationReadinessReview_IsFoundationReadOnlyAndNonProductive()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("CrmSprint2IntegrationReadinessService", source);
        Assert.Contains("Integration readiness review only; no productive activation", source);
        Assert.Contains("Sprint2P6ProductizationGateDecision", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-2/integration-readiness\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-2", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-2", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductizationGateDecision_ClosesSprint2WithoutProductiveActivation()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("CrmSprint2ProductizationGateService", source);
        Assert.Contains("Productization gate decision only; no productive activation", source);
        Assert.Contains("Sprint2Closed", source);
        Assert.Contains("NoGoForProductiveActivation", source);
        Assert.Contains("GoFoundationOnly", source);
        Assert.Contains("Sprint3P1DurablePersistenceSetupDesign", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-2/productization-gate\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-2/productization-gate", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-2/productization-gate", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", StripAllowedConnectionStringMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DurablePersistenceSetupDesign_IsFoundationReadOnlyAndDoesNotActivateDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("CrmDurablePersistenceSetupStatusService", source);
        Assert.Contains("Durable persistence setup design only; no database, EF runtime, migrations, or connection strings configured", source);
        Assert.Contains("DurablePersistenceSetupDesign", source);
        Assert.Contains("DesignOnly", source);
        Assert.Contains("Sprint3P2CommonDbConnectionContractAndSecretStrategy", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/durable-persistence-setup\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/durable-persistence-setup", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/durable-persistence-setup", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", StripAllowedConnectionStringMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommonDbConnectionStrategy_IsContractOnlyAndDoesNotReadSecretsOrConnectDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmCommonDbConnectionStrategyStatusService", source);
        Assert.Contains("CrmSecretProviderPlaceholder", source);
        Assert.Contains("CrmDatabaseConfigurationPlaceholder", source);
        Assert.Contains("Common DB connection contract only; no real database or secrets configured", source);
        Assert.Contains("CommonDbConnectionStrategy", source);
        Assert.Contains("NoRealValuesInRepository", source);
        Assert.Contains("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/common-db-connection-strategy\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/common-db-connection-strategy", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/common-db-connection-strategy", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", StripAllowedSecretProviderContractMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PersistenceSeam_UsesOnlyFoundationStorePortsAndInMemoryAdapters()
    {
        var applicationSource = ReadSourceFiles(Path.Combine("src", "CRM.Application"));
        var infrastructureSource = ReadSourceFiles(Path.Combine("src", "CRM.Infrastructure"));
        var source = applicationSource + Environment.NewLine + infrastructureSource;

        Assert.Contains("ILeadFoundationStore", applicationSource);
        Assert.Contains("IAccountFoundationStore", applicationSource);
        Assert.Contains("IContactFoundationStore", applicationSource);
        Assert.Contains("ICrmFoundationUnitOfWork", applicationSource);
        Assert.Contains("ICrmPersistenceFeatureFlagProvider", applicationSource);
        Assert.Contains("InMemoryLeadFoundationStore", infrastructureSource);
        Assert.Contains("InMemoryAccountFoundationStore", infrastructureSource);
        Assert.Contains("InMemoryContactFoundationStore", infrastructureSource);
        Assert.Contains("StaticCrmPersistenceFeatureFlagProvider", infrastructureSource);
        Assert.Contains("NonProductionSeam", source);
        Assert.Contains("ProductiveCrudEnabled", source);
        var repositoryScanSource = source.Replace("NoRealValuesInRepository", string.Empty, StringComparison.Ordinal)
            .Replace("RealValuesAllowedInRepository", string.Empty, StringComparison.Ordinal)
            .Replace("PasswordsAllowedInRepository", string.Empty, StringComparison.Ordinal);
        Assert.DoesNotContain("Repository", repositoryScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ControlledRuntimeProbeActivationPlan_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmControlledRuntimeProbeActivationPlanStatusService", source);
        Assert.Contains("ControlledRuntimeProbeActivationPlan", source);
        Assert.Contains("Runtime probe activation plan only; no runtime activation approved", source);
        Assert.Contains("Sprint5P2SecretProviderRuntimeContractValidation", source);
        Assert.Contains("sprint5P1ControlledRuntimeProbeActivationPlan: 'Exists'", source);
        Assert.Contains("runtimeProbeActivationApproved: false", source);
        Assert.Contains("commonDbProbeActivationApproved: false", source);
        Assert.Contains("portalAuthProbeActivationApproved: false", source);
        Assert.Contains("productiveRoutesActivationApproved: false", source);
        Assert.Contains("realActivationApproved: false", source);
        Assert.Contains("nonProductionOnly: true", source);
        Assert.Contains("syntheticDataRequired: true", source);
        Assert.Contains("rollbackPlanRequired: true", source);
        Assert.Contains("observabilityRequired: true", source);
        Assert.Contains("secretProviderRequired: true", source);
        Assert.Contains("deleteStillNoGo: true", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/runtime-probe-activation-plan\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/runtime-probe-activation-plan", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/runtime-probe-activation-plan", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void SecretProviderRuntimeContract_IsContractOnlyAndDoesNotReadSecretsOrActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmSecretProviderRuntimeContractStatusService", source);
        Assert.Contains("SecretProviderRuntimeContractValidation", source);
        Assert.Contains("SecretProviderRuntimeContractPlaceholder", source);
        Assert.Contains("Secret Provider contract validation only; no secrets are read", source);
        Assert.Contains("Sprint5P3CommonDbProbeOptionalActivationInNonProduction", source);
        Assert.Contains("sprint5P2SecretProviderRuntimeContract: 'Exists'", source);
        Assert.Contains("secretProviderContractExists: true", source);
        Assert.Contains("secretProviderReadsEnabled: false", source);
        Assert.Contains("secretReadAttemptedByRuntime: false", source);
        Assert.Contains("realSecretsConfigured: false", source);
        Assert.Contains("envFileRequired: false", source);
        Assert.Contains("p2ConnectionStringsConfigured: false", source);
        Assert.Contains("keyVaultClientConfigured: false", source);
        Assert.Contains("secretValuesExposed: false", source);
        Assert.Contains("p2RuntimeProbeActivationApproved: false", source);
        Assert.Contains("p2CommonDbProbeActivationApproved: false", source);
        Assert.Contains("p2PortalAuthProbeActivationApproved: false", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/secret-provider-runtime-contract\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/secret-provider-runtime-contract", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/secret-provider-runtime-contract", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommonDbProbeOptionalActivation_IsDisabledAndDoesNotConnectDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var commonDbConnectionScanSource = StripAllowedCommonDbConnectionContractMarkers(source);

        Assert.Contains("CrmCommonDbProbeOptionalActivationStatusService", source);
        Assert.Contains("CommonDbProbeOptionalActivation", source);
        Assert.Contains("CommonDbProbeOptionalActivationPlaceholder", source);
        Assert.Contains("Common DB probe optional activation only; no database connection is attempted", source);
        Assert.Contains("Sprint5P4PortalAuthProbeOptionalActivationInNonProduction", source);
        Assert.Contains("sprint5P3CommonDbProbeOptionalActivation: 'Exists'", source);
        Assert.Contains("commonDbProbeOptionalActivationExists: true", source);
        Assert.Contains("p3CommonDbProbeActivationApproved: false", source);
        Assert.Contains("p3CommonDbProbeEnabled: false", source);
        Assert.Contains("p3CommonDbConnectionAttempted: false", source);
        Assert.Contains("p3SecretProviderRuntimeRequired: true", source);
        Assert.Contains("p3SecretProviderRuntimeConnected: false", source);
        Assert.Contains("secretReadsRequiredBeforeActivation: true", source);
        Assert.Contains("p3SecretReadsEnabled: false", source);
        Assert.Contains("p3RealDatabaseConfigured: false", source);
        Assert.Contains("p3ConnectionStringsConfigured: false", source);
        Assert.Contains("p3EfRuntimeEnabled: false", source);
        Assert.Contains("p3MigrationsCreated: false", source);
        Assert.Contains("p3ApiRequiresDatabase: false", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/common-db-probe-optional-activation\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/common-db-probe-optional-activation", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/common-db-probe-optional-activation", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("SqlConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
    }


    [Fact]
    public void PortalAuthProbeOptionalActivation_IsDisabledAndDoesNotReadTokens()
    {
        var repositoryRoot = FindRepositoryRoot();
        var serviceSource = File.ReadAllText(Path.Combine(repositoryRoot, "src", "CRM.Application", "Foundation", "CrmPortalAuthProbeOptionalActivationStatusService.cs"));
        var contractSource = File.ReadAllText(Path.Combine(repositoryRoot, "src", "CRM.Application", "Foundation", "CrmPortalAuthProbeOptionalActivationContracts.cs"));
        var applicationSource = serviceSource + Environment.NewLine + contractSource;
        var placeholderSource = File.ReadAllText(Path.Combine(repositoryRoot, "src", "CRM.Infrastructure", "Portal", "RuntimeProbe", "PortalAuthProbeOptionalActivationPlaceholder.cs"));

        Assert.Contains("PortalAuthProbeOptionalActivation", applicationSource);
        Assert.Contains("PortalAuthProbeEnabled", applicationSource);
        Assert.Contains("PortalHttpAttempted", applicationSource);
        Assert.Contains("TokenReadAttempted", applicationSource);
        Assert.Contains("HeaderReadAttempted", applicationSource);
        Assert.Contains("Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted", applicationSource);
        Assert.Contains("ContractOnly", placeholderSource);
        Assert.DoesNotContain("HttpClient", placeholderSource);
        Assert.DoesNotContain("GetEnvironmentVariable", placeholderSource);
    }

    [Fact]
    public void LockedProductiveRouteStubTrial_IsDocumentOnlyAndDoesNotRegisterProductiveRoutes()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmLockedProductiveRouteStubTrialStatusService", source);
        Assert.Contains("LockedProductiveRouteStubTrial", source);
        Assert.Contains("DocumentOnlyPreferredWithNoRuntimeRegistration", source);
        Assert.Contains("Locked productive route stub trial only; no productive routes are registered by default", source);
        Assert.Contains("Sprint5P6Sprint5GateDecision", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Sprint5GateDecision_IsClosureOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmSprint5GateDecisionStatusService", source);
        Assert.Contains("Sprint5GateDecision", source);
        Assert.Contains("GoForControlledNonProductionPreparation", source);
        Assert.Contains("NoGoForRuntimeRead", source);
        Assert.Contains("NoGoForConnectionAttempt", source);
        Assert.Contains("NoGoForPortalHttpOrTokenRead", source);
        Assert.Contains("NoGoForRuntimeRegistration", source);
        Assert.Contains("Sprint6P1NonProductionRuntimeApprovalPackage", source);
        Assert.Contains("Sprint 5 gate decision only; no real activation", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/gate-decision\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/gate-decision", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/gate-decision", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void NonProductionRuntimeApprovalPackage_DoesNotGrantRuntimeOrActivateProductiveRoutes()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmNonProductionRuntimeApprovalPackageStatusService", source);
        Assert.Contains("NonProductionRuntimeApprovalPackage", source);
        Assert.Contains("NonProductionRuntimeApprovalPackageExists", source);
        Assert.Contains("NonProductionRuntimeApprovalGranted", source);
        Assert.Contains("SecretProviderMockApprovalGranted", source);
        Assert.Contains("CommonDbDryRunApprovalGranted", source);
        Assert.Contains("PortalAuthDryRunApprovalGranted", source);
        Assert.Contains("LockedStubRuntimeTrialApprovalGranted", source);
        Assert.Contains("RealActivationApprovalGranted", source);
        Assert.Contains("ProductiveRoutesApprovalGranted", source);
        Assert.Contains("DeleteApprovalGranted", source);
        Assert.Contains("Sprint6P2SecretProviderSafeMockActivation", source);
        Assert.Contains("NonProduction runtime approval package only; no runtime approval is granted", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void SecretProviderSafeMockActivation_UsesSyntheticValuesOnlyAndDoesNotReadRealSecrets()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmSecretProviderSafeMockActivationStatusService", source);
        Assert.Contains("SecretProviderSafeMock", source);
        Assert.Contains("SecretProviderSafeMockActivation", source);
        Assert.Contains("SecretProviderSafeMockExists", source);
        Assert.Contains("SecretProviderSafeMockEnabled", source);
        Assert.Contains("SecretProviderReadsRealSecrets", source);
        Assert.Contains("SecretProviderReadsSyntheticValues", source);
        Assert.Contains("SecretProviderReadsEnabledForMockOnly", source);
        Assert.Contains("RealSecretsConfigured", source);
        Assert.Contains("EnvFileRequired", source);
        Assert.Contains("KeyVaultClientConfigured", source);
        Assert.Contains("AzureSdkForSecretsConfigured", source);
        Assert.Contains("SecretValuesExposedInLogs", source);
        Assert.Contains("Sprint6P3CommonDbConnectivityDryRunContract", source);
        Assert.Contains("Secret Provider safe mock only; no real secrets are read", source);
        Assert.Contains("mock://crm/common-db", source);
        Assert.Contains("mock://crm/portal-auth-base-url", source);
        Assert.Contains("mock-client-id", source);
        Assert.Contains("mock-client-secret-not-real", source);
        Assert.Contains("mock://crm/observability", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommonDbConnectivityDryRun_IsContractOnlyAndDoesNotConnectDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source)
            .Replace("CommonDbConnectivityDryRun", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connectivity Dry-Run", string.Empty, StringComparison.Ordinal)
            .Replace("SyntheticConnectionReference", string.Empty, StringComparison.Ordinal)
            .Replace("syntheticConnectionReference", string.Empty, StringComparison.Ordinal)
            .Replace("UsesSyntheticConnectionReference", string.Empty, StringComparison.Ordinal)
            .Replace("usesSyntheticConnectionReference", string.Empty, StringComparison.Ordinal)
            .Replace("RealConnectionStringUsed", string.Empty, StringComparison.Ordinal)
            .Replace("realConnectionStringUsed", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringResolved", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringResolved", string.Empty, StringComparison.Ordinal)
            .Replace("Real Connection String Used", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Resolved", string.Empty, StringComparison.Ordinal);
        var commonDbConnectionScanSource = StripAllowedCommonDbConnectionContractMarkers(source)
            .Replace("CommonDbConnectivityDryRun", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connection Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("SqlConnectionCreated", string.Empty, StringComparison.Ordinal)
            .Replace("sqlConnectionCreated", string.Empty, StringComparison.Ordinal)
            .Replace("SqlConnection Created", string.Empty, StringComparison.Ordinal)
            .Replace("DbConnectionCreated", string.Empty, StringComparison.Ordinal)
            .Replace("dbConnectionCreated", string.Empty, StringComparison.Ordinal)
            .Replace("DbConnection Created", string.Empty, StringComparison.Ordinal);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmCommonDbConnectivityDryRunStatusService", source);
        Assert.Contains("CommonDbConnectivityDryRun", source);
        Assert.Contains("CommonDbConnectivityDryRunContract", source);
        Assert.Contains("CommonDbConnectivityDryRunContractExists", source);
        Assert.Contains("CommonDbDryRunApprovalGranted", source);
        Assert.Contains("CommonDbDryRunEnabled", source);
        Assert.Contains("CommonDbConnectionAttempted", source);
        Assert.Contains("UsesSecretProviderSafeMockMetadata", source);
        Assert.Contains("UsesSyntheticConnectionReference", source);
        Assert.Contains("mock://crm/common-db", source);
        Assert.Contains("RealConnectionStringUsed", source);
        Assert.Contains("ConnectionStringResolved", source);
        Assert.Contains("SqlConnectionCreated", source);
        Assert.Contains("DbConnectionCreated", source);
        Assert.Contains("EfRuntimeEnabled", source);
        Assert.Contains("MigrationsCreated", source);
        Assert.Contains("Sprint6P4PortalAuthTokenPropagationDryRunContract", source);
        Assert.Contains("Common DB connectivity dry-run contract only; no database connection is attempted", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-6/common-db-connectivity-dry-run\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-6/common-db-connectivity-dry-run", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-6/common-db-connectivity-dry-run", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("SqlConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalAuthTokenPropagationDryRun_IsContractOnlyAndDoesNotReadTokensHeadersOrCallPortal()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmPortalAuthTokenPropagationDryRunStatusService", source);
        Assert.Contains("PortalAuthTokenPropagationDryRun", source);
        Assert.Contains("PortalAuthTokenPropagationDryRunContract", source);
        Assert.Contains("PortalAuthTokenPropagationDryRunContractExists", source);
        Assert.Contains("PortalAuthDryRunApprovalGranted", source);
        Assert.Contains("PortalAuthDryRunEnabled", source);
        Assert.Contains("PortalAuthRuntimeConnected", source);
        Assert.Contains("TokenReadAttempted", source);
        Assert.Contains("HeaderReadAttempted", source);
        Assert.Contains("PortalHttpAttempted", source);
        Assert.Contains("UsesSyntheticTokenMetadata", source);
        Assert.Contains("mock://crm/portal-auth-token", source);
        Assert.Contains("mock://crm/portal-user", source);
        Assert.Contains("RealTokenUsed", source);
        Assert.Contains("RealHeadersRead", source);
        Assert.Contains("LoginImplementedByCrm", source);
        Assert.Contains("IdentityImplementedByCrm", source);
        Assert.Contains("PermissionsPersistedInCrm", source);
        Assert.Contains("ProductiveAuthorizationEnabled", source);
        Assert.Contains("Sprint6P5LockedStubRuntimeRegistrationTrial", source);
        Assert.Contains("Portal Auth token propagation dry-run contract only; no real tokens or headers are read", source);
        Assert.Contains("Sprint 6 P4 Portal Auth Token Propagation Dry-Run Contract: Exists", source);

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("HttpContext.Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Headers[", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("authorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("LockedProductiveRouteStubRuntime", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void LockedStubRuntimeRegistrationTrial_IsDocumentOnlyAndDoesNotRegisterProductiveRoutes()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmLockedStubRuntimeRegistrationTrialStatusService", source);
        Assert.Contains("LockedStubRuntimeRegistrationTrial", source);
        Assert.Contains("LockedStubRuntimeRegistrationTrialExists", source);
        Assert.Contains("LockedStubRuntimeRegistrationApprovalGranted", source);
        Assert.Contains("LockedStubRuntimeRegistrationEnabled", source);
        Assert.Contains("LockedStubsRegisteredAtRuntime", source);
        Assert.Contains("ProductiveRoutesRegistered", source);
        Assert.Contains("ProductiveCrudEnabled", source);
        Assert.Contains("DeleteEndpointsEnabled", source);
        Assert.Contains("DefaultNegativeRouteStatus", source);
        Assert.Contains("FutureLockedResponseStatusIfExplicitlyEnabled", source);
        Assert.Contains("RuntimeFlagDefaultEnabled", source);
        Assert.Contains("UsesDomainServices", source);
        Assert.Contains("UsesFoundationStores", source);
        Assert.Contains("UsesDatabase", source);
        Assert.Contains("UsesPortalAuth", source);
        Assert.Contains("UsesTokenOrHeaderReads", source);
        Assert.Contains("DocumentOnlyPreferredWithNoRuntimeRegistration", source);
        Assert.Contains("Sprint6P6Sprint6GateDecision", source);
        Assert.Contains("Locked stub runtime registration trial only; no productive routes are registered by default", source);
        Assert.Contains("Sprint 6 P5 Locked Stub Runtime Registration Trial: Exists", source);

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("LockedStubRuntimeRegistrationTrial.cs", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("LockedProductiveRouteStubRuntime", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpContext.Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Headers[", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("authorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Sprint6GateDecision_IsDecisionOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmSprint6GateDecisionStatusService", source);
        Assert.Contains("Sprint6GateDecision", source);
        Assert.Contains("GoForSprint7ControlledNonProductionActivationPlanning", source);
        Assert.Contains("Sprint7P1SecretProviderRealNonProductionApproval", source);
        Assert.Contains("Sprint 6 gate decision only; no real activation", source);
        Assert.Contains("Sprint 6: Closed", source);
        Assert.Contains("Sprint 6 Gate Decision: Completed", source);
        Assert.Contains("Productization Status: NotReady", source);

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-6/gate-decision\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-6/gate-decision", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-6/gate-decision", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("LockedProductiveRouteStubRuntime", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpContext.Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Headers[", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("authorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void SecretProviderRealNonProductionApproval_IsApprovalOnlyAndDoesNotReadSecrets()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmSecretProviderRealNonProductionApprovalStatusService", source);
        Assert.Contains("SecretProviderRealNonProductionApprovalPlaceholder", source);
        Assert.Contains("SecretProviderRealNonProductionApproval", source);
        Assert.Contains("Secret Provider real NonProduction approval package only; no real secrets are read", source);
        Assert.Contains("Sprint7P2SecretProviderRealNonProductionRuntimeProbe", source);
        Assert.Contains("secretProviderRealNonProductionApprovalGranted: false", source);
        Assert.Contains("realSecretReadAttempted: false", source);

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("LockedProductiveRouteStubRuntime", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpContext.Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Headers[", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("authorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SecretClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void SecretProviderRealNonProductionRuntimeProbe_IsSkippedAndDoesNotReadSecrets()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmSecretProviderRealNonProductionRuntimeProbeStatusService", source);
        Assert.Contains("SecretProviderRealNonProductionRuntimeProbe", source);
        Assert.Contains("SecretProviderRealNonProductionRuntimeProbeExists", source);
        Assert.Contains("SecretProviderRealNonProductionApprovalGranted", source);
        Assert.Contains("SecretProviderRealRuntimeProbeEnabled", source);
        Assert.Contains("SecretProviderRealRuntimeProbeAttempted", source);
        Assert.Contains("SecretProviderRealRuntimeConnected", source);
        Assert.Contains("RealSecretValueMaterialized", source);
        Assert.Contains("SecretValueReturnedToApi", source);
        Assert.Contains("ProbeSkippedBecauseApprovalNotGranted", source);
        Assert.Contains("Secret Provider real NonProduction runtime probe is prepared but skipped because approval is not granted", source);
        Assert.Contains("Sprint7P3CommonDbRealConnectivityNonProductionProbe", source);
        Assert.Contains("secretProviderRealNonProductionRuntimeProbeExists: true", source);
        Assert.Contains("probeSkippedBecauseApprovalNotGranted: true", source);

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("LockedProductiveRouteStubRuntime", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpContext.Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Headers[", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("authorizationHeader", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SecretClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DefaultAzureCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ManagedIdentityCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("EnvironmentCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommonDbRealConnectivityNonProductionProbe_IsSkippedAndDoesNotConnect()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source)
            .Replace("AddDbContextRuntimeEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("addDbContextRuntimeEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("AddDbContext Runtime Enabled", string.Empty, StringComparison.Ordinal);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source)
            .Replace("CommonDbRealConnectivityNonProductionProbe", string.Empty, StringComparison.Ordinal)
            .Replace("SyntheticConnectionReference", string.Empty, StringComparison.Ordinal)
            .Replace("syntheticConnectionReference", string.Empty, StringComparison.Ordinal)
            .Replace("Synthetic Connection Reference", string.Empty, StringComparison.Ordinal);
        var commonDbConnectionScanSource = StripAllowedCommonDbConnectionContractMarkers(source)
            .Replace("CommonDbRealConnectivityNonProductionProbe", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbProbeAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbProbeAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Probe Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnected", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnected", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connected", string.Empty, StringComparison.Ordinal);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmCommonDbRealConnectivityNonProductionProbeStatusService", source);
        Assert.Contains("CommonDbRealConnectivityNonProductionProbe", source);
        Assert.Contains("CommonDbRealConnectivityNonProductionProbeExists", source);
        Assert.Contains("CommonDbRealConnectivityApprovalGranted", source);
        Assert.Contains("SecretProviderRealNonProductionApprovalGranted", source);
        Assert.Contains("ConnectionStringResolved", source);
        Assert.Contains("ConnectionStringValueMaterialized", source);
        Assert.Contains("ConnectionStringLogged", source);
        Assert.Contains("ConnectionStringReturnedToApi", source);
        Assert.Contains("CommonDbProbeEnabled", source);
        Assert.Contains("CommonDbProbeAttempted", source);
        Assert.Contains("CommonDbConnected", source);
        Assert.Contains("SqlConnectionCreated", source);
        Assert.Contains("DbConnectionCreated", source);
        Assert.Contains("UseSqlServerEnabled", source);
        Assert.Contains("EfRuntimeEnabled", source);
        Assert.Contains("AddDbContextRuntimeEnabled", source);
        Assert.Contains("MigrationsCreated", source);
        Assert.Contains("DatabaseSchemaChanged", source);
        Assert.Contains("ProductivePersistenceEnabled", source);
        Assert.Contains("ApiRequiresDatabase", source);
        Assert.Contains("UsesSecretProviderRuntime", source);
        Assert.Contains("UsesSyntheticFallback", source);
        Assert.Contains("mock://crm/common-db", source);
        Assert.Contains("ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted", source);
        Assert.Contains("Common DB real connectivity NonProduction probe is prepared but skipped because Secret Provider approval is not granted", source);
        Assert.Contains("Sprint7P4PortalAuthRealRuntimeProbe", source);
        Assert.Contains("commonDbRealConnectivityNonProductionProbeExists: true", source);
        Assert.Contains("connectionProbeSkippedBecauseSecretProviderApprovalNotGranted: true", source);

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SqlConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SecretClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DefaultAzureCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ManagedIdentityCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("EnvironmentCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpContext.Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Headers[", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalAuthRealRuntimeProbe_IsSkippedAndDoesNotReadHeadersTokensOrCallPortal()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var rawSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Application", "Foundation", "CrmPortalAuthRealRuntimeProbeStatusService.cs")) +
            Environment.NewLine +
            File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Infrastructure", "Portal", "RuntimeProbe", "PortalAuthRealRuntimeProbe.cs")) +
            Environment.NewLine +
            File.ReadAllText(Path.Combine(FindRepositoryRoot(), "frontend", "crm-web", "src", "main.ts"));
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var commonDbConnectionScanSource = StripAllowedCommonDbConnectionContractMarkers(source);
        var portalAuthScanSource = StripAllowedPortalAuthRealRuntimeProbeMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmPortalAuthRealRuntimeProbeStatusService", rawSource);
        Assert.Contains("PortalAuthRealRuntimeProbe", rawSource);
        Assert.Contains("PortalAuthRealRuntimeProbeExists", rawSource);
        Assert.Contains("PortalAuthRealRuntimeApprovalGranted", rawSource);
        Assert.Contains("SecretProviderRealNonProductionApprovalGranted", rawSource);
        Assert.Contains("PortalAuthRealRuntimeProbeEnabled", rawSource);
        Assert.Contains("PortalAuthRealRuntimeProbeAttempted", rawSource);
        Assert.Contains("PortalAuthRuntimeConnected", rawSource);
        Assert.Contains("PortalAuthBaseUrlResolved", rawSource);
        Assert.Contains("PortalAuthBaseUrlMaterialized", rawSource);
        Assert.Contains("PortalAuthBaseUrlLogged", rawSource);
        Assert.Contains("PortalAuthBaseUrlReturnedToApi", rawSource);
        Assert.Contains("PortalHttpClientCreated", rawSource);
        Assert.Contains("PortalHttpCallAttempted", rawSource);
        Assert.Contains("PortalAuthTokenValidationAttempted", rawSource);
        Assert.Contains("TokenReadAttempted", rawSource);
        Assert.Contains("HeaderReadAttempted", rawSource);
        Assert.Contains("AuthorizationHeaderReadAttempted", rawSource);
        Assert.Contains("RealTokenMaterialized", rawSource);
        Assert.Contains("RealTokenLogged", rawSource);
        Assert.Contains("TokenReturnedToApi", rawSource);
        Assert.Contains("LoginImplementedByCrm", rawSource);
        Assert.Contains("LogoutImplementedByCrm", rawSource);
        Assert.Contains("IdentityImplementedByCrm", rawSource);
        Assert.Contains("RolesPersistedInCrm", rawSource);
        Assert.Contains("PermissionsPersistedInCrm", rawSource);
        Assert.Contains("ProductiveAuthorizationEnabled", rawSource);
        Assert.Contains("ApiRequiresPortalAuth", rawSource);
        Assert.Contains("UsesSyntheticFallback", rawSource);
        Assert.Contains("mock://crm/portal-auth", rawSource);
        Assert.Contains("mock://crm/portal-user", rawSource);
        Assert.Contains("ProbeSkippedBecausePortalAuthApprovalNotGranted", rawSource);
        Assert.Contains("Portal Auth real runtime probe is prepared but skipped because Portal Auth approval is not granted", rawSource);
        Assert.Contains("Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423", rawSource);

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SqlConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SecretClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DefaultAzureCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ManagedIdentityCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("EnvironmentCredential", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpContext.Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Request.Headers", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Headers[", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizationHeader", portalAuthScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", portalAuthScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", portalAuthScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
    }

    private static IReadOnlySet<string> ReferencedAssemblyNames(Assembly assembly) =>
        assembly.GetReferencedAssemblies().Select(reference => reference.Name ?? "").ToHashSet(StringComparer.OrdinalIgnoreCase);

    private static string ReadSourceFiles(params string[] roots)
    {
        var repositoryRoot = FindRepositoryRoot();
        var contents = new List<string>();
        foreach (var root in roots)
        {
            var path = Path.Combine(repositoryRoot, root);
            if (File.Exists(path))
            {
                contents.Add(File.ReadAllText(path));
                continue;
            }

            if (!Directory.Exists(path))
            {
                continue;
            }

            foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}node_modules{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}dist{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}.angular{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}tools{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)))
            {
                contents.Add(File.ReadAllText(file));
            }
        }

        return StripAllowedPortalAuthRealRuntimeProbeMarkers(string.Join(Environment.NewLine, contents));
    }

    private static string StripAllowedEfPrototypeMarkers(string source) =>
        source.Replace("DbContextConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("dbContextConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("DbContext Configured", string.Empty, StringComparison.Ordinal)
            .Replace("DbContextRuntimeActive", string.Empty, StringComparison.Ordinal)
            .Replace("dbContextRuntimeActive", string.Empty, StringComparison.Ordinal)
            .Replace("DbContext Runtime Active", string.Empty, StringComparison.Ordinal)
            .Replace("AddDbContextRuntimeEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("addDbContextRuntimeEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("AddDbContext Runtime Enabled", string.Empty, StringComparison.Ordinal)
            .Replace("EfRuntimeEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("efRuntimeEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("EF Runtime Enabled", string.Empty, StringComparison.Ordinal)
            .Replace("CrmDbContextPrototypeContract", string.Empty, StringComparison.Ordinal)
            .Replace("CrmDbContextPrototype", string.Empty, StringComparison.Ordinal)
            .Replace("InheritsRealDbContext", string.Empty, StringComparison.Ordinal)
            .Replace("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", string.Empty, StringComparison.Ordinal)
            .Replace("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", string.Empty, StringComparison.Ordinal)
            .Replace("EfDbContextPrototypeDisabled", string.Empty, StringComparison.Ordinal)
            .Replace("EF/DbContext prototype only; runtime disabled and no database configured", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedConnectionStringMarkers(string source) =>
        source.Replace("ConnectionStringsConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringsConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("Connection Strings Configured", string.Empty, StringComparison.Ordinal)
            .Replace("CrmConnectionStringPolicyContract", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringPolicy", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringPolicy", string.Empty, StringComparison.Ordinal)
            .Replace("RealConnectionStringUsed", string.Empty, StringComparison.Ordinal)
            .Replace("realConnectionStringUsed", string.Empty, StringComparison.Ordinal)
            .Replace("Real Connection String Used", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringResolved", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringResolved", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Resolved", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringValueMaterialized", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringValueMaterialized", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Value Materialized", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringLogged", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringLogged", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Logged", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringReturnedToApi", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringReturnedToApi", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Returned To API", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringReturned", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringReturned", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Returned", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringMaterializedInPublicContract", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringMaterializedInPublicContract", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Materialized In Public Contract", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringPersisted", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringPersisted", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Persisted", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringCached", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringCached", string.Empty, StringComparison.Ordinal)
            .Replace("Connection String Cached", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedProviderMarkers(string source) =>
        source.Replace("UseSqlServerConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("useSqlServerConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("UseSqlServer Configured", string.Empty, StringComparison.Ordinal)
            .Replace("UseSqlServerEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("useSqlServerEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("UseSqlServer Enabled", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedSecretProviderContractMarkers(string source) =>
        source.Replace("KeyVaultClientConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("keyVaultClientConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("Key Vault Client Configured", string.Empty, StringComparison.Ordinal)
            .Replace("KeyVaultRuntimeClientEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("keyVaultRuntimeClientEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("Key Vault Runtime Client Enabled", string.Empty, StringComparison.Ordinal)
            .Replace("KeyVaultRuntimeClientCreated", string.Empty, StringComparison.Ordinal)
            .Replace("keyVaultRuntimeClientCreated", string.Empty, StringComparison.Ordinal)
            .Replace("Key Vault Runtime Client Created", string.Empty, StringComparison.Ordinal)
            .Replace("KeyVaultRuntimeCallAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("keyVaultRuntimeCallAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Key Vault Runtime Call Attempted", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedCommonDbConnectionContractMarkers(string source) =>
        source.Replace("CommonDbConnectionStrategy", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectivityDryRun", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbRealConnectivityNonProductionProbe", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Real Connectivity NonProduction Probe", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connection Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionStringResolved", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionStringResolved", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connection String Resolved", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionStringReturnedToApi", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionStringReturnedToApi", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connection String Returned To API", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionStringLogged", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionStringLogged", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connection String Logged", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionStringPersisted", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionStringPersisted", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connection String Persisted", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionStringCached", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionStringCached", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connection String Cached", string.Empty, StringComparison.Ordinal)
            .Replace("DbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("dbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("DB Connection Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("SqlConnectionCreated", string.Empty, StringComparison.Ordinal)
            .Replace("sqlConnectionCreated", string.Empty, StringComparison.Ordinal)
            .Replace("SqlConnection Created", string.Empty, StringComparison.Ordinal)
            .Replace("DbConnectionCreated", string.Empty, StringComparison.Ordinal)
            .Replace("dbConnectionCreated", string.Empty, StringComparison.Ordinal)
            .Replace("DbConnection Created", string.Empty, StringComparison.Ordinal)
            .Replace("DbConnectionOpened", string.Empty, StringComparison.Ordinal)
            .Replace("dbConnectionOpened", string.Empty, StringComparison.Ordinal)
            .Replace("DbConnection Opened", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbControlledRealConnectivity", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Controlled Real Connectivity", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbControlledRealConnectivityExists", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbControlledRealConnectivityExists", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbControlledRealConnectivityApproved", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbControlledRealConnectivityApproved", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbControlledRealConnectivityEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbControlledRealConnectivityEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Controlled Real Connectivity Enabled", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectivityAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectivityAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connectivity Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbProbeEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbProbeEnabled", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Probe Enabled", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbProbeAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbProbeAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Probe Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnected", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnected", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connected", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionContract", string.Empty, StringComparison.Ordinal)
            .Replace("Sprint3P2CommonDbConnectionContractAndSecretStrategy", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionStringsConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionStringsConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("CrmCommonDbConnectionStrategyStatusService", string.Empty, StringComparison.Ordinal)
            .Replace("GetCrmFoundationSprint3CommonDbConnectionStrategy", string.Empty, StringComparison.Ordinal)
            .Replace("/api/crm/foundation/sprint-3/common-db-connection-strategy", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedPortalAuthRealRuntimeProbeMarkers(string source) =>
        source.Replace("PortalAuthBaseUrlResolved", string.Empty, StringComparison.Ordinal)
            .Replace("portalAuthBaseUrlResolved", string.Empty, StringComparison.Ordinal)
            .Replace("Portal Auth Base URL Resolved", string.Empty, StringComparison.Ordinal)
            .Replace("PortalAuthBaseUrlMaterialized", string.Empty, StringComparison.Ordinal)
            .Replace("portalAuthBaseUrlMaterialized", string.Empty, StringComparison.Ordinal)
            .Replace("Portal Auth Base URL Materialized", string.Empty, StringComparison.Ordinal)
            .Replace("PortalAuthBaseUrlLogged", string.Empty, StringComparison.Ordinal)
            .Replace("portalAuthBaseUrlLogged", string.Empty, StringComparison.Ordinal)
            .Replace("Portal Auth Base URL Logged", string.Empty, StringComparison.Ordinal)
            .Replace("PortalAuthBaseUrlReturnedToApi", string.Empty, StringComparison.Ordinal)
            .Replace("portalAuthBaseUrlReturnedToApi", string.Empty, StringComparison.Ordinal)
            .Replace("Portal Auth Base URL Returned To API", string.Empty, StringComparison.Ordinal)
            .Replace("PortalHttpClientCreated", string.Empty, StringComparison.Ordinal)
            .Replace("portalHttpClientCreated", string.Empty, StringComparison.Ordinal)
            .Replace("Portal HTTP Client Created", string.Empty, StringComparison.Ordinal)
            .Replace("PortalHttpCallAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("portalHttpCallAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Portal HTTP Call Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("PortalAuthTokenValidationAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("portalAuthTokenValidationAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Portal Auth Token Validation Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("AuthorizationHeaderReadAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("authorizationHeaderReadAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Authorization Header Read Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("SyntheticPortalAuthReference", string.Empty, StringComparison.Ordinal)
            .Replace("syntheticPortalAuthReference", string.Empty, StringComparison.Ordinal)
            .Replace("Synthetic Portal Auth Reference", string.Empty, StringComparison.Ordinal)
            .Replace("ProbeSkippedBecausePortalAuthApprovalNotGranted", string.Empty, StringComparison.Ordinal)
            .Replace("probeSkippedBecausePortalAuthApprovalNotGranted", string.Empty, StringComparison.Ordinal)
            .Replace("Probe Skipped Because Portal Auth Approval Not Granted", string.Empty, StringComparison.Ordinal);

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "CRM.sln")))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new InvalidOperationException("Repository root was not found.");
    }
}
